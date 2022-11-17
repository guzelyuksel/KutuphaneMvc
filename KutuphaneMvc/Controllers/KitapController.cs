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
            var kitapEkleVm = new KitapEkleViewModel()
            {
                TurlerDb = _dbContext.Tur.ToList(),
                YazarlarDb = _dbContext.Yazar.ToList(),
                YayinEvleriDb = _dbContext.YayinEvi.ToList()
            };
            return View(kitapEkleVm);
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
        public IActionResult Ekle(KitapEkleViewModel kitapEkleVm)
        {
            if (!ModelState.IsValid)
            {
                if (kitapEkleVm.TurlerDb == null)
                {
                    kitapEkleVm.TurlerDb = _dbContext.Tur.ToList();
                    kitapEkleVm.YazarlarDb = _dbContext.Yazar.ToList();
                    kitapEkleVm.YayinEvleriDb = _dbContext.YayinEvi.ToList();
                }
                return View(kitapEkleVm);
            }
            if (!_kitapRepository.Insert(kitapEkleVm.Kitap, kitapEkleVm.Turler, kitapEkleVm.Yazarlar, kitapEkleVm.YayinEvi)) return View(kitapEkleVm);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Duzenle(string id)
        {
            var kitap = _kitapRepository.GetById(id);
            if (kitap == null) return NotFound();
            var yazarlar = _dbContext.Yazar;
            var turler = _dbContext.Tur;
            var yayinEvleri = _dbContext.YayinEvi;
            KitapEkleViewModel kitapEkleVm = new KitapEkleViewModel()
            {
                Kitap = kitap,
                YayinEvi = kitap.YayinEviId,
                Turler = kitap.Turler.Select(x => x.Id).ToList(),
                Yazarlar = kitap.Yazarlar.Select(x => x.Id).ToList(),
                TurlerDb = turler.ToList(),
                YazarlarDb = yazarlar.ToList(),
                YayinEvleriDb = yayinEvleri.ToList()
            };
            return View(kitapEkleVm);
        }

        [HttpPost]
        public IActionResult Duzenle(KitapEkleViewModel kitapEkleVm)
        {
            if (!ModelState.IsValid) return View(kitapEkleVm);
            if (!_kitapRepository.Update(kitapEkleVm.Kitap, kitapEkleVm.Turler, kitapEkleVm.Yazarlar, kitapEkleVm.YayinEvi)) return View(kitapEkleVm);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Sil(string id)
        {
            if (!_kitapRepository.Delete(id)) return NotFound();
            return RedirectToAction(nameof(Index));
        }

    }
}
