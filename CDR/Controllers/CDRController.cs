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
}
