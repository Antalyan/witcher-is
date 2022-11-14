﻿using Microsoft.EntityFrameworkCore;

namespace WitcherProject.DAL.Models;

[Index(nameof(Login), IsUnique = true)]
public class Person
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string PasswordHash { get; set; }
    public string Name { get; set; }
    public string? Surname { get; set; }
    public string? Cv { get; set; }
    public DateTime Birthdate { get; set; }
    public bool IsActive { get; set; }

    public virtual List<Contract> Contracts { get; set; }
    
    public virtual List<RoleToPerson> RoleToPersons { get; set; }
    
    public virtual List<ContractRequest> ContractRequests { get; set; }
}