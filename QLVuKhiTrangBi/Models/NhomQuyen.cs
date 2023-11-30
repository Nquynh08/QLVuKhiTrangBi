using System;
using System.Collections.Generic;

namespace QLVuKhiTrangBi.Models;

public partial class NhomQuyen
{
    public string MaNhom { get; set; } = null!;

    public string MaQuyen { get; set; } = null!;

    public string? GhiChu { get; set; }

    public virtual NhomNguoiDung MaNhomNavigation { get; set; } = null!;

    public virtual Quyen MaQuyenNavigation { get; set; } = null!;
}
