using CDR.Models.Filters;

namespace CDR.Tests.Repositories.CDRRepositoryTests;

public partial class CDRRepositoryTests
{
    [Test]
    public async Task GetRecordsWithoutFilters()
    {
        var filters = new CallDetailRecordFilters();
        var records = await _repository.GetCallDetailRecordsAsync(filters);

        Assert.That(records, Has.Count.EqualTo(5));
    }

    [Test]
    public async Task GetRecordsWithPageSizeFilter()
    {
        var filters = new CallDetailRecordFilters
        {
            PageSize = 3,
        };
        var records = await _repository.GetCallDetailRecordsAsync(filters);

        Assert.That(records, Has.Count.EqualTo(3));
    }

    [Test]
    public async Task GetRecordsWithPageFilter()
    {
        var filters = new CallDetailRecordFilters
        {
            PageSize = 1,
        };
        var firstRecord = (await _repository.GetCallDetailRecordsAsync(filters)).FirstOrDefault();

        filters = new CallDetailRecordFilters
        {
            PageSize = 1,
            PageNumber = 2
        };
        var secondRecord = (await _repository.GetCallDetailRecordsAsync(filters)).FirstOrDefault();

        filters = new CallDetailRecordFilters
        {
            PageSize = 2,
        };
        var firstTwoRecords = await _repository.GetCallDetailRecordsAsync(filters);

        Assert.Multiple(() =>
        {
            Assert.That(firstTwoRecords[0].Reference, Is.EqualTo(firstRecord!.Reference));
            Assert.That(firstTwoRecords[1].Reference, Is.EqualTo(secondRecord!.Reference));
        });
    }

    [Test]
    public async Task GetRecordsWithFromFilter()
    {
        var filters = new CallDetailRecordFilters
        {
            From = DateTime.Now.AddDays(-2),
        };
        var records = await _repository.GetCallDetailRecordsAsync(filters);

        Assert.That(records, Has.Count.EqualTo(3));
    }

    [Test]
    public async Task GetRecordsWithToFilter()
    {
        var filters = new CallDetailRecordFilters
        {
            To = DateTime.Now.AddDays(-2),
        };
        var records = await _repository.GetCallDetailRecordsAsync(filters);

        Assert.That(records, Has.Count.EqualTo(4));
    }

    [Test]
    public async Task GetRecordsWithFromToFilter()
    {
        var filters = new CallDetailRecordFilters
        {
            From = DateTime.Now.AddDays(-2),
            To = DateTime.Now.AddDays(-2),
        };
        var records = await _repository.GetCallDetailRecordsAsync(filters);

        Assert.That(records, Has.Count.EqualTo(2));
    }

    [Test]
    public async Task GetRecordsWithCallerFilter()
    {
        var filters = new CallDetailRecordFilters
        {
            CallerId = "123456"
        };
        var records = await _repository.GetCallDetailRecordsAsync(filters);

        Assert.That(records, Has.Count.EqualTo(2));
    }

    [Test]
    public async Task GetRecordsWithRecipientFilter()
    {
        var filters = new CallDetailRecordFilters
        {
            Recipient = "987654"
        };
        var records = await _repository.GetCallDetailRecordsAsync(filters);

        Assert.That(records, Has.Count.EqualTo(1));
    }
}

