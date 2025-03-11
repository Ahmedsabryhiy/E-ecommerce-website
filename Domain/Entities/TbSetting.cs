using System;
using System.Collections.Generic;

namespace LapShop.Domain.Entities;

public partial class TbSetting
{
    public int Id { get; set; }

    public string WebSiteName { get; set; } = null!;

    public string Logo { get; set; } = null!;

    public string TiwetterLink { get; set; } = null!;

    public string FaseBookLink { get; set; } = null!;

    public string YouTupekLink { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string? ContactNubmer { get; set; }

    public string? LastPanner { get; set; }

    public string? MiddelPanner { get; set; }

    public string WebSiteDescription { get; set; } = null!;
}
