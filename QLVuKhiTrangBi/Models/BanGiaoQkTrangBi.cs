using System;
using System.Collections.Generic;

namespace QLVuKhiTrangBi.Models;

public partial class BanGiaoQkTrangBi
{
    public string MaBienBan { get; set; } = null!;

    public string MaTrangBi { get; set; } = null!;

    public string? HanhDong { get; set; }

    public int? SoLuong { get; set; }

    public string? GhiChu { get; set; }

    public virtual BbbanGiaoQk MaBienBanNavigation { get; set; } = null!;

    public virtual TrangBi MaTrangBiNavigation { get; set; } = null!;
}
