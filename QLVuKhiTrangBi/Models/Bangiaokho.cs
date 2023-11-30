namespace QLVuKhiTrangBi.Models
{
    public class Bangiaokho
    {
        public string MaBanGiaoKho { get; set; } = null!;

        public DateTime? ThoiGian { get; set; }

        public string? MaNguoiGiao { get; set; }
        public string? TenNguoiGiao { get; set; }

        public string? MaNguoiNhan { get; set; }
        public string? TenNguoiNhan { get; set; }

        public string? TinhTrangKho { get; set; }

        public string? GhiChu { get; set; }
    }
}
