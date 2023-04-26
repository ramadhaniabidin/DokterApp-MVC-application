using DokterApp.Models;

namespace DokterApp.Services
{
    public interface ICariService
    {
        public Task<IEnumerable<Cari>> SearchDoctor(string poli, string spesialisasi);
        public Task<List<Poli>> GetAllPoli();
        public Task<List<Spesialisasi>> GetDoctorSpecializations();
    }
}
