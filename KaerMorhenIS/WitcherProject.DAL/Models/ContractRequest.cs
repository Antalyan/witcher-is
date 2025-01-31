﻿using System.ComponentModel.DataAnnotations.Schema;
using WitcherProject.Shared.Enums;

namespace WitcherProject.DAL.Models;

public class ContractRequest
{
    public int Id { get; set; }

    public DateTime CreatedOn { get; set; }

    public string? Text { get; set; }

    public ContractRequestState State { get; set; }

    public int PersonId { get; set; }

    [ForeignKey(nameof(PersonId))] public virtual Person Person { get; set; }

    public int ContractId { get; set; }

    [ForeignKey(nameof(ContractId))] public virtual Contract Contract { get; set; }
}