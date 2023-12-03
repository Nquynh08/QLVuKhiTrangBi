using System;
using System.Collections.Generic;

namespace QLVuKhiTrangBi.Models;

public partial class BbmuonTraVktb
{
    public string MaBienBan { get; set; } = null!;

    public string? TenBienBan { get; set; }

    public string? CanBoKho { get; set; }

    public string? CanBoDaiDoi { get; set; }

    public DateTime? ThoiGianMuon { get; set; }

    public DateTime? ThoiGianTra { get; set; }

    public string? GhiChu { get; set; }

    public virtual ICollection<BbmuonTraSung> BbmuonTraSungs { get; set; } = new List<BbmuonTraSung>();

    public virtual ICollection<BbmuonTraTb> BbmuonTraTbs { get; set; } = new List<BbmuonTraTb>();
}
