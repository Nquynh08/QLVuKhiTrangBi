using System;
using System.Collections.Generic;

namespace QLVuKhiTrangBi.Models;

public partial class BbbanGiaoKho
{
    public string MaBanGiaoKho { get; set; } = null!;

    public DateTime? ThoiGian { get; set; }

    public string? MaNguoiGiao { get; set; }

    public string? MaNguoiNhan { get; set; }

    public string? TinhTrangKho { get; set; }

    public string? GhiChu { get; set; }

    public virtual CanBoPhuTrach? MaNguoiGiaoNavigation { get; set; }

    public virtual CanBoPhuTrach? MaNguoiNhanNavigation { get; set; }
}
