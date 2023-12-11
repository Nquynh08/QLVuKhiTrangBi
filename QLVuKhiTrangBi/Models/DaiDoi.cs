using System;
using System.Collections.Generic;

namespace QLVuKhiTrangBi.Models;

public partial class DaiDoi
{
    public string MaDaiDoi { get; set; } = null!;

    public string? TenDaiDoi { get; set; }

    public virtual ICollection<CanBoDaiDoi> CanBoDaiDois { get; set; } = new List<CanBoDaiDoi>();

    public virtual ICollection<HocVien> HocViens { get; set; } = new List<HocVien>();

    public virtual ICollection<LuotBienChe> LuotBienChes { get; set; } = new List<LuotBienChe>();
}
