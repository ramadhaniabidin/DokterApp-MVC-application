using DokterApp.Models;

namespace DokterApp.Repositories
{
    public interface IDoctorRepository
    {
        public Task<List<DoctorModel>> SelectAllDoctors();
        public Task<List<DoctorModel>> MatchDoctorAndPoli(int Id);
        public Task<bool> Insert(DoctorModel model);
        public Task<DoctorModel> SelectDoctorById(int Id);
        public Task<bool> Update(DoctorModel model);
        public Task<bool> Delete(int Id);
    }
}
