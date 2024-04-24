namespace CDR.Models.Filters;

public class CallDetailRecordFilters
{
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public string? CallerId { get; set; }
    public string? Recipient { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
