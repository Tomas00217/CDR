using Microsoft.EntityFrameworkCore;

namespace CDR.Models;

public class CDRContext : DbContext
{
    public CDRContext(DbContextOptions<CDRContext> options) : base(options)
    {

    }

    public DbSet<CallDetailRecordModel> CallDetailRecords { get; set; }
}
