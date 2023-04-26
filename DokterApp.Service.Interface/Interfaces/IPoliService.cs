using DokterApp.Models;

namespace DokterApp.Services
{
    public interface IPoliService
    {
        public Task<bool> InsertPoli(Poli poli);
        public Task<bool> UpdatePoli(Poli poli);
        public Task<bool> DeletePoli(int Id);
        public Task<List<Poli>> GetAllPoli();
        public Task<List<DoctorSpecialization>> MatchPoliAndDoctor(int Id);
        public Task<Poli> GetPoliById(int Id);
    }
}
