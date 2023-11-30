using System;
using System.Collections.Generic;

namespace QLVuKhiTrangBi.Models;

public partial class Yeucaumuonvktb
{
    public int MaYc { get; set; }

    public string? MaDaiDoi { get; set; }

    public string? MaCanBoDaiDoi { get; set; }

    public DateTime? ThoiGianDuKienMuon { get; set; }

    public DateTime? ThoiGianDuKienTra { get; set; }

    public string? GhiChu { get; set; }

    public virtual CanBoDaiDoi? MaCanBoDaiDoiNavigation { get; set; }

    public virtual ICollection<YeucauLoaisung> YeucauLoaisungs { get; set; } = new List<YeucauLoaisung>();

    public virtual ICollection<YeucauLoaitb> YeucauLoaitbs { get; set; } = new List<YeucauLoaitb>();
}
