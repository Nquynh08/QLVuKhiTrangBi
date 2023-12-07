using System;
using System.Collections.Generic;

namespace QLVuKhiTrangBi.Models;

public partial class CtbaoDuong
{
    public string MabbBaoDuong { get; set; } = null!;

    public string SoHieuSung { get; set; } = null!;

    public string? TinhTrang { get; set; }
    public string? GhiChu { get; set; }

    public virtual BbbaoDuong MabbBaoDuongNavigation { get; set; } = null!;
}
