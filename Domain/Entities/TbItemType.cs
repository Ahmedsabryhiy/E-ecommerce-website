using System;
using System.Collections.Generic;

namespace LapShop.Domain.Entities;

public partial class TbItemType
{
    public int ItemTypeId { get; set; }

    public string ItemTypeName { get; set; } = null!;

    public string? ImageName { get; set; }

    public int? CurrentState { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual ICollection<TbItem> TbItems { get; set; } = new List<TbItem>();
}
