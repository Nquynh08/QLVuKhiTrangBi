using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLVuKhiTrangBi.Data;
using QLVuKhiTrangBi.Models;
using System.Linq;

namespace QLVuKhiTrangBi.Controllers
{

    public class DonDeNghiController : Controller
    {
        QlvuKhiTrangBiContext db = new QlvuKhiTrangBiContext();
        [Authorize(Policy = "RequireDaiDoi")]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Policy = "RequireTieuDoan")]
        public IActionResult TieuDoan()
        {
            return View();
        }
        [Authorize(Policy = "RequireTroLy")]
        public IActionResult TroLy()
        {
            return View();
        }
        [Authorize(Policy = "RequireDaiDoi")]
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
            var madd = (
            from tk in db.TaiKhoans
            join c in db.CanBoDaiDois on tk.MaQn equals c.MaQn
            where tk.TenDn == tenDn
            select c.MaDaiDoi
            ).FirstOrDefault();

            var dsyc = db.Yeucaumuonvktbs.Where(yc => yc.MaDaiDoi == madd).Include(yc => yc.MaCanBoDaiDoiNavigation).ToList();
            return PartialView("_dsyc", dsyc);
        }
        [Authorize(Policy = "RequireDaiDoi")]
        public IActionResult Create()
        {
            var dsLoaiSung = db.LoaiSungs.ToList();
            ViewBag.dsLoaiSung = dsLoaiSung;
            var dsTB = db.TrangBis.ToList();
            ViewBag.dsTB = dsTB;

            var tenDn = HttpContext.Session.GetString("UserName");
            var canbo = (
               from tk in db.TaiKhoans
               join c in db.CanBoDaiDois on tk.MaQn equals c.MaQn
               where tk.TenDn == tenDn
               select c.HoTen
               ).FirstOrDefault();
            ViewBag.canbo = canbo;

            var daidoi = (
            from tk in db.TaiKhoans
            join c in db.CanBoDaiDois on tk.MaQn equals c.MaQn
            where tk.TenDn == tenDn
            select c.MaDaiDoiNavigation.MaDaiDoi
            ).FirstOrDefault();
            ViewBag.daidoi = daidoi;

            return View();
        }
        [HttpPost]
        [Authorize(Policy = "RequireDaiDoi")]
        public IActionResult Create(Yeucaumuonvktb model, List<string> MaLoaiSung, List<int> slLoaiSung, List<string> MaTrangBi, List<int> slTrangBi)
        {

            var tenDn = HttpContext.Session.GetString("UserName");
            var yeuCauMuon = new Yeucaumuonvktb
            {
                MaDaiDoi = model.MaDaiDoi,
                MaCanBoDaiDoi = tenDn,
                ThoiGianDuKienMuon = model.ThoiGianDuKienMuon,
                ThoiGianDuKienTra = model.ThoiGianDuKienTra,
                TrangThai = "Chờ xác nhận",
                GhiChu = model.GhiChu,
                TenYc = model.TenYc,
            };

            db.Yeucaumuonvktbs.Add(yeuCauMuon);
            db.SaveChanges();

            for (int i = 0; i < MaLoaiSung.Count; i++)
            {
                var detailSung = new YeucauLoaisung
                {
                    MaYc = yeuCauMuon.MaYc,
                    MaLoaiSung = MaLoaiSung[i],
                    SoLuong = slLoaiSung[i]
                };
                db.YeucauLoaisungs.Add(detailSung);
            }

            for (int i = 0; i < MaTrangBi.Count; i++)
            {
                var detailTB = new YeucauLoaitb
                {
                    MaYc = yeuCauMuon.MaYc,
                    MaTrangBi = MaTrangBi[i],
                    SoLuong = slTrangBi[i]
                };
                db.YeucauLoaitbs.Add(detailTB);
            }

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize(Policy = "RequireTieuDoan")]
        public IActionResult ChiTiet(int id)
        {
            var yc = db.Yeucaumuonvktbs.SingleOrDefault(yc => yc.MaYc == id);
            ViewBag.yc = yc;
            var ycSung = db.YeucauLoaisungs.Where(yc => yc.MaYc == id).ToList();
            ViewBag.ycSung = ycSung;
            var ycTB = db.YeucauLoaitbs.Where(yc => yc.MaYc == id).ToList();
            ViewBag.ycTB = ycTB;

            return View();
        }
        [Authorize(Policy = "RequireTieuDoan")]
        public PartialViewResult DSDNTieuDoan()
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
            return PartialView("_dsyctd", dsyc);
        }

        [HttpPost]
        [Authorize(Policy = "RequireTieuDoan")]
        public ActionResult AcceptRequest(int requestId)
        {
            var yc = db.Yeucaumuonvktbs.Find(requestId);

            yc.TrangThai = "Đã được duyệt";
            db.Yeucaumuonvktbs.Update(yc);
            db.SaveChanges();

            return Json(new { success = true });
        }

        [HttpPost]
        [Authorize(Policy = "RequireTieuDoan")]
        public ActionResult RejectRequest(int requestId)
        {
            var yc = db.Yeucaumuonvktbs.Find(requestId);

            yc.TrangThai = "Đề nghị bị từ chối";
            db.Yeucaumuonvktbs.Update(yc);
            db.SaveChanges();

            return Json(new { success = true });
        }

        [Authorize(Policy = "RequireTroLy")]
        public PartialViewResult DSDNTroLy()
        {
            var dsyc = db.Yeucaumuonvktbs.Where(y => y.TrangThai == "Đã được duyệt").Include(yc => yc.MaCanBoDaiDoiNavigation).ToList();
            return PartialView("_dsyctl", dsyc);
        }
        [Authorize(Policy = "RequireTroLy")]
        public IActionResult ChoMuon(int id)
        {
            var yc = db.Yeucaumuonvktbs.SingleOrDefault(yc => yc.MaYc == id);
            ViewBag.yc = yc;
            var ycSung = db.YeucauLoaisungs.Where(yc => yc.MaYc == id).ToList();
            ViewBag.ycSung = ycSung;
            var ycTB = db.YeucauLoaitbs.Where(yc => yc.MaYc == id).ToList();
            ViewBag.ycTB = ycTB;
            var dsCBc = db.CanBoDaiDois.Where(c => c.MaDaiDoi == yc.MaDaiDoi).ToList();
            ViewBag.dsCBc = dsCBc;

            var dsLoaiSung = db.LoaiSungs.ToList();
            ViewBag.dsLoaiSung = dsLoaiSung;

            //var dsSung = db.Sungs.Where(s => s.SuDung == true).ToList();
            //ViewBag.dsSung = dsSung;

            var dsTB = db.TrangBis.ToList();
            ViewBag.dsTB = dsTB;

            return View();
        }
    }
}
