using Dapper;
using DokterApp.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DokterApp.Repositories
{
    public class DokterPoliRepository : IDokterPoliRepository
    {
        private string _connection;
        public DokterPoliRepository(IConfiguration configuration)
        {
            _connection = configuration.GetConnectionString("SqlServer");
        }
        public async Task<IEnumerable<DoctorSpecialization>> MatchDoctorAndPoli(int Id)
        {
            using(var conn = new SqlConnection(_connection))
            {
                await conn.OpenAsync();
                var query = await conn.QueryAsync<DoctorSpecialization>($@"
                            SELECT DISTINCT JADWAL_JAGA.hari as hari, DOKTER.nama as nama, POLI.id, DOKTER.nip, DOKTER.nik, SPESIALISASI.nama as spesialisasi FROM DOKTER
                            JOIN JADWAL_JAGA ON JADWAL_JAGA.id_dokter = DOKTER.id
                            JOIN POLI ON POLI.id = JADWAL_JAGA.id_poli
                            JOIN SPESIALISASI_DOKTER ON SPESIALISASI_DOKTER.id_dokter = DOKTER.id
                            JOIN SPESIALISASI ON SPESIALISASI.id = SPESIALISASI_DOKTER.id_spesialisasi
                            WHERE POLI.id = {Id}");
                return query.ToList();
            }
        }

        public async Task<DokterPoli> SelectPoliById(int Id)
        {
            using (var conn = new SqlConnection(_connection))
            {
                var query = $@"SELECT DOKTER.id, DOKTER.nama, DOKTER.nip, DOKTER.nik, DOKTER.tanggal_lahir, DOKTER.tempat_lahir, DOKTER.jenis_kelamin, DOKTER.image_url, SPESIALISASI.nama as 'Spesialisasi'
                                  FROM DOKTER
                                  JOIN SPESIALISASI_DOKTER ON DOKTER.id = SPESIALISASI_DOKTER.id_dokter
                                  JOIN SPESIALISASI ON SPESIALISASI.id = SPESIALISASI_DOKTER.id_spesialisasi
                                  WHERE DOKTER.id = {Id}";
                var output = await conn.QueryFirstOrDefaultAsync<DokterPoli>(query);
                return output;
            }
        }
    }
}
