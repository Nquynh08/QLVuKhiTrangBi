using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLVuKhiTrangBi.Data;
using QLVuKhiTrangBi.Models;
using System.Collections.Immutable;

namespace QLVuKhiTrangBi.Controllers
{
    [Authorize(Policy = "RequireTroLy")]
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
        public IActionResult Muon(int id)
        {
            var ngayGac = db.LichGacs.Find(id);
            ViewBag.ngay = ngayGac.Ngay;
            var daidoi = ngayGac.DaiDoiGac;
            ViewBag.PhanDoiGac = daidoi;

            var dsCBc = db.CanBoDaiDois.Where(c => c.MaDaiDoi == daidoi).ToList();
            ViewBag.dsCBc = dsCBc;
            return View();
        }

        [HttpPost]
        public IActionResult Muon(string nguoigiao, string nguoinhan, string thoiGianMuon,
            List<string> soSung, List<string> tinhtrangmuon, List<string> MaTrangBi, List<int> sl)
        {
            var gac = db.LichGacs.FirstOrDefault(l => l.Ngay.Date == DateTime.Today);
            gac.TrangThai = "Đang gác";
            db.LichGacs.Update(gac);
            db.SaveChanges();

            var obj = new BbmuonTraVktb()
            {
                TenBienBan = "Bàn giao vũ khí, trang bị, vật chất canh gác",
                NgayGac = DateTime.Today,
                DaiDoiGac = gac.DaiDoiGac,
                CbkhoGiao = nguoigiao,
                CbdaidoiNhan = nguoinhan,
                ThoiGianMuon = thoiGianMuon,
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
            }
            for (int i = 0; i < 3; i++)
            {
                var objTB = new BbmuonTraTb()
                {
                    MaBienBan = obj.MaBienBan,
                    MaTrangBi = MaTrangBi[i],
                    SoLuong = sl[i],
                    MuonTra = 1,
                    TinhTrang = tinhtrangmuon[i],
                };
                db.BbmuonTraTbs.Add(objTB);
            }
            db.SaveChanges();
            return RedirectToAction("LichGac");
        }

        public IActionResult Tra(int id)
        {
            var ngayGac = db.LichGacs.Find(id);
            ViewBag.idGac = id;
            DateTime ngay = ngayGac.Ngay;
            ViewBag.ngay = ngay;

            var daidoi = ngayGac.DaiDoiGac;
            ViewBag.PhanDoiGac = daidoi;

            var dsCBc = db.CanBoDaiDois.Where(c => c.MaDaiDoi == daidoi).ToList();
            ViewBag.dsCBc = dsCBc;

            var ten = "Bàn giao vũ khí, trang bị, vật chất canh gác";
            var idBB = db.BbmuonTraVktbs
            .Where(b => b.TenBienBan == ten && b.NgayGac == ngay)
            .Select(b => b.MaBienBan)
            .SingleOrDefault();
            ViewBag.idBB = idBB;
            return View();
        }

        [HttpPost]
        public IActionResult Tra(int IdGac, int IdBB, string nguoigiaoC, string nguoinhanK, string thoiGianTra,
            List<string> soSung, List<string> tinhtrangtra, List<string> MaTrangBi, List<int> sl, List<int> slThieu)
        {
            var obj = db.BbmuonTraVktbs.Find(IdBB);
            obj.CbdaidoiGiao = nguoigiaoC;
            obj.CbkhoNhan = nguoinhanK;
            obj.ThoiGianTra = thoiGianTra;
            db.BbmuonTraVktbs.Update(obj);
            db.SaveChanges();

            // nếu mượn thì mượn trả bằng 1, trả thì bằng 0 
            for (int i = 0; i < soSung.Count; i++)
            {
                var objSung = new BbmuonTraSung()
                {
                    MaBienBan = obj.MaBienBan,
                    SoHieuSung = soSung[i],
                    MuonTra = 0,
                    TinhTrang = tinhtrangtra[i],
                };
                db.BbmuonTraSungs.Add(objSung);
            }
            for (int i = 0; i < 3; i++)
            {
                var j = i + 2;
                var objTB = new BbmuonTraTb()
                {
                    MaBienBan = obj.MaBienBan,
                    MaTrangBi = MaTrangBi[i],
                    SoLuong = sl[i],
                    MuonTra = 0,
                    TinhTrang = tinhtrangtra[j],
                    Slthieu = slThieu[i],
                };
                db.BbmuonTraTbs.Add(objTB);
            }
            db.SaveChanges();

            var gac = db.LichGacs.SingleOrDefault(g => g.Stt == IdGac);
            gac.TrangThai = "Đã hoàn thành";
            db.LichGacs.Update(gac);
            db.SaveChanges();
            return RedirectToAction("LichGac");
        }

        public IActionResult ChiTiet(int id)
        {
            var ngayGac = db.LichGacs.Find(id);
            DateTime ngay = ngayGac.Ngay;
            ViewBag.ngay = ngay;

            DateTime ngay1 = ngay.AddDays(1);
            ViewBag.ngay1 = ngay1;

            var daidoi = ngayGac.DaiDoiGac;
            ViewBag.PhanDoiGac = daidoi;

            var ten = "Bàn giao vũ khí, trang bị, vật chất canh gác";
            var bienbangac = db.BbmuonTraVktbs
            .Where(b => b.TenBienBan == ten && b.NgayGac == ngay)
            .SingleOrDefault();

            ViewBag.bienbangac = bienbangac;
            ViewBag.bbSung = db.BbmuonTraSungs.Where(b => b.MaBienBan == bienbangac.MaBienBan).ToList();
            ViewBag.bbTB = db.BbmuonTraTbs.Where(b => b.MaBienBan == bienbangac.MaBienBan).ToList();

            return View();
        }
    }
}
