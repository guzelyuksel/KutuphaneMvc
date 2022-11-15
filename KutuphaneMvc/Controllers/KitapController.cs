using KutuphaneMvc.Classes;
using KutuphaneMvc.DataAccess;
using KutuphaneMvc.Models;
using KutuphaneMvc.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace KutuphaneMvc.Controllers
{
    public class KitapController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly KitapRepository _kitapRepository;

        public KitapController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
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
            KitapViewModel kitapVM = new KitapViewModel
            {
                Yazarlar = _dbContext.Yazar.ToList(),
                Turler = _dbContext.Tur.ToList(),
                YayinEvleri = _dbContext.YayinEvi.ToList()
            };
            return View(kitapVM);
        }

        [HttpPost]
        public IActionResult Ekle(KitapViewModel kitapVM)
        {
            if (!ModelState.IsValid) return View(kitapVM);
            if(!_kitapRepository.Insert(kitapVM)) return View(kitapVM);
            return RedirectToAction(nameof(Index));
            //if (!_kitapRepository.Insert(kitap)) return View(kitap);
            //return RedirectToAction(nameof(Index));
        }

        //[HttpPost]
        //public IActionResult Ekle(Kitap kitap)
        //{
        //    if (!ModelState.IsValid) return View(kitap);
        //    if (!_kitapRepository.Insert(kitap)) return View(kitap);
        //    return RedirectToAction(nameof(Index));
        //}

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
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Sil(string isbn)
        {
            if (!_kitapRepository.Delete(isbn)) return NotFound();
            return RedirectToAction(nameof(Index));
        }
    }
}
