using KutuphaneMvc.Classes;
using KutuphaneMvc.DataAccess;
using KutuphaneMvc.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace KutuphaneMvc.Controllers
{
    public class TurController : Controller
    {
        private readonly TurRepository _turRepository;

        public TurController(ApplicationDbContext dbContext)
        {
            _turRepository = new TurRepository(dbContext);
        }

        public IActionResult Index()
        {
            return View(_turRepository.GetAll());
        }

        public IActionResult Duzenle(Guid id)
        {
            var turBul = _turRepository.GetById(id);
            if (turBul == null) return NotFound();
            return View(turBul);
        }

        [HttpPost]
        public IActionResult Duzenle(Tur tur)
        {
            if (!ModelState.IsValid) return View(tur);
            if (!_turRepository.Update(tur)) return View(tur);
            return RedirectToAction("Index");

        }

        public IActionResult Sil(Guid id)
        {
            _turRepository.Delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Ekle(Tur tur)
        {
            if (!ModelState.IsValid) return View(tur);
            if (!_turRepository.Insert(tur)) return View(tur);
            return RedirectToAction("Index");

        }

        public IActionResult Detaylar(Guid id)
        {
            var turBul = _turRepository.GetById(id);
            if (turBul == null) return NotFound();
            return View(turBul);
        }
    }
}
