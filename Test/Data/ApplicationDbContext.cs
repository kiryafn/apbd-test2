using Microsoft.EntityFrameworkCore;
using Test.Models;

namespace Test.Data;

public class ApplicationDbContext : DbContext
{
    private readonly string? _connectionString;

    
    public ApplicationDbContext(IConfiguration configuration, DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        _connectionString = configuration.GetConnectionString("Default") ??
                            throw new ArgumentNullException(nameof(configuration), "Connection string is not set");
    }
    
    public DbSet<PublishingHouse> PublishingHouses { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Genre> Genres { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Book>()
            .HasMany(e => e.Genres)
            .WithMany(e => e.Books);

        modelBuilder.Entity<Book>()
            .HasMany(e => e.Authors)
            .WithMany(e => e.Books);
        
        modelBuilder.Entity<PublishingHouse>(entity =>
        {

            entity.Property(ph => ph.Name)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(ph => ph.Country)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(ph => ph.City)
                .HasMaxLength(50)
                .IsRequired();
        });
        
        modelBuilder.Entity<Book>(entity =>
        {

            entity.Property(b => b.Name)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(b => b.ReleaseDate)
                .IsRequired();
        }); 
        
        modelBuilder.Entity<Genre>(entity =>
        {

            entity.Property(g => g.Name)
                .HasMaxLength(50)
                .IsRequired();
            
        }); 
        
        modelBuilder.Entity<Author>(entity =>
        {
            entity.Property(a => a.FirstName)
                .HasMaxLength(50)
                .IsRequired();
            
            entity.Property(a => a.LastName)
                .HasMaxLength(50)
                .IsRequired();
        }); 

    }
}