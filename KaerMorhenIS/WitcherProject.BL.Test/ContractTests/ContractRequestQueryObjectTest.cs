using System.Linq.Expressions;
using Moq;
using WitcherProject.BL.DTOs.ContractRequest;
using WitcherProject.BL.QueryObjects;
using WitcherProject.DAL.Models;
using WitcherProject.Infrastructure.Query;
using WitcherProject.Shared.Enums;
using Xunit;

namespace WitcherProject.BL.Test.ContractTests;

public class ContractRequestQueryObjectTest
{
    private Contract _devilByTheWell;

    private Person _geralt;

    private ContractRequest _wellDevilContractRequestByGeralt;

    public ContractRequestQueryObjectTest()
    {
        _devilByTheWell = BlTestDataInitalizator.GetContractDal("Devil by the Well");
        _geralt = BlTestDataInitalizator.GetPersonDal("Geralt");

        _wellDevilContractRequestByGeralt = new ContractRequest()
        {
            Person = _geralt,
            Contract = _devilByTheWell,
            Id = 1,
            CreatedOn = new DateTime(1446, 01, 01),
            State = ContractRequestState.Accepted,
            Text = "I'll  do it ok..."
        };
    }

    [Fact]
    public async Task Test()
    {
        var mockQuery = new Mock<IQuery<ContractRequest>>();

        var wellDevilContractRequestByGeraltDetailedDto = new ContractRequestDetailedDto
        {
            Id = 1,
            State = ContractRequestState.Accepted,
            Person = BlTestDataInitalizator.GetPersonSimpleDto("Geralt"),
            Contract = BlTestDataInitalizator.GetContractSimpleDto("Devil by the Well"),
            CreatedOn = _wellDevilContractRequestByGeralt.CreatedOn,
            Text = _wellDevilContractRequestByGeralt.Text
        };

        var contractRequestFilter = new ContractRequestFilterDto
        {
            State = ContractRequestState.Accepted,
            PersonId = _geralt.Id,
            SortAscending = false,
            RequestedPageNumber = 1
        };
        mockQuery.Setup(mcq => mcq.Filter(It.IsAny<Expression<Func<ContractRequest, bool>>>())).Verifiable();
        mockQuery.Setup(mcq => mcq.OrderBy(It.IsAny<Expression<Func<ContractRequest, DateTime>>>(), false))
            .Verifiable();
        mockQuery.Setup(mcq => mcq.Page(1, 10)).Verifiable();
        mockQuery.Setup(mcq => mcq.ExecuteAsync().Result)
            .Returns(new List<ContractRequest> { _wellDevilContractRequestByGeralt });

        var contractRequestQueryObject = new ContractRequestQueryObject(mockQuery.Object);

        var result = await contractRequestQueryObject.ExecuteQuery(contractRequestFilter);

        mockQuery.Verify(mcq => mcq.Filter(It.IsAny<Expression<Func<ContractRequest, bool>>>()), Times.AtLeast(2));

        mockQuery.Verify(mcq => mcq.OrderBy(It.IsAny<Expression<Func<ContractRequest, DateTime>>>(), false),
            Times.Once);

        mockQuery.Verify(mcq => mcq.Page(1, 10), Times.Once);

        Assert.Equal(wellDevilContractRequestByGeraltDetailedDto, result.First());
    }
}