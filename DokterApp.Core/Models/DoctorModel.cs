using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DokterApp.Models
{
    public class DoctorModel
    {
        [Display(Name = "ID")]
        public int id { get; set; }
       
        
        [Display(Name = "Nama")]
        public string nama { get; set; }
      
        
        [Display(Name = "NIP")]
        public string? nip { get; set; }


        [Display(Name = "NIK")]
        public string nik { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Tanggal Lahir")]
        public DateTime tanggal_lahir { get; set; }
       
        
        [Display(Name = "Tempat Lahir")]
        public string tempat_lahir { get; set; }


        [Display(Name = "Jenis Kelamin")]
        public int jenis_kelamin { get; set; }

        
    }
}
    