using AutoMapper;
using CDR.Mapper;
using CDR.Models;
using CDR.Repositories;
using MockQueryable.Moq;
using Moq;

namespace CDR.Tests.Repositories.CDRRepositoryTests;

[TestFixture]
public partial class CDRRepositoryTests
{
    private CDRRepository _repository;

    [SetUp]
    public void Setup()
    {
        var mockDbContext = new Mock<CDRContext>();

        var callRecordsData = new List<CallDetailRecordModel>
        {
            new() { CallerId = "123456", Recipient = "654321", CallDate = DateTime.Now.AddDays(-1), EndTime = TimeSpan.FromMinutes(5), Duration = 30, Cost = 10.00m, Reference = "REF1", Currency = "GBP" },
            new() { CallerId = "654321", Recipient = "123456", CallDate = DateTime.Now.AddDays(-2), EndTime = TimeSpan.FromMinutes(10), Duration = 60, Cost = 15.00m, Reference = "REF2", Currency = "GBP" },
            new() { CallerId = "987654", Recipient = "123456", CallDate = DateTime.Now.AddDays(-2), EndTime = TimeSpan.FromMinutes(8), Duration = 50, Cost = 15.00m, Reference = "REF3", Currency = "GBP" },
            new() { CallerId = "123456", Recipient = "789123", CallDate = DateTime.Now.AddDays(-3), EndTime = TimeSpan.FromMinutes(8), Duration = 50, Cost = 12.00m, Reference = "REF4", Currency = "EUR" },
            new() { CallerId = "654123", Recipient = "987654", CallDate = DateTime.Now.AddDays(-3), EndTime = TimeSpan.FromMinutes(5), Duration = 30, Cost = 12.00m, Reference = "REF5", Currency = "GBP" },
        };

        var mockDbSet = callRecordsData.AsQueryable().BuildMockDbSet();

        mockDbContext.Setup(m => m.CallDetailRecords).Returns(mockDbSet.Object);

        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        var mapper = configuration.CreateMapper();

        _repository = new CDRRepository(mockDbContext.Object, mapper);
    }
}
