using System.ComponentModel;
using System.Diagnostics.Contracts;
using Mapster;
using Microsoft.EntityFrameworkCore;
using WitcherProject.BL.DTOs;
using WitcherProject.BL.DTOs.Contract;
using WitcherProject.DAL;
using WitcherProject.Infrastructure.EFCore.Query;
using Contract = WitcherProject.DAL.Models.Contract;

namespace WitcherProject.BL.QueryObjects;

public class ContractQueryObject
{
    private EFQuery<Contract> _contractQuery;

    public ContractQueryObject(DbContext context)
    {
        _contractQuery = new EFQuery<Contract>(context);
    }

    public async Task<IEnumerable<ContractDetailedDto>> ExecuteQuery(ContractFilterDto filter)
    {
        _contractQuery.Filter(contract => !string.IsNullOrEmpty(filter.Name) && contract.Name == filter.Name);
        _contractQuery.Filter(contract => !string.IsNullOrEmpty(filter.Description) && contract.Description == filter.Description);
        _contractQuery.Filter(contract => filter.State != null && contract.State == filter.State);
        _contractQuery.Filter(contract => filter.StartDate != null && contract.StartDate == filter.StartDate);
        _contractQuery.Filter(contract => filter.EndDate != null && contract.EndDate == filter.EndDate);
        _contractQuery.Filter(contract => filter.Deadline != null && contract.Deadline == filter.Deadline);
        _contractQuery.Filter(contract => !string.IsNullOrEmpty(filter.Location) && contract.Location == filter.Location);
        _contractQuery.Filter(contract => filter.ContractorId != null && contract.ContractorId == filter.ContractorId);
        _contractQuery.Filter(contract => filter.PersonId != null && contract.PersonId == filter.PersonId);

        if (filter.RequestedPageNumber != null)
        {
            _contractQuery.Page(filter.RequestedPageNumber ?? -1);
        }

        if (filter.SortCriteria != null)
        {
            _contractQuery.OrderBy(contract => contract.GetType().GetProperties().First(
                    prop => prop.Name == filter.SortCriteria),
                filter.SortAscending);
        }

        var returnedContracts = await _contractQuery.ExecuteAsync();
        return returnedContracts.Select(contract => contract.Adapt<ContractDetailedDto>());
    }
}