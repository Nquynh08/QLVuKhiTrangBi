using System;
using System.Collections.Generic;

namespace QLVuKhiTrangBi.Models;

public partial class BbmuonTraSung
{
    public int MaBienBan { get; set; }

    public string SoHieuSung { get; set; } = null!;

    public int MuonTra { get; set; }

    public string? TinhTrang { get; set; }

    public string? GhiChu { get; set; }

    public virtual BbmuonTraVktb MaBienBanNavigation { get; set; } = null!;
}
