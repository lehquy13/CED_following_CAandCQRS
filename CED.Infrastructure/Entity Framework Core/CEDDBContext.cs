using CED.Domain.ClassInformations;
using CED.Domain.Subjects;
using CED.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CED.Infrastructure.Entity_Framework_Core;

public class CEDDBContext : DbContext
{
    public DbSet<Subject> Subjects { get; set; } = null!;
    public DbSet<ClassInformation> ClassInformations { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<City> Cities { get; set; } = null!;
    public DbSet<District> Districts { get; set; } = null!;
    public DbSet<Ward> Wards { get; set; } = null!;
    public CEDDBContext(DbContextOptions<CEDDBContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Subject>().ToTable("Subject");
        modelBuilder.Entity<User>().ToTable("User");
        modelBuilder.Entity<City>(re =>
        {
            re.ToTable("City");
            re.HasKey(r => r.Id);
        });
        modelBuilder.Entity<District>(re =>
        {
            re.ToTable("District");
            re.HasKey(r => r.Id);
            re.HasOne<City>().WithMany().HasForeignKey(r => r.CityId).IsRequired();

        });
        modelBuilder.Entity<Ward>(re =>
        {
            re.ToTable("Ward");
            re.HasKey(r => r.Id);
            re.HasOne<District>().WithMany().HasForeignKey(r => r.DistrictId).IsRequired();
        });

        modelBuilder.Entity<ClassInformation>(re =>
        {
            re.ToTable("ClassInformation");
            re.HasKey(r => r.Id);
            re.Property(r => r.Title).IsRequired().HasMaxLength(128);
            re.Property(r => r.Description).IsRequired().IsUnicode();
            re.Property(r => r.Fee).IsRequired();
            re.HasOne<Subject>().WithMany().HasForeignKey(x => x.SubjectId).IsRequired();

        });

        modelBuilder.Entity<UserClassInformation>(re =>
        {
            re.ToTable("UserClassInformation");
            re.HasKey(r => r.Id);
            re.HasOne<User>().WithMany().HasForeignKey(r => r.UserId).IsRequired();
            re.HasOne<ClassInformation>().WithMany().HasForeignKey(r => r.ClassInformationId).IsRequired();
        });
    }
}
//using to support addmigration
public class CEDDBContextFactory : IDesignTimeDbContextFactory<CEDDBContext>
{
    public CEDDBContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CEDDBContext>();
        optionsBuilder.UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Database=CED_DDD;Trusted_Connection=True;TrustServerCertificate=True");

        return new CEDDBContext(optionsBuilder.Options);
    }
}