using System.ComponentModel.DataAnnotations;

namespace DokterApp.Models
{
    public class DokterPoli: Poli
    {
        [Display(Name = "Hari")]
        public string hari { get; set; }
        public string nip { get; set; }
        public string nik { get; set; }
        public int id_poli { get; set; }
        //public int id { get; set; }
        [Display(Name = "Poli")]
        public string nama_poli { get; set; }
        [Display(Name = "Lokasi")]
        public string lokasi { get; set; }
        public string nama_spesialisasi { get; set; }
    }
}
