using System;
using System.Collections.Generic;

namespace QLVuKhiTrangBi.Models;

public partial class Bbmuon
{
    public string MaBienBan { get; set; } = null!;

    public string? TenBienBan { get; set; }

    public string? CanBoKho { get; set; }

    public string? CanBoDaiDoi { get; set; }

    public DateTime? ThoiGianMuon { get; set; }

    public DateTime? Ngay { get; set; }

    public string? GhiChu { get; set; }
}
