using System;
using System.Collections.Generic;

namespace QLVuKhiTrangBi.Models;

public partial class BienCheSung
{
    public string MaBienCheSung { get; set; } = null!;

    public string? GhiChu { get; set; }

    public string? MaLuotBienChe { get; set; }

    public string? MaHocVien { get; set; }

    public string? SoHieuSung { get; set; }

    public virtual HocVien? MaHocVienNavigation { get; set; }

    public virtual LuotBienChe? MaLuotBienCheNavigation { get; set; }

    public virtual Sung? SoHieuSungNavigation { get; set; }
}
