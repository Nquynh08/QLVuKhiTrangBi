using System;
using System.Collections.Generic;

namespace QLVuKhiTrangBi.Models;

public partial class BienCheTb
{
    public string MaBienCheTb { get; set; } = null!;

    public int? SoLuong { get; set; }

    public string? GhiChu { get; set; }

    public string? MaLuotBienChe { get; set; }

    public string? MaHocVien { get; set; }

    public string? MaTrangBi { get; set; }

    public virtual HocVien? MaHocVienNavigation { get; set; }

    public virtual LuotBienChe? MaLuotBienCheNavigation { get; set; }

    public virtual TrangBi? MaTrangBiNavigation { get; set; }
}
