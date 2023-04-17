using CED.Domain.Subjects;
using CED.Domain.Users;
using CED.Infrastructure.Persistence;
using CED.Domain.Shared.ClassInformationConsts;

namespace CED.Infrastructure.Entity_Framework_Core;

public static class DbInitializer
{
    public static void Initialize(CEDDBContext context)
    {
        context.Database.EnsureCreated();

        // Look for any students.
        if (!context._subjects.Any())
        {
            var subjects = new Subject[]
        {
            new Subject{Name = "Physics", Description="Alexander"},
            new Subject{Name = "Chemistry",Description="Alonso"},
            new Subject{Name = "Biology", Description = "Anand"},
            new Subject{Name = "Geography", Description = "Barzdukas"},
            new Subject{Name = "Information Technology", Description = "Li"},
            new Subject{Name = "Fine Art", Description = "Justice"},
            new Subject{Name = "Literature", Description = "Norman"},
            new Subject{Name = "History", Description = "Olivetto"},

            new Subject{Name = "Engineering", Description="Alexander"},
            new Subject{Name = "Informatics",Description="Alonso"},
            new Subject{Name = "Technology", Description = "Anand"},
            new Subject{Name = "Politics", Description = "Barzdukas"},
            new Subject{Name = "Psychology", Description = "Li"},
            new Subject{Name = "Economics", Description = "Justice"},
            new Subject{Name = "Physical Education", Description = "Norman"},
            new Subject{Name = "English", Description = "Olivetto"},

            new Subject{Name = "C# programing", Description = "Barzdukas"},
            new Subject{Name = "Java programing", Description = "Li"},
            new Subject{Name = "Python programing", Description = "Justice"},
            new Subject{Name = "Web programing", Description = "Norman"},
            new Subject{Name = "HTML,CSS & Javascript", Description = "Olivetto"} ,

            new Subject{Name = "Guitar", Description = "Barzdukas"},
            new Subject{Name = "Piano", Description = "Li"},
            new Subject{Name = "Vietnamese for foreigner", Description = "Justice"}
        };
            foreach (Subject s in subjects)
            {
                context._subjects.Add(s);
            }
        }


        if (!context._users.Any())
        {
            var users = new User[]
        {
            //Admin
            new User{FirstName = "Matt", LastName="Le", Description="Premium Admin", PhoneNumber="0965686172", Email="hoangle.q3@gmail.com",Role=UserRole.Admin},
            new User{FirstName = "John", LastName="Doe", Description="Third admin",PhoneNumber="0123123120",Email="lehquy13@gmail.com",Role=UserRole.Admin},
            //tutor
            new User{FirstName = "Meredith", LastName="Smith", Description="Premium tutor",PhoneNumber="0123123120",Email="20520727@gm.uit.edu.com",Role=UserRole.Tutor, University="University of Information Technology - VNUHCM"},
            new User{FirstName = "Nino", LastName="Walker",Gender= Gender.Female, Description = "Olivetto",PhoneNumber="0123123126",Email="hoangle.q6@gmail.com",Role=UserRole.Tutor},
            new User{FirstName = "Gytis", LastName="Gustang", Description = "Barzdukas",PhoneNumber="0123123122",Email="hoangle.q4@gmail.com",Role=UserRole.Tutor, University="University of Information Technology - VNUHCM"},
            new User{FirstName = "Yan", LastName="Woo",Gender= Gender.Female, Description = "Li",PhoneNumber="0123123123",Email="hoangle.q5@gmail.com",Role=UserRole.Tutor, University = "University of Economics HCMC (UEH)"},
            new User{FirstName = "Peggy", LastName="Scar",Gender= Gender.Female, Description = "Justice",PhoneNumber="0123123124",Email="hoangle.q3123123@gmail.com",Role=UserRole.Tutor,University = "University of Economics HCMC (UEH)"},
            new User{FirstName = "Continel", LastName="Wild", Description="Second tutor",PhoneNumber="0123123130",Email="20520728@gm.uit.edu.com",Role=UserRole.Tutor, University="Ho Chi Minh City University of Technology (HCMUT)"},
            new User{FirstName = "Anne", LastName="Alter",Gender= Gender.Female, Description = "Third tutor",PhoneNumber="0123123131",Email="hoangle.q10@gmail.com",Role=UserRole.Tutor},
            new User{FirstName = "Hector", LastName="Wunder", Description = "Barzdukas",PhoneNumber="0123123132",Email="hoangle.q11@gmail.com",Role=UserRole.Tutor, University="Ho Chi Minh City University of Technology (HCMUT)"},
            new User{FirstName = "Rosez", LastName="Rouge",Gender= Gender.Female, Description = "Li",PhoneNumber="0123123133",Email="hoangle.q12@gmail.com",Role=UserRole.Tutor, University = "Vietnam National University HCM, University of Economics and Law"},
            new User{FirstName = "Sam", LastName="Will",Gender= Gender.Female, Description = "Justice",PhoneNumber="0123123124",Email="hoangle.q312312312@gmail.com",Role=UserRole.Tutor,University = "Vietnam National University HCM, University of Economics and Law"},

            //Standard user
            new User{FirstName = "Laura", LastName="Grey",Gender= Gender.Female, Description = "Norman", PhoneNumber="0123123125", Email="hoangle.q2@gmail.com"},
            new User{FirstName = "Arturo", LastName="Swift", Description = "Anand", PhoneNumber="0123123121", Email="hoangle.q0@gmail.com"},
            new User{FirstName = "John", LastName="Wish", Description = "Forever student", PhoneNumber="0123123127", Email="hoangle.q7@gmail.com"},
            new User{FirstName = "Kang", LastName="Theconquerer", Description = "Second student", PhoneNumber="0123123128", Email="hoangle.q8@gmail.com"},
            new User{FirstName = "Shang", LastName="Ki" , Description = "Third Student", PhoneNumber="0123123129", Email="hoangle.q9@gmail.com"},

            new User{FirstName = "Loan", LastName="Stalk",Gender= Gender.Female, Description = "Norman", PhoneNumber="0123123135", Email="hoangle.q13@gmail.com"},
            new User{FirstName = "Clint", LastName="Barton", Description = "Nothing to say", PhoneNumber="0123123136", Email="hoangle.q14@gmail.com"},
            new User{FirstName = "Kien", LastName="Jeanner", Description = "Forever 2nd student", PhoneNumber="0123123137", Email="hoangle.q15@gmail.com"},
            new User{FirstName = "Morgan", LastName="Stark",Gender= Gender.Female, Description = "5th student", PhoneNumber="0123123138", Email="hoangle.q16@gmail.com"},
            new User{FirstName = "Sam", LastName="Cruise" , Description = "6th Student", PhoneNumber="0123123139", Email="hoangle.q17@gmail.com"},
        };
            foreach (User s in users)
            {
                context._users.Add(s);
            }
        }


        context.SaveChanges();

    }
}
