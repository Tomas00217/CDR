using CDR.Models.Dtos;
using CDR.Models.Filters;

namespace CDR.Repositories;

public interface ICDRRepository
{
    Task<List<CallDetailRecordDto>> GetCallDetailRecordsAsync(CallDetailRecordFilters filters);
    Task<decimal> GetAverageCallCostAsync(CallDetailRecordFilters filters);
    Task<double> GetAverageCallDurationAsync(CallDetailRecordFilters filters);
    Task<List<CallDetailRecordDto>> GetLongestCallRecordsAsync(CallDetailRecordFilters filters);
    Task<double> GetAverageNumberOfCallsADayAsync(CallDetailRecordFilters filters);
    Task<Dictionary<string, decimal>> GetTotalCostByCurrencyAsync(CallDetailRecordFilters filters);
}
