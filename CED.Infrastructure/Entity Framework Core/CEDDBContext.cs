using CED.Domain.ClassInformations;
using CED.Domain.Subjects;
using CED.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CED.Infrastructure.Persistence;

public class CEDDBContext : DbContext
{
    public DbSet<Subject> _subjects { get; set; } = null!;
    public DbSet<ClassInformation> _classInformation { get; set; } = null!;
    public DbSet<User> _users { get; set; } = null!;
    public CEDDBContext(DbContextOptions<CEDDBContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Subject>().ToTable("Subject");
        modelBuilder.Entity<User>().ToTable("User");

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