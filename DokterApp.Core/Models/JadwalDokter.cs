using System.ComponentModel.DataAnnotations;

namespace DokterApp.Models
{
    public class JadwalDokter: DoctorModel
    {
        [Display(Name = "Hari")]
        public string hari { get; set; }
        public int id_poli { get; set; }
        //public int id { get; set; }
        [Display(Name = "Poli")]
        public string nama_poli { get; set; }
        [Display(Name = "Lokasi")]
        public string lokasi { get; set; }
        public string spesialisasi { get; set; }


    }
}
