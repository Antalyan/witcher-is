using System.Linq.Expressions;
using WitcherProject.BL.DTOs.Contract;
using WitcherProject.BL.DTOs.Contractor;
using WitcherProject.BL.DTOs.Person;
using WitcherProject.DAL.Models;
using WitcherProject.Shared.Enums;

namespace WitcherProject.BL.Test;

public static class BlTestDataInitalizator
{
    public static Contractor GetContractorDal(int id)
    {
        switch (id)
        {
            case 1:
                return new Contractor
                {
                    Id = 1,
                    Name = "Odolan",
                    Surname = "White"
                };
            case 2:
                return new Contractor
                {
                    Id = 2,
                    Name = "Holy",
                    Surname = "Yevadic"
                };
            default:
                throw new ArgumentException("Id not found in method");
        }
    }

    public static Person GetPersonDal(string name)
    {
        switch (name)
        {
            case "Geralt":
                return new Person
                {
                    Id = 1,
                    Birthdate = new DateTime(1336, 12, 12),
                    Cv = "White wolf, butcherOfBlaviken",
                    IsActive = true,
                    Login = "whitewolf",
                    Name = "Geralt",
                    Surname = "of Rivia"
                };
            case "Lambert":
                return new Person
                {
                    Id = 2,
                    Birthdate = new DateTime(1356, 02, 05),
                    Cv = "what a prick",
                    IsActive = true,
                    Login = "jobForVesemir",
                    Name = "Lambert"
                };


            default:
                throw new ArgumentException("Name not found in method");
        }
    }

    public static Contract GetContractDal(string name)
    {
        var odolanDal = GetContractorDal(1);
        var geraltDal = GetPersonDal("Geralt");
        switch (name)
        {
            case "Devil by the Well":
                return new Contract
                {
                    Id = 1,
                    Name = "Devil by the Well",
                    Description = "Slay the bitch - Odolan",
                    State = ContractState.Open,
                    StartDate = new DateTime(2022, 8, 8),
                    EndDate = new DateTime(2022, 10, 1),
                    Contractor = odolanDal,
                    ContractorId = odolanDal.Id,
                    Deadline = new DateTime(2022, 11, 1),
                    Location = "White Orchard",
                    PersonId = null
                };
            case "Jenny o' the Woods":
                return new Contract
                {
                    Id = 2,
                    Name = "Jenny o' the Woods",
                    Description = "Beware the night - Odolan",
                    State = ContractState.Open,
                    StartDate = new DateTime(2022, 9, 9),
                    EndDate = new DateTime(2022, 10, 2),
                    Contractor = odolanDal,
                    ContractorId = odolanDal.Id,
                    Deadline = new DateTime(2022, 10, 20),
                    Location = "Honorton",
                    PersonId = null
                };
            case "The Beast of Honorton":
                var holyDal = GetContractorDal(2);
                return new Contract
                {
                    Id = 3,
                    Name = "The Beast of Honorton",
                    Description = "They eated dead, than my wife and children, kill them please - Holy",
                    State = ContractState.Assigned,
                    StartDate = new DateTime(2022, 9, 10),
                    EndDate = new DateTime(2022, 10, 3),
                    Contractor = holyDal,
                    ContractorId = holyDal.Id,
                    Deadline = new DateTime(2022, 11, 3),
                    Location = "Honorton",
                    Person = geraltDal,
                    PersonId = geraltDal.Id
                };
            default:
                throw new ArgumentException("Name not found in method");
        }
    }

    public static ContractorDto GetContractorDto(int id)
    {
        switch (id)
        {
            case 1:
                return new ContractorDto
                {
                    Id = 1,
                    Name = "Odolan",
                    Surname = "White"
                };
            case 2:
                return new ContractorDto
                {
                    Id = 2,
                    Name = "Holy",
                    Surname = "Yevadic"
                };
            default:
                throw new ArgumentException("Id not found in method");
        }
    }

    public static PersonCompleteDto GetPersonCompleteDto(string name)
    {
        switch (name)
        {
            case "Geralt":
                return new PersonCompleteDto
                {
                    Id = 1,
                    Birthdate = new DateTime(1336, 12, 12),
                    Cv = "White wolf, butcherOfBlaviken",
                    IsActive = true,
                    Login = "whitewolf",
                    Name = "Geralt",
                    Surname = "of Rivia",
                    Contracts = new List<ContractSimpleDto> { GetContractSimpleDto("The Beast of Honorton") }
                };

            case "Lambert":
                return new PersonCompleteDto
                {
                    Id = 2,
                    Birthdate = new DateTime(1356, 02, 05),
                    Cv = "what a prick",
                    IsActive = true,
                    Login = "jobForVesemir",
                    Name = "Lambert"
                };

            default:
                throw new ArgumentException("Name not found in method");
        }
    }

    public static PersonSimpleDto GetPersonSimpleDto(string name)
    {
        switch (name)
        {
            case "Geralt":
                return new PersonSimpleDto
                {
                    Id = 1,
                    Login = "whitewolf",
                    Name = "Geralt",
                    Surname = "of Rivia"
                };

            default:
                throw new ArgumentException("Name not found in method");
        }
    }

    public static ContractDetailedDto GetContractDetailedDto(string name)
    {
        var odolanDto = GetContractorDto(1);
        var geraltDto = GetPersonSimpleDto("Geralt");
        switch (name)
        {
            case "Devil by the Well":
                return new ContractDetailedDto
                {
                    Id = 1,
                    Name = "Devil by the Well",
                    Description = "Slay the bitch - Odolan",
                    State = ContractState.Open,
                    StartDate = new DateTime(2022, 8, 8),
                    EndDate = new DateTime(2022, 10, 1),
                    Contractor = odolanDto,
                    Deadline = new DateTime(2022, 11, 1),
                    Location = "White Orchard",
                    Person = null
                };
            case "Jenny o' the Woods":
                return new ContractDetailedDto
                {
                    Id = 2,
                    Name = "Jenny o' the Woods",
                    Description = "Beware the night - Odolan",
                    State = ContractState.Open,
                    StartDate = new DateTime(2022, 9, 9),
                    EndDate = new DateTime(2022, 10, 2),
                    Contractor = odolanDto,
                    Deadline = new DateTime(2022, 10, 20),
                    Location = "Honorton",
                    Person = null
                };
            case "The Beast of Honorton":
                var holyDto = GetContractorDto(2);
                return new ContractDetailedDto
                {
                    Id = 3,
                    Name = "The Beast of Honorton",
                    Description = "They eated dead, than my wife and children, kill them please - Holy",
                    State = ContractState.Assigned,
                    StartDate = new DateTime(2022, 9, 10),
                    EndDate = new DateTime(2022, 10, 3),
                    Contractor = holyDto,
                    Deadline = new DateTime(2022, 11, 3),
                    Location = "Honorton",
                    Person = geraltDto
                };
            default:
                throw new ArgumentException("Name not found in method");
        }
    }

    public static ContractSimpleDto GetContractSimpleDto(string name)
    {
        switch (name)
        {
            case "Devil by the Well":
                return new ContractSimpleDto()
                {
                    Id = 1,
                    Name = "Devil by the Well"
                };
            case "Jenny o' the Woods":
                return new ContractSimpleDto
                {
                    Id = 2,
                    Name = "Jenny o' the Woods"
                };
            case "The Beast of Honorton":
                return new ContractSimpleDto
                {
                    Id = 3,
                    Name = "The Beast of Honorton"
                };
            default:
                throw new ArgumentException("Name not found in method");
        }
    }
}