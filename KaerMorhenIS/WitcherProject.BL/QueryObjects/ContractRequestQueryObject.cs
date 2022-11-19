using System.ComponentModel;
using System.Diagnostics.Contracts;
using Mapster;
using Microsoft.EntityFrameworkCore;
using WitcherProject.BL.DTOs;
using WitcherProject.BL.DTOs.Contract;
using WitcherProject.BL.DTOs.ContractRequest;
using WitcherProject.DAL;
using WitcherProject.DAL.Models;
using WitcherProject.Infrastructure.EFCore.Query;
using Contract = WitcherProject.DAL.Models.Contract;

namespace WitcherProject.BL.QueryObjects;

public class ContractRequestQueryObject
{
    private EFQuery<ContractRequest> _contractRequestQuery;

    public ContractRequestQueryObject(DbContext context)
    {
        _contractRequestQuery = new EFQuery<ContractRequest>(context);
    }

    public async Task<IEnumerable<ContractRequestDetailedDto>> ExecuteQuery(ContractRequestFilterDto filter)
    {
        _contractRequestQuery.Filter(contract => filter.State != null && contract.State == filter.State);
        _contractRequestQuery.Filter(contract => filter.ContractId != null && contract.ContractId == filter.ContractId);
        _contractRequestQuery.Filter(contract => filter.PersonId != null && contract.PersonId == filter.PersonId);

        if (filter.RequestedPageNumber != null)
        {
            _contractRequestQuery.Page(filter.RequestedPageNumber ?? -1);
        }

        if (filter.SortCriteria != null)
        {
            _contractRequestQuery.OrderBy(request => request.GetType().GetProperties().First(
                    prop => prop.Name == filter.SortCriteria), filter.SortAscending);
        }

        var returnedRequests = await _contractRequestQuery.ExecuteAsync();
        return returnedRequests.Select(request => request.Adapt<ContractRequestDetailedDto>());
    }
}