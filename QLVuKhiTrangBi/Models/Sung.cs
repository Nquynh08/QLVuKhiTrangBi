using System;
using System.Collections.Generic;

namespace QLVuKhiTrangBi.Models;

public partial class Sung
{
    public string SoHieuSung { get; set; } = null!;

    public string? DonViTinh { get; set; }

    public string? TinhTrang { get; set; }

    public string? GhiChu { get; set; }

    public string? MaLoaiSung { get; set; }

    public string? SoQd { get; set; }

    public virtual ICollection<BanGiaoQkSung> BanGiaoQkSungs { get; set; } = new List<BanGiaoQkSung>();

    public virtual LoaiSung? MaLoaiSungNavigation { get; set; }

    public virtual QuyetDinh? SoQdNavigation { get; set; }
}
