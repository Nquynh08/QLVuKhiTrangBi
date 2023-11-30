using System;
using System.Collections.Generic;

namespace QLVuKhiTrangBi.Models;

public partial class CanBoDaiDoi
{
    public string MaQn { get; set; } = null!;

    public string? HoTen { get; set; }

    public string? CapBac { get; set; }

    public string? ChucVu { get; set; }

    public string? Sdt { get; set; }

    public string? MaDaiDoi { get; set; }

    public virtual DaiDoi? MaDaiDoiNavigation { get; set; }

    public virtual ICollection<Yeucaumuonvktb> Yeucaumuonvktbs { get; set; } = new List<Yeucaumuonvktb>();
}
