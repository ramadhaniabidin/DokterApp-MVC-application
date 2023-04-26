using DokterApp.Models;

namespace DokterApp.Services
{
    public interface IJadwalDokterService
    {
        public Task<List<JadwalDokter>> GetAllJadwalDokter();
        public Task<List<Poli>> GetAllPoli();
        public Task<List<JadwalDokter>> GetJadwalById(int Id);
        public Task<List<JadwalDokter>> GetJadwalByNIK(string NIK);
        public Task<JadwalDokter> SelectDoctorById(int Id);
        public Task<JadwalDokter> SelectDoctorByNIK(string NIK);
        public Task<JadwalDokter> PickJadwal(int Id);
        public Task<bool> InsertJadwalDokter(JadwalDokter jadwalDokter);
        public Task<bool> UpdateJadwalDokter(JadwalDokter jadwalDokter);
        public Task<bool> DeleteJadwalDokter(int Id);
    }
}
