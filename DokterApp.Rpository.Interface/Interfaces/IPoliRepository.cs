using DokterApp.Models;

namespace DokterApp.Repositories
{
    public interface IPoliRepository
    {
        public Task<List<Poli>> GetAllPoli();
        public Task<Poli> GetPoliById(int Id);
        public Task<List<DoctorSpecialization>> MatchDoctorAndPoli(int Id);
        public Task<bool> Insert(Poli poli);
        public Task<bool> Update(Poli poli);
        public Task<bool> Delete(int Id);
    }
}
