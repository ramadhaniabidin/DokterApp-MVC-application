using DokterApp.Models;
using DokterApp.Repositories;

namespace DokterApp.Services
{
    public class DoctorService: IDoctorService
    {
        private readonly IDoctorRepository _doctor;

        public DoctorService(IDoctorRepository doctor)
        {
            _doctor = doctor;
        }

        public Task<bool> DeleteDoctor(int Id)
        {
            return _doctor.Delete(Id);
        }

        public Task<List<DoctorModel>> GetAllDoctors()
        {
            return _doctor.SelectAllDoctors();
        }

        public Task<bool> InsertDoctor(DoctorModel model)
        {
            return _doctor.Insert(model);
        }

        public Task<DoctorModel> SelectDoctorById(int Id)
        {
            return _doctor.SelectDoctorById(Id);
        }

        //public Task<DoctorAndSpecialization>DoctorAndSpecializationById(int Id)
        //{
        //    return _doctor.DoctorAndSpecializationById(Id);
        //}

        public Task<bool> UpdateDoctor(DoctorModel model)
        {
            return _doctor.Update(model);
        }

        //public Task<IEnumerable<DoctorAndSpecialization>> GetAllDoctorAndSpecializations()
        //{
        //    return _doctor.GetAllDoctors();
        //}
    }
}
