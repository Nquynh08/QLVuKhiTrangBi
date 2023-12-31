﻿using System;
using System.Collections.Generic;

namespace QLVuKhiTrangBi.Models;

public partial class Sung
{
    public string SoHieuSung { get; set; } = null!;

    public string? DonViTinh { get; set; }

    public int? PhanCap { get; set; }

    public bool? SuDung { get; set; }

    public string? GhiChu { get; set; }

    public string? MaLoaiSung { get; set; }

    public string? SoQd { get; set; }

    public virtual ICollection<BienCheSung> BienCheSungs { get; set; } = new List<BienCheSung>();

    public virtual LoaiSung? MaLoaiSungNavigation { get; set; }

    public virtual QuyetDinh? SoQdNavigation { get; set; }
}
