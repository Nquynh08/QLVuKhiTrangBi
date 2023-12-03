using System;
using System.Collections.Generic;

namespace QLVuKhiTrangBi.Models;

public partial class LichGac
{
    public int Stt { get; set; }

    public DateTime Ngay { get; set; }

    public string? DaiDoiGac { get; set; }

    public TimeSpan? ThoiGianBatDau { get; set; }

    public TimeSpan? ThoiGianKetThuc { get; set; }

    public string? TrangThai { get; set; }

    public string? GhiChu { get; set; }
}
