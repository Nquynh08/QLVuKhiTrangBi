using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLVuKhiTrangBi.Data;
using QLVuKhiTrangBi.Models;

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
        public PartialViewResult DSSung()
        {
            var dssung = db.Sungs.ToList();
            return PartialView("_dssung", dssung);
        }
        [HttpPost]
        public IActionResult Index(string sosung, string dvt, string tinhtrang, string MaLoaiSung, string SoQd)
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
                        TinhTrang = tinhtrang,
                        MaLoaiSung = MaLoaiSung,
                        SoQd = SoQd,
                    };
                    db.Sungs.Add(sung);
                    db.SaveChanges();

                    var bb = string.Format("BB-{0}", SoQd);
                    var bbSung = new BanGiaoQkSung()
                    {
                        MaBienBan = bb,
                        SoHieuSung = sosung,
                        HanhDong = "Nhập kho",
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
            db.Sungs.Remove(s);
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
                s.SoHieuSung = model.SoHieuSung;
                s.DonViTinh = model.DonViTinh;
                s.TinhTrang = model.TinhTrang;
                s.MaLoaiSung = model.MaLoaiSung;
                s.SoQd = model.SoQd;
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
