using CED.Domain.ClassInformations;
using CED.Domain.Shared.ClassInformationConsts;
using CED.Domain.Subjects;
using CED.Domain.Subscribers;
using CED.Domain.Users;

namespace CED.Domain;

/// <summary>
/// TODO: insert the request getting class record 
/// </summary>
public class DataSeeder
{
    public List<User> Users { get; private set; } = new List<User>();
    public List<Tutor> Tutors { get; private set; } = new List<Tutor>();
    public List<TutorMajor> TutorMajors { get; private set; } = new List<TutorMajor>();
    public List<Learner> Leanrner { get; private set; } = new List<Learner>();
    public List<Subject> Subjects { get; private set; } = new List<Subject>();
    public List<Subscriber> Subscribers { get; private set; } = new List<Subscriber>();
    public List<ClassInformation> ClassInformations { get; private set; } = new List<ClassInformation>();

    public DataSeeder()
    {
        SeedData();
    }

    private void SeedData()
    {
        //handle subject object
        var programming = new Subject { Id = Guid.NewGuid(), Name = "Programming", Description = "Basic subject" };
        var java = new Subject { Id = Guid.NewGuid(), Name = "Java programming", Description = "Li" };
        var informatics = new Subject { Id = Guid.NewGuid(), Name = "Informatics", Description = "Alonso" };
        var otherSubject = new Subject { Id = Guid.NewGuid(), Name = "Other", Description = "Other subject" };
        var korean = new Subject { Id = Guid.NewGuid(), Name = "Korean", Description = "25 Bucket List Adventures" };
        var spain = new Subject { Id = Guid.NewGuid(), Name = "Spanish", Description = "25 Bucket List Adventures" };
        var vietnameses = new Subject { Id = Guid.NewGuid(), Name = "Vietnamese for foreigner", Description = "Justice" };
        var german = new Subject { Id = Guid.NewGuid(), Name = "German", Description = "25 Bucket List Adventures" };
        var english = new Subject { Id = Guid.NewGuid(), Name = "English", Description = "25 Bucket List Adventures" };
        var guitar = new Subject { Id = Guid.NewGuid(), Name = "Guitar", Description = "25 Bucket List Adventures" };
        var chemistry = new Subject { Id = Guid.NewGuid(), Name = "Chemistry", Description = "Alonso" };
        var dance = new Subject { Id = Guid.NewGuid(), Name = "Dance", Description = "Private & Custom Tours" };
        var piano = new Subject { Id = Guid.NewGuid(), Name = "Piano", Description = "Private & Custom Tours" };
        var fit = new Subject { Id = Guid.NewGuid(), Name = "Fitness", Description = "Private & Custom Tours" };
        var paint = new Subject { Id = Guid.NewGuid(), Name = "Paint", Description = "25 Bucket List Adventures" };
        var math = new Subject { Id = Guid.NewGuid(), Name = "Mathematics", Description = "Private & Custom Tours" };
        var cook = new Subject { Id = Guid.NewGuid(), Name = "Cooking", Description = "Top Food Experiences" };

        var subjects = new List<Subject>()
        {
            spain,fit,paint,math,cook,
            new Subject { Id = Guid.NewGuid(), Name = "Physics", Description = "Alexander" },
            chemistry,
            new Subject { Id = Guid.NewGuid(), Name = "Biology", Description = "Anand" },
            new Subject { Id = Guid.NewGuid(), Name = "Geography", Description = "Barzdukas" },
            new Subject { Id = Guid.NewGuid(), Name = "Information Technology", Description = "Li" },
            new Subject { Id = Guid.NewGuid(), Name = "Fine Art", Description = "Justice" },
            new Subject { Id = Guid.NewGuid(), Name = "Literature", Description = "Norman" },
            new Subject { Id = Guid.NewGuid(), Name = "History", Description = "Olivetto" },

            new Subject { Id = Guid.NewGuid(), Name = "Engineering", Description = "Alexander" },
            informatics,
            new Subject { Id = Guid.NewGuid(), Name = "Technology", Description = "Anand" },
            new Subject { Id = Guid.NewGuid(), Name = "Politics", Description = "Barzdukas" },
            new Subject { Id = Guid.NewGuid(), Name = "Psychology", Description = "Li" },
            new Subject { Id = Guid.NewGuid(), Name = "Economics", Description = "Justice" },
            new Subject { Id = Guid.NewGuid(), Name = "Physical Education", Description = "Norman" },
            english,

            new Subject { Id = Guid.NewGuid(), Name = "C# programming", Description = "Barzdukas" },
            programming,
            java ,
            new Subject { Id = Guid.NewGuid(), Name = "Python programming", Description = "Justice" },
            new Subject { Id = Guid.NewGuid(), Name = "Web programming", Description = "Norman" },
            new Subject { Id = Guid.NewGuid(), Name = "HTML,CSS & Javascript", Description = "Olivetto" },

            guitar, dance,
            piano,
            otherSubject,
            german, korean, vietnameses
        };
        this.Subjects = subjects;

        var standardUser = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "User",
            LastName = "Grey",
            Gender = Gender.Female,
            Description = "25 Top Food Experiences Bucket List Adventures",
            PhoneNumber = "0123123125",
            Email = "hoangle.s2@gmail.com"
        };
        var standardUser1 = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "Laura",
            LastName = "Norman",
            Gender = Gender.Female,
            Description = "Top Food Experiences Bucket List Adventures",
            PhoneNumber = "0123123125",
            Email = "hoangle.s3@gmail.com"
        };
        var standardUser2 = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "Female",
            LastName = "Grey",
            Gender = Gender.Female,
            Description = "25 Bucket Lis Top Food Experiences Adventures",
            PhoneNumber = "0123123125",
            Email = "hoangle.s4@gmail.com"
        };
        var standardUser3 = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "Norman",
            LastName = "Grey",
            Gender = Gender.Female,
            Description = "25 Bucket List Adventures Top Food Experiences",
            PhoneNumber = "0123123125",
            Email = "hoangle.s5@gmail.com"
        };
        var standardUser4 = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "Com",
            LastName = "Guid",
            Gender = Gender.Female,
            Description = "25 Bucket Guid List Adventures Top Food Guid Experiences",
            PhoneNumber = "0123123125",
            Email = "hoangle.s6@gmail.com"
        };
        //handle tutor object


        var tutor = new Tutor //Tutor
        {
            Id = Guid.NewGuid(),
            FirstName = "Meredith",
            LastName = "Smith",
            Description = "Premium tutor",
            PhoneNumber = "0123123120",
            Email = "20520727@gm.uit.edu.com",
            Role = UserRole.Tutor,
            CreationTime = DateTime.Now - TimeSpan.FromDays(12),
            University = "University of Information Technology - VNUHCM",
            Rate = 5,
            AcademicLevel = AcademicLevel.Graduated,
            IsVerified = true,
        };
        var tutor1 = new Tutor
        {
            Id = Guid.NewGuid(),
            FirstName = "Yan",
            LastName = "Woo",
            Gender = Gender.Female,
            Description = "Multi-subject tutor with At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id est laborum et dolorum fuga.",
            PhoneNumber = "0123123123",
            Email = "hoangle.q5@gmail.com",
            Role = UserRole.Tutor,
            CreationTime = DateTime.Now - TimeSpan.FromDays(8),
            University = "University of Economics HCMC (UEH)"
        };
        var tutor2 = new Tutor
        {
            Id = Guid.NewGuid(),
            FirstName = "Nay",
            LastName = "Woo",
            Gender = Gender.Female,
            Description = "Multi-subject tutor, Yan's sister, At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id est laborum et dolorum fuga.",
            PhoneNumber = "0123123332",
            Email = "hoangle.qq5@gmail.com",
            Role = UserRole.Tutor,

            CreationTime = DateTime.Now - TimeSpan.FromDays(8),
            University = "University of Economics HCMC (UEH)"
        };


        var tutorUser3 = new Tutor
        {
            Id = Guid.NewGuid(),
            FirstName = "Grace",
            LastName = "Johnson",
            Description = "Experienced tutor specialized in mathematics At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id est laborum et dolorum fuga.",
            Address = "Viet Nam",
            PhoneNumber = "0987654321",
            Email = "gracejohnson@gmail.com"
    ,
            Role = UserRole.Tutor,
            CreationTime = DateTime.Now - TimeSpan.FromDays(1),
            University = "Harvard University",
            AcademicLevel = AcademicLevel.Lecturer
        };
        var tutorUser4 = new Tutor
        {
            Id = Guid.NewGuid(),
            FirstName = "Ethan",
            LastName = "Miller",
            Description = "Passionate about teaching languages and culture At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id est laborum et dolorum fuga.",
            Address = "Viet Nam",
            PhoneNumber = "0909090909",
            Email = "ethanmiller@gmail.com",

            Role = UserRole.Tutor,
            CreationTime = DateTime.Now - TimeSpan.FromDays(1),
            University = "Stanford University",
            AcademicLevel = AcademicLevel.Student
        };
        var tutorUser5 = new Tutor
        {
            Id = Guid.NewGuid(),
            FirstName = "Sophia",
            LastName = "Davis",
            Description = "Skilled in science subjects with a focus on practical experiments Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est, omnis dolor repellendus.",
            Address = "Viet Nam",
            PhoneNumber = "0765432109",
            Email = "sophiadavis@gmail.com",
            Role = UserRole.Tutor,
            CreationTime = DateTime.Now - TimeSpan.FromDays(1),
            University = "Massachusetts Institute of Technology (MIT)",
            AcademicLevel = AcademicLevel.Student
        };
        var tutorUser6 = new Tutor
        {
            Id = Guid.NewGuid(),
            FirstName = "Noah",
            LastName = "Wilson",
            Description = "Expert in programming languages and software development Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est, omnis dolor repellendus.",
            Address = "Viet Nam",
            PhoneNumber = "0843210987",
            Email = "noahwilson1@gmail.com",
            Role = UserRole.Tutor,
            CreationTime = DateTime.Now - TimeSpan.FromDays(1),
            University = "University of Oxford",
            AcademicLevel = AcademicLevel.Graduated
        };
        var tutorUser7 = new Tutor
        {
            Id = Guid.NewGuid(),
            FirstName = "Emma",
            LastName = "Martinez",
            Description = "Creative tutor offering personalized art lessons Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est, omnis dolor repellendus.",
            Address = "Viet Nam",
            PhoneNumber = "0912345678",
            Email = "emmamartinez@gmail.com",
            Role = UserRole.Tutor,
            CreationTime = DateTime.Now - TimeSpan.FromDays(1),
            University = "California Institute of Technology (Caltech)",
            AcademicLevel = AcademicLevel.Graduated
        };
        var tutorUser8 = new Tutor
        {
            Id = Guid.NewGuid(),
            FirstName = "Lucas",
            LastName = "Thompson",
            Description = "Patient and supportive tutor for students with learning difficulties Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est, omnis dolor repellendus. Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae. Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus asperiores repellat",
            Address = "Viet Nam",
            PhoneNumber = "0789087654",
            Email = "lucasthompson2@gmail.com",
            Role = UserRole.Tutor,
            CreationTime = DateTime.Now - TimeSpan.FromDays(1),
            University = "ETH Zurich - Swiss Federal Institute of Technology",
            AcademicLevel = AcademicLevel.Student
        };
        var tutorUser9 = new Tutor
        {
            Id = Guid.NewGuid(),
            FirstName = "Ava",
            LastName = "Jones",
            Description = "Specialized in exam preparation for college admissions and Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est, omnis dolor repellendus. Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae. Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus asperiores repellat",
            Address = "Viet Nam",
            PhoneNumber = "0854321098",
            Email = "avajones@gmail.com",
            Role = UserRole.Tutor,
            CreationTime = DateTime.Now - TimeSpan.FromDays(1),
            University = "University of Cambridge",
            AcademicLevel = AcademicLevel.Lecturer
        };
        var tutorUser10 = new Tutor
        {
            Id = Guid.NewGuid(),
            FirstName = "Liam",
            LastName = "Brown",
            Description = "Experienced in teaching music theory and instrument techniques n the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will",
            Address = "Viet Nam",
            PhoneNumber = "0698765432",
            Email = "liambrown@gmail.com",
            Role = UserRole.Tutor,
            CreationTime = DateTime.Now - TimeSpan.FromDays(3),
            University = "Princeton University",
            AcademicLevel = AcademicLevel.Optional
        };
        var tutorUser11 = new Tutor
        {
            Id = Guid.NewGuid(),
            FirstName = "Isabella",
            LastName = "Taylor",
            Description = "Passionate about history and providing engaging lessons n the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will",
            Address = "Viet Nam",
            PhoneNumber = "0823456789",
            Email = "isabellataylor2@gmail.com",
            Role = UserRole.Tutor,
            CreationTime = DateTime.Now - TimeSpan.FromDays(3),
            University = "California Institute of Technology (Caltech)",
            AcademicLevel = AcademicLevel.Graduated
        };
        var tutorUser12 = new Tutor
        {
            Id = Guid.NewGuid(),
            FirstName = "Oliver",
            LastName = "Anderson",
            Description = "Expert tutor for business and entrepreneurship n the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will",
            Address = "Viet Nam",
            PhoneNumber = "0876543210",
            Email = "oliveranderson@gmail.com",
            Role = UserRole.Tutor,
            CreationTime = DateTime.Now - TimeSpan.FromDays(32),
            University = "ETH Zurich - Swiss Federal Institute of Technology",
            AcademicLevel = AcademicLevel.Student
        };
        var tutorUser13 = new Tutor
        {
            Id = Guid.NewGuid(),
            FirstName = "Mia",
            LastName = "Lee",
            Description = "Experienced tutor specialized in mathematics n the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will",
            Address = "Viet Nam",
            PhoneNumber = "0956789012",
            Email = "miale@gmail.com",
            Role = UserRole.Tutor,
            CreationTime = DateTime.Now - TimeSpan.FromDays(32),
            University = "University of Cambridge",
            AcademicLevel = AcademicLevel.Lecturer
        };
        var tutorUser14 = new Tutor
        {
            Id = Guid.NewGuid(),
            FirstName = "Elijah",
            LastName = "Clark",
            Description = "Passionate about teaching languages and culture n the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will",
            Address = "Viet Nam",
            PhoneNumber = "0832109876",
            Email = "elijahclark@gmail.com",
            Role = UserRole.Tutor,
            University = "Princeton University",
            AcademicLevel = AcademicLevel.Optional
        };
        var tutorUser15 = new Tutor
        {
            Id = Guid.NewGuid(),
            FirstName = "William",
            LastName = "Harrison",
            Description = "Experienced tutor providing comprehensive math lessons n the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will",
            Address = "Viet Nam",
            PhoneNumber = "0976543210",
            Email = "williamharrison@gmail.com",
            Role = UserRole.Tutor,
            CreationTime = DateTime.Now - TimeSpan.FromDays(8),
            University = "California Institute of Technology (Caltech)",
            AcademicLevel = AcademicLevel.Graduated
        };
        var tutorUser16 = new Tutor
        {
            Id = Guid.NewGuid(),
            FirstName = "Charlotte",
            LastName = "Harris",
            Description = "Skilled in science subjects with a focus on practical experiments n the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will",
            Address = "Viet Nam",
            PhoneNumber = "0890123456",
            Email = "charlotteharris@gmail.com",
            Role = UserRole.Tutor,
            CreationTime = DateTime.Now - TimeSpan.FromDays(9),
            University = "ETH Zurich - Swiss Federal Institute of Technology",
            AcademicLevel = AcademicLevel.Student
        };
        var tutorUser17 = new Tutor
        {
            Id = Guid.NewGuid(),
            FirstName = "Luna",
            LastName = "Clark",
            Description = "Patient tutor specialized in language acquisition n the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will",
            Address = "Viet Nam",
            PhoneNumber = "0901234567",
            Email = "lunaclark@gmail.com",
            Role = UserRole.Tutor,
            University = "University of Cambridge",
            AcademicLevel = AcademicLevel.Lecturer
        };
        var tutorUser18 = new Tutor
        {
            Id = Guid.NewGuid(),
            FirstName = "Henry",
            LastName = "Miller",
            Description = "Expert in programming languages and software development n the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will",
            Address = "Viet Nam",
            PhoneNumber = "0865432109",
            Email = "henrymiller@gmail.com",
            Role = UserRole.Tutor,
            CreationTime = DateTime.Now - TimeSpan.FromDays(1),
            University = "Princeton University",
            AcademicLevel = AcademicLevel.Optional
        };
        var tutorUser19 = new Tutor
        {
            Id = Guid.NewGuid(),
            FirstName = "Amelia",
            LastName = "Garcia",
            Description = "Creative tutor offering personalized art lessons n the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will",
            Address = "Viet Nam",
            PhoneNumber = "0912345678",
            Email = "ameliagarcia@gmail.com",
            Role = UserRole.Tutor,
            CreationTime = DateTime.Now - TimeSpan.FromDays(1),
            University = "California Institute of Technology (Caltech)",
            AcademicLevel = AcademicLevel.Graduated
        };
        var tutorUser20 = new Tutor
        {
            Id = Guid.NewGuid(),
            FirstName = "James",
            LastName = "Adams",
            Description = "Skilled tutor offering customized physics lessons n the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will",
            Address = "Viet Nam",
            PhoneNumber = "0912345678",
            Email = "jamesadams@gmail.com",
            Role = UserRole.Tutor,
            University = "ETH Zurich - Swiss Federal Institute of Technology",
            AcademicLevel = AcademicLevel.Student
        };
        var tutorUser21 = new Tutor
        {
            Id = Guid.NewGuid(),
            FirstName = "Harper",
            LastName = "Turner",
            Description = "Expert in computer science, specializing in algorithms n the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will",
            Address = "Viet Nam",
            PhoneNumber = "0987654321",
            Email = "harperturner@gmail.com",
            Role = UserRole.Tutor,
            CreationTime = DateTime.Now - TimeSpan.FromDays(1),
            University = "University of Cambridge",
            AcademicLevel = AcademicLevel.Lecturer
        };
        var tutorUser22 = new Tutor
        {
            Id = Guid.NewGuid(),
            FirstName = "Victoria",
            LastName = "Smith",
            Description = "Passionate about teaching literature and critical analysis n the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will",
            Address = "Viet Nam",
            PhoneNumber = "0943210987",
            Email = "victoriasmith@gmail.com",
            Role = UserRole.Tutor,
            CreationTime = DateTime.Now,
            University = "Princeton University",
            AcademicLevel = AcademicLevel.Optional
        };
        var tutorUser23 = new Tutor
        {
            Id = Guid.NewGuid(),
            FirstName = "Sebastian",
            LastName = "Carter",
            Description = "Creative tutor providing engaging art history lessons n the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will",
            Address = "Viet Nam",
            PhoneNumber = "0967890123",
            Email = "sebastiancarter@gmail.com",
            Role = UserRole.Tutor,
            CreationTime = DateTime.Now,
            University = "California Institute of Technology (Caltech)",
            AcademicLevel = AcademicLevel.Graduated
        };
        var tutorUser24 = new Tutor
        {
            Id = Guid.NewGuid(),
            FirstName = "Avery",
            LastName = "Gonzalez",
            Description = "Patient tutor for students with learning disabilities n the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will",
            Address = "Viet Nam",
            PhoneNumber = "0932109876",
            Email = "averygonzalez@gmail.com",
            Role = UserRole.Tutor,
            CreationTime = DateTime.Now,
            University = "ETH Zurich - Swiss Federal Institute of Technology",
            AcademicLevel = AcademicLevel.Student
        };
        var tutorUser25 = new Tutor
        {
            Id = Guid.NewGuid(),
            FirstName = "Abigail",
            LastName = "Young",
            Description = "Passionate tutor offering engaging biology lessons n the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will",
            Address = "Viet Nam",
            PhoneNumber = "0943216789",
            Email = "abigailyoung@gmail.com",
            Role = UserRole.Tutor,
            CreationTime = DateTime.Now,
            University = "University of Cambridge",
            AcademicLevel = AcademicLevel.Lecturer
        };
        var tutorUser26 = new Tutor
        {
            Id = Guid.NewGuid(),
            FirstName = "Scarlett",
            LastName = "Moore",
            Description = "Specialized tutor for SAT/ACT verbal and writing sections n the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will",
            Address = "Viet Nam",
            PhoneNumber = "0909876543",
            Email = "scarlettmoore@gmail.com",
            Role = UserRole.Tutor,
            CreationTime = DateTime.Now,
            University = "Princeton University",
            AcademicLevel = AcademicLevel.Optional
        };
        var tutorUser27 = new Tutor
        {
            Id = Guid.NewGuid(),
            FirstName = "Jack",
            LastName = "Rivera",
            Description = "Experienced tutor providing practical chemistry experiments n the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will",
            Address = "Viet Nam",
            PhoneNumber = "0976501234",
            Email = "jackrivera@gmail.com",
            Role = UserRole.Tutor,
            CreationTime = DateTime.Now,
            University = "Princeton University",
            AcademicLevel = AcademicLevel.Optional
        };

        Tutors = new List<Tutor>()
        {
            tutor,
            tutor1,
            tutor2,
            tutorUser3,
            tutorUser4,
            tutorUser5,
            tutorUser6,
            tutorUser7,
            tutorUser8,
            tutorUser9,
            tutorUser10,
            tutorUser11,
            tutorUser12,
            tutorUser13,
            tutorUser14,
            tutorUser15,
            tutorUser16,
            tutorUser17,
            tutorUser18,
            tutorUser19,
            tutorUser20,
            tutorUser21,
            tutorUser22,
            tutorUser23,
            tutorUser24,
            tutorUser25,
            tutorUser26,
            tutorUser27,
            //----------------------------------
        };
        //handle user object
        Users = new List<User>()
        {
            //Admin
            new User
            {
                FirstName = "Matt", LastName = "Le", Description = "Premium Admin", Email = "hoangle.q3@gmail.com",
                Address = "Viet Nam",
                PhoneNumber = "0965686172", Role = UserRole.Admin
            },
            new User
            {
                FirstName = "John", LastName = "Doe", Description = "Third admin", Email = "lehquy13@gmail.com",
                Address = "Viet Nam",
                PhoneNumber = "0123123120", Role = UserRole.Admin
            },

           
            //Standard user

            new User
            {
                FirstName = "Gytis", LastName = "Gustang", Description = "Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est, omnis dolor repellendus. Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae. Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus asperiores repellat", PhoneNumber = "0123123122",
                Address = "Viet Nam",
                Email = "hoangle.q4@gmail.com"
            },
            new User
            {
                FirstName = "Peggy", LastName = "Scar", Gender = Gender.Female, Description = "Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est, omnis dolor repellendus. Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae. Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus asperiores repellat",
                Address = "Viet Nam",
                PhoneNumber = "0123123124", Email = "hoangle.q3123123@gmail.com",
            },
            new User
            {
                FirstName = "Continel", LastName = "Wild", Description = "Second tutor and i'm Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est, omnis dolor repellendus. Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae. Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus asperiores repellat", PhoneNumber = "0123123130",
                Address = "Viet Nam",
                Email = "20520728@gm.uit.edu.com",
            },
            new User
            {
                FirstName = "Hector", LastName = "Wunder", Description = "Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est, omnis dolor repellendus. Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae. Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus asperiores repellat", PhoneNumber = "0123123132",
                Address = "Viet Nam",
                Email = "hoangle.q11@gmail.com",
            },
            new User
            {
                FirstName = "Rosez", LastName = "Rouge", Gender = Gender.Female, Description = "Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est, omnis dolor repellendus. Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae. Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus asperiores repellat",
                Address = "Viet Nam",
                PhoneNumber = "0123123133", Email = "hoangle.q12@gmail.com",
            },
            new User
            {
                FirstName = "Sam", LastName = "Will", Gender = Gender.Female, Description = "Sam Will love Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est, omnis dolor repellendus. Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae. Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus asperiores repellat",
                Address = "Viet Nam",
                PhoneNumber = "0123123124", Email = "hoangle.q312312312@gmail.com"
            },
            standardUser,
            standardUser1,
            standardUser2,
            standardUser3,
            standardUser4,
            new User
            {
                FirstName = "Arturo", LastName = "Swift", Description = "Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est, omnis dolor repellendus. Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae. Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus asperiores repellat", PhoneNumber = "0123123121",
                Address = "Viet Nam",
                Email = "hoangle.q0@gmail.com"
            },
            new User
            {
                FirstName = "John", LastName = "Wish", Description = "Forever student but Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est, omnis dolor repellendus. Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae. Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus asperiores repellat", PhoneNumber = "0123123127",
                Address = "Viet Nam",
                Email = "hoangle.q7@gmail.com"
            },
            new User
            {
                FirstName = "Kang", LastName = "Theconquerer", Description = "Second student with Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est, omnis dolor repellendus. Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae. Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus asperiores repellat", Address = "Viet Nam",
                PhoneNumber = "0123123128", Email = "hoangle.q8@gmail.com"
            },
            new User
            {
                FirstName = "Shang", LastName = "Ki", Description = "Third Student or Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est, omnis dolor repellendus. Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae. Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus asperiores repellat", PhoneNumber = "0123123129",
                Address = "Viet Nam",
                Email = "hoangle.q9@gmail.com"
            },
            new User
            {
                FirstName = "Loan", LastName = "Stalk", Gender = Gender.Female, Description = "Norman",
                Address = "Viet Nam",
                PhoneNumber = "0123123135", Email = "hoangle.q13@gmail.com"
            },
            new User
            {
                FirstName = "Clint", LastName = "Barton", Description = "Nothing to say", Address = "Viet Nam",
                PhoneNumber = "0123123136", Email = "hoangle.q14@gmail.com"
            },
            new User
            {
                FirstName = "Kien", LastName = "Jeanner", Description = "Forever 2nd student when Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est, omnis dolor repellendus. Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae. Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus asperiores repellat", Address = "Viet Nam",
                PhoneNumber = "0123123137", Email = "hoangle.q15@gmail.com"
            },
            new User
            {
                FirstName = "Morgan", LastName = "Stark", Gender = Gender.Female, Description = "5th student then Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est, omnis dolor repellendus. Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae. Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus asperiores repellat",
                Address = "Viet Nam",
                PhoneNumber = "0123123138", Email = "hoangle.q16@gmail.com"
            },
            new User
            {
                FirstName = "Sam", LastName = "Cruise", Description = "6th Student if Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est, omnis dolor repellendus. Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae. Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus asperiores repellat", PhoneNumber = "0123123139",
                Address = "Viet Nam",
                Email = "hoangle.q17@gmail.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(35)
            },
            new User
            {
                FirstName = "Olivia", LastName = "Martinez", Description = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like).", PhoneNumber = "0654321098",
                Address = "Tanzania", Email = "oliviamartinez@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(35)
            },
            new User
            {
                FirstName = "Sophia", LastName = "Davis", Description = "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.", PhoneNumber = "0912345678",
                Address = "Madagascar", Email = "sophiadavis@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(35)
            },
            new User
            {
                FirstName = "Liam", LastName = "Taylor", Description = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like).", PhoneNumber = "0789087654",
                Address = "Mauritius", Email = "liamtaylor@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(35)
            },
            new User
            {
                FirstName = "Jackson", LastName = "Johnson", Description = "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.", PhoneNumber = "0854321098",
                Address = "Comoros", Email = "jacksonjohnson@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(35)
            },
            new User
            {
                FirstName = "Ava", LastName = "Brown", Description = "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.", PhoneNumber = "0912345678",
                Address = "Trinidad and Tobago", Email = "avabrown2@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(35)
            },
            new User
            {
                FirstName = "Liam", LastName = "Davis", Description = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like).", PhoneNumber = "0654321098",
                Address = "Belize", Email = "liamdavis3@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(35)
            },
            new User
            {
                FirstName = "Isabella", LastName = "Taylor", Description = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like).", PhoneNumber = "0843210987",
                Address = "Jamaica", Email = "isabellataylo5@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(35)
            },
            new User
            {
                FirstName = "Noah", LastName = "Wilson", Description = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like).", PhoneNumber = "0789087654",
                Address = "Bahamas", Email = "noahwilson2@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(35)
            },

            new User
            {
                FirstName = "Jackson", LastName = "Martinez", Description = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like).", PhoneNumber = "0843210987",
                Address = "Somalia", Email = "jacksonmartinez2@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(35)
            },
            new User
            {
                FirstName = "Olivia", LastName = "Johnson", Description = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like).", PhoneNumber = "0765432109",
                Address = "Namibia", Email = "oliviajohnson1@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(35)
            },
            new User
            {
                FirstName = "Aiden", LastName = "Brown", Description = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like).", PhoneNumber = "0854321098",
                Address = "Cameroon", Email = "aidenbrown@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(35)
            },

            new User
            {
                FirstName = "Emma", LastName = "Wilson", Description = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like).", PhoneNumber = "0698765432",
                Address = "Mali", Email = "emmawilson@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(31)
            },
            
            //30 records
            new User
            {
                FirstName = "Lucas", LastName = "Thomas", Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", PhoneNumber = "0789087654",
                Address = "Senegal", Email = "lucasthomas@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(31)
            },

            new User
            {
                FirstName = "Ava", LastName = "Martinez", Description = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like).", PhoneNumber = "0843210987",
                Address = "Somalia", Email = "avamartinez@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(31)
            },

            new User
            {
                FirstName = "Liam", LastName = "Johnson", Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", PhoneNumber = "0765432109",
                Address = "Namibia", Email = "liamjohnson@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(32)
            },
            new User
            {
                FirstName = "Isabella", LastName = "Brown", Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", PhoneNumber = "0854321098",
                Address = "Ivory Coast", Email = "isabellabrown@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(14)
            },
            new User
            {
                FirstName = "Noah", LastName = "Davis", Description = "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.", PhoneNumber = "0912345678",
                Address = "Burkina Faso", Email = "noahdavis1@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(14)
            },
            new User
            {
                FirstName = "Sophia", LastName = "Taylor", Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", PhoneNumber = "0698765432",
                Address = "Malawi", Email = "sophiataylor@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(14)
            },
            new User
            {
                FirstName = "Jackson", LastName = "Wilson", Description = "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.", PhoneNumber = "0843210987",
                Address = "Sierra Leone", Email = "jacksonwilson1@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(14)
            },
            new User
            {
                FirstName = "Olivia", LastName = "Thomas", Description = "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.", PhoneNumber = "0854321098",
                Address = "Chad", Email = "oliviathomas@example.com"
            },
            new User
            {
                FirstName = "Aiden", LastName = "Martinez", Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", PhoneNumber = "0798765432",
                Address = "Togo", Email = "aidenmartinez@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(14)
            },
            new User
            {
                FirstName = "Emma", LastName = "Johnson", Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", PhoneNumber = "0912345678",
                Address = "Benin", Email = "emmajohn1son@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(14)
            },
            //40
            new User
            {
                FirstName = "Lucas", LastName = "Brown", Description = "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.", PhoneNumber = "0654321098",
                Address = "Lesotho", Email = "lucasbrown@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(14)
            },
            new User
            {
                FirstName = "Ava", LastName = "Davis", Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", PhoneNumber = "0843210987",
                Address = "Gambia", Email = "avadavis@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(14)
            },
            new User
            {
                FirstName = "Liam", LastName = "Taylor", Description = "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.", PhoneNumber = "0789087654",
                Address = "Mauritius", Email = "liamtaylor@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(10)
            },
            new User
            {
                FirstName = "Isabella", LastName = "Wilson", Description = "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.", PhoneNumber = "0854321098",
                Address = "Swaziland", Email = "isabellawilson1@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(10)
            },
            new User
            {
                FirstName = "Noah", LastName = "Thomas", Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", PhoneNumber = "0698765432",
                Address = "Guinea-Bissau", Email = "noahthomas@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(10)
            },
            new User
            {
                FirstName = "Sophia", LastName = "Martinez", Description = "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.", PhoneNumber = "0843210987",
                Address = "Congo", Email = "sophiamartinez@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(10)
            },
            new User
            {
                FirstName = "Jackson", LastName = "Johnson", Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", PhoneNumber = "0854321098",
                Address = "Comoros", Email = "jacksonjohnson@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(8)
            },
            new User
            {
                FirstName = "Olivia", LastName = "Brown", Description = "It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of \"de Finibus Bonorum et Malorum\" (The Extremes of Good and Evil) by Cicero, written in 45 BC.", PhoneNumber = "0912345678",
                Address = "Equatorial Guinea", Email = "oliviabrown@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(8)
            },
            new User
            {
                FirstName = "Aiden", LastName = "Davis", Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", PhoneNumber = "0654321098",
                Address = "Seychelles", Email = "aidendavis@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(8)
            },
            new User
            {
                FirstName = "Emma", LastName = "Taylor", Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", PhoneNumber = "0843210987",
                Address = "Mauritania", Email = "emmataylor@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(8)
            },
            //50
            new User
            {
                FirstName = "Lucas", LastName = "Wilson", Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", PhoneNumber = "0789087654",
                Address = "Djibouti", Email = "lucaswilson@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(8)
            },
            new User
            {
                FirstName = "Ava", LastName = "Thomas", Description = "It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of \"de Finibus Bonorum et Malorum\" (The Extremes of Good and Evil) by Cicero, written in 45 BC.", PhoneNumber = "0854321098",
                Address = "Eswatini", Email = "avathomas1@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(8)
            },
            new User
            {
                FirstName = "Liam", LastName = "Martinez", Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", PhoneNumber = "0654321098",
                Address = "Cabo Verde", Email = "liammartinez1@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(8)
            },
            new User
            {
                FirstName = "Isabella", LastName = "Johnson", Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", PhoneNumber = "0843210987",
                Address = "São Tomé and Príncipe", Email = "isabellajohnson@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(7)
            },
            new User
            {
                FirstName = "Noah", LastName = "Brown", Description = "It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of \"de Finibus Bonorum et Malorum\" (The Extremes of Good and Evil) by Cicero, written in 45 BC.", PhoneNumber = "0854321098",
                Address = "Grenada", Email = "noahbrown1@example.com"
            },
            new User
            {
                FirstName = "Sophia", LastName = "Davis", Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", PhoneNumber = "0912345678",
                Address = "Antigua and Barbuda", Email = "sophiadavis@example.com"
            },
            new User
            {
                FirstName = "Jackson", LastName = "Taylor", Description = "It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of \"de Finibus Bonorum et Malorum\" (The Extremes of Good and Evil) by Cicero, written in 45 BC.", PhoneNumber = "0698765432",
                Address = "Dominica", Email = "jacksontaylor1@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(7)
            },
            new User
            {
                FirstName = "Olivia", LastName = "Wilson", Description = "The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from \"de Finibus Bonorum et Malorum\" by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.", PhoneNumber = "0854321098",
                Address = "Saint Kitts and Nevis", Email = "oliviawilson@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(7)
            },
            new User
            {
                FirstName = "Aiden", LastName = "Thomas", Description = "It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of \"de Finibus Bonorum et Malorum\" (The Extremes of Good and Evil) by Cicero, written in 45 BC.", PhoneNumber = "0789087654",
                Address = "Saint Lucia", Email = "aidenthomas1@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(7)
            },
            new User
            {
                FirstName = "Emma", LastName = "Martinez", Description = "The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from \"de Finibus Bonorum et Malorum\" by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.", PhoneNumber = "0843210987",
                Address = "Saint Vincent and the Grenadines", Email = "emmamartinez1@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(7)
            },
            new User
            {
                FirstName = "Lucas", LastName = "Johnson", Description = "The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from \"de Finibus Bonorum et Malorum\" by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.", PhoneNumber = "0854321098",
                Address = "Barbados", Email = "lucasjohnson1@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(7)
            },
            new User
            {
                FirstName = "Ava", LastName = "Brown", Description = "It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of \"de Finibus Bonorum et Malorum\" (The Extremes of Good and Evil) by Cicero, written in 45 BC.", PhoneNumber = "0912345678",
                Address = "Trinidad and Tobago", Email = "avabrown@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(7)
            },
            new User
            {
                FirstName = "Liam", LastName = "Davis", Description = "The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from \"de Finibus Bonorum et Malorum\" by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.", PhoneNumber = "0654321098",
                Address = "Belize", Email = "liamdavis1@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(7)
            },
            new User
            {
                FirstName = "Isabella", LastName = "Taylor", Description = "It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of \"de Finibus Bonorum et Malorum\" (The Extremes of Good and Evil) by Cicero, written in 45 BC.", PhoneNumber = "0843210987",
                Address = "Jamaica", Email = "isabellataylor@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(7)
            },
            new User
            {
                FirstName = "Noah", LastName = "Wilson", Description = "The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from \"de Finibus Bonorum et Malorum\" by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.", PhoneNumber = "0789087654",
                Address = "Bahamas", Email = "noahwilson@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(7)
            },
            new User
            {
                FirstName = "Sophia", LastName = "Thomas", Description = "It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of \"de Finibus Bonorum et Malorum\" (The Extremes of Good and Evil) by Cicero, written in 45 BC.", PhoneNumber = "0854321098",
                Address = "Chad", Email = "sophiathomas1@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(7)
            },
            new User
            {
                FirstName = "Jackson", LastName = "Martinez", Description = "The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from \"de Finibus Bonorum et Malorum\" by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.", PhoneNumber = "0843210987",
                Address = "Somalia", Email = "jacksonmartinez@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(7)
            },
            new User
            {
                FirstName = "Olivia", LastName = "Johnson", Description = "The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from \"de Finibus Bonorum et Malorum\" by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.", PhoneNumber = "0765432109",
                Address = "Namibia", Email = "oliviajohnson2@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(7)
            },
            new User
            {
                FirstName = "Aiden", LastName = "Brown", Description = "It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of \"de Finibus Bonorum et Malorum\" (The Extremes of Good and Evil) by Cicero, written in 45 BC.", PhoneNumber = "0854321098",
                Address = "Cameroon", Email = "aidenbrown1@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(7)
            },
            new User
            {
                FirstName = "Emma", LastName = "Wilson", Description = "It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of \"de Finibus Bonorum et Malorum\" (The Extremes of Good and Evil) by Cicero, written in 45 BC.", PhoneNumber = "0698765432",
                Address = "Mali", Email = "emmawilson@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(7)
            },
            //70
            new User
            {
                FirstName = "Lucas", LastName = "Thomas", Description = "It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of \"de Finibus Bonorum et Malorum\" (The Extremes of Good and Evil) by Cicero, written in 45 BC.", PhoneNumber = "0789087654",
                Address = "Senegal", Email = "lucasthomas3@example.com",

                CreationTime = DateTime.Now - TimeSpan.FromDays(5)
            },
            new User
            {
                FirstName = "Ava", LastName = "Martinez", Description = "It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of \"de Finibus Bonorum et Malorum\" (The Extremes of Good and Evil) by Cicero, written in 45 BC.", PhoneNumber = "0843210987",
                Address = "Somalia", Email = "avamartinez@example.com",

                CreationTime = DateTime.Now - TimeSpan.FromDays(5)
            },
            new User
            {
                FirstName = "Liam", LastName = "Johnson", Description = "The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from \"de Finibus Bonorum et Malorum\" by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.", PhoneNumber = "0765432109",
                Address = "Namibia", Email = "liamjohnson@example.com",

                CreationTime = DateTime.Now - TimeSpan.FromDays(5)
            },
            new User
            {
                FirstName = "Isabella", LastName = "Brown", Description = "The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from \"de Finibus Bonorum et Malorum\" by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.", PhoneNumber = "0854321098",
                Address = "Ivory Coast", Email = "isabellabrown1@example.com",

                CreationTime = DateTime.Now - TimeSpan.FromDays(5)
            },
            new User
            {
                FirstName = "Noah", LastName = "Davis", Description = "The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from \"de Finibus Bonorum et Malorum\" by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.", PhoneNumber = "0912345678",
                Address = "Burkina Faso", Email = "noahdavis@example.com",

                CreationTime = DateTime.Now - TimeSpan.FromDays(5)
            },
            new User
            {
                FirstName = "Sophia", LastName = "Taylor", Description = "It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of \"de Finibus Bonorum et Malorum\" (The Extremes of Good and Evil) by Cicero, written in 45 BC.", PhoneNumber = "0698765432",
                Address = "Malawi", Email = "sophiataylor@example.com",

                CreationTime = DateTime.Now - TimeSpan.FromDays(5)
            },
            new User
            {
                FirstName = "Jackson", LastName = "Wilson", Description = "The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from \"de Finibus Bonorum et Malorum\" by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.", PhoneNumber = "0843210987",
                Address = "Sierra Leone", Email = "jacksonwilson@example.com",

                CreationTime = DateTime.Now - TimeSpan.FromDays(5)
            },
            new User
            {
                FirstName = "Olivia", LastName = "Thomas", Description = "The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from \"de Finibus Bonorum et Malorum\" by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.", PhoneNumber = "0854321098",
                Address = "Chad", Email = "oliviathomas@example.com",

                CreationTime = DateTime.Now - TimeSpan.FromDays(5)
            },
            new User
            {
                FirstName = "Aiden", LastName = "Martinez", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", PhoneNumber = "0798765432",
                Address = "Togo", Email = "aidenmartinez@example.com",

                CreationTime = DateTime.Now - TimeSpan.FromDays(5)
            },
            new User
            {
                FirstName = "Emma", LastName = "Johnson", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", PhoneNumber = "0912345678",
                Address = "Benin", Email = "emmajohnson@example.com",

                CreationTime = DateTime.Now - TimeSpan.FromDays(5)
            },
            //80
            new User
            {
                FirstName = "Lucas", LastName = "Brown", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", PhoneNumber = "0654321098",
                Address = "Lesotho", Email = "lucasbrown1@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(4)
            },
            new User
            {
                FirstName = "Ava", LastName = "Davis", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", PhoneNumber = "0843210987",
                Address = "Gambia", Email = "avadavi1s@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(4)
            },
            new User
            {
                FirstName = "Liam", LastName = "Taylor", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", PhoneNumber = "0789087654",
                Address = "Mauritius", Email = "liamtaylor@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(3)
            },
            new User
            {
                FirstName = "Isabella", LastName = "Wilson", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", PhoneNumber = "0854321098",
                Address = "Swaziland", Email = "isabellawilson@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(3)
            },
            new User
            {
                FirstName = "Noah", LastName = "Thomas", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", PhoneNumber = "0698765432",
                Address = "Guinea-Bissau", Email = "noahthomas@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(3)
            },
            new User
            {
                FirstName = "Sophia", LastName = "Martinez", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", PhoneNumber = "0843210987",
                Address = "Congo", Email = "sophiamartinez@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(3)
            },
            new User
            {
                FirstName = "Jackson", LastName = "Johnson", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", PhoneNumber = "0854321098",
                Address = "Comoros", Email = "jacksonjohnson@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(3)
            },
            new User
            {
                FirstName = "Olivia", LastName = "Brown", Description = "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est, omnis dolor repellendus. Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae. Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus asperiores repellat.", PhoneNumber = "0912345678",
                Address = "Equatorial Guinea", Email = "oliviabrown@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(3)
            },
            new User
            {
                FirstName = "Aiden", LastName = "Davis", Description = "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est, omnis dolor repellendus. Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae. Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus asperiores repellat.", PhoneNumber = "0654321098",
                Address = "Seychelles", Email = "aidendavis@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(3)
            },
            new User
            {
                FirstName = "Emma", LastName = "Taylor", Description = "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est, omnis dolor repellendus. Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae. Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus asperiores repellat.", PhoneNumber = "0843210987",
                Address = "Mauritania", Email = "emmataylor1@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(3)
            },
            //90
            new User
            {
                FirstName = "Lucas", LastName = "Wilson", Description = "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est, omnis dolor repellendus. Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae. Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus asperiores repellat.", PhoneNumber = "0789087654",
                Address = "Djibouti", Email = "lucaswilson@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(2)
            },

            new User
            {
                FirstName = "Ava", LastName = "Thomas", Description = "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est, omnis dolor repellendus. Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae. Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus asperiores repellat.", PhoneNumber = "0854321098",
                Address = "Eswatini", Email = "avathomas@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(2)
            },
            new User
            {
                FirstName = "Liam", LastName = "Martinez", Description = "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est, omnis dolor repellendus. Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae. Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus asperiores repellat.", PhoneNumber = "0654321098",
                Address = "Cabo Verde", Email = "liammartinez@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(2)
            },
            new User
            {
                FirstName = "Isabella", LastName = "Johnson", Description = "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est, omnis dolor repellendus. Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae. Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus asperiores repellat.", PhoneNumber = "0843210987",
                Address = "São Tomé and Príncipe", Email = "isabellajohnson@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(2)
            },
            new User
            {
                FirstName = "Noah", LastName = "Brown", Description = "On the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will, which is the same as saying through shrinking from toil and pain. These cases are perfectly simple and easy to distinguish. In a free hour, when our power of choice is untrammelled and when nothing prevents our being able to do what we like best, every pleasure is to be welcomed and every pain avoided. But in certain circumstances and owing to the claims of duty or the obligations of business it will frequently occur that pleasures have to be repudiated and annoyances accepted. The wise man therefore always holds in these matters to this principle of selection: he rejects pleasures to secure other greater pleasures, or else he endures pains to avoid worse pains.", PhoneNumber = "0854321098",
                Address = "Grenada", Email = "noahbrown@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(2)
            },
            new User
            {
                FirstName = "Sophia", LastName = "Davis", Description = "On the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will, which is the same as saying through shrinking from toil and pain. These cases are perfectly simple and easy to distinguish. In a free hour, when our power of choice is untrammelled and when nothing prevents our being able to do what we like best, every pleasure is to be welcomed and every pain avoided. But in certain circumstances and owing to the claims of duty or the obligations of business it will frequently occur that pleasures have to be repudiated and annoyances accepted. The wise man therefore always holds in these matters to this principle of selection: he rejects pleasures to secure other greater pleasures, or else he endures pains to avoid worse pains.", PhoneNumber = "0912345678",
                Address = "Antigua and Barbuda", Email = "sophiadavis@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(2)
            },
            new User
            {
                FirstName = "Jackson", LastName = "Taylor", Description = "On the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will, which is the same as saying through shrinking from toil and pain. These cases are perfectly simple and easy to distinguish. In a free hour, when our power of choice is untrammelled and when nothing prevents our being able to do what we like best, every pleasure is to be welcomed and every pain avoided. But in certain circumstances and owing to the claims of duty or the obligations of business it will frequently occur that pleasures have to be repudiated and annoyances accepted. The wise man therefore always holds in these matters to this principle of selection: he rejects pleasures to secure other greater pleasures, or else he endures pains to avoid worse pains.", PhoneNumber = "0698765432",
                Address = "Dominica", Email = "jacksontaylor@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(2)
            },
            new User
            {
                FirstName = "Olivia", LastName = "Wilson", Description = "On the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will, which is the same as saying through shrinking from toil and pain. These cases are perfectly simple and easy to distinguish. In a free hour, when our power of choice is untrammelled and when nothing prevents our being able to do what we like best, every pleasure is to be welcomed and every pain avoided. But in certain circumstances and owing to the claims of duty or the obligations of business it will frequently occur that pleasures have to be repudiated and annoyances accepted. The wise man therefore always holds in these matters to this principle of selection: he rejects pleasures to secure other greater pleasures, or else he endures pains to avoid worse pains.", PhoneNumber = "0854321098",
                Address = "Saint Kitts and Nevis", Email = "oliviawilson@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(2)
            },
            new User
            {
                FirstName = "Aiden", LastName = "Thomas", Description = "On the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will, which is the same as saying through shrinking from toil and pain. These cases are perfectly simple and easy to distinguish. In a free hour, when our power of choice is untrammelled and when nothing prevents our being able to do what we like best, every pleasure is to be welcomed and every pain avoided. But in certain circumstances and owing to the claims of duty or the obligations of business it will frequently occur that pleasures have to be repudiated and annoyances accepted. The wise man therefore always holds in these matters to this principle of selection: he rejects pleasures to secure other greater pleasures, or else he endures pains to avoid worse pains.", PhoneNumber = "0789087654",
                Address = "Saint Lucia", Email = "aidenthomas@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(2)
            },
            new User
            {
                FirstName = "Emma", LastName = "Martinez", Description = "On the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will, which is the same as saying through shrinking from toil and pain. These cases are perfectly simple and easy to distinguish. In a free hour, when our power of choice is untrammelled and when nothing prevents our being able to do what we like best, every pleasure is to be welcomed and every pain avoided. But in certain circumstances and owing to the claims of duty or the obligations of business it will frequently occur that pleasures have to be repudiated and annoyances accepted. The wise man therefore always holds in these matters to this principle of selection: he rejects pleasures to secure other greater pleasures, or else he endures pains to avoid worse pains.", PhoneNumber = "0843210987",
                Address = "Saint Vincent and the Grenadines", Email = "emmamartinez@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(2)
            },
            new User
            {
                FirstName = "Lucas", LastName = "Johnson", Description = "On the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will, which is the same as saying through shrinking from toil and pain. These cases are perfectly simple and easy to distinguish. In a free hour, when our power of choice is untrammelled and when nothing prevents our being able to do what we like best, every pleasure is to be welcomed and every pain avoided. But in certain circumstances and owing to the claims of duty or the obligations of business it will frequently occur that pleasures have to be repudiated and annoyances accepted. The wise man therefore always holds in these matters to this principle of selection: he rejects pleasures to secure other greater pleasures, or else he endures pains to avoid worse pains.", PhoneNumber = "0854321098",
                Address = "Barbados", Email = "lucasjohnson@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(2)
            },
            //100
            new User
            {
                FirstName = "Ava", LastName = "Brown", Description = "On the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will, which is the same as saying through shrinking from toil and pain. These cases are perfectly simple and easy to distinguish. In a free hour, when our power of choice is untrammelled and when nothing prevents our being able to do what we like best, every pleasure is to be welcomed and every pain avoided. But in certain circumstances and owing to the claims of duty or the obligations of business it will frequently occur that pleasures have to be repudiated and annoyances accepted. The wise man therefore always holds in these matters to this principle of selection: he rejects pleasures to secure other greater pleasures, or else he endures pains to avoid worse pains.", PhoneNumber = "0912345678",
                Address = "Trinidad and Tobago", Email = "avabrown1@example.com" ,
                CreationTime = DateTime.Now - TimeSpan.FromDays(1)
            },
            new User
            {
                FirstName = "Liam", LastName = "Davis", Description = "On the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will, which is the same as saying through shrinking from toil and pain. These cases are perfectly simple and easy to distinguish. In a free hour, when our power of choice is untrammelled and when nothing prevents our being able to do what we like best, every pleasure is to be welcomed and every pain avoided. But in certain circumstances and owing to the claims of duty or the obligations of business it will frequently occur that pleasures have to be repudiated and annoyances accepted. The wise man therefore always holds in these matters to this principle of selection: he rejects pleasures to secure other greater pleasures, or else he endures pains to avoid worse pains.", PhoneNumber = "0654321098",
                Address = "Belize", Email = "liamdavis@example.com" ,
                CreationTime = DateTime.Now - TimeSpan.FromDays(1)
            },
            new User
            {
                FirstName = "Isabella", LastName = "Taylor", Description = "On the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will, which is the same as saying through shrinking from toil and pain. These cases are perfectly simple and easy to distinguish. In a free hour, when our power of choice is untrammelled and when nothing prevents our being able to do what we like best, every pleasure is to be welcomed and every pain avoided. But in certain circumstances and owing to the claims of duty or the obligations of business it will frequently occur that pleasures have to be repudiated and annoyances accepted. The wise man therefore always holds in these matters to this principle of selection: he rejects pleasures to secure other greater pleasures, or else he endures pains to avoid worse pains.", PhoneNumber = "0843210987",
                Address = "Jamaica", Email = "isabellatay1lor@example.com" ,
                CreationTime = DateTime.Now - TimeSpan.FromDays(1)
            },
            new User
            {
                FirstName = "Noah", LastName = "Wilson", Description = "On the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will, which is the same as saying through shrinking from toil and pain. These cases are perfectly simple and easy to distinguish. In a free hour, when our power of choice is untrammelled and when nothing prevents our being able to do what we like best, every pleasure is to be welcomed and every pain avoided. But in certain circumstances and owing to the claims of duty or the obligations of business it will frequently occur that pleasures have to be repudiated and annoyances accepted. The wise man therefore always holds in these matters to this principle of selection: he rejects pleasures to secure other greater pleasures, or else he endures pains to avoid worse pains.", PhoneNumber = "0789087654",
                Address = "Bahamas", Email = "noahwilson@example.com" ,
                CreationTime = DateTime.Now - TimeSpan.FromDays(1)
            },
            new User
            {
                FirstName = "Sophia", LastName = "Thomas", Description = "On the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will, which is the same as saying through shrinking from toil and pain. These cases are perfectly simple and easy to distinguish. In a free hour, when our power of choice is untrammelled and when nothing prevents our being able to do what we like best, every pleasure is to be welcomed and every pain avoided. But in certain circumstances and owing to the claims of duty or the obligations of business it will frequently occur that pleasures have to be repudiated and annoyances accepted. The wise man therefore always holds in these matters to this principle of selection: he rejects pleasures to secure other greater pleasures, or else he endures pains to avoid worse pains.", PhoneNumber = "0854321098",
                Address = "Chad", Email = "sophiathomas@example.com" ,
                CreationTime = DateTime.Now - TimeSpan.FromDays(1)
            },
            new User
            {
                FirstName = "Jackson", LastName = "Martinez", Description = "On the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will, which is the same as saying through shrinking from toil and pain. These cases are perfectly simple and easy to distinguish. In a free hour, when our power of choice is untrammelled and when nothing prevents our being able to do what we like best, every pleasure is to be welcomed and every pain avoided. But in certain circumstances and owing to the claims of duty or the obligations of business it will frequently occur that pleasures have to be repudiated and annoyances accepted. The wise man therefore always holds in these matters to this principle of selection: he rejects pleasures to secure other greater pleasures, or else he endures pains to avoid worse pains.", PhoneNumber = "0843210987",
                Address = "Somalia", Email = "jacksonmartinez1@example.com" ,
                CreationTime = DateTime.Now - TimeSpan.FromDays(1)
            },
            new User
            {
                FirstName = "Olivia", LastName = "Johnson", Description = "On the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will, which is the same as saying through shrinking from toil and pain. These cases are perfectly simple and easy to distinguish. In a free hour, when our power of choice is untrammelled and when nothing prevents our being able to do what we like best, every pleasure is to be welcomed and every pain avoided. But in certain circumstances and owing to the claims of duty or the obligations of business it will frequently occur that pleasures have to be repudiated and annoyances accepted. The wise man therefore always holds in these matters to this principle of selection: he rejects pleasures to secure other greater pleasures, or else he endures pains to avoid worse pains.", PhoneNumber = "0765432109",
                Address = "Namibia", Email = "oliviajohnson@example.com" ,
                CreationTime = DateTime.Now - TimeSpan.FromDays(1)
            },
            new User
            {
                FirstName = "Aiden", LastName = "Brown", Description = "Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est, omnis dolor repellendus. Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae. Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus asperiores repellat", PhoneNumber = "0854321098",
                Address = "Cameroon", Email = "aidenbrown2@example.com"                ,
                CreationTime = DateTime.Now - TimeSpan.FromDays(1)

            },
            new User
            {
                FirstName = "Emma", LastName = "Wilson", Description = "Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est, omnis dolor repellendus. Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae. Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus asperiores repellat", PhoneNumber = "0698765432",
                Address = "Mali", Email = "emmawilson1@example.com"
                ,CreationTime = DateTime.Now - TimeSpan.FromDays(1)
            },
            new User
            {
                FirstName = "Lucas", LastName = "Thomas", Description = "Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est, omnis dolor repellendus. Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae. Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus asperiores repellat", PhoneNumber = "0789087654",
                Address = "Senegal", Email = "lucasthomas4@example.com",
                CreationTime = DateTime.Now - TimeSpan.FromDays(1)
            },
            //110
        };

        ClassInformations = new List<ClassInformation>()
        {
            new ClassInformation
            {
                Title = "Tìm Gia Sư Dạy Laravel Tại Thủ Đức, Hồ Chí Minh", Description = "Không có nội dung mô tả",
                Address = "Thu Duc, HCMC",
                SubjectId = programming.Id,
                Status = Status.Confirmed,
                IsDeleted = false,
                TutorId = tutor.Id,
                CreationTime = DateTime.Now - TimeSpan.FromDays(3),
                LearnerId = standardUser.Id,
                LearnerName = standardUser.GetFullNAme(),
                LearnerGender = standardUser.Gender,
                ContactNumber = standardUser.PhoneNumber

            },
            new ClassInformation
            {
                Title = "Tìm Gia Sư Dạy Laravel Tại Thủ Đức, Hồ Chí Minh - HỌC ONLINE",
                Description = "Không có nội dung mô tả",
                Address = "Kha Vạn Cân, phường Linh Trung, Thủ Đức, Hồ Chí Minh - HỌC ONLINE",
                SubjectId = programming.Id, LearningMode = LearningMode.Online, Fee = 300, ChargeFee = 108,
                IsDeleted = false,
                Status = Status.Confirmed,
                TutorId = tutor.Id,
                CreationTime = DateTime.Now - TimeSpan.FromDays(1),
                LearnerId = standardUser1.Id,
                LearnerName = standardUser1.GetFullNAme(),
                LearnerGender = standardUser1.Gender,
                ContactNumber = standardUser.PhoneNumber

            },
            new ClassInformation
            {
                Title = "Cần Gia Sư Môn Lập Trình Python Tại Quận Long Biên, Hà Nội",
                Description =
                    "học viên làm bên lĩnh vực kinh tế, muốn học lập trình Python để áp dụng vào công việc",
                Address = "gõ 71/1 Gia Thượng, Ngọc Thụy, quận Long Biên, Hà Nội", SubjectId = programming.Id,
                LearningMode = LearningMode.Offline, LearnerGender = Gender.Female, Fee = 400, ChargeFee = 480,
                Status = Status.Confirmed,

                IsDeleted = false, TutorId = tutor.Id,
                CreationTime = DateTime.Now - TimeSpan.FromDays(1),
                LearnerId = standardUser.Id,
                LearnerName = standardUser.GetFullNAme(),
                ContactNumber = standardUser.PhoneNumber


            },
            new ClassInformation
            {
                Title = "Cần Gia Sư Dạy Lập Trình Game - HỌC Online", Description = "học viên 25 tuổi",
                Address = "Học Online", SubjectId = programming.Id, LearningMode = LearningMode.Online,
                Status = Status.Confirmed,
                Fee = 400, ChargeFee = 720,
                IsDeleted = false, TutorId = tutor.Id,
                CreationTime = DateTime.Now - TimeSpan.FromDays(1),
                LearnerId = standardUser.Id,
                LearnerName = standardUser.GetFullNAme(),
                LearnerGender = standardUser.Gender,
                ContactNumber = standardUser.PhoneNumber

            },
            new ClassInformation
            {
                Title = "Cần Gia Sư Môn Lập Trình C# Tại Quận 8, Hồ Chí Minh",
                Description = "học viên cần học những môn Lý Thuyết đồ thị Lập trình thiết bị di động",
                Address = "Đào Cam Mộc, phường 4, Quận 8, Hồ Chí Minh", SubjectId = programming.Id,
                LearningMode = LearningMode.Offline,
                Fee = 200, ChargeFee = 240,
                Status = Status.Confirmed,
                LearnerId = standardUser.Id,
                LearnerName = standardUser.GetFullNAme(),
                LearnerGender = standardUser.Gender,
                IsDeleted = false, TutorId = tutor.Id,
                CreationTime = DateTime.Now - TimeSpan.FromDays(8),
                ContactNumber = standardUser.PhoneNumber

            },
            new ClassInformation
            {
                Title = "Cần Gia Sư Môn Yoga Tại Nam Từ Liêm, Hà Nội",
                Description = "Học viên 40 tuổi",
                Address = "ngõ 199 Hồ Tùng Mậu, Nam Từ Liêm, Hà Nội", SubjectId = otherSubject.Id,
                LearningMode = LearningMode.Offline, NumberOfLearner = 2, LearnerGender = Gender.None, Fee = 300,
                ChargeFee = 300,
                IsDeleted = false,
                Status = Status.Available,
                CreationTime = DateTime.Now - TimeSpan.FromDays(8),
                 LearnerId = standardUser3.Id,
                LearnerName = standardUser3.GetFullNAme(),

                ContactNumber = standardUser3.PhoneNumber

            },
            new ClassInformation
            {
                Title = "Cần Gia Sư Môn Võ Thuật Tại Quận 8, Hồ Chí Minh",
                Description = "bé học tại trung tâm, hoặc vị trí của giáo viên dạy theo lớp hoặc theo nhóm",
                Address = "khu dân cư Phú Lợi, phường 7, quận 8, Hồ Chí Minh", SubjectId = otherSubject.Id,
                LearningMode = LearningMode.Offline,
                 Fee = 250, ChargeFee = 500,
                IsDeleted = false,
                Status = Status.Available,
                CreationTime = DateTime.Now - TimeSpan.FromDays(1),
                 LearnerId = standardUser.Id,
                LearnerName = standardUser.GetFullNAme(),
                LearnerGender = standardUser.Gender,
                ContactNumber = standardUser.PhoneNumber
            },
            new ClassInformation
            {
                Title = "Cần Gia Sư Môn Thanh Nhạc Tại Vũng Tàu, Bà Rịa - Vũng Tàu",
                Description = "bé học tại trung tâm, hoặc vị trí của giáo viên dạy theo lớp hoặc theo nhóm",
                Address = "khu dân cư Phú Lợi, phường 7, quận 8, Hồ Chí Minh", SubjectId = otherSubject.Id,
                LearningMode = LearningMode.Offline,Fee = 300, ChargeFee = 600,
                IsDeleted = false, Status = Status.Available,
                CreationTime = DateTime.Now - TimeSpan.FromDays(4),
                 LearnerId = standardUser.Id,
                LearnerName = standardUser.GetFullNAme(),
                LearnerGender = standardUser.Gender,
                ContactNumber = standardUser.PhoneNumber
            },
            new ClassInformation
            {
                Title = "Cần Gia Sư Môn Tiếng Hàn Tại Quận 7, Hồ Chí Minh",
                Description = "chủ yếu học tiếng hàn để giao tiếp và nói chuyện với người hàn",
                Address = "Nguyễn Thị Thập, Tân Phú, quận 7, Hồ Chí Minh", SubjectId = korean.Id,
                SessionPerWeek = 3,
                LearningMode = LearningMode.Offline,
                Fee = 300, ChargeFee = 600,
                IsDeleted = false,
                Status = Status.Available,
                CreationTime = DateTime.Now - TimeSpan.FromDays(4),
                 LearnerId = standardUser.Id,
                LearnerName = standardUser.GetFullNAme(),
                LearnerGender = standardUser.Gender,
                ContactNumber = standardUser.PhoneNumber
            },
            new ClassInformation
            {
                Title = "Cần Gia Sư Môn Tiếng Đức Tại Vũng Tàu, Bà Rịa - Vũng Tàu",
                Description = "Học viên 20 tuổi, thời gian linh động sáng, chiều hoặc tối",
                Address = "đường 30 tháng 4, Phường 12, Vũng Tàu, Bà Rịa - Vũng Tàu", SubjectId = german.Id,
                SessionPerWeek = 3,
                LearningMode = LearningMode.Offline,
                Fee = 350, ChargeFee = 840,
                IsDeleted = false,
                TutorId = tutor1.Id,
                Status = Status.Confirmed,
                CreationTime = DateTime.Now - TimeSpan.FromDays(1),
                LearnerId = standardUser1.Id,
                LearnerName = standardUser1.GetFullNAme(),
                LearnerGender = standardUser1.Gender,
                ContactNumber = standardUser1.PhoneNumber

            },
            new ClassInformation
            {
                Title = "Cần Gia Sư Môn Tin Học Tại Dĩ An, Bình Dương",
                Description = "Học viên 40 tuổi",
                Address = "Trần Hưng Đạo, Đông Hòa, Dĩ An, Bình Dương", SubjectId = informatics.Id,
                SessionPerWeek = 2,
                LearningMode = LearningMode.Offline, Fee = 300, ChargeFee = 360,
                IsDeleted = false,
                TutorId = tutor1.Id,
                Status = Status.Confirmed,
                CreationTime = DateTime.Now - TimeSpan.FromDays(11),
                LearnerId = standardUser.Id,
                LearnerName = standardUser.GetFullNAme(),
                LearnerGender = standardUser.Gender,
                ContactNumber = standardUser.PhoneNumber
            },
            new ClassInformation
            {
                Title = "Tiếng Việt Cho Người Hàn Tại Bình Thạnh, Hồ Chí Minh",
                Description =
                    "Học viên là Hai bác người Hàn mới qua Việt Nam chưa được bao lâu và chưa biết gì về tiếng Việt, do ban ngày đi làm nên chỉ học được vào buổi tối, học viên giao tiếp bằng Tiếng Hàn học môn Tiếng Việt",
                Address = "Vinhomes centralpark, Nguyễn Hữu Cảnh, phường 22, Bình Thạnh, Hồ Chí Minh",
                SubjectId = vietnameses.Id,
                SessionPerWeek = 2, NumberOfLearner = 2,
                LearningMode = LearningMode.Offline,
                LearnerGender = Gender.Male,
                Fee = 500, ChargeFee = 1500,
                IsDeleted = false,
                TutorId = tutor2.Id,
                Status = Status.Confirmed,
                CreationTime = DateTime.Now - TimeSpan.FromDays(12),
                LearnerName = "Strager User Name",
                ContactNumber = "0983123123"

            },
            new ClassInformation
            {
                Title = "Cần Gia Sư Môn Tiếng Anh Lớp 9 Tại Quận 2, Hồ Chí Minh",
                Description =
                    "dạy văn phạm cho bé lớp 9, giáo viên 250k/buổi/90phut, yêu cầu gia sư bên ngành Tiếng Anh",
                Address = "Hoàng Anh River View - 37 Nguyễn Văn Hưởng, Phường Thảo Điền, quận 2, Hồ Chí Minh",
                SubjectId = english.Id,
                SessionPerWeek = 2, NumberOfLearner = 2,
                LearningMode = LearningMode.Offline,
                LearnerGender = Gender.Female,
                Fee = 200, ChargeFee = 400,
                IsDeleted = false,
                Status = Status.Available,
                CreationTime = DateTime.Now - TimeSpan.FromDays(1),
                LearnerId = standardUser3.Id,
                LearnerName = standardUser3.GetFullNAme(),
                ContactNumber = standardUser3.PhoneNumber

            },
            new ClassInformation
            {
                Title = "Cần Gia Sư Môn Guitar Tại Vĩnh Thạnh, Cần Thơ",
                Description = "học viên 26 tuổi, học từ cơ bản đệm hát",
                Address = "ấp Thắng Lợi, xã Thạnh Lộc, huyện Vĩnh Thạnh, Cần Thơ",
                SubjectId = guitar.Id, SessionPerWeek = 2, NumberOfLearner = 1,
                LearningMode = LearningMode.Offline, LearnerGender = Gender.Female, Fee = 200, ChargeFee = 400,
                IsDeleted = false, Status = Status.Available,
                CreationTime = DateTime.Now - TimeSpan.FromDays(9),

                LearnerName = "Stranger User Name 1",
                ContactNumber = "011111112223333"
            },
            new ClassInformation
            {
                Title = "Cần Gia Sư Môn Cờ Vua Tại Nha Trang, Khánh Hòa",
                Description = "Không có nội dung",
                Address = "đường Thích Quảng Đức, khu đô thị Hà Quang 2, phường Phước Hải, Nha Trang, Khánh Hòa",
                SubjectId = otherSubject.Id, SessionPerWeek = 2, NumberOfLearner = 1,
                LearningMode = LearningMode.Offline, LearnerGender = Gender.Female, Fee = 200, ChargeFee = 200,
                IsDeleted = false, Status = Status.Available,
                CreationTime = DateTime.Now - TimeSpan.FromDays(9),
                LearnerName = "Stranger User Name 2",
                ContactNumber = "011111112223333"

            },
            new ClassInformation
            {
                Title = "Cần Gia Sư Môn Guitar Tại Gò Dầu, Tây Ninh",
                Description = "học viên 44 tuổi, học từ cơ bản",
                Address = "Gò Dầu, Tây Ninh",
                SubjectId = guitar.Id, SessionPerWeek = 2, NumberOfLearner = 1,
                LearningMode = LearningMode.Offline, LearnerGender = Gender.Female, Fee = 200, ChargeFee = 200,
                IsDeleted = false, Status = Status.Available,
                CreationTime = DateTime.Now - TimeSpan.FromDays(9),
                LearnerName = "Stranger User Name 3",
                ContactNumber = "011111112223333"

            },
            new ClassInformation
            {
                Title = "Cần Gia Sư Môn Hóa Lớp 11 Tại Phường Phước Hưng, Bà Rịa Vũng Tàu",
                Description = "thứ 6 từ 18h30 - 20h00, thứ 7 từ 15h00 đến 16h30",
                Address =
                    "hẻm 271 Phan Đăng Lưu, tổ 8, khu phố Hương Điền, phường Long Hương, tp Bà Rịa, Bà Rịa Vũng Tàu",
                SubjectId = chemistry.Id, SessionPerWeek = 2, NumberOfLearner = 1,
                LearningMode = LearningMode.Offline, LearnerGender = Gender.Male, Fee = 250, ChargeFee = 500,
                Status = Status.Confirmed,
                IsDeleted = false, TutorId = tutor2.Id,
                CreationTime = DateTime.Now - TimeSpan.FromDays(8),
                LearnerName = "Stranger User Name 3",
                ContactNumber = "011111112223333"

            },
            new ClassInformation
            {
                Title = "Cần Gia Sư Môn Nhảy Shuffle Dance Tại Củ Chi, Hồ Chí Minh",
                Description = "hoặc học tại Vũ Duy Chí, khu phố 2, tt Củ Chi",
                Address = "đường Tỉnh Lộ 7, ấp Chợ Cũ, Tỉnh Lộ 7, Củ Chi, Hồ Chí Minh",
                SubjectId = dance.Id,

                SessionPerWeek = 2,
                NumberOfLearner = 6,
                MinutePerSession = 120,
                LearningMode = LearningMode.Offline,
                LearnerGender = Gender.Male,
                Fee = 900,
                ChargeFee = 1800,
                IsDeleted = false,
                Status = Status.Available,
                CreationTime = DateTime.Now - TimeSpan.FromDays(8),
                LearnerName = "Stranger User Name 4",
                ContactNumber = "011111112223333"

            },
            new ClassInformation
            {
                Title = "Private tutor for improving English language skills",
                Description = "Enhance your English language proficiency through personalized lessons. Improve your grammar, vocabulary, and conversation skills with a qualified English tutor.",
                Address = "London, UK",
                IsDeleted = false,
                SessionPerWeek = 2,
                SubjectId = english.Id,

                NumberOfLearner = 6,
                MinutePerSession = 120,
                LearningMode = LearningMode.Offline,
                LearnerGender = Gender.Male,
                Fee = 900,
                ChargeFee = 1800,
                Status = Status.Confirmed,
                CreationTime = DateTime.Now - TimeSpan.FromDays(8),
                LearnerName = "Strager User Name",
                ContactNumber = "0983123123"

            },
            new ClassInformation
            {
                Title = "Private lessons for learning classical piano",
                Description = "Immerse yourself in the world of classical music through piano lessons. From classical compositions to musical interpretation, develop your piano skills with a dedicated tutor.",
                Address = "Vienna, Austria",
                IsDeleted = false,
                SessionPerWeek = 2,
                SubjectId = piano.Id,

                NumberOfLearner = 2,
                MinutePerSession = 90,
                LearningMode = LearningMode.Offline,
                LearnerGender = Gender.Female,
                Fee = 900,
                ChargeFee = 1200,
                Status = Status.Available,
                CreationTime = DateTime.Now - TimeSpan.FromDays(7),
                LearnerName = "Stranger User Name 1",
                ContactNumber = "011111112223333"

            },
            new ClassInformation
            {
                Title = "Learn the art of calligraphy with a master calligrapher",
                Description = "Unleash your creativity through the art of calligraphy. From brush strokes to lettering styles, develop your skills and create stunning calligraphic compositions.",
                Address = "Seoul, South Korea",
                IsDeleted = false,
                SessionPerWeek = 2,
                SubjectId = otherSubject.Id,

                NumberOfLearner = 2,
                MinutePerSession = 90,
                LearningMode = LearningMode.Offline,
                LearnerGender = Gender.Female,
                Fee = 1000,
                ChargeFee = 2100,
                Status = Status.Available,
                CreationTime = DateTime.Now - TimeSpan.FromDays(2),
                LearnerName = "Stranger User Name 1",
                ContactNumber = "011111112223333"
            },
            new ClassInformation
            {
                Title = "Private tutoring for chemistry",
                Description = "Master the concepts of chemistry and excel in your studies. From basic principles to advanced topics, a dedicated chemistry tutor will guide you towards success.",
                Address = "Melbourne, Australia",
                IsDeleted = false,
                SessionPerWeek = 2,
                SubjectId = chemistry.Id,

                NumberOfLearner = 2,
                MinutePerSession = 90,
                LearningMode = LearningMode.Offline,
                LearnerGender = Gender.Female,
                Fee = 1800,
                ChargeFee = 200,
                Status = Status.Available,
                CreationTime = DateTime.Now - TimeSpan.FromDays(3),
            },
            new ClassInformation
            {
                Title = "Discover the art of bonsai with personalized guidance",
                Description = "Learn the ancient art of bonsai and create miniature masterpieces. Understand the techniques of shaping, pruning, and caring for bonsai trees with an experienced tutor.",
                Address = "Kyoto, Japan",
                IsDeleted = false,
                SessionPerWeek = 2,
                SubjectId = otherSubject.Id,

                NumberOfLearner = 2,
                MinutePerSession = 90,
                LearningMode = LearningMode.Offline,
                LearnerGender = Gender.Female,
                Fee = 1800,
                ChargeFee = 700,
                Status = Status.Available,
                CreationTime = DateTime.Now - TimeSpan.FromDays(3),
                LearnerName = "Stranger User Name 1",
                ContactNumber = "011111112223333"
            },
            new ClassInformation
            {
                Title = "Private lessons for learning salsa dancing",
                Description = "Experience the vibrant rhythms of salsa dancing. Learn fundamental steps, partner work, and impressive moves from a skilled salsa instructor.",
                Address = "Miami, US",
                IsDeleted = false,
                SessionPerWeek = 2,
                SubjectId = dance.Id,

                NumberOfLearner = 2,
                MinutePerSession = 90,
                LearningMode = LearningMode.Offline,
                LearnerGender = Gender.Female,
                Fee = 500,
                ChargeFee = 100,
                Status = Status.Available,
                CreationTime = DateTime.Now - TimeSpan.FromDays(3),
                LearnerName = "Stranger User Name",
                ContactNumber = "011111112223333"
            },
            new ClassInformation
            {
                Title = "Private lessons for learning salsa dancing",
                Description = "Experience the vibrant rhythms of salsa dancing. Learn fundamental steps, partner work, and impressive moves from a skilled salsa instructor.",
                Address = "Miami, US",
                IsDeleted = false,
                SessionPerWeek = 2,
                SubjectId = dance.Id,

                NumberOfLearner = 2,
                MinutePerSession = 90,
                LearningMode = LearningMode.Offline,
                LearnerGender = Gender.Female,
                Fee = 100,
                ChargeFee = 100,
                Status = Status.Available,
                CreationTime = DateTime.Now - TimeSpan.FromDays(3),
                LearnerName = "Stranger User Name 5",
                ContactNumber = "011111112223333"
            },
            new ClassInformation
            {
                Title = "Explore the world of Java programming with a private astronomer",
                Description = "Embark on a cosmic journey and unravel the mysteries of the universe. Learn about celestial objects, planetary systems, and the latest discoveries in astronomy.",
                Address = "Houston, US",
                IsDeleted = false,
                SessionPerWeek = 2,
                SubjectId = java.Id,

                NumberOfLearner = 2,
                MinutePerSession = 90,
                LearningMode = LearningMode.Offline,
                LearnerGender = Gender.Female,
                Fee = 800,
                ChargeFee = 1800,
                Status = Status.Available,
                CreationTime = DateTime.Now - TimeSpan.FromDays(4),
                 LearnerName = "Stranger User Name 5",
                ContactNumber = "011111112223333"
            },
            new ClassInformation
            {
                Title = "Private tutoring for SAT preparation",
                Description = "Prepare for the SAT exam with personalized tutoring. Master test-taking strategies, improve your critical reading and math skills, and maximize your score potential.",
                Address = "New Delhi, India",
                IsDeleted = false,
                SessionPerWeek = 2,
                SubjectId = korean.Id,

                NumberOfLearner = 2,
                MinutePerSession = 90,
                LearningMode = LearningMode.Offline,
                LearnerGender = Gender.Female,
                Fee = 900,
                ChargeFee = 1800,
                Status = Status.Canceled,
                CreationTime = DateTime.Now - TimeSpan.FromDays(5),
                 LearnerName = "Stranger User Name 5",
                ContactNumber = "011111112223333"
            },
            new ClassInformation
            {
                Title = "Private tutor for learning advanced Spanish",
                Description = "Take your Spanish language skills to the next level. Learn advanced grammar, expand your vocabulary, and practice conversational Spanish with a dedicated tutor.",
                Address = "Madrid, Spain",

                IsDeleted = false,
                SessionPerWeek = 2,
                SubjectId = spain.Id,

                NumberOfLearner = 2,
                MinutePerSession = 90,
                LearningMode = LearningMode.Online,
                LearnerGender = Gender.Male,
                Fee = 200,
                ChargeFee = 700,
                Status = Status.Canceled,
                CreationTime = DateTime.Now - TimeSpan.FromDays(5),

            },
            new ClassInformation
            {
                Title = "Get fit with personalized fitness training",
                Description = "Achieve your fitness goals with customized training sessions. Whether you want to build strength, improve flexibility, or lose weight, a personal trainer will guide you on your fitness journey.",
                Address = "Vancouver, Canada",

                IsDeleted = false,
                SessionPerWeek = 2,
                SubjectId = fit.Id,

                NumberOfLearner = 2,
                MinutePerSession = 90,
                LearningMode = LearningMode.Offline,
                LearnerGender = Gender.Male,
                Fee = 300,
                ChargeFee = 500,
                Status = Status.Available,
                CreationTime = DateTime.Now - TimeSpan.FromDays(4),
                 LearnerName = "Stranger User Name 5",
                ContactNumber = "011111112223333"
            },
            new ClassInformation
            {
                Title = "Private lessons for learning watercolor painting",
                Description = "Explore the mesmerizing world of watercolor painting. Learn various techniques, color mixing, and create stunning watercolor artworks with guidance from a skilled artist.",
                Address = "Florence, Italy",

                IsDeleted = false,
                SessionPerWeek = 2,
                SubjectId = paint.Id,

                NumberOfLearner = 2,
                MinutePerSession = 90,
                LearningMode = LearningMode.Offline,
                LearnerGender = Gender.Male,
                Fee = 500,
                ChargeFee = 500,
                Status = Status.Available,
                CreationTime = DateTime.Now - TimeSpan.FromDays(32),
                LearnerName = "Stranger User Name 5",
                ContactNumber = "011111112223333"
            },
            new ClassInformation
            {
                Title = "Improve your public speaking skills with a communication coach",
                Description = "Overcome stage fright and master the art of public speaking. Enhance your communication skills, boost your confidence, and deliver impactful presentations with personalized coaching.",
                Address = "Berlin, Germany",

                IsDeleted = false,
                SessionPerWeek = 2,
                SubjectId = fit.Id,

                NumberOfLearner = 2,
                MinutePerSession = 90,
                LearningMode = LearningMode.Offline,
                LearnerGender = Gender.Male,
                Fee = 500,
                ChargeFee = 500,
                Status = Status.Available,
                CreationTime = DateTime.Now - TimeSpan.FromDays(33),
                LearnerId = standardUser3.Id,
                LearnerName = standardUser3.GetFullNAme(),

                ContactNumber = standardUser3.PhoneNumber
            },
            new ClassInformation
            {
                Title = "Private tutoring for computer programming",
                Description = "Dive into the world of coding and learn computer programming languages from an expert. From Python to Java, develop your coding skills and unleash your creativity.",
                Address = "San Francisco, US",

                IsDeleted = false,
                SessionPerWeek = 2,
                SubjectId = programming.Id,

                NumberOfLearner = 2,
                MinutePerSession = 90,
                LearningMode = LearningMode.Online,
                LearnerGender = Gender.Male,
                Fee = 1200,
                ChargeFee = 2000,
                Status = Status.Available,
                CreationTime = DateTime.Now - TimeSpan.FromDays(35),
                LearnerId = standardUser4.Id,
                LearnerName = standardUser4.GetFullNAme(),

                ContactNumber = standardUser4.PhoneNumber
            },
            new ClassInformation
            {
                Title = "Learn ballet from a professional dancer",
                Description = "Discover the grace and elegance of ballet through private lessons with a professional dancer. Improve your technique, flexibility, and expression in a nurturing environment.",
                Address = "Toronto, Canada",
                IsDeleted = false,
                SessionPerWeek = 2,
                SubjectId = dance.Id,

                NumberOfLearner = 2,
                MinutePerSession = 90,
                LearningMode = LearningMode.Online,
                LearnerGender = Gender.Male,
                Fee = 200,
                ChargeFee = 800,
                Status = Status.Available,
                CreationTime = DateTime.Now - TimeSpan.FromDays(32),
                LearnerId = standardUser4.Id,
                LearnerName = standardUser4.GetFullNAme(),

                ContactNumber = standardUser4.PhoneNumber
            },
            new ClassInformation
            {
                Title = "Private tutor for advanced mathematics",
                Description = "Delve into the world of advanced mathematics and tackle complex concepts with ease. Gain a deeper understanding of calculus, algebra, and mathematical proofs through personalized lessons.",
                Address = "Sydney, Australia",
                IsDeleted = false,
                SessionPerWeek = 2,
                SubjectId = math.Id,

                NumberOfLearner = 2,
                MinutePerSession = 90,
                LearningMode = LearningMode.Online,
                LearnerGender = Gender.Male,
                Fee = 200,
                ChargeFee = 800,
                Status = Status.Available,
                CreationTime = DateTime.Now - TimeSpan.FromDays(32),
                LearnerId = standardUser4.Id,
                LearnerName = standardUser4.GetFullNAme(),

                ContactNumber = standardUser4.PhoneNumber
            },
            new ClassInformation
            {
                Title = "Master the art of pottery with personalized lessons",
                Description = "Shape clay into beautiful pottery pieces with guidance from an experienced potter. Learn hand-building techniques, glazing, and create your unique ceramic artworks.",
                Address = "Tokyo, Japan",
                IsDeleted = false,
                SessionPerWeek = 2,
                SubjectId = otherSubject.Id,

                NumberOfLearner = 2,
                MinutePerSession = 90,
                LearningMode = LearningMode.Online,
                LearnerGender = Gender.Male,
                Fee = 200,
                ChargeFee = 800,
                Status = Status.Available,
                CreationTime = DateTime.Now - TimeSpan.FromDays(32),
                LearnerId = standardUser4.Id,
                LearnerName = standardUser4.GetFullNAme(),

                ContactNumber = standardUser4.PhoneNumber
            },
            new ClassInformation
            {
                Title = "Explore the world of culinary arts with a private chef",
                Description = "Unleash your inner chef and learn culinary techniques from a professional. From knife skills to gourmet recipes, embark on a culinary adventure in the comfort of your own kitchen.",
                Address = "Paris, France",
                IsDeleted = false,
                SessionPerWeek = 2,
                SubjectId = cook.Id,

                NumberOfLearner = 2,
                MinutePerSession = 90,
                LearningMode = LearningMode.Online,
                LearnerGender = Gender.Male,
                Fee = 200,
                ChargeFee = 800,
                Status = Status.Available,
                CreationTime = DateTime.Now - TimeSpan.FromDays(60),
                LearnerId = standardUser4.Id,
                LearnerName = standardUser4.GetFullNAme(),

                ContactNumber = standardUser4.PhoneNumber
            },
            new ClassInformation
            {
                Title = "Private lessons for learning classical guitar",
                Description = "Embark on a musical journey by learning classical guitar techniques. From fingerstyle to reading sheet music, develop your playing abilities under the guidance of a skilled tutor.",
                Address = "London, UK",
                IsDeleted = false,
                SessionPerWeek = 2,
                SubjectId = guitar.Id,

                NumberOfLearner = 2,
                MinutePerSession = 90,
                LearningMode = LearningMode.Online,
                LearnerGender = Gender.Male,
                Fee = 200,
                ChargeFee = 800,
                Status = Status.Available,
                CreationTime = DateTime.Now - TimeSpan.FromDays(62),
                LearnerId = standardUser4.Id,
                LearnerName = standardUser4.GetFullNAme(),

                ContactNumber = standardUser4.PhoneNumber
            },
            new ClassInformation
            {
               Title = "Improve your photography skills with a private tutor",
                Description = "Capture breathtaking photos and master the art of composition, lighting, and post-processing. Enhance your photography skills through personalized lessons with a professional photographer.",
                Address = "Los Angeles, US",
                IsDeleted = false,
                SessionPerWeek = 2,
                SubjectId = otherSubject.Id,

                NumberOfLearner = 2,
                MinutePerSession = 90,
                LearningMode = LearningMode.Online,
                LearnerGender = Gender.Male,
                Fee = 200,
                ChargeFee = 800,
                Status = Status.Available,
                CreationTime = DateTime.Now - TimeSpan.FromDays(62),
                LearnerId = standardUser4.Id,
                LearnerName = standardUser4.GetFullNAme(),

                ContactNumber = standardUser4.PhoneNumber
            },




        };

        TutorMajors = new List<TutorMajor>()
        {
            //Tutor1
            new()
            {
                TutorId = tutor.Id,
                SubjectId = programming.Id
            },
            new()
            {
                TutorId = tutor.Id,
                SubjectId = math.Id
            },
            new()
            {
                TutorId = tutor.Id,
                SubjectId = math.Id
            },
            new()
            {
                TutorId = tutor.Id,
                SubjectId = dance.Id
            },
            //Tutor2
            new()
            {
                TutorId = tutor1.Id,
                SubjectId = piano.Id
            },
            new()
            {
                TutorId = tutor1.Id,
                SubjectId = chemistry.Id
            },
            new()
            {
                TutorId = tutor1.Id,
                SubjectId = vietnameses.Id
            },
            new()
            {
                TutorId = tutor1.Id,
                SubjectId = german.Id
            },
            new()
            {
                TutorId = tutor1.Id,
                SubjectId = cook.Id
            },
            new()
            {
                TutorId = tutor1.Id,
                SubjectId = paint.Id
            },
            //Tutor2
            new()
            {
                TutorId = tutor2.Id,
                SubjectId = java.Id
            },
            new()
            {
                TutorId = tutor2.Id,
                SubjectId = informatics.Id
            },
            new()
            {
                TutorId = tutor2.Id,
                SubjectId = spain.Id
            },
            new()
            {
                TutorId = tutor2.Id,
                SubjectId = piano.Id
            },
            new()
            {
                TutorId = tutor2.Id,
                SubjectId = cook.Id
            },
            new()
            {
                TutorId = tutor2.Id,
                SubjectId = fit.Id
            },
            //Tutor3
            new()
            {
                TutorId = tutorUser3.Id,
                SubjectId = java.Id
            },
            new()
            {
                TutorId = tutorUser3.Id,
                SubjectId = informatics.Id
            },
            new()
            {
                TutorId = tutorUser3.Id,
                SubjectId = spain.Id
            },
            new()
            {
                TutorId = tutorUser3.Id,
                SubjectId = piano.Id
            },
            new()
            {
                TutorId = tutorUser3.Id,
                SubjectId = cook.Id
            },
            new()
            {
                TutorId = tutorUser3.Id,
                SubjectId = chemistry.Id
            },
            //Tutor4
            new()
            {
                TutorId = tutorUser3.Id,
                SubjectId = german.Id
            },
            new()
            {
                TutorId = tutorUser3.Id,
                SubjectId = spain.Id
            },
            new()
            {
                TutorId = tutorUser3.Id,
                SubjectId = korean.Id
            },
            new()
            {
                TutorId = tutorUser3.Id,
                SubjectId = piano.Id
            },
            new()
            {
                TutorId = tutorUser3.Id,
                SubjectId = cook.Id
            },
            new()
            {
                TutorId = tutorUser3.Id,
                SubjectId = fit.Id
            },

            //Tutor5
            new()
            {
                TutorId = tutorUser5.Id,
                SubjectId = german.Id
            },
            new()
            {
                TutorId = tutorUser5.Id,
                SubjectId = spain.Id
            },
            new()
            {
                TutorId = tutorUser5.Id,
                SubjectId = korean.Id
            },
            //Tutor6
            new()
            {
                TutorId = tutorUser6.Id,
                SubjectId = german.Id
            },
            new()
            {
                TutorId = tutorUser6.Id,
                SubjectId = spain.Id
            },
            new()
            {
                TutorId = tutorUser6.Id,
                SubjectId = korean.Id
            },
            //Tutor10
            new()
            {
                TutorId = tutorUser10.Id,
                SubjectId = german.Id
            },
            new()
            {
                TutorId = tutorUser10.Id,
                SubjectId = spain.Id
            },
            new()
            {
                TutorId = tutorUser10.Id,
                SubjectId = korean.Id
            },
            //Tutor7
            new()
            {
                TutorId = tutorUser7.Id,
                SubjectId = german.Id
            },
            new()
            {
                TutorId = tutorUser7.Id,
                SubjectId = spain.Id
            },
            new()
            {
                TutorId = tutorUser7.Id,
                SubjectId = korean.Id
            },
            //Tutor12
            new()
            {
                TutorId = tutorUser12.Id,
                SubjectId = german.Id
            },
            new()
            {
                TutorId = tutorUser12.Id,
                SubjectId = spain.Id
            },
            new()
            {
                TutorId = tutorUser12.Id,
                SubjectId = korean.Id
            },

        };

        Subscribers = new List<Subscriber>()
        {
            new()
            {
                TutorId = tutor.Id,
            },
            new()
            {
                TutorId = tutor1.Id,
            },
            new()
            {
                TutorId = tutor2.Id,
            },
            new()
            {
                TutorId = tutorUser3.Id,
            },new()
            {
                TutorId = tutorUser5.Id,
            },
        };

    }
}