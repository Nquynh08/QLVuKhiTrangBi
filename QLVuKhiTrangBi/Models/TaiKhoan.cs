﻿using System;
using System.Collections.Generic;

namespace QLVuKhiTrangBi.Models;

public partial class TaiKhoan
{
    public string MaQn { get; set; } = null!;

    public string? HoTen { get; set; }

    public string? TenDn { get; set; }

    public string? MatKhau { get; set; }

    public string Role { get; set; } = null!;
}
