using System;
using System.Collections.Generic;

namespace AccountDeletion.Models;

public partial class UserRequest
{
    public int Id { get; set; }

    public int AppId { get; set; }

    public string Data { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual App App { get; set; } = null!;
}
