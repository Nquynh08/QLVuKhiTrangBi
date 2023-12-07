using System;
using System.Collections.Generic;

namespace QLVuKhiTrangBi.Models;

public partial class BbbaoDuong
{
    public string MabbBaoDuong { get; set; } = null!;

    public string? TenbbBaoDuong { get; set; }

    public string? LoaiBaoDuong { get; set; }

    public string? CanBoKho { get; set; }

    public string? DaiDoi { get; set; }

    public string? CanBoDaiDoi { get; set; }

    public int? ThoiGianBaoDuong { get; set; }

    public DateTime? ThoiGian { get; set; }

    public virtual ICollection<CtbaoDuong> CtbaoDuongs { get; set; } = new List<CtbaoDuong>();
}
