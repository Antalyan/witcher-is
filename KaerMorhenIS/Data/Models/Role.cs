using Microsoft.EntityFrameworkCore;

namespace WitcherProject.Data.Models;

[Index(nameof(RoleName), IsUnique = true)]
public class Role
{
    public int Id { get; set; }

    public string RoleName { get; set; }
}