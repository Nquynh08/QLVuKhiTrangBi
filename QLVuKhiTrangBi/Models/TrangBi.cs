using System;
using System.Collections.Generic;

namespace QLVuKhiTrangBi.Models;

public partial class TrangBi
{
    public string MaTrangBi { get; set; } = null!;

    public string? TenTrangBi { get; set; }

    public string? DonViTinh { get; set; }

    public int? SoLuong { get; set; }

    public string? TinhTrang { get; set; }

    public string? GhiChu { get; set; }

    public string? MaLoaiTb { get; set; }

    public string? SoQd { get; set; }

    public virtual ICollection<BanGiaoQkTrangBi> BanGiaoQkTrangBis { get; set; } = new List<BanGiaoQkTrangBi>();

    public virtual LoaiTrangBi? MaLoaiTbNavigation { get; set; }

    public virtual QuyetDinh? SoQdNavigation { get; set; }
}
