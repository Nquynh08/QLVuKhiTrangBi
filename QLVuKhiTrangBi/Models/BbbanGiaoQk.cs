using System;
using System.Collections.Generic;

namespace QLVuKhiTrangBi.Models;

public partial class BbbanGiaoQk
{
    public string MaBienBan { get; set; } = null!;

    public string? TenBienBan { get; set; }

    public string? GhiChu { get; set; }

    public DateTime? ThoiGian { get; set; }

    public string? NguoiTao { get; set; }

    public virtual ICollection<BanGiaoQkSung> BanGiaoQkSungs { get; set; } = new List<BanGiaoQkSung>();

    public virtual ICollection<BanGiaoQkTrangBi> BanGiaoQkTrangBis { get; set; } = new List<BanGiaoQkTrangBi>();

    public virtual CanBoPhuTrach? NguoiTaoNavigation { get; set; }
}
