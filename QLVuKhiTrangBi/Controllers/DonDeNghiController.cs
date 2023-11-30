using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLVuKhiTrangBi.Data;

namespace QLVuKhiTrangBi.Controllers
{
    [Authorize]
    public class DonDeNghiController : Controller
    {
        QlvuKhiTrangBiContext db = new QlvuKhiTrangBiContext();
        public IActionResult Index()
        {
            return View();
        }

        public PartialViewResult DSDNDaiDoi()
        {
            var tenDn = HttpContext.Session.GetString("UserName");
            var daidoi = (
            from tk in db.TaiKhoans
            join c in db.CanBoDaiDois on tk.MaQn equals c.MaQn
            where tk.TenDn == tenDn
            select c.MaDaiDoiNavigation.TenDaiDoi
            ).FirstOrDefault();
            ViewBag.daidoi = daidoi;

            var dsyc = db.Yeucaumuonvktbs.Include(yc => yc.MaCanBoDaiDoiNavigation).ToList();
            return PartialView("_dsyc", dsyc);
        }
        public IActionResult Create()
        {
            var dsLoaiSung = db.LoaiSungs.ToList();
            ViewBag.dsLoaiSung = dsLoaiSung;
            var dsTB = db.TrangBis.ToList();
            ViewBag.dsTB = dsTB;    
            return View();
        }
    }
}
