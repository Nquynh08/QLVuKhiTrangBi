using System;
using System.Collections.Generic;

namespace QLVuKhiTrangBi.Models;

public partial class NhomNguoiDung
{
    public string MaNhom { get; set; } = null!;

    public string? TenNhom { get; set; }

    public virtual ICollection<NhomQuyen> NhomQuyens { get; set; } = new List<NhomQuyen>();
}
