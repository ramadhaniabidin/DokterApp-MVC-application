using DokterApp.Models;
using DokterApp.Repositories;

namespace DokterApp.Services
{
    public class PoliService : IPoliService
    {
        private readonly IPoliRepository _poli;
        public PoliService(IPoliRepository poli)
        {
            _poli = poli;
        }
        public Task<bool> DeletePoli(int Id)
        {
            return _poli.Delete(Id);
        }

        public Task<List<Poli>> GetAllPoli()
        {
            return _poli.GetAllPoli();
        }

        public Task<Poli> GetPoliById(int Id)
        {
            return _poli.GetPoliById(Id);
        }

        public Task<bool> InsertPoli(Poli poli)
        {
            return _poli.Insert(poli);
        }

        public Task<List<DoctorSpecialization>> MatchPoliAndDoctor(int Id)
        {
            return _poli.MatchDoctorAndPoli(Id);
        }

        public Task<bool> UpdatePoli(Poli poli)
        {
            return _poli.Update(poli);
        }
    }
}
