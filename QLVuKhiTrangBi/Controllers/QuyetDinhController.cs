using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLVuKhiTrangBi.Data;
using QLVuKhiTrangBi.Models;
using System.Linq;

namespace QLVuKhiTrangBi.Controllers
{
    [Authorize(Policy = "RequireTieuDoan")]
    public class QuyetDinhController : Controller
    {
        QlvuKhiTrangBiContext db = new QlvuKhiTrangBiContext();
        private readonly IWebHostEnvironment _webHost;
        public QuyetDinhController(IWebHostEnvironment webHost)
        {
            _webHost = webHost;
        }
        public IActionResult Index()
        {
            return View();
        }
      
        [HttpPost]
        public async Task<IActionResult> Index(string soqd, IFormFile file)
        {
            if (String.IsNullOrEmpty(soqd))
            {
                ViewBag.MessQD = "Vui lòng nhập số quyết định";
                return View();
            }
            if (file == null || file.Length <= 0)
            {
                ViewBag.MessFile = "Vui lòng chọn file";
                return View();
            }
            string uploadsFolder = Path.Combine(_webHost.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            string fileName = Path.GetFileName(file.FileName);
            string fileSavePath = Path.Combine(uploadsFolder, fileName);
            using(FileStream stream = new FileStream(fileSavePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            var obj = new QuyetDinh()
            {
                SoQd = soqd,
                ChiTiet = fileName,
                ThoiGian = DateTime.Now,
                NguoiGui = "QN02",
            };
            db.QuyetDinhs.Add(obj);
            db.SaveChanges();

            // sau khi thêm quyết định, tự động thêm biên bản bàn giao quân khí
            // mỗi vũ khí, trang bị được thêm bởi quyết định nào sẽ được thêm tương ứng với biên bản ấy
            var bgQK = new BbbanGiaoQk()
            {
                MaBienBan = string.Format("BB-{0}", soqd),
                TenBienBan = string.Format("Biên bản bàn giao quân khí theo quyết định {0}", soqd),
                ThoiGian = DateTime.Now,
                NguoiTao = "QN09",
            };
            db.BbbanGiaoQks.Add(bgQK);
            db.SaveChanges();
            return View();
        }

        public PartialViewResult DSQuyetDinh()
        {
            var dsqd = db.QuyetDinhs.Include(qd => qd.NguoiGuiNavigation).ToList();
            return PartialView("_dsqd", dsqd);
        }
        public IActionResult Delete(string id)
        {
            var qd = db.QuyetDinhs.Find(id);
            db.QuyetDinhs.Remove(qd);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(string id)
        {
            var qd = db.QuyetDinhs.Find(id);
            return View(qd);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(QuyetDinh model, IFormFile file)
        {
            var qd = db.QuyetDinhs.Find(model.SoQd);
            if (file == null || file.Length <= 0)
            {
                ViewBag.MessFile = "Vui lòng chọn file";
                return View(qd);
            }
            try
            {
                qd.SoQd = model.SoQd;
                qd.ChiTiet = model.ChiTiet;
                qd.ThoiGian = DateTime.Now;
                
                string uploadsFolder = Path.Combine(_webHost.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }
                    string fileName = Path.GetFileName(file.FileName);
                    string fileSavePath = Path.Combine(uploadsFolder, fileName);
                    using (FileStream stream = new FileStream(fileSavePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    qd.ChiTiet = fileName;
                
                db.QuyetDinhs.Update(qd);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(qd);
            }
        }
    }
}
