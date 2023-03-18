using CED.Domain.Subjects;
using CED.Infrastructure.Persistence;

namespace CED.Infrastructure.Entity_Framework_Core;

public static class DbInitializer
{
    public static void Initialize(CEDDBContext context)
    {
        context.Database.EnsureCreated();

        // Look for any students.
        if (context._subjects.Any())
        {
            return;   // DB has been seeded
        }

        var subjects = new Subject[]
        {
            new Subject{Name="Carson",Description="Alexander"},
            new Subject{Name="Meredith",Description="Alonso"},
            new Subject{Name = "Arturo", Description = "Anand"},
            new Subject{Name = "Gytis", Description = "Barzdukas"},
            new Subject{Name = "Yan", Description = "Li"},
            new Subject{Name = "Peggy", Description = "Justice"},
            new Subject{Name = "Laura", Description = "Norman"},
            new Subject{Name = "Nino", Description = "Olivetto"}
        };
        foreach (Subject s in subjects)
        {
            context._subjects.Add(s);
        }
        context.SaveChanges();
      
    }
}
