using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DokterApp.Models
{
    public class DoctorSpecialization: DoctorModel
    {
        [Display(Name = "Poli")]
        public string nama_poli { get; set; }
        [Display(Name = "Ruangan")]
        public string lokasi { get; set; }
        public string hari { get; set; }
        [Display(Name = "Spesialisasi")]
        public string spesialisasi { get; set; }
        public string? image_url { get; set; }


    }
}
