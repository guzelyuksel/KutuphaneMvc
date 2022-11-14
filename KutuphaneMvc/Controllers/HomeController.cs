using KutuphaneMvc.DataAccess;
using KutuphaneMvc.Models;
using KutuphaneMvc.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KutuphaneMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var toplamKitap = _dbContext.Kitap.Count();
            var toplamYazar = _dbContext.Yazar.Count();
            var toplamYayinEvi = _dbContext.YayinEvi.Count();
            var toplamTur = _dbContext.Tur.Count();

            var toplamBilgiVM = new ToplamBilgiVM(toplamKitap, toplamYazar, toplamYayinEvi, toplamTur);

            return View(toplamBilgiVM);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}