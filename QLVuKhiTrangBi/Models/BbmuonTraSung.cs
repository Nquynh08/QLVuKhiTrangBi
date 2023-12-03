using System;
using System.Collections.Generic;

namespace QLVuKhiTrangBi.Models;

public partial class BbmuonTraSung
{
    public string MaBienBan { get; set; } = null!;

    public string SoHieuSung { get; set; } = null!;

    public string? TinhTrang { get; set; }

    public string? GhiChu { get; set; }

    public virtual BbmuonTraVktb MaBienBanNavigation { get; set; } = null!;
}
