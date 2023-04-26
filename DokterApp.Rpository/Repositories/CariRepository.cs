using Dapper;
using DokterApp.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DokterApp.Repositories
{
    public class CariRepository : ICariRepository
    {
        private string connection;
        public CariRepository(IConfiguration configuration)
        {
            connection = configuration.GetConnectionString("SqlServer");
        }
        public async Task<IEnumerable<Cari>> SearchDoctor(string nama_poli, string spesialisasi)
        {
            using (var conn = new SqlConnection(connection))
            {
                await conn.OpenAsync();
                var query = ($@"
                SELECT DISTINCT DOKTER.nama as nama, DOKTER.nip, DOKTER.nik, DOKTER.jenis_kelamin, SPESIALISASI.nama as spesialisasi
                FROM DOKTER
                JOIN SPESIALISASI_DOKTER ON DOKTER.id = SPESIALISASI_DOKTER.id_dokter
                JOIN SPESIALISASI ON SPESIALISASI.id = SPESIALISASI_DOKTER.id_spesialisasi
                JOIN JADWAL_JAGA ON JADWAL_JAGA.id_dokter = DOKTER.id
                JOIN POLI ON JADWAL_JAGA.id_poli = POLI.id
                WHERE POLI.nama LIKE '%' + @nama + '%' OR SPESIALISASI.nama LIKE '%' + @spesialisasi + '%'
                ");
                var output = conn.Query<Cari>(query, new {nama=nama_poli, spesialisasi=spesialisasi}).ToList();
                return output;
            }
        }

        public async Task<List<Poli>> SelectAllPoli()
        {
            using (var conn = new SqlConnection(connection))
            {
                await conn.OpenAsync();
                var query = await conn.QueryAsync<Poli>(@"
                SELECT POLI.id, POLI.nama, POLI.lokasi FROM POLI");
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
    }
}
