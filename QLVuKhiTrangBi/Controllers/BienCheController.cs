using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLVuKhiTrangBi.Data;
using QLVuKhiTrangBi.Models;

namespace QLVuKhiTrangBi.Controllers
{
    public class BienCheController : Controller
    {
        QlvuKhiTrangBiContext db = new QlvuKhiTrangBiContext();
        [Authorize(Policy = "RequireDaiDoi")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Policy = "RequireDaiDoi")]
        public IActionResult Index(DateTime thoigianbd,DateTime thoigiankt,string ghichu)
        {
            var tenDn = HttpContext.Session.GetString("UserName");
            string madaidoi = (from dd in db.DaiDois
                           join qn in db.CanBoDaiDois on dd.MaDaiDoi equals qn.MaDaiDoi
                           where qn.MaQn == tenDn
                           select dd.MaDaiDoi).FirstOrDefault();

            var lastlbc = (from luotbc in db.LuotBienChes
                           where luotbc.MaDaiDoi == madaidoi
                           orderby luotbc.MaLuotBienChe descending // Replace YourDateField with the actual date field you want to use for ordering
                           select luotbc).FirstOrDefault();

            string malbc;
            int c = int.Parse(madaidoi.Substring(3));
            if (lastlbc != null)
            {
                // Tạo mã mới dựa trên mã cuối cùng và tăng lên 1
                int lastId = int.Parse(lastlbc.MaLuotBienChe.Substring(3)); // Lấy phần số cuối cùng từ mã cũ
                malbc = "LBC" +c.ToString("D3") + (lastId + 1).ToString("D3");
            }
            else
            {
                // Nếu không có khách hàng nào, bắt đầu từ 00001
                malbc = "LBC" + c.ToString("D3") + "001";
            }
            var lbc = new LuotBienChe()
            {
                MaLuotBienChe = malbc,
                ThoiGianBd = thoigianbd,
                ThoiGianKt = thoigiankt,
                GhiChu = ghichu,
                MaDaiDoi =  madaidoi,
            };
            db.LuotBienChes.Add(lbc);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Policy = "RequireDaiDoi")]
        public IActionResult DSbienche()
        {
            var tenDn = HttpContext.Session.GetString("UserName");
            string madaidoi = (from dd in db.DaiDois
                               join qn in db.CanBoDaiDois on dd.MaDaiDoi equals qn.MaDaiDoi
                               where qn.MaQn == tenDn
                               select dd.MaDaiDoi).FirstOrDefault();
            var dslbc = from luotbc in db.LuotBienChes
                           where luotbc.MaDaiDoi == madaidoi
                           orderby luotbc.MaLuotBienChe descending // Replace YourDateField with the actual date field you want to use for ordering
                           select luotbc;
            return PartialView("_dslbc", dslbc);
        }
        [Authorize(Policy = "RequireDaiDoi")]
        public IActionResult AddBCSung(string malbc)
        {
            var tenDn = HttpContext.Session.GetString("UserName");
            var luotbc = (from lbc in db.LuotBienChes
                          where lbc.MaLuotBienChe == malbc
                          select lbc).FirstOrDefault();
            var dshvbc = from hv in db.HocViens
                         join bcs in db.BienCheSungs
                            on hv.MaHocVien equals bcs.MaHocVien
                         join cb in db.CanBoDaiDois
                             on hv.MaDaiDoi equals cb.MaDaiDoi
                         where cb.MaQn == tenDn
                         select hv;
            var dsallhv =from hv in db.HocViens
                        join cb in db.CanBoDaiDois
                            on hv.MaDaiDoi equals cb.MaDaiDoi
                        where cb.MaQn == tenDn
                        select hv;
            var dshv = dsallhv.Except(dshvbc).ToList();

            var dsallsung = db.Sungs.ToList();
            var dssungbc = from s in db.Sungs
                           join bcs in db.BienCheSungs
                              on s.SoHieuSung equals bcs.SoHieuSung
                            join hv in db.HocViens
                                on bcs.MaHocVien equals hv.MaHocVien
                           join cb in db.CanBoDaiDois
                               on hv.MaDaiDoi equals cb.MaDaiDoi
                           where cb.MaQn == tenDn
                           select s;
            var dssung = dsallsung.Except(dssungbc).ToList();
            if (luotbc != null)
            {
                ViewBag.malbc = luotbc.MaLuotBienChe;
                ViewBag.tgbd = luotbc.ThoiGianBd;
                ViewBag.tgkt = luotbc.ThoiGianKt;
                ViewBag.ghichu = luotbc.GhiChu;
            }
            ViewBag.hv = dshv;
            ViewBag.sung = dssung;
            return View();
        }

        [Authorize(Policy = "RequireDaiDoi")]
        public IActionResult DSBCSung()
        {
            var tenDn = HttpContext.Session.GetString("UserName");
            string madaidoi = (from dd in db.DaiDois
                               join qn in db.CanBoDaiDois on dd.MaDaiDoi equals qn.MaDaiDoi
                               where qn.MaQn == tenDn
                               select dd.MaDaiDoi).FirstOrDefault();
            var allbcsung = (from bcs in db.BienCheSungs
                            join lbc in db.LuotBienChes
                            on bcs.MaLuotBienChe equals lbc.MaLuotBienChe
                            where lbc.MaDaiDoi==madaidoi
                            select bcs).Include(sung=>sung.MaHocVienNavigation);
            return PartialView("_bcsung", allbcsung);
        }

        [HttpPost]
        [Authorize(Policy ="RequireDaiDoi")]
        public IActionResult AddBCSung(string MaLuotBienChe,string MaHocViensung,string SoHieuSung)
        {
            try
            {
                // Your existing code to add BienCheSung
                var tenDn = HttpContext.Session.GetString("UserName");
                string madaidoi = (from dd in db.DaiDois
                                   join qn in db.CanBoDaiDois on dd.MaDaiDoi equals qn.MaDaiDoi
                                   where qn.MaQn == tenDn
                                   select dd.MaDaiDoi).FirstOrDefault();
                var lastbcsung = ( from bcsung in db.BienCheSungs
                                    join luotbc in db.LuotBienChes
                                    on bcsung.MaLuotBienChe equals luotbc.MaLuotBienChe
                               where luotbc.MaDaiDoi == madaidoi
                               orderby bcsung.MaBienCheSung descending // Replace YourDateField with the actual date field you want to use for ordering
                               select bcsung).FirstOrDefault();

                string mabcsung;
                if (lastbcsung != null)
                {
                    // Tạo mã mới dựa trên mã cuối cùng và tăng lên 1
                    int lastId = int.Parse(lastbcsung.MaBienCheSung.Substring(3)); // Lấy phần số cuối cùng từ mã cũ
                    mabcsung = "BCS" + (lastId + 1).ToString("D3");
                }
                else
                {
                    // Nếu không có khách hàng nào, bắt đầu từ 00001
                    mabcsung = "BCS156" + "001";
                }

                var allbcsung = new BienCheSung()
                {
                    MaBienCheSung = mabcsung,
                    MaLuotBienChe= MaLuotBienChe,
                    MaHocVien=MaHocViensung,
                    SoHieuSung=SoHieuSung,
                };
                    db.BienCheSungs.Add(allbcsung);
                db.SaveChanges();
                return Json(new { success = true, message = "Biên chế súng thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Biên chế súng thất bại!" });
            }
            
        }
        [Authorize(Policy = "RequireDaiDoi")]
        public IActionResult AddBCTb(string malbc)
        {
            var tenDn = HttpContext.Session.GetString("UserName");
            var luotbc = (from lbc in db.LuotBienChes
                          where lbc.MaLuotBienChe == malbc
                          select lbc).FirstOrDefault();
            var dshv = (from hv in db.HocViens
                        join cb in db.CanBoDaiDois
                            on hv.MaDaiDoi equals cb.MaDaiDoi
                        where cb.MaQn == tenDn
                        select hv).ToList();
            var dstb = db.TrangBis.ToList();
            if (luotbc != null)
            {
                ViewBag.malbc = luotbc.MaLuotBienChe;
                ViewBag.tgbd = luotbc.ThoiGianBd;
                ViewBag.tgkt = luotbc.ThoiGianKt;
                ViewBag.ghichu = luotbc.GhiChu;
            }
            ViewBag.hv = dshv;
            ViewBag.tb = dstb;
            return View();
        }


        [Authorize(Policy = "RequireDaiDoi")]
        public IActionResult DSBCTb()
        {
            var tenDn = HttpContext.Session.GetString("UserName");
            string madaidoi = (from dd in db.DaiDois
                               join qn in db.CanBoDaiDois on dd.MaDaiDoi equals qn.MaDaiDoi
                               where qn.MaQn == tenDn
                               select dd.MaDaiDoi).FirstOrDefault();
            var allbctb = (from bcs in db.BienCheTbs
                             join lbc in db.LuotBienChes
                             on bcs.MaLuotBienChe equals lbc.MaLuotBienChe
                             where lbc.MaDaiDoi == madaidoi
                             select bcs).Include(tb => tb.MaHocVienNavigation);
            return PartialView("_bctb", allbctb);
        }
        [HttpPost]
        [Authorize(Policy = "RequireDaiDoi")]
        public IActionResult AddBCTb(string MaLuotBienChe,string MaHocVientb,string MaTrangBi,int SoLuong)
        {
            try
            {
                var tenDn = HttpContext.Session.GetString("UserName");
                string madaidoi = (from dd in db.DaiDois
                                   join qn in db.CanBoDaiDois on dd.MaDaiDoi equals qn.MaDaiDoi
                                   where qn.MaQn == tenDn
                                   select dd.MaDaiDoi).FirstOrDefault();
                var lastbctb = (from bctb in db.BienCheTbs
                                join luotbc in db.LuotBienChes
                                on bctb.MaLuotBienChe equals luotbc.MaLuotBienChe
                                where luotbc.MaDaiDoi == madaidoi
                                orderby bctb.MaBienCheTb descending // Replace YourDateField with the actual date field you want to use for ordering
                                select bctb).FirstOrDefault();

                string mabctb;
                if (lastbctb != null)
                {
                    // Tạo mã mới dựa trên mã cuối cùng và tăng lên 1
                    int lastId = int.Parse(lastbctb.MaBienCheTb.Substring(3)); // Lấy phần số cuối cùng từ mã cũ
                    mabctb = "BCTB"  + (lastId + 1).ToString("D3");
                }
                else
                {
                    // Nếu không có khách hàng nào, bắt đầu từ 00001
                    mabctb = "BCTB156"  + "001";
                }
                var allbctb = new BienCheTb()
                {
                    MaBienCheTb = mabctb,
                    MaLuotBienChe = MaLuotBienChe,
                    MaHocVien = MaHocVientb,
                    MaTrangBi = MaTrangBi,
                    SoLuong = SoLuong,
                };
                db.BienCheTbs.Add(allbctb);
                db.SaveChanges();

                return Json(new { success = true, message = "Biên chế trang bị thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Biên chế trang bi thất bại!" });
            }
            
        }

    }
}