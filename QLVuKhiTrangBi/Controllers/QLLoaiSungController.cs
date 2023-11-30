using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLVuKhiTrangBi.Data;
using QLVuKhiTrangBi.Models;

namespace QLVuKhiTrangBi.Controllers
{
    [Authorize]
    public class QLLoaiSungController : Controller
    {
        QlvuKhiTrangBiContext db = new QlvuKhiTrangBiContext();
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(string maloaisung, string tenloaisung)
        {
            if (string.IsNullOrEmpty(tenloaisung) || string.IsNullOrEmpty(maloaisung))
            {
                ViewBag.Message = "Vui lòng nhập đầy đủ thông tin loại súng trước khi xác nhận";
                return View();
            }
            var loaiSung = new LoaiSung()
            {
                MaLoaiSung = maloaisung,
                TenLoaiSung = tenloaisung,
            };
            db.LoaiSungs.Add(loaiSung);
            db.SaveChanges();
            return View();
        }
        public PartialViewResult DSLoaiSung()
        {
            var dsloaiSung = db.LoaiSungs.ToList();
            return PartialView("_dsloaiSung", dsloaiSung);
        }
        public IActionResult Delete(string id)
        {
            var ls = db.LoaiSungs.Find(id);
            db.LoaiSungs.Remove(ls);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(string id)
        {
            var ls = db.LoaiSungs.Find(id);
            return View(ls);
        }
        [HttpPost]
        public IActionResult Edit(LoaiSung model)
        {
            var ls = db.LoaiSungs.Find(model.MaLoaiSung);
            try
            {
                ls.TenLoaiSung = model.TenLoaiSung;
                db.LoaiSungs.Update(ls);
                db.SaveChanges();
                return RedirectToAction("Index");
            }catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(ls);
            }
           
        }
    }
}
