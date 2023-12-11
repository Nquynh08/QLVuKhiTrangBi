using System;
using System.Collections.Generic;

namespace QLVuKhiTrangBi.Models;

public partial class LuotBienChe
{
    public string MaLuotBienChe { get; set; } = null!;

    public DateTime? ThoiGianBd { get; set; }

    public DateTime? ThoiGianKt { get; set; }

    public string? GhiChu { get; set; }

    public string? MaDaiDoi { get; set; }

    public virtual ICollection<BienCheSung> BienCheSungs { get; set; } = new List<BienCheSung>();

    public virtual ICollection<BienCheTb> BienCheTbs { get; set; } = new List<BienCheTb>();

    public virtual DaiDoi? MaDaiDoiNavigation { get; set; }
}
