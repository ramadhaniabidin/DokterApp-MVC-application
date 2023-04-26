using Dapper;
using DokterApp.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DokterApp.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private string connection;
        public DoctorRepository(IConfiguration configuration)
        {
            connection = configuration.GetConnectionString("SqlServer");
        }
        public async Task<bool> Delete(int Id)
        {
            using (var c = new SqlConnection(connection))
            {
                var query = @"  DELETE FROM SPESIALISASI_DOKTER WHERE SPESIALISASI_DOKTER.id_dokter = (SELECT id FROM DOKTER WHERE DOKTER.id = SPESIALISASI_DOKTER.id_dokter)
                                delete from DOKTER where DOKTER.id = @Id";
                var deletedRow = c.Execute(query, new { id = Id });
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

        public async Task<bool> Insert(DoctorModel model)
        {
            using (var c = new SqlConnection(connection))
            {
                await c.OpenAsync();
                var query =
                    @" 
                    insert into DOKTER (nama, nip, nik, tanggal_lahir, tempat_lahir, jenis_kelamin) 
                    values (@nama, '', @nik, @tanggal_lahir, @tempat_lahir, @jenis_kelamin)
                    
                    
                    DECLARE @id_dokter_baru INT	
                    SELECT TOP 1 @id_dokter_baru = DOKTER.id FROM DOKTER ORDER BY DOKTER.id DESC
                    EXEC UpdateNIP @id_dokter_baru";

                await c.ExecuteAsync(query, model);
            }
            return true;
        }

        public async Task<List<DoctorModel>> SelectAllDoctors()
        {
            using (var c = new SqlConnection(connection))
            {
                await c.OpenAsync();
                var query = await c.QueryAsync<DoctorModel>($@"select * from DOKTER go");
                return query.ToList();
            }
        }

        public Task<List<DoctorModel>> MatchDoctorAndPoli(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<DoctorModel> SelectDoctorById(int Id)
        {
            using (var c = new SqlConnection(connection))
            {
                var statement = @"select * from DOKTER where DOKTER.id = @Id";
                var output = await c.QueryFirstOrDefaultAsync<DoctorModel>(statement, new { id = Id });
                return output;
            }
        }

        //public async Task<DoctorAndSpecialization> DoctorAndSpecializationById(int Id)
        //{
        //    using (var c = new SqlConnection(connection))
        //    {
        //        var query = @"select * from DOKTER where DOKTER.id = @Id";
        //        var output = await c.QueryFirstOrDefaultAsync<DoctorAndSpecialization>(query, new { id = Id });
        //        return output;
        //    }
        //}


        public async Task<bool> Update(DoctorModel model)
        {
            using (var c = new SqlConnection(connection))
            {
                c.OpenAsync();
                var statement = $@"update DOKTER set Name=@Name, NIP=@NIP, NIK=@NIK, TanggalLahir=@TanggalLahir, 
                TempatLahir=@TempatLahir, JenisKelamin=@JenisKelamin where Id=@Id";
                await c.QueryAsync<DoctorModel>(statement, model);
                return true;
            }
        }

        //public async Task<IEnumerable<DoctorAndSpecialization>> GetAllDoctors()
        //{
        //    using (var c = new SqlConnection(connection))
        //    {
        //        await c.OpenAsync();
        //        var query = await c.QueryAsync<DoctorAndSpecialization>($@"select DOKTER.nama, Dokter.id, DOKTER.nip, DOKTER.nik, SPESIALISASI.nama as 'spesialisasi' from DOKTER
        //        join SPESIALISASI_DOKTER on DOKTER.id = SPESIALISASI_DOKTER.id_dokter
        //        join SPESIALISASI on SPESIALISASI.id = SPESIALISASI_DOKTER.id_spesialisasi");
        //        return query.ToList();
        //    }
        //}
    }
}
