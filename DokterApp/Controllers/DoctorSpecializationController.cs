using DokterApp.Models;
using DokterApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using static System.Net.Mime.MediaTypeNames;
using System;
using Dapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Numerics;

namespace DokterApp.Controllers
{
    public class DoctorSpecializationController : Controller
    {
        IDoctorSpecializationService service;

        public DoctorSpecializationController(IDoctorSpecializationService _service)
        {
            service = _service;
        }


        public async Task<IActionResult> Index()
        {
            var doc = await service.GetAllDoctors();
            return View(doc);
        }

        public async Task<IActionResult> JadwalIndex()
        {
            var jadwal = await service.GetAllJadwalDokter();
            return View(jadwal);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult CreateJadwal()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DoctorSpecialization model)
        {
            var create = await service.InsertDoctor(model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CreateJadwal(DoctorSpecialization jadwal)
        {
            var create_jadwal = await service.InsertJadwalDokter(jadwal);
            return RedirectToAction("JadwalIndex");
        }

        public async Task<IActionResult> Details(int Id)
        {
            var select = await service.SelectDoctorById(Id);
            return View(select);
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var getDoctor = await service.SelectDoctorById(Id);
            return View(getDoctor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int Id)
        {
            if(service == null)
            {
                return Problem(" is null!");
            }
            var delete_doctor = await service.DeleteDoctor(Id);
            return Redirect("/DoctorSpecialization/");
        }

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteJadwal(int Id)
        //{
        //    if(service == null)
        //    {
        //        return Problem(" is null");
        //    }
        //    var delete_jadwal = await service.DeleteJadwalDokter(Id);
        //    return RedirectToAction("JadwalIndex");
        //}

        public async Task<IActionResult> Edit(int Id)
        {
            var doctor = await service.SelectDoctorById(Id);
            var list_spesialisasi = new List<SelectListItem>();
            foreach (var spesialisasi in await service.GetDoctorSpecializations())
            {
                list_spesialisasi.Add(new SelectListItem { Value=spesialisasi.id.ToString(), Text=spesialisasi.nama });
            }

            ViewData["ListSpesialisasi"] = list_spesialisasi;
            return View(doctor);
        }

        [HttpPost]
        public async Task<IActionResult> EditDoctor(DoctorSpecialization model)
        {
            var edit_doctor = await service.UpdateDoctor(model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditJadwal(DoctorSpecialization jadwal)
        {
            var edit_jadwal = await service.UpdateJadwalDokter(jadwal);
            return RedirectToAction("Index");
        }

    }
}
