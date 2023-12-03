using System;
using System.Collections.Generic;

namespace QLVuKhiTrangBi.Models;

public partial class BbmuonTraTb
{
    public int MaBienBan { get; set; }

    public string MaTrangBi { get; set; } = null!;

    public int? SoLuong { get; set; }

    public int MuonTra { get; set; }

    public string? TinhTrang { get; set; }

    public int? Slthieu { get; set; }

    public string? GhiChu { get; set; }

    public virtual BbmuonTraVktb MaBienBanNavigation { get; set; } = null!;
}
