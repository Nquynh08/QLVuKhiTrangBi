using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLVuKhiTrangBi.Data;
using QLVuKhiTrangBi.Models;
using System;
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

            //var dsLoaiSung = db.LoaiSungs.ToList();
            //ViewBag.dsLoaiSung = dsLoaiSung;

            var dsSung = db.Sungs.Where(s => s.SuDung == true).ToList();
            ViewBag.dsSung = dsSung;

            var dsTB = db.TrangBis.ToList();
            ViewBag.dsTB = dsTB;

            return View();
        }
        [HttpPost]
        [Authorize(Policy = "RequireTroLy")]
        public IActionResult ChoMuon(int mayc, string nguoigiao, string cbDaiDoi, string MaDaiDoi,
            List<string> soSung, List<string> tinhtrangmuon, List<string> tinhtrangmuonTB, List<string> MaTrangBi, List<int> soluong)
        {
            // biên bản mượn trả theo yêu cầu từ đại đội
            var obj = new BbmuonTraVktb()
            {
                TenBienBan = "Bàn giao vũ khí, trang bị, thực hiện nhiệm vụ (Yêu cầu " + mayc + ")",
                DaiDoi = MaDaiDoi,
                CbkhoGiao = nguoigiao,
                CbdaidoiNhan = cbDaiDoi,
                ThoiGianMuon = DateTime.Now.ToString(),
            };
            db.BbmuonTraVktbs.Add(obj);
            db.SaveChanges();

            // nếu mượn thì mượn trả bằng 1, trả thì bằng 0 
            for (int i = 0; i < soSung.Count; i++)
            {
                var objSung = new BbmuonTraSung()
                {
                    MaBienBan = obj.MaBienBan,
                    SoHieuSung = soSung[i],
                    MuonTra = 1,
                    TinhTrang = tinhtrangmuon[i],
                };
                db.BbmuonTraSungs.Add(objSung);

                // ghi nhận súng đang được dùng
                var s = db.Sungs.SingleOrDefault(s => s.SoHieuSung == soSung[i]);
                s.SuDung = false;
                s.GhiChu = "Đang sử dụng";
                db.Sungs.Update(s);
            }
            for (int i = 0; i < MaTrangBi.Count; i++)
            {
                var objTB = new BbmuonTraTb()
                {
                    MaBienBan = obj.MaBienBan,
                    MaTrangBi = MaTrangBi[i],
                    SoLuong = soluong[i],
                    MuonTra = 1,
                    TinhTrang = tinhtrangmuonTB[i],
                };
                db.BbmuonTraTbs.Add(objTB);

                // ghi nhận số lượng trang bị đang được dùng hoặc không dùng được nữa
                var tb = db.TrangBis.SingleOrDefault(m => m.MaTrangBi == MaTrangBi[i]);
                tb.KhongDungDuoc += soluong[i];
                tb.GhiChu += soluong[i] + "đang sử dụng";
                db.TrangBis.Update(tb);
            }
            db.SaveChanges();

            var yc = db.Yeucaumuonvktbs.Find(mayc);
            yc.GhiChu = "Đang mượn";
            db.Yeucaumuonvktbs.Update(yc);
            db.SaveChanges();

            return View();
        }
        [Authorize(Policy = "RequireTroLy")]
        public IActionResult TraVKTB(int id)
        {
            var yc = db.Yeucaumuonvktbs.SingleOrDefault(yc => yc.MaYc == id);
            ViewBag.yc = yc;

            var ten = "Bàn giao vũ khí, trang bị, thực hiện nhiệm vụ (Yêu cầu " + id + ")" ;
            var bbMuonTra = db.BbmuonTraVktbs.Where(m => m.TenBienBan == ten).SingleOrDefault();
            ViewBag.bbMuonTra = bbMuonTra;
            ViewBag.bbMuonTraSung = db.BbmuonTraSungs.Where(b => b.MaBienBan == bbMuonTra.MaBienBan).ToList();
            ViewBag.bbMuonTraTB = db.BbmuonTraTbs.Where(b => b.MaBienBan == bbMuonTra.MaBienBan).ToList();

            var dsCBc = db.CanBoDaiDois.Where(c => c.MaDaiDoi == bbMuonTra.DaiDoi).ToList();
            ViewBag.dsCBc = dsCBc;

            return View();
        }
        [HttpPost]
        [Authorize(Policy = "RequireTroLy")]
        public IActionResult TraVKTB(int mayc, int mabb, string nguoinhan, string cbDaiDoi, List<string> soSung, List<string> tinhtrangtra,
            List<string> tinhtrangtraTB, List<string> MaTrangBi, List<int> soluong, List<int> slThieu)
        {
            var bb = db.BbmuonTraVktbs.FirstOrDefault(b => b.MaBienBan == mabb);
            bb.CbdaidoiGiao = cbDaiDoi;
            bb.CbkhoNhan = nguoinhan;
            bb.ThoiGianTra = DateTime.Now.ToString();
            db.BbmuonTraVktbs.Update(bb);
            db.SaveChanges();

            // nếu mượn thì mượn trả bằng 1, trả thì bằng 0 
            for (int i = 0; i < soSung.Count; i++)
            {
                var objSung = new BbmuonTraSung()
                {
                    MaBienBan = bb.MaBienBan,
                    SoHieuSung = soSung[i],
                    MuonTra = 0,
                    TinhTrang = tinhtrangtra[i],
                };
                db.BbmuonTraSungs.Add(objSung);

                // trả lại trạng thái súng không được dùng
                var s = db.Sungs.Find(soSung[i]);
                s.SuDung = true;
                db.Sungs.Update(s);
            }
            for (int i = 0; i < MaTrangBi.Count; i++)
            {
                var objTB = new BbmuonTraTb()
                {
                    MaBienBan = bb.MaBienBan,
                    MaTrangBi = MaTrangBi[i],
                    SoLuong = soluong[i],
                    MuonTra = 0,
                    TinhTrang = tinhtrangtraTB[i],
                    Slthieu = slThieu[i],
                };
                db.BbmuonTraTbs.Add(objTB);

                // trả lại trang bị cho hệ thống
                var tb = db.TrangBis.Find(MaTrangBi[i]);
                tb.KhongDungDuoc -= soluong[i];
                db.TrangBis.Update(tb);
            }
            db.SaveChanges();

            var yc = db.Yeucaumuonvktbs.Find(mayc);
            yc.GhiChu = "Đã hoàn thành";
            db.Yeucaumuonvktbs.Update(yc);
            db.SaveChanges();

            return RedirectToAction("BbMuonTraVKTB", "BanGiaoQuanKhi");
        }
    }
}
