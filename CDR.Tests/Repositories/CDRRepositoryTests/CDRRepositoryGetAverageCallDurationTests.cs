using CDR.Models.Filters;

namespace CDR.Tests.Repositories.CDRRepositoryTests;

public partial class CDRRepositoryTests
{
    [Test]
    public async Task GetAverageCallDurationWithoutFilters()
    {
        var filters = new CallDetailRecordFilters();
        var averageCallDuration = await _repository.GetAverageCallDurationAsync(filters);

        Assert.That(averageCallDuration, Is.EqualTo(44));
    }

    [Test]
    public async Task GetAverageCallDurationWithFilters()
    {
        var filters = new CallDetailRecordFilters
        {
            From = DateTime.Now.AddDays(-2),
            To = DateTime.Now.AddDays(-2),
        };
        var averageCallDuration = await _repository.GetAverageCallDurationAsync(filters);

        Assert.That(averageCallDuration, Is.EqualTo(55));
    }
}
