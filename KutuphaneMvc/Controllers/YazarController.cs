using KutuphaneMvc.Classes;
using KutuphaneMvc.DataAccess;
using KutuphaneMvc.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace KutuphaneMvc.Controllers
{
    public class YazarController : Controller
    {
        private readonly YazarRepository _yazarRepository;

        public YazarController(ApplicationDbContext dbContext)
        {
            _yazarRepository = new YazarRepository(dbContext);
        }

        public IActionResult Index()
        {
            return View(_yazarRepository.GetAll());
        }

        public IActionResult Detaylar(Guid id)
        {
            var yazar = _yazarRepository.GetById(id);
            if (yazar == null) return NotFound();
            return View(yazar);
        }

        public IActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Ekle(Yazar yazar)
        {
            if (!ModelState.IsValid) return View(yazar);
            if (!_yazarRepository.Insert(yazar)) return View(yazar);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Sil(Guid id)
        {
            _yazarRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Duzenle(Guid id)
        {
            var yazar = _yazarRepository.GetById(id);
            if (yazar == null) return NotFound();
            return View(yazar);
        }

        [HttpPost]
        public IActionResult Duzenle(Yazar yazar)
        {
            if (!ModelState.IsValid) return View(yazar);
            if (!_yazarRepository.Update(yazar)) return View(yazar);
            return RedirectToAction(nameof(Index));
        }
    }
}
