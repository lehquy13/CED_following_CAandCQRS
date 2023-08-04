using CED.Domain.ClassInformations;
using CED.Domain.Notifications;
using CED.Domain.Review;
using CED.Domain.Subjects;
using CED.Domain.Subscribers;
using CED.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CED.Infrastructure.Entity_Framework_Core;

public class AppDbContext : DbContext
{
    public DbSet<Subject> Subjects { get; set; } = null!;
    public DbSet<ClassInformation> ClassInformations { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Tutor> Tutors { get; set; } = null!;

    public DbSet<TutorVerificationInfo> TutorVerificationInfos { get; set; } = null!;

    public DbSet<TutorReview> TutorReviews { get; set; } = null!;

    public DbSet<TutorMajor> TutorMajors { get; set; } = null!;

    public DbSet<RequestGettingClass> RequestGettingClasses { get; set; } = null!;
    public DbSet<Subscriber> Subscribers { get; set; } = null!;
    public DbSet<Notification> Notifications { get; set; } = null!;
    public DbSet<City> Cities { get; set; } = null!;
    public DbSet<District> Districts { get; set; } = null!;
    public DbSet<Ward> Wards { get; set; } = null!;
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Subject>().ToTable("Subject");
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
        modelBuilder.Entity<User>(re =>
        {
            re.ToTable("User");
            //re.HasOne<Ward>().WithMany().HasForeignKey(r => r.WardId).IsRequired();
            re.Property(x => x.FirstName).IsRequired().HasMaxLength(128);
            re.Property(x => x.LastName).IsRequired().HasMaxLength(128);
            re.Property(x => x.Email).IsRequired().HasMaxLength(128);
            re.Property(x => x.Password).IsRequired().HasMaxLength(128);
            re.Property(x => x.Role).IsRequired();
            re.Property(x => x.Gender).IsRequired();
        });
        modelBuilder.Entity<Tutor>(re =>
        {
            re.ToTable("Tutor");
            re.HasOne<User>().WithOne().HasForeignKey<Tutor>(x => x.Id).IsRequired();
            re.HasMany(x=>x.Subjects).WithMany().UsingEntity<TutorMajor>();
        });
        modelBuilder.Entity<TutorMajor>(re =>
        {
            re.ToTable("TutorMajor");
            re.HasKey(r => r.Id);
            //re.HasOne<Tutor>().WithMany().HasForeignKey(r => r.TutorId).IsRequired();
            //re.HasOne<Subject>().WithMany().HasForeignKey(r => r.SubjectId).IsRequired();
        });
        modelBuilder.Entity<TutorVerificationInfo>(re =>
        {
            re.ToTable("TutorVerificationInfos");
           // re.HasOne<Tutor>().WithMany().HasForeignKey(x => x.TutorId).IsRequired();
        });
        modelBuilder.Entity<ClassInformation>(re =>
        {
            re.ToTable("ClassInformation");
            re.HasKey(r => r.Id);
            re.Property(r => r.Title).IsRequired().HasMaxLength(128);
            re.Property(r => r.Description).IsRequired().IsUnicode();
            re.Property(r => r.Fee).IsRequired();
            //re.HasOne<Subject>().WithMany().HasForeignKey(x => x.SubjectId).IsRequired();
            //re.HasOne<User>().WithMany().HasForeignKey(x => x.LearnerId);
            //re.HasOne<Tutor>().WithMany().HasForeignKey(x => x.TutorId);

        });
        modelBuilder.Entity<RequestGettingClass>(re =>
        {
            re.ToTable("RequestGettingClass");
            //re.HasOne<Tutor>().WithMany().HasForeignKey(x => x.TutorId).IsRequired();
           // re.HasOne<ClassInformation>().WithMany().HasForeignKey(x => x.ClassInformationId);
        });
        modelBuilder.Entity<TutorReview>(re =>
        {
            re.ToTable("TutorReview");
            //re.HasOne<ClassInformation>().WithOne().HasForeignKey<TutorReview>(x => x.ClassInformationId).IsRequired();
        });
        modelBuilder.Entity<Subscriber>(re =>
        {
            re.ToTable("Subscriber");
            re.HasOne<Tutor>().WithMany().HasForeignKey(x => x.TutorId).IsRequired();
        });
        modelBuilder.Entity<Notification>(re =>
        {
            re.ToTable("Notification");
            re.Property(x => x.Message).IsRequired();
        });

    }
}

//using to support addmigration
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlServer(
            "Server=(localdb)\\MSSQLLocalDB; Database=ced_ddd; Trusted_Connection=True;MultipleActiveResultSets=true"
            );
        // optionsBuilder.UseSqlServer(
        //"workstation id=edusmart.mssql.somee.com;packet size=4096;user id=EduSmart_SQLLogin_1;pwd=av5rgw92zs;data source=edusmart.mssql.somee.com;persist security info=False;TrustServerCertificate=True;initial catalog=edusmart"
        //);

        return new AppDbContext(optionsBuilder.Options);
    }
}