using System;
using System.Collections.Generic;

namespace QLVuKhiTrangBi.Models;

public partial class BbmuonTraTb
{
    public string MaBienBan { get; set; } = null!;

    public string MaTrangBi { get; set; } = null!;

    public int? SoLuong { get; set; }

    public string? TinhTrang { get; set; }

    public string? GhiChu { get; set; }

    public virtual BbmuonTraVktb MaBienBanNavigation { get; set; } = null!;
}
