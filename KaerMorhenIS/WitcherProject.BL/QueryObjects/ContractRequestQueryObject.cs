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
        _contractRequestQuery.Filter(cr => filter.Id == null || cr.Id == filter.Id);
        _contractRequestQuery.Filter(cr => filter.State == null || cr.State == filter.State);
        _contractRequestQuery.Filter(cr => filter.ContractId == null || cr.ContractId == filter.ContractId);
        _contractRequestQuery.Filter(cr => filter.PersonId == null || cr.PersonId == filter.PersonId);
        _contractRequestQuery.Filter(cr => filter.CreatedOn == null || cr.CreatedOn >= filter.CreatedOn);

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