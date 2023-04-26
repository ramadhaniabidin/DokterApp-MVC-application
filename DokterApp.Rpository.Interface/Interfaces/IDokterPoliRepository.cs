using DokterApp.Models;

namespace DokterApp.Repositories
{
    public interface IDokterPoliRepository
    {
        public Task<IEnumerable<DoctorSpecialization>> MatchDoctorAndPoli(int Id);
        public Task<DokterPoli> SelectPoliById(int Id);
    }
}
