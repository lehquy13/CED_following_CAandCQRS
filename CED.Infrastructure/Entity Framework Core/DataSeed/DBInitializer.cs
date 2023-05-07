using CED.Domain;
using CED.Domain.Users;
using CED.Infrastructure.Persistence;

namespace CED.Infrastructure.Entity_Framework_Core.DataSeed;

public static class DbInitializer
{
    public static void Initialize(CEDDBContext context)
    {
        context.Database.EnsureCreated();
        
        // Look for any subjects.
        if (!context.Subjects.Any())
        {
            var seeder = new DataSeeder();
            context.Subjects.AddRange(seeder.Subjects);
            context.Users.AddRange(seeder.Users);
            context.ClassInformations.AddRange(seeder.ClassInformations);

            context.SaveChanges();
        }
    }
    
    
}

