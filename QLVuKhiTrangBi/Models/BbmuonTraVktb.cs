using System;
using System.Collections.Generic;

namespace QLVuKhiTrangBi.Models;

public partial class BbmuonTraVktb
{
    public int MaBienBan { get; set; }

    public string? TenBienBan { get; set; }

    public DateTime? NgayGac { get; set; }

    public string? DaiDoiGac { get; set; }

    public string? CbkhoGiao { get; set; }

    public string? CbdaidoiNhan { get; set; }

    public string? ThoiGianMuon { get; set; }

    public string? CbdaidoiGiao { get; set; }

    public string? CbkhoNhan { get; set; }

    public string? ThoiGianTra { get; set; }

    public string? GhiChu { get; set; }

    public virtual ICollection<BbmuonTraSung> BbmuonTraSungs { get; set; } = new List<BbmuonTraSung>();

    public virtual ICollection<BbmuonTraTb> BbmuonTraTbs { get; set; } = new List<BbmuonTraTb>();
}
