using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace WitcherProject.DAL.Models;

public class UserRole : IdentityUserRole<int> {

    public virtual Role Role { get; set; }

    public virtual Person User { get; set; }
    
    public DateTime ValidFrom { get; set; }

    public DateTime? ValidTo { get; set; }

}