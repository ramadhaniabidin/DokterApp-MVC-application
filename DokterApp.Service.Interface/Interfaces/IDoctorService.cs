using DokterApp.Models;

namespace DokterApp.Services
{
    public interface IDoctorService
    {
        public Task<bool> InsertDoctor(DoctorModel model);
        public Task<DoctorModel> SelectDoctorById(int Id);
        public Task<bool> DeleteDoctor(int Id);
        public Task<List<DoctorModel>> GetAllDoctors();
        public Task<bool> UpdateDoctor(DoctorModel model);
    }
}
