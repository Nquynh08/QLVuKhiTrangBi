using System;
using System.Collections.Generic;

namespace QLVuKhiTrangBi.Models;

public partial class QuyetDinh
{
    public string SoQd { get; set; } = null!;

    public string? ChiTiet { get; set; }

    public DateTime? ThoiGian { get; set; }

    public string? NguoiGui { get; set; }

    public virtual CanBoTieuDoan? NguoiGuiNavigation { get; set; }

    public virtual ICollection<Sung> Sungs { get; set; } = new List<Sung>();

    public virtual ICollection<TrangBi> TrangBis { get; set; } = new List<TrangBi>();
}
