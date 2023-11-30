using System;
using System.Collections.Generic;

namespace QLVuKhiTrangBi.Models;

public partial class CanBoPhuTrach
{
    public string MaQn { get; set; } = null!;

    public string? HoTen { get; set; }

    public string? CapBac { get; set; }

    public string? ChucVu { get; set; }

    public string? Sdt { get; set; }

    public virtual ICollection<BbbanGiaoKho> BbbanGiaoKhoMaNguoiGiaoNavigations { get; set; } = new List<BbbanGiaoKho>();

    public virtual ICollection<BbbanGiaoKho> BbbanGiaoKhoMaNguoiNhanNavigations { get; set; } = new List<BbbanGiaoKho>();

    public virtual ICollection<BbbanGiaoQk> BbbanGiaoQks { get; set; } = new List<BbbanGiaoQk>();
}
