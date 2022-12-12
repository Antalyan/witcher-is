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
using WitcherProject.Infrastructure.Query;
using Contract = WitcherProject.DAL.Models.Contract;

namespace WitcherProject.BL.QueryObjects;

public class ContractRequestQueryObject: IContractRequestQueryObject
{
    private IQuery<ContractRequest> _contractRequestQuery;

    public ContractRequestQueryObject(IQuery<ContractRequest> contractRequestQuery)
    {
        _contractRequestQuery = contractRequestQuery;
    }

    public async Task<IEnumerable<ContractRequestDetailedDto>> ExecuteQuery(ContractRequestFilterDto filter)
    {
        _contractRequestQuery.Filter(contract => filter.State == null || contract.State == filter.State);
        _contractRequestQuery.Filter(contract => filter.ContractId == null || contract.ContractId == filter.ContractId);
        _contractRequestQuery.Filter(contract => filter.PersonId == null || contract.PersonId == filter.PersonId);

        if (filter.RequestedPageNumber != null)
        {
            _contractRequestQuery.Page(filter.RequestedPageNumber ?? -1);
        }

        _contractRequestQuery.OrderBy(x => x.CreatedOn, filter.SortAscending);

        _contractRequestQuery.Include("Person");
        _contractRequestQuery.Include("Contract");
        
        var returnedRequests = await _contractRequestQuery.ExecuteAsync();
        return returnedRequests.Select(request => request.Adapt<ContractRequestDetailedDto>());
    }
}