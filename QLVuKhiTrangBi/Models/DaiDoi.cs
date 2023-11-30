﻿using System;
using System.Collections.Generic;

namespace QLVuKhiTrangBi.Models;

public partial class DaiDoi
{
    public string MaDaiDoi { get; set; } = null!;

    public string? TenDaiDoi { get; set; }

    public virtual ICollection<CanBoDaiDoi> CanBoDaiDois { get; set; } = new List<CanBoDaiDoi>();
}
