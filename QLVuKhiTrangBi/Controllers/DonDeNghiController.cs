using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLVuKhiTrangBi.Data;
using QLVuKhiTrangBi.Models;

namespace QLVuKhiTrangBi.Controllers
{
    [Authorize(Policy = "RequireDaiDoi")]
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
                GhiChu = model.GhiChu
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
    }
}
