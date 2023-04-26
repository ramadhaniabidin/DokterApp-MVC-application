using DokterApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace DokterApp.Controllers
{
    public class CariController : Controller
    {
        ICariService _CariService;
        public CariController(ICariService cariService)
        {
            _CariService = cariService;
        }
        public async Task<IActionResult> Index()
        {
            ViewData["ListPoli"] = await _CariService.GetAllPoli();
            ViewData["ListSpesialisasi"] = await _CariService.GetDoctorSpecializations();
            return View();
        }

        public async Task<IActionResult> SearchDoctor(string poli, string spesialisasi)
        {
            var doc = await _CariService.SearchDoctor(poli, spesialisasi);
            return View(doc);
        }
    }
}
