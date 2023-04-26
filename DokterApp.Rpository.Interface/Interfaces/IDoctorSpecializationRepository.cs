using DokterApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Numerics;

namespace DokterApp.Repositories
{
    public interface IDoctorSpecializationRepository
    {
        public Task<List<DoctorSpecialization>> SelectAllDoctors();
        public Task<List<Spesialisasi>> SelectAllSpecializations();
        public Task<bool> InsertDoctor(DoctorSpecialization model);
        public Task<DoctorSpecialization> SelectDoctorById(int Id);
        public Task<bool> UpdateDoctor(DoctorSpecialization model);
        public Task<bool> DeleteDoctor(int Id);
        public Task<List<DoctorSpecialization>> GetAllJadwal();
        public Task<DoctorSpecialization> GetJadwalById(int Id);
        public Task<bool> InsertJadwal(DoctorSpecialization jadwalDokter);
        public Task<bool> UpdateJadwal(DoctorSpecialization jadwalDokter);
        public Task<bool> DeleteJadwal(int Id);
    }
}
