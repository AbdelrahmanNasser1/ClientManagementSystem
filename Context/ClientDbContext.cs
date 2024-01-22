namespace ClientManagementSystemMVC.Context;
public class ClientDbContext: DbContext
{
    public ClientDbContext(DbContextOptions<ClientDbContext> options) : base(options)
    {

    }
    public DbSet<ClientModel> Clients { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClientModel>()
            .Property(i => i.Id).ValueGeneratedOnAdd();

        base.OnModelCreating(modelBuilder);
    }
}
