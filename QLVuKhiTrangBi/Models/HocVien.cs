using System;
using System.Collections.Generic;

namespace QLVuKhiTrangBi.Models;

public partial class HocVien
{
    public string MaHocVien { get; set; } = null!;

    public string? TenHocVien { get; set; }

    public string? MaDaiDoi { get; set; }

    public virtual ICollection<BienCheSung> BienCheSungs { get; set; } = new List<BienCheSung>();

    public virtual ICollection<BienCheTb> BienCheTbs { get; set; } = new List<BienCheTb>();

    public virtual DaiDoi? MaDaiDoiNavigation { get; set; }
}
