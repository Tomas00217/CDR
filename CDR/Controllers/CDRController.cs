using CDR.Services;
using Microsoft.AspNetCore.Mvc;

namespace CDR.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CDRController : ControllerBase
{
    private readonly CDRService _cdrService;

    public CDRController(CDRService cdrService)
    {
        _cdrService = cdrService;
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
}
