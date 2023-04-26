using DokterApp.Models;
using DokterApp.Repositories;

namespace DokterApp.Services
{
    public class CariService: ICariService
    {
        private readonly ICariRepository _cariRepository;
        public CariService(ICariRepository cariRepository)
        {
            _cariRepository = cariRepository;
        }

        public Task<IEnumerable<Cari>> SearchDoctor(string poli, string spesialisasi)
        {
            return _cariRepository.SearchDoctor(poli, spesialisasi);
        }

        public Task<List<Poli>> GetAllPoli()
        {
            return _cariRepository.SelectAllPoli();
        }
        public Task<List<Spesialisasi>> GetDoctorSpecializations()
        {
            return _cariRepository.SelectAllSpecializations();
        }
    }
}
