using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLVuKhiTrangBi.Data;
using QLVuKhiTrangBi.Models;
using System.Reflection.Metadata.Ecma335;

namespace QLVuKhiTrangBi.Controllers
{
    [Authorize(Policy = "RequireTroLy")]
    public class QLSungController : Controller
    {
        QlvuKhiTrangBiContext db = new QlvuKhiTrangBiContext();
        public IActionResult Index()
        {
            var dsLoaiSung = db.LoaiSungs.ToList();
            ViewBag.dsLoaiSung = dsLoaiSung;
            var dsQD = db.QuyetDinhs.ToList();
            ViewBag.dsQD = dsQD;
            return View();
        }
        public PartialViewResult DSSung(string loai, int phanCap, string tkiem)
        {
            ViewBag.loaisung = db.LoaiSungs.ToList();

            //var dssung = (from s in db.Sungs
            //             where (string.IsNullOrEmpty(loai) || s.MaLoaiSung.Contains(loai))
            //              && (phanCap == null || s.PhanCap==phanCap)
            //             select s).ToList();
            var dssung = db.Sungs.ToList();
            return PartialView("_dssung", dssung);

        }
        [HttpPost]
        public IActionResult Index(string sosung, string dvt, int phancap, string MaLoaiSung, string SoQd)
        {
            try
            {
                var s = db.Sungs.Find(sosung);
                if(s == null)
                {
                    var sung = new Sung()
                    {
                        SoHieuSung = sosung,
                        DonViTinh = dvt,
                        PhanCap = phancap,
                        SuDung = true, //sử dụng = true là có thể sử dụng, false là đang sử dụng hoặc không sử dụng được
                        MaLoaiSung = MaLoaiSung,
                        SoQd = SoQd,
                    };
                    db.Sungs.Add(sung);
                    db.SaveChanges();

                    var loaiSung = db.LoaiSungs.SingleOrDefault(l => l.MaLoaiSung == MaLoaiSung);
                    loaiSung.SoLuong += 1;
                    db.LoaiSungs.Update(loaiSung);
                    db.SaveChanges();

                    var bb = string.Format("BB-{0}", SoQd);
                    var bbSung = new BanGiaoQkSung()
                    {
                        MaBienBan = bb,
                        SoHieuSung = sosung,
                        PhanCap = phancap,
                        HanhDong = "Nhập kho",
                        LoaiSung = sung.MaLoaiSung,
                    }; 
                    db.BanGiaoQkSungs.Add(bbSung);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    // đưa thông báo số hiệu súng này đã có rồi, kiểm tra lại thông tin
                    TempData["Message"] = "Số hiệu súng này đã tồn tại. Vui lòng kiểm tra thông tin";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }
        public IActionResult Delete(string id)
        {
            var s = db.Sungs.Find(id);
            ViewBag.Sung = s;
            var dsQD = db.QuyetDinhs.ToList();
            ViewBag.dsQD = dsQD;
            var bbQK = db.BanGiaoQkSungs
                 .Where(b => b.SoHieuSung == id)
                 .Select(b => b.MaBienBan)
                 .FirstOrDefault();
            ViewBag.bbQK = bbQK;    

            return View();
        }

        [HttpPost]
        // Xóa 1 là xóa súng do xuất kho, có quyết định đổi trả, xuất kho kèm theo
        public IActionResult Xoa1(string sosung, string SoQd, string ghichu)
        {
            var sung = db.Sungs.Find(sosung);

            var bb = string.Format("BB-{0}", SoQd);

            var bgSung = new BanGiaoQkSung()
            {
                MaBienBan = bb,
                SoHieuSung = sosung,
                PhanCap = sung.PhanCap,
                HanhDong = "Xuất kho, đổi trả súng",
                GhiChu = ghichu,
                LoaiSung = sung.MaLoaiSung,
            };
            db.BanGiaoQkSungs.Add(bgSung);
            db.SaveChanges();

            db.Sungs.Remove(sung);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        // Xóa 2 là xóa súng do hỏng, mất, không sử dụng được
        public IActionResult Xoa2(string sosung, string lydo)
        {
            var sung = db.Sungs.Find(sosung);
            sung.SuDung = false;
            sung.GhiChu = lydo;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        // Xóa 3 là xóa súng do nhập sai thông tin, xóa đi để nhập lại
        // xóa thông tin súng và thông tin biên bản bàn giao quân khí
        public IActionResult Xoa3(string sosung, string mabb)
        {
            var sung = db.Sungs.Find(sosung);

            var bbS = db.BanGiaoQkSungs.SingleOrDefault(b => b.MaBienBan == mabb && b.SoHieuSung == sosung);

            db.BanGiaoQkSungs.Remove(bbS);
            db.SaveChanges();

            db.Sungs.Remove(sung);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(string id)
        {
            var s = db.Sungs.Find(id);
            var dsLoaiSung = db.LoaiSungs.ToList();
            ViewBag.dsLoaiSung = dsLoaiSung;
            var dsQD = db.QuyetDinhs.ToList();
            ViewBag.dsQD = dsQD;
            return View(s);
        }
        [HttpPost]
        public IActionResult Edit(Sung model)
        {
            var s = db.Sungs.Find(model.SoHieuSung);
            try
            {
                s.DonViTinh = model.DonViTinh;
                s.PhanCap = model.PhanCap;
                db.Sungs.Update(s);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(s);
            }
        }
    }
}
