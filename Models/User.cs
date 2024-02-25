using System;
using System.Collections.Generic;

namespace AccountDeletion.Models;

public partial class User
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string DateOfBirth { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
