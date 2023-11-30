using System;
using System.Collections.Generic;

namespace QLVuKhiTrangBi.Models;

public partial class LoaiTrangBi
{
    public string MaLoaiTb { get; set; } = null!;

    public string? TenLoaiTb { get; set; }

    public virtual ICollection<TrangBi> TrangBis { get; set; } = new List<TrangBi>();
}
