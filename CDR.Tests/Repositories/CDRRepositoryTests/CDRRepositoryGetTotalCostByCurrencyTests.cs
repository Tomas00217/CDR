using CDR.Models.Filters;

namespace CDR.Tests.Repositories.CDRRepositoryTests;

public partial class CDRRepositoryTests
{
    [Test]
    public async Task GetTotalCostsByCurrecyWithoutFilters()
    {
        var filters = new CallDetailRecordFilters();
        var totalCosts = await _repository.GetTotalCostByCurrencyAsync(filters);

        var expected = new Dictionary<string, decimal>
        {
            { "GBP", 52 },
            { "EUR", 12 }
        };

        Assert.That(totalCosts, Is.EqualTo(expected));
    }

    [Test]
    public async Task GetTotalCostsByCurrecyWithFilters()
    {
        var filters = new CallDetailRecordFilters
        {
            From = DateTime.Now.AddDays(-3),
            To = DateTime.Now.AddDays(-3),
        };
        var totalCosts = await _repository.GetTotalCostByCurrencyAsync(filters);

        var expected = new Dictionary<string, decimal>
        {
            { "GBP", 12 },
            { "EUR", 12 }
        };

        Assert.That(totalCosts, Is.EqualTo(expected));
    }
}
