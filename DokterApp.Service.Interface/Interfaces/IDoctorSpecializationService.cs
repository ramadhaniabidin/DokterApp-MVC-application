using DokterApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Numerics;

namespace DokterApp.Services
{
    public interface IDoctorSpecializationService
    {
        public Task<bool> InsertDoctor(DoctorSpecialization model);
        public Task<DoctorSpecialization> SelectDoctorById(int Id);
        public Task<bool> DeleteDoctor(int Id);
        public Task<List<DoctorSpecialization>> GetAllDoctors();
        public Task<List<Spesialisasi>> GetDoctorSpecializations();
        public Task<bool> UpdateDoctor(DoctorSpecialization model);
        public Task<List<DoctorSpecialization>> GetAllJadwalDokter();
        public Task<DoctorSpecialization> GetJadwalById(int Id);
        public Task<bool> InsertJadwalDokter(DoctorSpecialization jadwalDokter);
        public Task<bool> UpdateJadwalDokter(DoctorSpecialization jadwalDokter);
        public Task<bool> DeleteJadwalDokter(int Id);
    }
}
