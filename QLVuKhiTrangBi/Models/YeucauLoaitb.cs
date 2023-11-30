using System;
using System.Collections.Generic;

namespace QLVuKhiTrangBi.Models;

public partial class YeucauLoaitb
{
    public int MaYc { get; set; }

    public string MaTrangBi { get; set; } = null!;

    public int? SoLuong { get; set; }

    public string? GhiChu { get; set; }

    public virtual Yeucaumuonvktb MaYcNavigation { get; set; } = null!;
}
