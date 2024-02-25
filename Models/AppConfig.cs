using System;
using System.Collections.Generic;

namespace AccountDeletion.Models;

public partial class AppConfig
{
    public int Id { get; set; }

    public int AppId { get; set; }

    public string BannerColor { get; set; } = null!;

    public string FormColor { get; set; } = null!;

    public string Logo { get; set; } = null!;

    public string InputLabel { get; set; } = null!;

    public string Placeholder { get; set; } = null!;

    public string ButtonColor { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual App App { get; set; } = null!;
}
