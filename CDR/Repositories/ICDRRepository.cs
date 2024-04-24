using CDR.Models.Dtos;
using CDR.Models.Filters;

namespace CDR.Repositories;

public interface ICDRRepository
{
    /// <summary>
    /// Returns a list of call detail records from the database based on provided filters
    /// </summary>
    /// <param name="filters">Filters for the query</param>
    /// <returns>List of call detail records</returns>
    Task<List<CallDetailRecordDto>> GetCallDetailRecordsAsync(CallDetailRecordFilters filters);

    /// <summary>
    /// Returns the avarage call cost 
    /// </summary>
    /// <param name="filters">Filters for the query</param>
    /// <returns>Avarage call cost</returns>
    Task<decimal> GetAverageCallCostAsync(CallDetailRecordFilters filters);

    /// <summary>
    /// Returns the avarage call duration 
    /// </summary>
    /// <param name="filters">Filters for the query</param>
    /// <returns>Avarage call duration</returns>
    Task<double> GetAverageCallDurationAsync(CallDetailRecordFilters filters);

    /// <summary>
    /// Returns a list of longest call records
    /// </summary>
    /// <param name="filters">Filters for the query</param>
    /// <returns>List of longest calls if there are multiple with the same duration. Usually returns a single call in a list</returns>
    Task<List<CallDetailRecordDto>> GetLongestCallRecordsAsync(CallDetailRecordFilters filters);

    /// <summary>
    /// Returns the avarage number of calls per day 
    /// </summary>
    /// <param name="filters">Filters for the query</param>
    /// <returns>Average number of calls</returns>
    Task<double> GetAverageNumberOfCallsADayAsync(CallDetailRecordFilters filters);

    /// <summary>
    /// Returns total cost of calls for each currency
    /// </summary>
    /// <param name="filters">Filters for the query</param>
    /// <returns>Dictionary with currency as key and total cost as value</returns>
    Task<Dictionary<string, decimal>> GetTotalCostByCurrencyAsync(CallDetailRecordFilters filters);
}
