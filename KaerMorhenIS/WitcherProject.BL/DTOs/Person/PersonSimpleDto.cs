﻿namespace WitcherProject.BL.DTOs.Person;

public class PersonSimpleDto
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Name { get; set; }
    public string? Surname { get; set; }
}