using Microsoft.AspNetCore.Mvc;
using QLVuKhiTrangBi.Data;
using QLVuKhiTrangBi.Models;

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
    }
}
