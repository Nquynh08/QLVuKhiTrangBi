using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLVuKhiTrangBi.Data;

namespace QLVuKhiTrangBi.Controllers
{
    [Authorize(Policy = "RequireTroLy")]
    public class BanGiaoQuanKhiController : Controller
    {
        QlvuKhiTrangBiContext db = new QlvuKhiTrangBiContext();
        public IActionResult Index()
        {
            var dsbb = db.BbbanGiaoQks.Include(bb => bb.NguoiTaoNavigation).ToList();
            return View(dsbb);
        }

        public IActionResult ChiTiet(string id)
        {
            //var bb = db.BbbanGiaoQks.Find(id);
            var bb = db.BbbanGiaoQks.Include(bb => bb.NguoiTaoNavigation).FirstOrDefault(b => b.MaBienBan == id);
            ViewBag.BienBan = bb;
            var bbSung = db.BanGiaoQkSungs.Where(bbs => bbs.MaBienBan == id).ToList();
            ViewBag.dsSung = bbSung;
            var bbTB = db.BanGiaoQkTrangBis.Where(bbtb => bbtb.MaBienBan == id).Include(b => b.MaTrangBiNavigation).ToList();
            ViewBag.dsTB = bbTB;
            return View();
        }
    }
}
