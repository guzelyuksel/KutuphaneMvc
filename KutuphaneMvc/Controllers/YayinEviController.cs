using KutuphaneMvc.Classes;
using KutuphaneMvc.DataAccess;
using KutuphaneMvc.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace KutuphaneMvc.Controllers
{
    public class YayinEviController : Controller
    {
        private readonly YayinEviRepository _yayinEviRepository;

        public YayinEviController(ApplicationDbContext dbContext)
        {
            _yayinEviRepository = new YayinEviRepository(dbContext);
        }

        public IActionResult Index()
        {
            return View(_yayinEviRepository.GetAll());
        }

        public IActionResult Duzenle(Guid id)
        {
            var yayinEviBul = _yayinEviRepository.GetById(id);
            if (yayinEviBul == null) return NotFound();
            return View(yayinEviBul);
        }

        [HttpPost]
        public IActionResult Duzenle(YayinEvi yayinEvi)
        {
            if (!ModelState.IsValid) return View(yayinEvi);
            if (!_yayinEviRepository.Update(yayinEvi)) return View(yayinEvi);
            return RedirectToAction("Index");

        }

        public IActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Ekle(YayinEvi yayinEvi)
        {
            if (!ModelState.IsValid) return View(yayinEvi);
            if (!_yayinEviRepository.Insert(yayinEvi)) return View(yayinEvi);
            return RedirectToAction("Index");
        }

        public IActionResult Sil(Guid id)
        {
            var yayinEviBul = _yayinEviRepository.GetById(id);
            if (yayinEviBul == null) return NotFound();
            _yayinEviRepository.Delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult Detaylar(Guid id)
        {
            var yayinEviBul = _yayinEviRepository.GetById(id);
            if (yayinEviBul == null) return NotFound();
            return View(yayinEviBul);
        }
    }
}
