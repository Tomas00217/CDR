using CDR.Models;
using Microsoft.EntityFrameworkCore;

namespace CDR.Services;

public class CDRService
{
    private const string FieldSeparator = ",";
    private readonly CDRContext _context;

    public CDRService(CDRContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Processes a csv file containing cdr records into database
    /// </summary>
    /// <param name="fileStream">Stream from which to read content</param>
    /// <returns>Task</returns>
    public async Task ProcessCsvFileAsync(Stream fileStream)
    {
        using var reader = new StreamReader(fileStream);

        while (!reader.EndOfStream)
        {
            var line = await reader.ReadLineAsync();
            var values = !string.IsNullOrEmpty(line) ? line.Split(FieldSeparator) : [];

            if (values.Length == 8)
            {
                if (!DateTime.TryParse(values[2], out var callDate) ||
                    !TimeSpan.TryParse(values[3], out var endTime) ||
                    !int.TryParse(values[4], out var duration) ||
                    !decimal.TryParse(values[5], out var cost))
                {
                    continue;
                };

                var reference = values[6];

                if (await _context.CallDetailRecords.AnyAsync(cdr => cdr.Reference == reference))
                {
                    continue;
                }

                var cdr = new CallDetailRecordModel
                {
                    CallerId = values[0],
                    Recipient = values[1],
                    CallDate = callDate,
                    EndTime = endTime,
                    Duration = duration,
                    Cost = cost,
                    Reference = reference,
                    Currency = values[7],
                };

                await _context.CallDetailRecords.AddAsync(cdr);
            }
        }

        await _context.SaveChangesAsync();
    }
}
