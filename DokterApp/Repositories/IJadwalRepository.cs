using DokterApp.Models;

namespace DokterApp.Repositories
{
    public interface IJadwalRepository
    {
        public Task<List<Jadwal>> GetAllJadwal();
        public Task<Jadwal> GetJadwalById(int Id);
        public Task<bool> Insert(Jadwal jadwal);
        public Task<bool> Update(Jadwal jadwal);
        public Task<bool> Delete(int Id);
    }
}
