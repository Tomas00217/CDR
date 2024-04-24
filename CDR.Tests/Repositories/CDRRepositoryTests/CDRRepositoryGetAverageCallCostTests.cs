using CDR.Models.Filters;

namespace CDR.Tests.Repositories.CDRRepositoryTests;

public partial class CDRRepositoryTests
{
    [Test]
    public async Task GetAverageCallCostWithoutFilters()
    {
        var filters = new CallDetailRecordFilters();
        var averageCallCost = await _repository.GetAverageCallCostAsync(filters);

        Assert.That(averageCallCost, Is.EqualTo(12.8));
    }

    [Test]
    public async Task GetAverageCallCostWithFilters()
    {
        var filters = new CallDetailRecordFilters
        {
            From = DateTime.Now.AddDays(-2),
            To = DateTime.Now.AddDays(-2),
        };
        var averageCallCost = await _repository.GetAverageCallCostAsync(filters);

        Assert.That(averageCallCost, Is.EqualTo(15));
    }
}
