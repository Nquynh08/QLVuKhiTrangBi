using System;
using System.Collections.Generic;

namespace QLVuKhiTrangBi.Models;

public partial class LoaiSung
{
    public string MaLoaiSung { get; set; } = null!;

    public string? TenLoaiSung { get; set; }

    public int? SoLuong { get; set; }

    public virtual ICollection<Sung> Sungs { get; set; } = new List<Sung>();
}
