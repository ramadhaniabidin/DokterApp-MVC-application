using DokterApp.Models;
using DokterApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace DokterApp.Controllers
{
    public class DokterPoliController : Controller
    {

        //IJadwalDokterService _jadwalDokterService;
        //public JadwalDokterController(IJadwalDokterService jadwalDokterService)
        //{
        //    _jadwalDokterService = jadwalDokterService;
        //}

        //public async Task<IActionResult> Details(int Id, string NIK)
        //{
        //    var jadwal = await _jadwalDokterService.GetJadwalById(Id);
        //    var dokter = await _jadwalDokterService.SelectDoctorByNIK(NIK);

        //    var model = new JadwalDokterViewModel { Doctor = dokter, Jadwals = jadwal };
        //    return View(model);
        //}

        IDokterPoliService _service;
        IPoliService _poliService;
        public DokterPoliController(IDokterPoliService service, IPoliService poliService)
        {
            _service = service;
            _poliService = poliService;
        }
        public async Task<IActionResult> Index(int Id)
        {
            var poli = await _poliService.GetPoliById(Id);
            var dokter = await _service.MatchDoctorAndPoli(Id);
            //var poli = await _service.SelectPoliById(Id);

            var model = new DokterPoliView { Doctor = dokter, poli = poli };
            return View(model);
        }
    }
}
