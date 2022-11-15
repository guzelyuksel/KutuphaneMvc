using KutuphaneMvc.Classes;
using KutuphaneMvc.DataAccess;
using KutuphaneMvc.Models;
using KutuphaneMvc.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            //ViewBag.Turler = _dbContext.Tur.Select(x => new SelectListItem
            //{
            //    Text = x.Ad,
            //    Value = x.Id.ToString()
            //}).ToList();
            //ViewBag.Yazarlar = _dbContext.Yazar.Select(x => new SelectListItem
            //{
            //    Text = x.AdSoyad,
            //    Value = x.Id.ToString()
            //}).ToList();
            //ViewBag.YayinEvleri = _dbContext.YayinEvi.Select(x => new SelectListItem
            //{
            //    Text = x.Ad,
            //    Value = x.Id.ToString()
            //}).ToList();
            KitapViewModel kitapVM = new()
            {
                Turler = _dbContext.Tur.Select(x => new SelectListItem
                {
                    Text = x.Ad,
                    Value = x.Id.ToString()
                }).ToList(),
                Yazarlar = _dbContext.Yazar.Select(x => new SelectListItem
                {
                    Text = x.AdSoyad,
                    Value = x.Id.ToString()
                }).ToList(),
                YayinEvleri = _dbContext.YayinEvi.Select(x => new SelectListItem
                {
                    Text = x.Ad,
                    Value = x.Id.ToString()
                }).ToList()
            };
            return View(kitapVM);
        }

        [HttpPost]
        public IActionResult Ekle(KitapViewModel kitapVM)
        {
            if (!ModelState.IsValid)
            {
                kitapVM = new()
                {
                    Turler = _dbContext.Tur.Select(x => new SelectListItem
                    {
                        Text = x.Ad,
                        Value = x.Id.ToString()
                    }).ToList(),
                    Yazarlar = _dbContext.Yazar.Select(x => new SelectListItem
                    {
                        Text = x.AdSoyad,
                        Value = x.Id.ToString()
                    }).ToList(),
                    YayinEvleri = _dbContext.YayinEvi.Select(x => new SelectListItem
                    {
                        Text = x.Ad,
                        Value = x.Id.ToString()
                    }).ToList()
                };
                return View(kitapVM);
            }
            if (!_kitapRepository.Insert(kitapVM.Kitap)) return View(kitapVM);
            return RedirectToAction(nameof(Index));
            //if (!_kitapRepository.Insert(kitap)) return View(kitap);
            //return RedirectToAction(nameof(Index));
        }

        //[HttpPost]
        //public IActionResult Ekle(Kitap kitap, Guid[] turGuids, Guid[] yazarGuids, Guid yayinEviGuid)
        //{
        //    if (!ModelState.IsValid) return View(kitap);
        //    //if (turGuids == null || yazarGuids == null || yayinEviGuid == Guid.Empty)
        //    if(turGuids.Count() == 0)
        //    {
        //        ModelState.AddModelError(nameof(kitap.Turler), "Tür kısmı boş geçilemez !");
        //        return View(kitap);
        //    }
        //    if (!_kitapRepository.Insert(kitap, turGuids, yazarGuids, yayinEviGuid)) return View(kitap);
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
