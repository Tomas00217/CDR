using CDR.Models.Filters;
using CDR.Repositories;
using CDR.Services;
using Microsoft.AspNetCore.Mvc;

namespace CDR.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CDRController : ControllerBase
{
    private readonly CDRService _cdrService;
    private readonly ICDRRepository _cdrRepository;

    public CDRController(CDRService cdrService, ICDRRepository cdrRepository)
    {
        _cdrService = cdrService;
        _cdrRepository = cdrRepository;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadCdrFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest(Constants.ErrorMessages.InvalidFile);
        }

        try
        {
            using (var stream = file.OpenReadStream())
            {
                await _cdrService.ProcessCsvFileAsync(stream);
            }

            return Ok(Constants.SuccessMessages.Upload);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("records")]
    public async Task<IActionResult> GetCallRecords([FromQuery] CallDetailRecordFilters filters)
    {
        var callRecords = await _cdrRepository.GetCallDetailRecordsAsync(filters);
        return Ok(callRecords);
    }

    [HttpGet("average/call-cost")]
    public async Task<IActionResult> GetAverageCallCost(DateTime? From, DateTime? To)
    {
        var filters = new CallDetailRecordFilters
        {
            From = From,
            To = To,
        };

        var callRecords = await _cdrRepository.GetAverageCallCostAsync(filters);
        return Ok(callRecords);
    }

    [HttpGet("average/call-duration")]
    public async Task<IActionResult> GetAverageCallDuration(DateTime? From, DateTime? To)
    {
        var filters = new CallDetailRecordFilters
        {
            From = From,
            To = To,
        };

        var callRecords = await _cdrRepository.GetAverageCallDurationAsync(filters);
        return Ok(callRecords);
    }

    [HttpGet("average/num-of-calls")]
    public async Task<IActionResult> GetAverageNumberOfCallsADayAsync(DateTime? From, DateTime? To)
    {
        var filters = new CallDetailRecordFilters
        {
            From = From,
            To = To,
        };

        var callRecords = await _cdrRepository.GetAverageNumberOfCallsADayAsync(filters);
        return Ok(callRecords);
    }

    [HttpGet("longest-calls")]
    public async Task<IActionResult> GetLongestCallRecords([FromQuery] CallDetailRecordFilters filters)
    {
        var callRecords = await _cdrRepository.GetLongestCallRecordsAsync(filters);
        return Ok(callRecords);
    }

    [HttpGet("total-costs")]
    public async Task<IActionResult> GetTotalCostByCurrency(DateTime? From, DateTime? To)
    {
        var filters = new CallDetailRecordFilters
        {
            From = From,
            To = To,
        };

        var callRecords = await _cdrRepository.GetTotalCostByCurrencyAsync(filters);
        return Ok(callRecords);
    }
}
