namespace WitcherProject.BL.DTOs.Person;

public class PersonCreateNewDto
{
    public string Login { get; set; }
    public string Name { get; set; }
    public string? Surname { get; set; }
    public string? Cv { get; set; }
    public DateTime Birthdate { get; set; }
    public bool IsActive { get; set; }
}