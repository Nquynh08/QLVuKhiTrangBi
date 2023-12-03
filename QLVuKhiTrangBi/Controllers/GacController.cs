using Microsoft.AspNetCore.Mvc;
using QLVuKhiTrangBi.Data;

namespace QLVuKhiTrangBi.Controllers
{
    public class GacController : Controller
    {
        QlvuKhiTrangBiContext db = new QlvuKhiTrangBiContext();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult LichGac()
        {
            var gac = db.LichGacs.ToList();
            return View(gac);
        }
        public IActionResult Muon()
        {
            return View();
        }
    }
}
