using DokterApp.Models;

namespace DokterApp.Services
{
    public interface IDokterPoliService
    {
        public Task<IEnumerable<DoctorSpecialization>> MatchDoctorAndPoli(int Id);
        public Task<DokterPoli> SelectPoliById(int Id);
    }
}
