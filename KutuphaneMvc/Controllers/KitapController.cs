using KutuphaneMvc.Classes;
using KutuphaneMvc.DataAccess;
using KutuphaneMvc.Models;
using KutuphaneMvc.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;

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

        public IActionResult Detaylar(string id)
        {
            var kitap = _kitapRepository.GetById(id);
            if (kitap == null) return NotFound();
            return View(kitap);
        }

        public IActionResult Ekle()
        {
            //KitapEkleViewModel kitapVM = new()
            //{
            //    Turler = _dbContext.Tur.Select(x => new TurListViewModel
            //    {
            //        Id = x.Id,
            //        Ad = x.Ad,
            //    }).ToList(),
            //    Yazarlar = _dbContext.Yazar.Select(x => new YazarListViewModel
            //    {
            //        Id = x.Id,
            //        Ad = x.Ad,
            //        Soyad = x.Soyad,
            //        DogumTarihi = x.DogumTarihi,
            //        Cinsiyet = x.Cinsiyet,
            //        Adres = x.Adres,
            //        Telefon = x.Telefon,
            //        Mail = x.Mail
            //    }).ToList(),
            //    YayinEvleri = _dbContext.YayinEvi.Select(x => new YayinEviListViewModel
            //    {
            //        Id = x.Id,
            //        Ad = x.Ad,
            //        KurulusYili = x.KurulusYili,
            //        Adres = x.Ad,
            //        Telefon = x.Telefon,
            //        Mail = x.Mail
            //    }).ToList()
            //};
            ViewBag.TurId = _dbContext.Tur.ToList();
            ViewBag.YazarId = _dbContext.Yazar.ToList();
            ViewBag.YayinEviId = _dbContext.YayinEvi.ToList();
            return View(Tuple.Create(new Kitap(), new List<Tur>(), new List<Yazar>(), new YayinEvi()));
        }

        //[HttpPost]
        //public IActionResult Ekle(KitapEkleViewModel kitapVM)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        if (kitapVM.Turler == null)
        //        {
        //            kitapVM = new()
        //            {
        //                Turler = _dbContext.Tur.Select(x => new TurListViewModel
        //                {
        //                    Id = x.Id,
        //                    Ad = x.Ad,
        //                }).ToList(),
        //                Yazarlar = _dbContext.Yazar.Select(x => new YazarListViewModel
        //                {
        //                    Id = x.Id,
        //                    Ad = x.Ad,
        //                    Soyad = x.Soyad,
        //                    DogumTarihi = x.DogumTarihi,
        //                    Cinsiyet = x.Cinsiyet,
        //                    Adres = x.Adres,
        //                    Telefon = x.Telefon,
        //                    Mail = x.Mail
        //                }).ToList(),
        //                YayinEvleri = _dbContext.YayinEvi.Select(x => new YayinEviListViewModel
        //                {
        //                    Id = x.Id,
        //                    Ad = x.Ad,
        //                    KurulusYili = x.KurulusYili,
        //                    Adres = x.Ad,
        //                    Telefon = x.Telefon,
        //                    Mail = x.Mail
        //                }).ToList()
        //            };
        //        }
        //        return View(kitapVM);
        //    }
        //    if (!_kitapRepository.Insert(kitapVM.Kitap)) return View(kitapVM);
        //    return RedirectToAction(nameof(Index));
        //    //if (!_kitapRepository.Insert(kitap)) return View(kitap);
        //    //return RedirectToAction(nameof(Index));
        //}

        [HttpPost]
        public IActionResult Ekle([Bind(Prefix = "Item1")] Kitap kitap, [Bind(Prefix = "Item2")] List<Tur> turler,
            [Bind(Prefix = "Item3")] List<Yazar> yazarlar, [Bind(Prefix = "Item4")] YayinEvi yayinEvi)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.TurId = new MultiSelectList(_dbContext.Tur.ToList(), "Id", "Ad", turler);
                ViewBag.YazarId = new MultiSelectList(_dbContext.Yazar.ToList(), "Id", "AdSoyad", yazarlar);
                ViewBag.YayinEviId = new SelectList(_dbContext.YayinEvi.ToList(), "Id", "Ad", yayinEvi);
                return View(kitap);
            }
            if (!_kitapRepository.Insert(kitap)) return View(kitap);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Guncelle(string id)
        {
            var kitap = _kitapRepository.GetById(id);
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

        public IActionResult Sil(string id)
        {
            if (!_kitapRepository.Delete(id)) return NotFound();
            return RedirectToAction(nameof(Index));
        }
    }
}
