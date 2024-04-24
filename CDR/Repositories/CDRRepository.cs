using AutoMapper;
using CDR.Models;
using CDR.Models.Dtos;
using CDR.Models.Filters;
using Microsoft.EntityFrameworkCore;

namespace CDR.Repositories;

public class CDRRepository : ICDRRepository
{
    private readonly CDRContext _context;
    private readonly IMapper _mapper;

    public CDRRepository(CDRContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<CallDetailRecordDto>> GetCallDetailRecordsAsync(CallDetailRecordFilters filters)
    {
        var query = GetCallDetailRecordsFromTo(filters);

        if (!string.IsNullOrEmpty(filters.CallerId))
        {
            query = query.Where(cdr => cdr.CallerId == filters.CallerId);
        }

        if (!string.IsNullOrEmpty(filters.Recipient))
        {
            query = query.Where(cdr => cdr.Recipient == filters.Recipient);
        }

        var cdrs = await query.Skip((filters.PageNumber - 1) * filters.PageSize)
                             .Take(filters.PageSize)
                             .ToListAsync();

        var cdrDtos = _mapper.Map<List<CallDetailRecordDto>>(cdrs);

        return cdrDtos;
    }

    public async Task<decimal> GetAverageCallCostAsync(CallDetailRecordFilters filters)
    {
        var query = GetCallDetailRecordsFromTo(filters);

        var averageCost = await query.AverageAsync(cdr => cdr.Cost);

        return averageCost;
    }

    public async Task<double> GetAverageCallDurationAsync(CallDetailRecordFilters filters)
    {
        var query = GetCallDetailRecordsFromTo(filters);

        var averageCost = await query.AverageAsync(cdr => cdr.Duration);

        return averageCost;
    }

    public async Task<List<CallDetailRecordDto>> GetLongestCallRecordsAsync(CallDetailRecordFilters filters)
    {
        var query = GetCallDetailRecordsFromTo(filters);

        var longestCallDuration = query.Max(cdr => cdr.Duration);
        var longestCalls = await query.Where(cdr => cdr.Duration == longestCallDuration)
                                    .Skip((filters.PageNumber - 1) * filters.PageSize)
                                    .Take(filters.PageSize)
                                    .ToListAsync(); 

        var longestCallsDtos = _mapper.Map<List<CallDetailRecordDto>>(longestCalls);
        return longestCallsDtos;
    }

    public async Task<double> GetAverageNumberOfCallsADayAsync(CallDetailRecordFilters filters)
    {
        var query = GetCallDetailRecordsFromTo(filters);

        var averageCallsPerDay = await query.GroupBy(cdr => cdr.CallDate.Date)
                                    .Select(grp => new
                                    {
                                        Date = grp.Key,
                                        CallCount = grp.Count()
                                    })
                                    .AverageAsync(grp => grp.CallCount);

        return averageCallsPerDay;
    }

    public async Task<Dictionary<string, decimal>> GetTotalCostByCurrencyAsync(CallDetailRecordFilters filters)
    {
        var query = GetCallDetailRecordsFromTo(filters);

        var totalCostsByCurrencies = await query.GroupBy(cdr => cdr.Currency)
                                        .Select(grp => new
                                        {
                                            Currency = grp.Key,
                                            TotalCost = grp.Sum(cdr => cdr.Cost)
                                        })
                                        .ToDictionaryAsync(grp => grp.Currency, grp => grp.TotalCost);

        return totalCostsByCurrencies;
    }

    private IQueryable<CallDetailRecordModel> GetCallDetailRecordsFromTo(CallDetailRecordFilters filters)
    {
        var query = _context.CallDetailRecords.AsQueryable();

        if (filters.From.HasValue)
        {
            query = query.Where(cdr => cdr.CallDate.Date >= filters.From.Value.Date);
        }

        if (filters.To.HasValue)
        {
            query = query.Where(cdr => cdr.CallDate.Date <= filters.To.Value.Date);
        }

        return query;
    }
}
