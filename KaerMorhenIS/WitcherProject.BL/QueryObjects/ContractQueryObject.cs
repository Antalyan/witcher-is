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

    public async Task<IEnumerable<ContractUpdateDto>> ExecuteQuery(ContractFilterDto filter)
    {
        // TODO: Ask whether it can be nicer or even generic and how (via reflection)
        // var filterProperties = TypeDescriptor.GetProperties(typeof(ContractFilterDto));
        // var entityProperties = TypeDescriptor.GetProperties(typeof(Contract));
        //
        // foreach (string property in filterProperties)
        // {
        //     var attributeValue = filterProperties[property]?.GetValue(filter);
        //     if (attributeValue != null && entityProperties[property] != null)
        //     {
        //         _contractQuery.Filter(contract => entityProperties[property].GetValue(contract) == attributeValue);
        //     }
        // }

        if (!string.IsNullOrEmpty(filter.Name))
        {
            _contractQuery.Filter(contract => contract.Name == filter.Name);
        }

        if (!string.IsNullOrEmpty(filter.Description))
        {
            _contractQuery.Filter(contract => contract.Description == filter.Description);
        }

        if (filter.State != null)
        {
            _contractQuery.Filter(contract => contract.State == filter.State);
        }

        if (filter.StartDate != null)
        {
            _contractQuery.Filter(contract => contract.StartDate == filter.StartDate);
        }

        if (filter.EndDate != null)
        {
            _contractQuery.Filter(contract => contract.EndDate == filter.EndDate);
        }

        if (filter.Deadline != null)
        {
            _contractQuery.Filter(contract => contract.Deadline == filter.Deadline);
        }

        if (filter.ContractorId != null)
        {
            _contractQuery.Filter(contract => contract.ContractorId == filter.ContractorId);
        }

        if (filter.PersonId != null)
        {
            _contractQuery.Filter(contract => contract.PersonId == filter.PersonId);
        }

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
        return returnedContracts.Select(contract => contract.Adapt<ContractUpdateDto>());
    }
}