using DokterApp.Models;
using DokterApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DokterApp.Controllers
{
    public class JadwalDokterController : Controller
    {
        IJadwalDokterService _jadwalDokterService;
        public JadwalDokterController(IJadwalDokterService jadwalDokterService)
        {
            _jadwalDokterService = jadwalDokterService;
        }

        public async Task<IActionResult> Index()
        {
            var jadwal_dokter = await _jadwalDokterService.GetAllJadwalDokter();
            return View(jadwal_dokter);
        }

        public async Task<IActionResult> Details(int Id, string NIK)
        {
            var jadwal = await _jadwalDokterService.GetJadwalById(Id);
            var dokter = await _jadwalDokterService.SelectDoctorByNIK(NIK);

            var model = new JadwalDokterViewModel { Doctor = dokter, Jadwals = jadwal };
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["ListPoli"] = await _jadwalDokterService.GetAllPoli();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(JadwalDokter jadwal)
        {
            var create_jadwal = await _jadwalDokterService.InsertJadwalDokter(jadwal);
            //return Redirect("/JadwalDokter/Details/" + jadwal.id+"?nik="+jadwal.nik);
            return Redirect("/DoctorSpecialization/");

        }

        public async Task<IActionResult> Delete(int Id)
        {
            var getJadwal = await _jadwalDokterService.PickJadwal(Id);
            return View(getJadwal);
        }



        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int Id)
        {
            if (_jadwalDokterService == null)
            {
                return Problem(" is null!");
            }
            var delete_doctor = await _jadwalDokterService.DeleteJadwalDokter(Id);
            return Redirect("/DoctorSpecialization");
        }

        public async Task<IActionResult> Edit(int Id)
        {
            //var jadwal = await _jadwalDokterService.PickJadwal(Id);
            //var list_poli = new List<SelectListItem>();
            //foreach(var poli in await _jadwalDokterService.GetAllPoli())
            //{
            //    list_poli.Add(new SelectListItem { Value = poli.nama, Text = poli.nama });
            //}
            //ViewData["ListPoli"] = list_poli;
            ViewData["ListPoli"] = await _jadwalDokterService.GetAllPoli();
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> EditJadwal(JadwalDokter model)
        {
            var edit_doctor = await _jadwalDokterService.UpdateJadwalDokter(model);
            return Redirect("/DoctorSpecialization");
        }

    }
}
