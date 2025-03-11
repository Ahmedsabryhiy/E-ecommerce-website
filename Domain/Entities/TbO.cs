﻿using System;
using System.Collections.Generic;

namespace LapShop.Domain.Entities;

public partial class TbO
{
    public int OsId { get; set; }

    public string OsName { get; set; } = null!;

    public string? ImageName { get; set; }

    public bool ShowInHomePage { get; set; }

    public int? CurrentState { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual ICollection<TbItem> TbItems { get; set; } = new List<TbItem>();
}
