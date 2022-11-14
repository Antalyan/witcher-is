namespace WitcherProject.BL.DTOs.Person;

public class PersonUpdateDto
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string PasswordHash { get; set; }
    public string Name { get; set; }
    public string? Surname { get; set; }
    public string? Cv { get; set; }
    public DateTime Birthdate { get; set; }
    public bool IsActive { get; set; }

    public virtual List<int> ContractsIds { get; set; }
}