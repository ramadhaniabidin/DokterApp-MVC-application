using Dapper;
using DokterApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Numerics;

namespace DokterApp.Repositories
{
    public class DoctorSpecializationRepository : IDoctorSpecializationRepository
    {
        private string connection;

        public DoctorSpecializationRepository(IConfiguration configuration)
        {
            connection = configuration.GetConnectionString("SqlServer");
        }
        public async Task<bool> DeleteDoctor(int Id)
        {
            using (var conn = new SqlConnection(connection))
            {
                var query = @"DELETE FROM DOKTER WHERE DOKTER.id = @Id";
                var deletedRow = conn.Execute(query, new { id = Id });
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

        public async Task<bool> InsertDoctor(DoctorSpecialization model)
        {
            using (var c = new SqlConnection(connection))
            {
                await c.OpenAsync();
                var query =
                    @" 
                    insert into DOKTER (nama, nip, nik, tanggal_lahir, tempat_lahir, jenis_kelamin, image_url) 
                    values (@nama, '', @nik, @tanggal_lahir, @tempat_lahir, @jenis_kelamin, @image_url)
                    
                    DECLARE @id_dokter_baru INT	
                    SELECT TOP 1 @id_dokter_baru = DOKTER.id FROM DOKTER ORDER BY DOKTER.id DESC
                    EXEC UpdateNIP @id_dokter_baru

                    DECLARE @id_spesialisasi INT
                    SELECT @id_spesialisasi = SPESIALISASI.id FROM SPESIALISASI WHERE SPESIALISASI.id = @spesialisasi


                    INSERT INTO SPESIALISASI_DOKTER (id_dokter, id_spesialisasi)
                    VALUES (@id_dokter_baru, @id_spesialisasi)
                    
                    --UPDATE SPESIALISASI_DOKTER
                    --SET id_dokter = @id_dokter_baru, id_spesialisasi=@id_spesialisasi";

                await c.ExecuteAsync(query, model);
            }
            return true;
        }

        public async Task<bool> UpdateDoctor(DoctorSpecialization model)
        {
            using (var c = new SqlConnection(connection))
            {
                c.OpenAsync();
                var statement =
                    $@"
                    UPDATE DOKTER set nama=@nama, nip='', tanggal_lahir=@tanggal_lahir, tempat_lahir=@tempat_lahir, 
                    jenis_kelamin=@jenis_kelamin, image_url=@image_url where DOKTER.id=@Id
                    EXEC UpdateNIP @Id

                    UPDATE SPESIALISASI_DOKTER
                    SET id_spesialisasi = @spesialisasi FROM SPESIALISASI WHERE SPESIALISASI_DOKTER.id_dokter = @Id";
                await c.QueryAsync<DoctorSpecialization>(statement, model);
                return true;
            }
        }

        public async Task<List<DoctorSpecialization>> SelectAllDoctors()
        {
            using (var conn = new SqlConnection(connection))
            {
                await conn.OpenAsync();
                var query = await conn.QueryAsync<DoctorSpecialization>(@"
                SELECT DOKTER.id, DOKTER.nama, DOKTER.nip, DOKTER.nik, DOKTER.jenis_kelamin, DOKTER.image_url, SPESIALISASI.nama as 'spesialisasi'
                FROM DOKTER
                JOIN SPESIALISASI_DOKTER ON DOKTER.id = SPESIALISASI_DOKTER.id_dokter
                JOIN SPESIALISASI ON SPESIALISASI.id = SPESIALISASI_DOKTER.id_spesialisasi");
                return query.ToList();
            }
        }

        public async Task<List<Spesialisasi>> SelectAllSpecializations()
        {
            using (var conn = new SqlConnection(connection))
            {
                await conn.OpenAsync();
                var query = await conn.QueryAsync<Spesialisasi>(@"
                            SELECT SPESIALISASI.id, SPESIALISASI.nama FROM SPESIALISASI");
                return query.ToList();
            }
        }

        public async Task<DoctorSpecialization> SelectDoctorById(int Id)
        {
            using (var conn = new SqlConnection(connection))
            {
                var query = $@"SELECT DOKTER.id, DOKTER.nama, DOKTER.nip, DOKTER.nik, DOKTER.tanggal_lahir, DOKTER.tempat_lahir, DOKTER.jenis_kelamin, DOKTER.image_url, SPESIALISASI.nama as 'Spesialisasi'
                              FROM DOKTER
                              JOIN SPESIALISASI_DOKTER ON DOKTER.id = SPESIALISASI_DOKTER.id_dokter
                              JOIN SPESIALISASI ON SPESIALISASI.id = SPESIALISASI_DOKTER.id_spesialisasi
                              WHERE DOKTER.id = {Id}";
                var output = await conn.QueryFirstOrDefaultAsync<DoctorSpecialization>(query);
                return output;
            }
        }

        public async Task<DoctorSpecialization> GetJadwalById(int Id)
        {
            using (var conn = new SqlConnection(connection))
            {
                var query = $@"SELECT DOKTER.id, DOKTER.nama, DOKTER.nip, DOKTER.nik, DOKTER.tanggal_lahir, DOKTER.tempat_lahir, DOKTER.jenis_kelamin, DOKTER.image_url, SPESIALISASI.nama as 'Spesialisasi'
                              FROM DOKTER
                              JOIN SPESIALISASI_DOKTER ON DOKTER.id = SPESIALISASI_DOKTER.id_dokter
                              JOIN SPESIALISASI ON SPESIALISASI.id = SPESIALISASI_DOKTER.id_spesialisasi
                              WHERE DOKTER.id = {Id}";
                var output = await conn.QueryFirstOrDefaultAsync<DoctorSpecialization>(query);
                return output;
            }
        }

        public async Task<List<DoctorSpecialization>> GetAllJadwal()
        {
            using (var conn = new SqlConnection(connection))
            {
                await conn.OpenAsync();
                var query = await conn.QueryAsync<DoctorSpecialization>(@"
                SELECT JADWAL_JAGA.id, POLI.nama, POLI.lokasi, JADWAL_JAGA.hari FROM POLI
                JOIN JADWAL_JAGA ON POLI.id = JADWAL_JAGA.id_poli
                --JOIN DOKTER ON DOKTER.id = JADWAL_JAGA.id_dokter
                ");
                return query.ToList();
            }
        }

        public async Task<bool> InsertJadwal(DoctorSpecialization jadwalDokter)
        {
            using (var conn = new SqlConnection(connection))
            {
                await conn.OpenAsync();
                var query = @"
                DECLARE @id_poli INT
                SELECT @id_poli = POLI.id FROM POLI WHERE POLI.nama = @nama
                
                --DECLARE @id_dokter INT
                --SELECT @id_dokter = DOKTER.id FROM DOKTER WHERE DOKTER.id = @Id

                INSERT INTO JADWAL_JAGA (hari, id_poli, id_dokter) VALUES (@hari, @id_poli, @id_dokter)";
                await conn.ExecuteAsync(query, jadwalDokter);
            }
            return true;
        }

        public async Task<bool> UpdateJadwal(DoctorSpecialization jadwalDokter)
        {
            using (var conn = new SqlConnection(connection))
            {
                await conn.OpenAsync();
                var query = @"UPDATE JADWAL_JAGA SET hari=@hari";
                await conn.QueryAsync<DoctorSpecialization>(query, jadwalDokter);
                return true;
            }
        }

        public async Task<bool> DeleteJadwal(int Id)
        {
            using (var conn = new SqlConnection(connection))
            {
                var query = @"DELETE FROM JADWAL_JAGA WHERE JADWAL_JAGA.id_poli = @Id";
                var deletedRow = conn.Execute(query, new { id_dokter = Id });
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
    }
}
