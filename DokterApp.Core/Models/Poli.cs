using System.ComponentModel.DataAnnotations;

namespace DokterApp.Models
{
    public class Poli
    {
        public int id { get; set; }
        [Display(Name = "Poli")]
        public string nama { get; set; }
        [Display(Name = "Lokasi")]
        public string lokasi { get; set; }
    }
}
