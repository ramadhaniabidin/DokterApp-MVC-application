using DokterApp.Models;
using DokterApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DokterApp.Controllers
{
    public class DoctorController : Controller
    {
        IDoctorService service;
        public DoctorController(IDoctorService _service)
        {
            service = _service;
        }

        public async Task<IActionResult> Index()
        {
            var m = await service.GetAllDoctors();
            return View(m);
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Details(int Id)
        {
            var select = await service.SelectDoctorById(Id);
            if (select == null) return NotFound();
            return View(select);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DoctorModel model)
        {
            if (ModelState.IsValid)
            {
                var create = await service.InsertDoctor(model);
                return RedirectToAction(nameof(Index));
            }
            return View();
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
            if (service == null)
            {
                return Problem(" is null!");
            }
            var delete_doctor = await service.DeleteDoctor(Id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int Id)
        {
            var doctor = await service.SelectDoctorById(Id);
            return View(doctor);
        }

        [HttpPost]
        public async Task<IActionResult> EditDoctor(DoctorModel doctor)
        {
            var edit_doctor = await service.UpdateDoctor(doctor);
            return RedirectToAction(nameof(Index));
        }
    }
}
