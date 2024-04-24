using CDR.Models.Filters;

namespace CDR.Tests.Repositories.CDRRepositoryTests;

public partial class CDRRepositoryTests
{
    [Test]
    public async Task GetLongestCallRecordsWithoutFilters()
    {
        var filters = new CallDetailRecordFilters();
        var longestCalls = await _repository.GetLongestCallRecordsAsync(filters);

        Assert.That(longestCalls, Has.Count.EqualTo(1));
        Assert.That(longestCalls[0].Duration, Is.EqualTo(60));
    }

    [Test]
    public async Task GetLongestCallRecordsWithFilters()
    {
        var filters = new CallDetailRecordFilters
        {
            From = DateTime.Now.AddDays(-1),
        };
        var longestCalls = await _repository.GetLongestCallRecordsAsync(filters);

        Assert.That(longestCalls, Has.Count.EqualTo(1));
        Assert.That(longestCalls[0].Duration, Is.EqualTo(30));
    }
}
