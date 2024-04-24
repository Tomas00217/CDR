using Microsoft.EntityFrameworkCore;

namespace CDR.Models;

public class CDRContext : DbContext
{
    public CDRContext() : base()
    {
    }

    public CDRContext(DbContextOptions<CDRContext> options) : base(options)
    {

    }

    public virtual DbSet<CallDetailRecordModel> CallDetailRecords { get; set; }
}
