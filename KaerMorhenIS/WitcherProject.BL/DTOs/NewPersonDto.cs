namespace WitcherProject.BL.DTOs;

public class NewPersonDto
{
    public string Login { get; set; }
    public string Name { get; set; }
    public string? Surname { get; set; }
    public string? Cv { get; set; }
    public DateTime Birthdate { get; set; }
    public bool IsActive { get; set; }
}