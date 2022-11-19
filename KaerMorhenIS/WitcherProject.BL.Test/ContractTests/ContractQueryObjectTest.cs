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
            Deadline = _beastOfHonorton.Deadline
        };
        
        mockQuery.Setup(mcq => mcq.Filter(cont => !string.IsNullOrEmpty(filter.Name) && cont.Name == filter.Name)).Verifiable();
        mockQuery.Setup(mcq => mcq.Filter(contract => filter.Deadline != null && contract.Deadline == filter.Deadline)).Verifiable();
        mockQuery.Setup(mcq => mcq.ExecuteAsync().Result)
            .Returns(new List<Contract>{_beastOfHonorton});

        var contractQueryObject = new ContractQueryObject(mockQuery.Object);

        var result = await contractQueryObject.ExecuteQuery(filter);
        
        mockQuery.Verify(mcq => mcq.Filter(cont => !string.IsNullOrEmpty(filter.Name) && cont.Name == filter.Name), Times.Once);
        
        mockQuery.Verify(mcq => mcq.Filter(contract => filter.Deadline != null && contract.Deadline == filter.Deadline), Times.Once);
        
        Assert.Single(result);

    }
}