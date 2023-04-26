using DokterApp.Models;
using DokterApp.Repositories;

namespace DokterApp.Services
{
    public class JadwalDokterService : IJadwalDokterService
    {
        private readonly IJadwalDokterRepository _repository;
        public JadwalDokterService(IJadwalDokterRepository repository)
        {
            _repository = repository;
        }

        public Task<bool> DeleteJadwalDokter(int Id)
        {
            return _repository.Delete(Id);
        }

        public Task<List<JadwalDokter>> GetAllJadwalDokter()
        {
            return _repository.GetAllJadwal();
        }

        public Task<List<JadwalDokter>> GetJadwalById(int Id)
        {
            return _repository.GetJadwalById(Id);
        }

        public Task<List<JadwalDokter>> GetJadwalByNIK(string NIK)
        {
            return _repository.GetJadwalByNIK(NIK);
        }

        public Task<bool> InsertJadwalDokter(JadwalDokter jadwalDokter)
        {
            return _repository.Insert(jadwalDokter);
        }

        public Task<JadwalDokter> SelectDoctorById(int Id)
        {
            return _repository.SelectDoctorById(Id);
        }

        public Task<JadwalDokter> SelectDoctorByNIK(string NIK)
        {
            return _repository.SelectDoctorByNIK(NIK);
        }

        public Task<JadwalDokter> PickJadwal(int Id)
        {
            return _repository.PickJadwal(Id);
        }

        public Task<bool> UpdateJadwalDokter(JadwalDokter jadwalDokter)
        {
            return _repository.Update(jadwalDokter);
        }

        public Task<List<Poli>> GetAllPoli()
        {
            return _repository.SelectAllPoli();
        }
    }
}
