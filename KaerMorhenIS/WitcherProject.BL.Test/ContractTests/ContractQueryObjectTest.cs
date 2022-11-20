using System.Linq.Expressions;
using Moq;
using WitcherProject.BL.DTOs.Contract;
using WitcherProject.BL.QueryObjects;
using WitcherProject.DAL.Models;
using WitcherProject.Infrastructure.Query;
using Xunit;

namespace WitcherProject.BL.Test.ContractTests;

public class ContractQueryObjectTest : ContractServiceTest
{
    [Fact]
    public async Task ContractQueryObject_Filtered()
    {
        var mockQuery = new Mock<IQuery<Contract>>();

        var filter = new ContractFilterDto
        {
            Name = _beastOfHonorton.Name,
            Deadline = _beastOfHonorton.Deadline,
            SortAscending = false,
        };

        mockQuery.Setup(mcq => mcq.Filter(It.IsAny<Expression<Func<Contract, bool>>>())).Verifiable();
        mockQuery.Setup(mcq => mcq.OrderBy(It.IsAny<Expression<Func<Contract, int>>>(), false)).Verifiable();
        mockQuery.Setup(mcq => mcq.ExecuteAsync().Result)
            .Returns(new List<Contract> { _beastOfHonorton });

        var contractQueryObject = new ContractQueryObject(mockQuery.Object);

        var result = await contractQueryObject.ExecuteQuery(filter);

        mockQuery.Verify(mcq => mcq.Filter(It.IsAny<Expression<Func<Contract, bool>>>()), Times.AtLeast(2));

        mockQuery.Verify(mcq => mcq.OrderBy(It.IsAny<Expression<Func<Contract, int>>>(), false), Times.Once);

        Assert.Single(result);
    }
}