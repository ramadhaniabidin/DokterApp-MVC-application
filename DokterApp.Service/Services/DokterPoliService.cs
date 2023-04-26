using DokterApp.Models;
using DokterApp.Repositories;

namespace DokterApp.Services
{
    public class DokterPoliService : IDokterPoliService
    {
        private readonly IDokterPoliRepository _repository;
        public DokterPoliService(IDokterPoliRepository repository)
        {
            _repository = repository;
        }
        public Task<IEnumerable<DoctorSpecialization>> MatchDoctorAndPoli(int Id)
        {
            return _repository.MatchDoctorAndPoli(Id);
        }

        public Task<DokterPoli> SelectPoliById(int Id)
        {
            return _repository.SelectPoliById(Id);
        }
    }
}
