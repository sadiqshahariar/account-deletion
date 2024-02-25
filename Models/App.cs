using System;
using System.Collections.Generic;

namespace AccountDeletion.Models;

public partial class App
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string AppId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<AppConfig> AppConfigs { get; set; } = new List<AppConfig>();

    public virtual ICollection<UserRequest> UserRequests { get; set; } = new List<UserRequest>();
}
