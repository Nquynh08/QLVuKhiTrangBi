using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using QLVuKhiTrangBi.Data;
using QLVuKhiTrangBi.Models;

namespace QLVuKhiTrangBi.Controllers
{
    [Authorize(Policy = "RequireTroLy")]
    public class QLLoaiTrangBiController : Controller
    {
        QlvuKhiTrangBiContext db = new QlvuKhiTrangBiContext(); 
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(string maloaitb, string tenloaitb)
        {
            if (string.IsNullOrEmpty(tenloaitb) || string.IsNullOrEmpty(maloaitb))
            {
                ViewBag.Message = "Vui lòng nhập đầy đủ thông tin loại trang bị trước khi xác nhận";
                return View();
            }
            var loaiTB = new LoaiTrangBi()
            {
                MaLoaiTb = maloaitb,
                TenLoaiTb = tenloaitb,
            };
            db.LoaiTrangBis.Add(loaiTB);
            db.SaveChanges();
            return View();
        }
        public PartialViewResult DSLoaiTB()
        {
            var dsloaiTB = db.LoaiTrangBis.ToList();
            return PartialView("_dsloaiTB", dsloaiTB);
        }
        public IActionResult Delete(string id)
        {
            var lTB = db.LoaiTrangBis.Find(id);
            db.LoaiTrangBis.Remove(lTB);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(string id)
        {
            var lTB = db.LoaiTrangBis.Find(id);
            return View(lTB);
        }
        [HttpPost]
        public IActionResult Edit(LoaiTrangBi model)
        {
            var lTB = db.LoaiTrangBis.Find(model.MaLoaiTb);
            try
            {
                lTB.TenLoaiTb = model.TenLoaiTb;
                db.LoaiTrangBis.Update(lTB);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(lTB);
            }

        }
    }
}
