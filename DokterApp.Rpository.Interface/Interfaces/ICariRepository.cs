using DokterApp.Models;

namespace DokterApp.Repositories
{
    public interface ICariRepository
    {
        public Task<IEnumerable<Cari>> SearchDoctor(string poli, string spesialisasi);
        public Task<List<Poli>> SelectAllPoli();
        public Task<List<Spesialisasi>> SelectAllSpecializations();
    }
}
