using Microsoft.EntityFrameworkCore;

public class DocumentDbContext : DbContext
{
    public DocumentDbContext(DbContextOptions<DocumentDbContext> options) : base(options) { }

    public DbSet<Document> Documents { get; set; }
    public DbSet<ParameterType> ParameterTypes { get; set; }
    public DbSet<Parameter> Parameters { get; set; }
    public DbSet<DocumentParameter> DocumentParameters { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Document>().ToTable("Document");
        modelBuilder.Entity<ParameterType>().ToTable("ParameterType");
        modelBuilder.Entity<Parameter>().ToTable("Parameter");
        modelBuilder.Entity<DocumentParameter>().ToTable("DocumentParameter");
        modelBuilder.Entity<Document>()
          .HasKey(d => d.DocumentID);

        modelBuilder.Entity<DocumentParameter>()
            .HasKey(dp => dp.ID);

        modelBuilder.Entity<DocumentParameter>()
            .HasOne(dp => dp.ParameterNavigation)
            .WithMany()
            .HasForeignKey(dp => dp.Parameter)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<DocumentParameter>()
            .HasOne(dp => dp.DocumentNavigation)
            .WithMany()
            .HasForeignKey(dp => dp.Document)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Parameter>()
            .HasKey(p => p.ParameterID);

        modelBuilder.Entity<Parameter>()
            .HasOne(p => p.TypeNavigation)
            .WithMany()
            .HasForeignKey(p => p.Type)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ParameterType>()
            .HasKey(pt => pt.ParameterTypeId);

        // Other configurations or constraints here.

        base.OnModelCreating(modelBuilder);
    

}

    // Define the relationships and other model configurations here
}
