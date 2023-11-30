using System;
using System.Collections.Generic;

namespace QLVuKhiTrangBi.Models;

public partial class Quyen
{
    public string MaQuyen { get; set; } = null!;

    public string? TenQuyen { get; set; }

    public virtual ICollection<NhomQuyen> NhomQuyens { get; set; } = new List<NhomQuyen>();
}
