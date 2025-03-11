using System;
using System.Collections.Generic;

namespace LapShop.Domain.Entities;

public partial class TbPage
{
    public int PageId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? MetaKeyWord { get; set; }

    public string? MetaDescriptiuon { get; set; }

    public string? ImageName { get; set; }

    public int CurrentState { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public string Slug { get; set; } = null!;

    public string TemplateName { get; set; } = null!;
}
