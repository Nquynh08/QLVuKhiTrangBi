using Microsoft.AspNetCore.Mvc;
using QLVuKhiTrangBi.Data;
using QLVuKhiTrangBi.Models;

namespace QLVuKhiTrangBi.Controllers
{
    public class BanGiaoKhoController : Controller
    {
        QlvuKhiTrangBiContext db = new QlvuKhiTrangBiContext();
        public IActionResult Index()
        {
            var bgklast = db.BbbanGiaoKhos.OrderByDescending(x => x.ThoiGian).FirstOrDefault();
            var nguoinhan= db.CanBoPhuTraches.Where(x=>x.MaQn==bgklast.MaNguoiNhan).FirstOrDefault();
            var cbpt = db.CanBoPhuTraches;
            ViewBag.cbpt=cbpt;
            ViewBag.tencbpt = nguoinhan.HoTen;
            ViewBag.macbpt = nguoinhan.MaQn;
            ViewBag.thoigianpt = bgklast.ThoiGian;
            return View();
        }
        public PartialViewResult bbbgk()
        {
            var dsbgk = (from bgk in db.BbbanGiaoKhos
                        join qn1 in db.CanBoPhuTraches
                            on bgk.MaNguoiGiao equals qn1.MaQn
                        join qn2 in db.CanBoPhuTraches
                            on bgk.MaNguoiNhan equals qn2.MaQn
                        select new Bangiaokho
                        {
                            MaBanGiaoKho = bgk.MaBanGiaoKho,
                            ThoiGian = bgk.ThoiGian,
                            MaNguoiGiao = bgk.MaNguoiGiao,
                            TenNguoiGiao = qn1.HoTen,
                            MaNguoiNhan = bgk.MaNguoiNhan,
                            TenNguoiNhan = qn2.HoTen,
                            TinhTrangKho = bgk.TinhTrangKho,
                            GhiChu = bgk.GhiChu
                        }).OrderByDescending(x=>x.ThoiGian);
                        
            return PartialView("_dsbgk",dsbgk);
        }
        [HttpPost]
        public IActionResult Index( string MaNguoiNhan, string tinhtrangkho, string ghichu)
        {
            try
            {
                var bgklast = db.BbbanGiaoKhos.OrderByDescending(x => x.ThoiGian).FirstOrDefault();
                var nguoinhan = db.CanBoPhuTraches.Where(x => x.MaQn == bgklast.MaNguoiNhan).FirstOrDefault();
                string manguoigiao = nguoinhan.MaQn;

                string mabgk;
                DateTime thoigian = DateTime.Now;

                // Nếu chưa tồn tại, tạo mới mã
                var lastbgk = db.BbbanGiaoKhos.OrderByDescending(bgk => bgk.MaBanGiaoKho).FirstOrDefault();

                if (lastbgk != null)
                {
                    // Tạo mã mới dựa trên mã cuối cùng và tăng lên 1
                    int lastId = int.Parse(lastbgk.MaBanGiaoKho.Substring(5)); // Lấy phần số cuối cùng từ mã cũ
                    mabgk= "BGK" + (lastId + 1).ToString("D5");
                }
                else
                {
                    // Nếu không có khách hàng nào, bắt đầu từ 00001
                    mabgk = "BGK00001";
                }
                var bgk = new BbbanGiaoKho()
                {
                    MaBanGiaoKho=mabgk,
                    ThoiGian=thoigian,
                    MaNguoiGiao=manguoigiao,
                    MaNguoiNhan=MaNguoiNhan,
                    TinhTrangKho = tinhtrangkho,
                    GhiChu=ghichu,
                };
                db.BbbanGiaoKhos.Add(bgk);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }
    }
}
