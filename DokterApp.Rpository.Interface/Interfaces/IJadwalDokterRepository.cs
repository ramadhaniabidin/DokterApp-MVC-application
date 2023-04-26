using DokterApp.Models;

namespace DokterApp.Repositories
{
    public interface IJadwalDokterRepository
    {
        public Task<List<JadwalDokter>> GetAllJadwal();
        public Task<List<Poli>> SelectAllPoli();
        public Task<List<JadwalDokter>> GetJadwalById(int id);
        public Task<List<JadwalDokter>> GetJadwalByNIK(string NIK);
        public Task<JadwalDokter> SelectDoctorById(int Id);
        public Task<JadwalDokter> SelectDoctorByNIK(string NIK);
        public Task<JadwalDokter> PickJadwal(int Id);
        public Task<bool> Insert(JadwalDokter jadwalDokter);
        public Task<bool> Update(JadwalDokter jadwalDokter);
        public Task<bool> Delete(int Id);

    }
}
