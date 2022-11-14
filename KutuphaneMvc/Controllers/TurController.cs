using KutuphaneMvc.Classes;
using KutuphaneMvc.DataAccess;
using KutuphaneMvc.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace KutuphaneMvc.Controllers
{
    public class TurController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly TurRepository _turRepository;

        public TurController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _turRepository = new TurRepository(dbContext);
        }

        public IActionResult Index()
        {
            return View(_dbContext.Tur.ToList());
        }

        public IActionResult Duzenle(Guid id)
        {
            var turBul = _dbContext.Tur.Find(id);
            if (turBul == null) return NotFound();
            return View(turBul);
        }

        [HttpPost]
        public IActionResult Duzenle(Tur tur)
        {
            if (ModelState.IsValid)
            {
                if (_turRepository.Update(tur))
                    return RedirectToAction("Index");
                return View(tur);
            }
            return View(tur);
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
            if (ModelState.IsValid)
            {
                if (_turRepository.Insert(tur))
                    return RedirectToAction("Index");
                return View(tur);
            }
            return View(tur);
        }

        public IActionResult Detaylar(Guid id)
        {
            var turBul = _dbContext.Tur.Find(id);
            if (turBul == null) return NotFound();
            return View(turBul);
        }
    }
}
