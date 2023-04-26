using Dapper;
using DokterApp.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DokterApp.Repositories
{
    public class JadwalDokterRepository : IJadwalDokterRepository
    {
        private string connection;
        public JadwalDokterRepository(IConfiguration configuration)
        {
            connection = configuration.GetConnectionString("SqlServer");
        }
        public async Task<bool> Delete(int Id)
        {
            using (var conn = new SqlConnection(connection))
            {
                var query = $@"DELETE FROM JADWAL_JAGA WHERE JADWAL_JAGA.id = {Id}";
                var deletedRow = conn.Execute(query);
                if (deletedRow == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            
        }

        public async Task<List<JadwalDokter>> GetAllJadwal()
        {
            using (var conn = new SqlConnection(connection))
            {
                await conn.OpenAsync();
                var query = await conn.QueryAsync<JadwalDokter>(@"
                SELECT POLI.id, POLI.nama, POLI.lokasi, JADWAL_JAGA.hari FROM POLI
                JOIN JADWAL_JAGA ON POLI.id = JADWAL_JAGA.id_poli
                --JOIN DOKTER ON DOKTER.id = JADWAL_JAGA.id_dokter
                ");
                return query.ToList();
            }
        }

        public async Task<List<Poli>> SelectAllPoli()
        {
            using (var conn = new SqlConnection(connection))
            {
                await conn.OpenAsync();
                var query = await conn.QueryAsync<Poli>(@"
                SELECT POLI.id, POLI.nama as nama, POLI.lokasi FROM POLI");
                return query.ToList();
            }
        }

        public async Task<List<JadwalDokter>> GetJadwalById(int Id)
        {
            using(var conn = new SqlConnection(connection))
            {
                await conn.OpenAsync();
                var query = await conn.QueryAsync<JadwalDokter>($@"
                SELECT JADWAL_JAGA.id, JADWAL_JAGA.hari, POLI.nama as nama_poli, POLI.lokasi FROM POLI
                JOIN JADWAL_JAGA ON JADWAL_JAGA.id_poli = POLI.id
                JOIN DOKTER ON DOKTER.id = JADWAL_JAGA.id_dokter
                WHERE DOKTER.id = {Id}
                ");
                return query.ToList();
            }
        }

        public async Task<List<JadwalDokter>> GetJadwalByNIK(string NIK)
        {
            using (var conn = new SqlConnection(connection))
            {
                await conn.OpenAsync();
                var query = await conn.QueryAsync<JadwalDokter>($@"
                SELECT JADWAL_JAGA.hari, POLI.nama, POLI.lokasi FROM POLI
                JOIN JADWAL_JAGA ON JADWAL_JAGA.id_poli = POLI.id
                JOIN DOKTER ON DOKTER.id = JADWAL_JAGA.id_dokter
                WHERE DOKTER.nik LIKE '%' + @nik + '%'
                ", new {nik=NIK});
                return query.ToList();
            }
        }

        public async Task<bool> Insert(JadwalDokter jadwalDokter)
        {
            using (var conn = new SqlConnection(connection))
            {
                await conn.OpenAsync();
                var query = @"
                DECLARE @id_poli_baru INT
                SELECT @id_poli_baru = POLI.id FROM POLI WHERE POLI.id = @nama_poli

                DECLARE @HariJaga VARCHAR(10)
                SELECT @HariJaga = COUNT (JADWAL_JAGA.id_dokter )
                FROM JADWAL_JAGA
                WHERE hari = @hari

                --SELECT @HariJaga
                IF(@HariJaga = 0)
                BEGIN
                    INSERT INTO JADWAL_JAGA (hari, id_poli, id_dokter) VALUES (@hari, @id_poli_baru, @Id)
                END
                ";
                await conn.ExecuteAsync(query, jadwalDokter);
            }
            return true;
        }

        public async Task<JadwalDokter> PickJadwal(int Id)
        {
            using(var conn = new SqlConnection(connection))
            {
                var query = $@"
                            SELECT JADWAL_JAGA.id, JADWAL_JAGA.hari, POLI.nama, POLI.lokasi FROM POLI
                            JOIN JADWAL_JAGA ON JADWAL_JAGA.id_poli = POLI.id
                            JOIN DOKTER ON DOKTER.id = JADWAL_JAGA.id_dokter
                            WHERE JADWAL_JAGA.id = {Id}";
                var output = await conn.QueryFirstOrDefaultAsync<JadwalDokter>(query);
                return output;
            }
            
        }

        public async Task<JadwalDokter> SelectDoctorById(int Id)
        {
            using (var conn = new SqlConnection(connection))
            {
                var query = $@"SELECT DOKTER.id, DOKTER.nama, DOKTER.nip, DOKTER.nik, DOKTER.tanggal_lahir, DOKTER.tempat_lahir, DOKTER.jenis_kelamin, DOKTER.image_url, SPESIALISASI.nama as 'Spesialisasi'
                              FROM DOKTER
                              JOIN SPESIALISASI_DOKTER ON DOKTER.id = SPESIALISASI_DOKTER.id_dokter
                              JOIN SPESIALISASI ON SPESIALISASI.id = SPESIALISASI_DOKTER.id_spesialisasi
                              WHERE DOKTER.id = {Id}";
                var output = await conn.QueryFirstOrDefaultAsync<JadwalDokter>(query);
                return output;
            } 
        }

        public async Task<JadwalDokter> SelectDoctorByNIK(string NIK)
        {
            using (var conn = new SqlConnection(connection))
            {
                var query = $@"SELECT DOKTER.id, DOKTER.nama, DOKTER.nip, DOKTER.nik, DOKTER.tanggal_lahir, DOKTER.tempat_lahir, DOKTER.jenis_kelamin, DOKTER.image_url, SPESIALISASI.nama as 'Spesialisasi'
                              FROM DOKTER
                              JOIN SPESIALISASI_DOKTER ON DOKTER.id = SPESIALISASI_DOKTER.id_dokter
                              JOIN SPESIALISASI ON SPESIALISASI.id = SPESIALISASI_DOKTER.id_spesialisasi
                              WHERE DOKTER.nik LIKE '%' + @nik + '%'";
                var output = await conn.QueryFirstOrDefaultAsync<JadwalDokter>(query, new {nik=NIK});
                return output;
            }
        }

        public async Task<bool> Update(JadwalDokter jadwalDokter)
        {
            using (var conn = new SqlConnection(connection))
            {
                await conn.OpenAsync();
                var query = @"UPDATE JADWAL_JAGA SET hari=@hari, id_poli=@nama_poli WHERE JADWAL_JAGA.id=@Id";
                await conn.QueryAsync<JadwalDokter>(query, jadwalDokter);
                return true;
            }
        }
    }
}
