namespace DokterApp.Models
{
    public class DokterPoliView
    {
        public Poli poli { get; set; }
        public IEnumerable<DoctorSpecialization> Doctor { get; set; }
        
    }
}
