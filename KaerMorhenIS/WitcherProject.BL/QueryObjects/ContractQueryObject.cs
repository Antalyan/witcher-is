﻿using System.ComponentModel;
using Mapster;
using Microsoft.EntityFrameworkCore;
using WitcherProject.BL.DTOs.Contract;
using WitcherProject.DAL.Models;
using WitcherProject.Infrastructure.EFCore.Query;
using WitcherProject.Infrastructure.Query;

namespace WitcherProject.BL.QueryObjects;

public class ContractQueryObject: IContractQueryObject
{
    private IQuery<Contract> _contractQuery;

    public ContractQueryObject(IQuery<Contract> contractQuery)
    {
        _contractQuery = contractQuery;
    }

    public async Task<IEnumerable<ContractDetailedDto>> ExecuteQuery(ContractFilterDto filter)
    {
        _contractQuery.Filter(contract => filter.Id == null || contract.Id == filter.Id);
        _contractQuery.Filter(contract => string.IsNullOrEmpty(filter.Name) || contract.Name.Contains(filter.Name));
        _contractQuery.Filter(contract => string.IsNullOrEmpty(filter.Description) || contract.Description == filter.Description);
        _contractQuery.Filter(contract => filter.State == null || contract.State == filter.State);
        _contractQuery.Filter(contract => filter.StartDate == null || contract.StartDate <= filter.StartDate);
        _contractQuery.Filter(contract => filter.EndDate == null || contract.EndDate <= filter.EndDate);
        _contractQuery.Filter(contract => filter.Deadline == null || contract.Deadline <= filter.Deadline);
        _contractQuery.Filter(contract => string.IsNullOrEmpty(filter.Location) || contract.Location == filter.Location);
        _contractQuery.Filter(contract => filter.ContractorId == null || contract.ContractorId == filter.ContractorId);
        _contractQuery.Filter(contract => filter.PersonId == null || contract.PersonId == filter.PersonId);

        if (filter.RequestedPageNumber != null)
        {
            _contractQuery.Page(filter.RequestedPageNumber ?? -1);
        }

        switch (filter.SortCriteria)
        {
            case null:
            {
                _contractQuery.OrderBy(x => x.StartDate, false);
                break;
            }
            case "StartDate":
            {
                _contractQuery.OrderBy(x => x.StartDate, filter.SortAscending);
                break;
            }
            case "EndDate":
            {
                _contractQuery.OrderBy(x => x.EndDate, filter.SortAscending);
                break;
            }
            case "Name":
            {
                _contractQuery.OrderBy(x => x.Name, filter.SortAscending);
                break;
            }
            case "Deadline":
            {
                _contractQuery.OrderBy(x => x.Deadline, filter.SortAscending);
                break;
            }
            default:
            {
                throw new ArgumentException($@"Filtering by {filter.SortCriteria} not supported");
            }
        }

        _contractQuery.Include("Person");
        _contractQuery.Include("Contractor");

        var returnedContracts = await _contractQuery.ExecuteAsync();
        return returnedContracts.Select(contract => contract.Adapt<ContractDetailedDto>());
    }
}