using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLVuKhiTrangBi.Data;
using QLVuKhiTrangBi.Models;
using System.Security.Cryptography;

namespace QLVuKhiTrangBi.Controllers
{
    [Authorize(Policy = "RequireTroLy")]
    public class QLTrangBiController : Controller
    {
        QlvuKhiTrangBiContext db = new QlvuKhiTrangBiContext();
        public IActionResult Index()
        {
            var dsLoaiTB = db.LoaiTrangBis.ToList();
            ViewBag.dsLoaiTB = dsLoaiTB;
            var dsQD = db.QuyetDinhs.ToList();
            ViewBag.dsQD = dsQD;
            return View();
        }
        public PartialViewResult DSTB()
        {
            var dsTB = db.TrangBis.ToList();
            return PartialView("_dsTB", dsTB);
        }
        [HttpPost]
        public IActionResult Index(string maTB, string tenTB, string dvt, int soluong, int phancap, string MaLoaiTb, string SoQd)
        {
            try
            {
                // kiểm tra xem trang bị này đã có trong ht hay chưa, nếu chưa có thì thêm mới
                var tb = db.TrangBis.Find(maTB);
                var bb = string.Format("BB-{0}", SoQd);
                if (tb == null)
                {
                    var tbmoi = new TrangBi()
                    {
                        MaTrangBi = maTB,
                        TenTrangBi = tenTB,
                        DonViTinh = dvt,
                        SoLuong = soluong,
                        PhanCap = phancap,
                        KhongDungDuoc = 0,
                        MaLoaiTb = MaLoaiTb,
                        SoQd = SoQd,
                    };
                    db.TrangBis.Add(tbmoi);
                    db.SaveChanges();

                    var bbTB = new BanGiaoQkTrangBi()
                    {
                        MaBienBan = bb,
                        MaTrangBi = maTB,
                        PhanCap = phancap,
                        HanhDong = "Nhập kho",
                        SoLuong = soluong,
                    };
                    db.BanGiaoQkTrangBis.Add(bbTB);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                // nếu có rồi thì bổ sung số lượng
                else
                {
                    tb.SoLuong += soluong;
                    db.TrangBis.Update(tb);
                    db.SaveChanges();

                    var bbTB = db.BanGiaoQkTrangBis.FirstOrDefault(b => b.MaBienBan == bb && b.MaTrangBi == maTB && b.PhanCap == phancap);

                    //nếu có phân cấp chất lượng khác nhau thì phải thêm cái khác
                    if (bbTB == null)
                    {
                        bbTB = new BanGiaoQkTrangBi()
                        {
                            MaBienBan = bb,
                            MaTrangBi = maTB,
                            PhanCap = phancap,
                            HanhDong = "Nhập kho",
                            SoLuong = soluong,
                        };
                        db.BanGiaoQkTrangBis.Add(bbTB);
                    }
                    else
                    {
                        bbTB.SoLuong += soluong;
                        db.BanGiaoQkTrangBis.Update(bbTB);
                    }
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }
        //public IActionResult Delete(string id)
        //{
        //    var tb = db.TrangBis.Find(id);
        //    db.TrangBis.Remove(tb);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        public IActionResult Edit(string id)
        {
            var tb = db.TrangBis.Find(id);
            var dsLoaiTB = db.LoaiTrangBis.ToList();
            ViewBag.dsLoaiTB = dsLoaiTB;
            var dsQD = db.QuyetDinhs.ToList();
            ViewBag.dsQD = dsQD;
            return View(tb);
        }
        [HttpPost]
        // sửa trang bị là chỉ sửa số lượng trang bị không dùng được
        public IActionResult Edit(TrangBi model)
        {
            var tb = db.TrangBis.Find(model.MaTrangBi);
            try
            {
                tb.KhongDungDuoc = model.KhongDungDuoc;
                db.TrangBis.Update(tb);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(tb);
            }
        }
        [HttpGet]
        public IActionResult KiemTraMaTB(string maTB)
        {
            var trangBi = db.TrangBis.Find(maTB);

            if (trangBi != null)
            {
                return Json(new
                {
                    exists = true,
                    tenTrangBi = trangBi.TenTrangBi,
                    donViTinh = trangBi.DonViTinh,
                    maLoaiTb = trangBi.MaLoaiTb,
                });
            }
            return Json(new { exists = false });
        }

    }
}
