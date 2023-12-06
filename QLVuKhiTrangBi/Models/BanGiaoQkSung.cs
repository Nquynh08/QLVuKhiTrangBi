using System;
using System.Collections.Generic;

namespace QLVuKhiTrangBi.Models;

public partial class BanGiaoQkSung
{
    public string MaBienBan { get; set; } = null!;

    public string SoHieuSung { get; set; } = null!;

    public int? PhanCap { get; set; }

    public string? HanhDong { get; set; }

    public string? GhiChu { get; set; }

    public virtual BbbanGiaoQk MaBienBanNavigation { get; set; } = null!;

    public virtual Sung SoHieuSungNavigation { get; set; } = null!;
}
