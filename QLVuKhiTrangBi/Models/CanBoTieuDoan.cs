using System;
using System.Collections.Generic;

namespace QLVuKhiTrangBi.Models;

public partial class CanBoTieuDoan
{
    public string MaQn { get; set; } = null!;

    public string? HoTen { get; set; }

    public string? CapBac { get; set; }

    public string? ChucVu { get; set; }

    public string? Sdt { get; set; }

    public virtual ICollection<QuyetDinh> QuyetDinhs { get; set; } = new List<QuyetDinh>();
}
