using CDR.Models.Filters;

namespace CDR.Tests.Repositories.CDRRepositoryTests;

public partial class CDRRepositoryTests
{
    [Test]
    public async Task GetAverageNumOfCallsADayWithoutFilters()
    {
        var filters = new CallDetailRecordFilters();
        var averageNumOfCalls = await _repository.GetAverageNumberOfCallsADayAsync(filters);

        Assert.That(double.Round(averageNumOfCalls, 2), Is.EqualTo(1.67));
    }

    [Test]
    public async Task GetAverageNumOfCallsADayWithFilters()
    {
        var filters = new CallDetailRecordFilters
        {
            From = DateTime.Now.AddDays(-2),
            To = DateTime.Now.AddDays(-2),
        };
        var averageNumOfCalls = await _repository.GetAverageNumberOfCallsADayAsync(filters);

        Assert.That(averageNumOfCalls, Is.EqualTo(2));
    }
}
