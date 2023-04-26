using DokterApp.Models;
using DokterApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace DokterApp.Controllers
{
    public class PoliController : Controller
    {
        IPoliService _poliService;
        IDokterPoliService _service;
        public PoliController(IPoliService poliService)
        {
            _poliService = poliService;
        }
        public async Task<IActionResult> Index()
        {
            var poli = await _poliService.GetAllPoli();
            return View(poli);
        }

        public async Task<IActionResult> Details(int Id)
        {
            var poli = await _poliService.MatchPoliAndDoctor(Id);
            return View(poli);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Poli poli)
        {
            var create = await _poliService.InsertPoli(poli);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Edit(int Id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditJadwal(Poli model)
        {
            var edit_doctor = await _poliService.UpdatePoli(model);
            return Redirect("/Poli");
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var getDoctor = await _poliService.GetPoliById(Id);
            return View(getDoctor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int Id)
        {
            if (_poliService == null)
            {
                return Problem(" is null!");
            }
            var delete_doctor = await _poliService.DeletePoli(Id);
            return Redirect("/Poli/");
        }

    }
}
