using KutuphaneMvc.Classes;
using KutuphaneMvc.DataAccess;
using KutuphaneMvc.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace KutuphaneMvc.Controllers
{
    public class KitapController : Controller
    {
        private readonly KitapRepository _kitapRepository;

        public KitapController(ApplicationDbContext dbContext)
        {
            _kitapRepository = new KitapRepository(dbContext);
        }

        public IActionResult Index()
        {
            return View(_kitapRepository.GetAll());
        }

        public IActionResult Detaylar(string isbn)
        {
            var kitap = _kitapRepository.GetById(isbn);
            if (kitap == null) return NotFound();
            return View(kitap);
        }

        public IActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Ekle(Kitap kitap)
        {
            if (!ModelState.IsValid) return View(kitap);
            if (!_kitapRepository.Insert(kitap)) return View(kitap);
            return RedirectToAction("Index");
        }

        public IActionResult Guncelle(string isbn)
        {
            var kitap = _kitapRepository.GetById(isbn);
            if (kitap == null) return NotFound();
            return View(kitap);
        }

        [HttpPost]
        public IActionResult Guncelle(Kitap kitap)
        {
            if (!ModelState.IsValid) return View(kitap);
            if (!_kitapRepository.Update(kitap)) return View(kitap);
            return RedirectToAction("Index");
        }

        public IActionResult Sil(string isbn)
        {
            if (!_kitapRepository.Delete(isbn)) return NotFound();
            return RedirectToAction("Index");
        }
    }
}
