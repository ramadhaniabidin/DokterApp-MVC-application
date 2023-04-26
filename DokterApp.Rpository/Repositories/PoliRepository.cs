using Dapper;
using DokterApp.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DokterApp.Repositories
{
    public class PoliRepository : IPoliRepository
    {
        private string connection;
        public PoliRepository(IConfiguration config)
        {
            connection = config.GetConnectionString("SqlServer");
        }
        public async Task<bool> Delete(int Id)
        {
            using (var conn = new SqlConnection(connection))
            {
                var query = $@"DELETE FROM POLI WHERE POLI.id = {Id}";
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

        public async Task<List<Poli>> GetAllPoli()
        {
            using (var c = new SqlConnection(connection))
            {
                await c.OpenAsync();
                var query = await c.QueryAsync<Poli>($@"select * from POLI");
                return query.ToList();
            }
        }

        public async Task<Poli> GetPoliById(int Id)
        {
            using (var c = new SqlConnection(connection))
            {
                var statement = @"select * FROM POLI where POLI.id = @Id";
                var output = await c.QueryFirstOrDefaultAsync<Poli>(statement, new { id = Id });
                return output;
            }
        }

        public async Task<bool> Insert(Poli poli)
        {
            using(var c = new SqlConnection(connection))
            {
                await c.OpenAsync();
                var query = @"
                INSERT INTO POLI (nama, lokasi) VALUES (@nama, @lokasi)";
                await c.ExecuteAsync(query, poli);
            }
            return true;
        }

        public async Task<List<DoctorSpecialization>> MatchDoctorAndPoli(int Id)
        {
            using (var conn = new SqlConnection(connection))
            {
                var query = await conn.QueryAsync<DoctorSpecialization>($@"
                        SELECT DOKTER.nama, DOKTER.nip, DOKTER.nik, SPESIALISASI.nama FROM DOKTER
                        JOIN JADWAL_JAGA ON JADWAL_JAGA.id_dokter = DOKTER.id
                        JOIN POLI ON POLI.id = JADWAL_JAGA.id_poli
                        JOIN SPESIALISASI_DOKTER ON SPESIALISASI_DOKTER.id_dokter = DOKTER.id
                        JOIN SPESIALISASI ON SPESIALISASI.id = SPESIALISASI_DOKTER.id_spesialisasi
                        WHERE POLI.id = {Id}");
                return query.ToList();  
            }
        }

        public async Task<bool> Update(Poli poli)
        {
            using (var c = new SqlConnection(connection))
            {
                c.OpenAsync();
                var query = $@"UPDATE POLI set nama=@nama, lokasi=@lokasi WHERE POLI.id=@Id";
                await c.QueryAsync<Poli>(query, poli);
                return true;
            }
        }
    }
}
