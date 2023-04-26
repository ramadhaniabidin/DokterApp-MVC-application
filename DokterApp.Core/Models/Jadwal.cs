namespace DokterApp.Models
{
    public class Jadwal: DoctorModel
    {
        public string hari { get; set; } = null!;
        public int id_poli { get; set; }
        public int id_dokter { get; set; }
    }
}
