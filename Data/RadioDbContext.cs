using Microsoft.EntityFrameworkCore;

public class RadioDbContext : DbContext
{
    public DbSet<RadioDevice> Radios { get; set; }
public RadioDbContext(DbContextOptions<RadioDbContext> options):base(options)
{
    
}
}