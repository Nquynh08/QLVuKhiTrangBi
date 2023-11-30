using System;
using System.Collections.Generic;

namespace QLVuKhiTrangBi.Models;

public partial class YeucauLoaisung
{
    public int MaYc { get; set; }

    public string MaLoaiSung { get; set; } = null!;

    public int? SoLuong { get; set; }

    public string? GhiChu { get; set; }

    public virtual Yeucaumuonvktb MaYcNavigation { get; set; } = null!;
}
