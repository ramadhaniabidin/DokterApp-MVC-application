using DokterApp.Models;
using DokterApp.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Numerics;

namespace DokterApp.Services
{
    public class DoctorSpecializationService : IDoctorSpecializationService
    {
        private readonly IDoctorSpecializationRepository _doctorSpecialization;
        public DoctorSpecializationService(IDoctorSpecializationRepository doctorSpecialization)
        {
            _doctorSpecialization = doctorSpecialization;
        }

        public Task<bool> DeleteDoctor(int Id)
        {
            return _doctorSpecialization.DeleteDoctor(Id);
        }

        public Task<bool> DeleteJadwalDokter(int Id)
        {
            return _doctorSpecialization.DeleteJadwal(Id);
        }

        public Task<List<DoctorSpecialization>> GetAllDoctors()
        {
            return _doctorSpecialization.SelectAllDoctors();
        }
        public Task<List<Spesialisasi>> GetDoctorSpecializations()
        {
            return _doctorSpecialization.SelectAllSpecializations();
        }

        public Task<List<DoctorSpecialization>> GetAllJadwalDokter()
        {
            return _doctorSpecialization.GetAllJadwal();
        }

        public Task<bool> InsertDoctor(DoctorSpecialization model)
        {
            return _doctorSpecialization.InsertDoctor(model);
        }

        public Task<bool> InsertJadwalDokter(DoctorSpecialization    jadwalDokter)
        {
            return _doctorSpecialization.InsertJadwal(jadwalDokter);
        }

        public Task<DoctorSpecialization> SelectDoctorById(int Id)
        {
            return _doctorSpecialization.SelectDoctorById(Id);
        }

        public Task<DoctorSpecialization> GetJadwalById(int Id)
        {
            return _doctorSpecialization.GetJadwalById(Id);
        }

        public Task<bool> UpdateDoctor(DoctorSpecialization model)
        {
            return _doctorSpecialization.UpdateDoctor(model);
        }

        public Task<bool> UpdateJadwalDokter(DoctorSpecialization jadwalDokter)
        {
            return _doctorSpecialization.UpdateJadwal(jadwalDokter);
        }


    }
}
