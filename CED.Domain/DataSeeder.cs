using CED.Domain.ClassInformations;
using CED.Domain.Shared.ClassInformationConsts;
using CED.Domain.Subjects;
using CED.Domain.Users;

namespace CED.Domain;

public class DataSeeder
{
    public List<User> Users { get; private set; } = new List<User>();
    public List<Subject> Subjects { get; private set; } = new List<Subject>();
    public List<ClassInformation> ClassInformations { get; private set; } = new List<ClassInformation>();

    public DataSeeder()
    {
        SeedData();
    }

    private void SeedData()
    {
        var programming = new Subject { Id= Guid.NewGuid(), Name = "Programing", Description = "Basic subject" };
        var informatics = new Subject { Id = Guid.NewGuid(), Name = "Informatics", Description = "Alonso" };
        var otherSubject = new Subject { Id = Guid.NewGuid(), Name = "Other", Description = "Other subject" };
        var korean = new Subject { Id = Guid.NewGuid(), Name = "Korean", Description = "korean" };
        var vietnameses = new Subject { Id = Guid.NewGuid(), Name = "Vietnamese for foreigner", Description = "Justice" };
        var german = new Subject { Id = Guid.NewGuid(), Name = "German", Description = "german" };
        var english = new Subject { Id = Guid.NewGuid(), Name = "English", Description = "Olivetto" };
        var guitar = new Subject { Id = Guid.NewGuid(), Name = "Guitar", Description = "Barzdukas" };
        var chemistry = new Subject { Id = Guid.NewGuid(), Name = "Chemistry", Description = "Alonso" };
        var dance = new Subject { Id = Guid.NewGuid(), Name = "Dance", Description = "Oslon" };

        var subjects = new List<Subject>()
        {
            new Subject { Id= Guid.NewGuid(),Name = "Physics", Description = "Alexander" },
            chemistry,
            new Subject { Id= Guid.NewGuid(),Name = "Biology", Description = "Anand" },
            new Subject { Id= Guid.NewGuid(),Name = "Geography", Description = "Barzdukas" },
            new Subject {Id= Guid.NewGuid(), Name = "Information Technology", Description = "Li" },
            new Subject {Id = Guid.NewGuid(),  Name = "Fine Art", Description = "Justice" },
            new Subject {Id= Guid.NewGuid(), Name = "Literature", Description = "Norman" },
            new Subject {Id = Guid.NewGuid(),  Name = "History", Description = "Olivetto" },

            new Subject { Id= Guid.NewGuid(),Name = "Engineering", Description = "Alexander" },
            informatics,
            new Subject {Id = Guid.NewGuid(),  Name = "Technology", Description = "Anand" },
            new Subject {Id = Guid.NewGuid(),  Name = "Politics", Description = "Barzdukas" },
            new Subject {Id= Guid.NewGuid(), Name = "Psychology", Description = "Li" },
            new Subject {Id = Guid.NewGuid(),  Name = "Economics", Description = "Justice" },
            new Subject {Id= Guid.NewGuid(), Name = "Physical Education", Description = "Norman" },
            english,

            new Subject {Id = Guid.NewGuid(),  Name = "C# programing", Description = "Barzdukas" },
            programming,
            new Subject {Id = Guid.NewGuid(),  Name = "Java programing", Description = "Li" },
            new Subject {Id= Guid.NewGuid(), Name = "Python programing", Description = "Justice" },
            new Subject {Id = Guid.NewGuid(),  Name = "Web programing", Description = "Norman" },
            new Subject {Id = Guid.NewGuid(),  Name = "HTML,CSS & Javascript", Description = "Olivetto" },

            guitar, dance,
            new Subject {Id = Guid.NewGuid(),  Name = "Piano", Description = "Li" },
            otherSubject,
            german, korean, vietnameses
        };
        this.Subjects = subjects;
        var tutor = new User //Tutor
        {
            Id = Guid.NewGuid(),
            FirstName = "Meredith", LastName = "Smith", Description = "Premium tutor",
            PhoneNumber = "0123123120", Email = "20520727@gm.uit.edu.com",
            University = "University of Information Technology - VNUHCM",
            Role = UserRole.Tutor
        };

        var tutor1 = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "Yan", LastName = "Woo", Gender = Gender.Female, Description = "Multi-subject tutor",
            PhoneNumber = "0123123123", Email = "hoangle.q5@gmail.com", Role = UserRole.Tutor,
            University = "University of Economics HCMC (UEH)"
        };

        var tutor2 = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "Nay", LastName = "Woo", Gender = Gender.Female,
            Description = "Multi-subject tutor, Yan's sister",
            PhoneNumber = "0123123332", Email = "hoangle.qq5@gmail.com", Role = UserRole.Tutor,
            University = "University of Economics HCMC (UEH)"
        };

        var standardUser = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "Laura", LastName = "Grey", Gender = Gender.Female, Description = "Norman",
            PhoneNumber = "0123123125", Email = "hoangle.q2@gmail.com"
        };


        var users = new List<User>()
        {
            //Admin
            new User
            {
                FirstName = "Matt", LastName = "Le", Description = "Premium Admin", Email = "hoangle.q3@gmail.com", Address = "Viet Nam",
                PhoneNumber = "0965686172", Role = UserRole.Admin
            },
            new User
            {
                FirstName = "John", LastName = "Doe", Description = "Third admin", Email = "lehquy13@gmail.com",Address = "Viet Nam",
                PhoneNumber = "0123123120", Role = UserRole.Admin
            },
            tutor,
            new User
            {
                FirstName = "Nino", LastName = "Walker", Gender = Gender.Female, PhoneNumber = "0123123126",Address = "Viet Nam",
                Description = "Olivetto", Email = "hoangle.q6@gmail.com", Role = UserRole.Tutor
            },
            new User
            {
                FirstName = "Gytis", LastName = "Gustang", Description = "Barzdukas", PhoneNumber = "0123123122",Address = "Viet Nam",
                Email = "hoangle.q4@gmail.com", Role = UserRole.Tutor,
                University = "University of Information Technology - VNUHCM"
            },
            tutor1, tutor2,
            new User
            {
                FirstName = "Peggy", LastName = "Scar", Gender = Gender.Female, Description = "Justice",Address = "Viet Nam",
                PhoneNumber = "0123123124", Email = "hoangle.q3123123@gmail.com", Role = UserRole.Tutor,
                University = "University of Economics HCMC (UEH)"
            },
            new User
            {
                FirstName = "Continel", LastName = "Wild", Description = "Second tutor", PhoneNumber = "0123123130",Address = "Viet Nam",
                Email = "20520728@gm.uit.edu.com", Role = UserRole.Tutor,
                University = "Ho Chi Minh City University of Technology (HCMUT)"
            },
            new User
            {
                FirstName = "Anne", LastName = "Alter", Gender = Gender.Female, Description = "Third tutor",Address = "Viet Nam",
                PhoneNumber = "0123123131", Email = "hoangle.q10@gmail.com", Role = UserRole.Tutor
            },
            new User
            {
                FirstName = "Hector", LastName = "Wunder", Description = "Barzdukas", PhoneNumber = "0123123132",Address = "Viet Nam",
                Email = "hoangle.q11@gmail.com", Role = UserRole.Tutor,
                University = "Ho Chi Minh City University of Technology (HCMUT)"
            },
            new User
            {
                FirstName = "Rosez", LastName = "Rouge", Gender = Gender.Female, Description = "Li",Address = "Viet Nam",
                PhoneNumber = "0123123133", Email = "hoangle.q12@gmail.com", Role = UserRole.Tutor,
                University = "Vietnam National University HCM, University of Economics and Law"
            },
            new User
            {
                FirstName = "Sam", LastName = "Will", Gender = Gender.Female, Description = "Justice",Address = "Viet Nam",
                PhoneNumber = "0123123124", Email = "hoangle.q312312312@gmail.com", Role = UserRole.Tutor,
                University = "Vietnam National University HCM, University of Economics and Law"
            },

            //Standard user
            standardUser,
            new User
            {
                FirstName = "Arturo", LastName = "Swift", Description = "Anand", PhoneNumber = "0123123121",Address = "Viet Nam",
                Email = "hoangle.q0@gmail.com"
            },
            new User
            {
                FirstName = "John", LastName = "Wish", Description = "Forever student", PhoneNumber = "0123123127",Address = "Viet Nam",
                Email = "hoangle.q7@gmail.com"
            },
            new User
            {
                FirstName = "Kang", LastName = "Theconquerer", Description = "Second student",Address = "Viet Nam",
                PhoneNumber = "0123123128", Email = "hoangle.q8@gmail.com"
            },
            new User
            {
                FirstName = "Shang", LastName = "Ki", Description = "Third Student", PhoneNumber = "0123123129",Address = "Viet Nam",
                Email = "hoangle.q9@gmail.com"
            },

            new User
            {
                FirstName = "Loan", LastName = "Stalk", Gender = Gender.Female, Description = "Norman",Address = "Viet Nam",
                PhoneNumber = "0123123135", Email = "hoangle.q13@gmail.com"
            },
            new User
            {
                FirstName = "Clint", LastName = "Barton", Description = "Nothing to say",Address = "Viet Nam",
                PhoneNumber = "0123123136", Email = "hoangle.q14@gmail.com"
            },
            new User
            {
                FirstName = "Kien", LastName = "Jeanner", Description = "Forever 2nd student",Address = "Viet Nam",
                PhoneNumber = "0123123137", Email = "hoangle.q15@gmail.com"
            },
            new User
            {
                FirstName = "Morgan", LastName = "Stark", Gender = Gender.Female, Description = "5th student",Address = "Viet Nam",
                PhoneNumber = "0123123138", Email = "hoangle.q16@gmail.com"
            },
            new User
            {
                FirstName = "Sam", LastName = "Cruise", Description = "6th Student", PhoneNumber = "0123123139",Address = "Viet Nam",
                Email = "hoangle.q17@gmail.com"
            },
        };

        Users = users;
        var classInfos = new List<ClassInformation>()
        {
       
            new ClassInformation
            {
                Title = "Tìm Gia Sư Dạy Laravel Tại Thủ Đức, Hồ Chí Minh", Description = "Không có nội dung mô tả",
                Address = "Thu Duc, HCMC", SubjectId = programming.Id,
                IsDeleted = false, TutorId = tutor.Id
            },
            new ClassInformation
            {
                Title = "Tìm Gia Sư Dạy Laravel Tại Thủ Đức, Hồ Chí Minh - HỌC ONLINE",
                Description = "Không có nội dung mô tả",
                Address = "Kha Vạn Cân, phường Linh Trung, Thủ Đức, Hồ Chí Minh - HỌC ONLINE",
                SubjectId = programming.Id, LearningMode = LearningMode.Online, Fee = 300, ChargeFee = 108,
                IsDeleted = false, TutorId = tutor.Id
            },
            new ClassInformation
            {
                Title = "Cần Gia Sư Môn Lập Trình Python Tại Quận Long Biên, Hà Nội",
                Description =
                    "học viên làm bên lĩnh vực kinh tế, muốn học lập trình Python để áp dụng vào công việc",
                Address = "gõ 71/1 Gia Thượng, Ngọc Thụy, quận Long Biên, Hà Nội", SubjectId = programming.Id,
                LearningMode = LearningMode.Offline, StudentGender = Gender.Female, Fee = 400, ChargeFee = 480,
                IsDeleted = false, TutorId = tutor.Id
            },
            new ClassInformation
            {
                Title = "Cần Gia Sư Dạy Lập Trình Game - HỌC Online", Description = "học viên 25 tuổi",
                Address = "Học Online", SubjectId = programming.Id, LearningMode = LearningMode.Online,
                StudentGender = Gender.Male, Fee = 400, ChargeFee = 720,
                IsDeleted = false, TutorId = tutor.Id
            },
            new ClassInformation
            {
                Title = "Cần Gia Sư Môn Lập Trình C# Tại Quận 8, Hồ Chí Minh",
                Description = "học viên cần học những môn Lý Thuyết đồ thị Lập trình thiết bị di động",
                Address = "Đào Cam Mộc, phường 4, Quận 8, Hồ Chí Minh", SubjectId = programming.Id,
                LearningMode = LearningMode.Offline, StudentGender = Gender.Female, Fee = 200, ChargeFee = 240,
                IsDeleted = false, TutorId = tutor.Id
            },
            new ClassInformation
            {
                Title = "Cần Gia Sư Môn Yoga Tại Nam Từ Liêm, Hà Nội",
                Description = "Học viên 40 tuổi",
                Address = "ngõ 199 Hồ Tùng Mậu, Nam Từ Liêm, Hà Nội", SubjectId = otherSubject.Id,
                LearningMode = LearningMode.Offline, NumberOfStudent = 2, StudentGender = Gender.None, Fee = 300,
                ChargeFee = 300,
                IsDeleted = false, TutorId = tutor1.Id
            },
            new ClassInformation
            {
                Title = "Cần Gia Sư Môn Võ Thuật Tại Quận 8, Hồ Chí Minh",
                Description = "bé học tại trung tâm, hoặc vị trí của giáo viên dạy theo lớp hoặc theo nhóm",
                Address = "khu dân cư Phú Lợi, phường 7, quận 8, Hồ Chí Minh", SubjectId = otherSubject.Id,
                LearningMode = LearningMode.Offline, StudentGender = Gender.Male, Fee = 250, ChargeFee = 500,
                IsDeleted = false, TutorId = tutor1.Id
            },
            new ClassInformation
            {
                Title = "Cần Gia Sư Môn Thanh Nhạc Tại Vũng Tàu, Bà Rịa - Vũng Tàu",
                Description = "bé học tại trung tâm, hoặc vị trí của giáo viên dạy theo lớp hoặc theo nhóm",
                Address = "khu dân cư Phú Lợi, phường 7, quận 8, Hồ Chí Minh", SubjectId = otherSubject.Id,
                LearningMode = LearningMode.Offline, StudentGender = Gender.Male, Fee = 300, ChargeFee = 600,
                IsDeleted = false, TutorId = tutor1.Id
            },
            new ClassInformation
            {
                Title = "Cần Gia Sư Môn Tiếng Hàn Tại Quận 7, Hồ Chí Minh",
                Description = "chủ yếu học tiếng hàn để giao tiếp và nói chuyện với người hàn",
                Address = "Nguyễn Thị Thập, Tân Phú, quận 7, Hồ Chí Minh", SubjectId = korean.Id,
                SessionPerWeek = 3,
                LearningMode = LearningMode.Offline, StudentGender = Gender.Female, Fee = 300, ChargeFee = 600,
                IsDeleted = false, TutorId = tutor1.Id
            },
            new ClassInformation
            {
                Title = "Cần Gia Sư Môn Tiếng Đức Tại Vũng Tàu, Bà Rịa - Vũng Tàu",
                Description = "Học viên 20 tuổi, thời gian linh động sáng, chiều hoặc tối",
                Address = "đường 30 tháng 4, Phường 12, Vũng Tàu, Bà Rịa - Vũng Tàu", SubjectId = german.Id,
                SessionPerWeek = 3,
                LearningMode = LearningMode.Offline, StudentGender = Gender.Female, Fee = 350, ChargeFee = 840,
                IsDeleted = false, TutorId = tutor1.Id
            },
            new ClassInformation
            {
                Title = "Cần Gia Sư Môn Tin Học Tại Dĩ An, Bình Dương",
                Description = "Học viên 40 tuổi",
                Address = "Trần Hưng Đạo, Đông Hòa, Dĩ An, Bình Dương", SubjectId = informatics.Id,
                SessionPerWeek = 2,
                LearningMode = LearningMode.Offline, StudentGender = Gender.Male, Fee = 300, ChargeFee = 360,
                IsDeleted = false, TutorId = tutor.Id
            },
            new ClassInformation
            {
                Title = "Tiếng Việt Cho Người Hàn Tại Bình Thạnh, Hồ Chí Minh",
                Description =
                    "Học viên là Hai bác người Hàn mới qua Việt Nam chưa được bao lâu và chưa biết gì về tiếng Việt, do ban ngày đi làm nên chỉ học được vào buổi tối, học viên giao tiếp bằng Tiếng Hàn học môn Tiếng Việt",
                Address = "Vinhomes centralpark, Nguyễn Hữu Cảnh, phường 22, Bình Thạnh, Hồ Chí Minh",
                SubjectId = vietnameses.Id, SessionPerWeek = 2, NumberOfStudent = 2,
                LearningMode = LearningMode.Offline, StudentGender = Gender.Male, Fee = 500, ChargeFee = 1500,
                IsDeleted = false, TutorId = tutor2.Id
            },
            new ClassInformation
            {
                Title = "Cần Gia Sư Môn Tiếng Anh Lớp 9 Tại Quận 2, Hồ Chí Minh",
                Description =
                    "dạy văn phạm cho bé lớp 9, giáo viên 250k/buổi/90phut, yêu cầu gia sư bên ngành Tiếng Anh",
                Address = "Hoàng Anh River View - 37 Nguyễn Văn Hưởng, Phường Thảo Điền, quận 2, Hồ Chí Minh",
                SubjectId = english.Id, SessionPerWeek = 2, NumberOfStudent = 2,
                LearningMode = LearningMode.Offline, StudentGender = Gender.Female, Fee = 200, ChargeFee = 400,
                IsDeleted = false, TutorId = tutor2.Id
            },
            new ClassInformation
            {
                Title = "Cần Gia Sư Môn Guitar Tại Vĩnh Thạnh, Cần Thơ",
                Description = "học viên 26 tuổi, học từ cơ bản đệm hát",
                Address = "ấp Thắng Lợi, xã Thạnh Lộc, huyện Vĩnh Thạnh, Cần Thơ",
                SubjectId = guitar.Id, SessionPerWeek = 2, NumberOfStudent = 1,
                LearningMode = LearningMode.Offline, StudentGender = Gender.Female, Fee = 200, ChargeFee = 400,
                IsDeleted = false, TutorId = tutor2.Id
            },
            new ClassInformation
            {
                Title = "Cần Gia Sư Môn Cờ Vua Tại Nha Trang, Khánh Hòa",
                Description = "Không có nội dung",
                Address = "đường Thích Quảng Đức, khu đô thị Hà Quang 2, phường Phước Hải, Nha Trang, Khánh Hòa",
                SubjectId = otherSubject.Id, SessionPerWeek = 2, NumberOfStudent = 1,
                LearningMode = LearningMode.Offline, StudentGender = Gender.Female, Fee = 200, ChargeFee = 200,
                IsDeleted = false, TutorId = tutor2.Id
            },
            new ClassInformation
            {
                Title = "Cần Gia Sư Môn Guitar Tại Gò Dầu, Tây Ninh",
                Description = "học viên 44 tuổi, học từ cơ bản",
                Address = "Gò Dầu, Tây Ninh",
                SubjectId = guitar.Id, SessionPerWeek = 2, NumberOfStudent = 1,
                LearningMode = LearningMode.Offline, StudentGender = Gender.Female, Fee = 200, ChargeFee = 200,
                IsDeleted = false, TutorId = tutor2.Id
            },
            new ClassInformation
            {
                Title = "Cần Gia Sư Môn Hóa Lớp 11 Tại Phường Phước Hưng, Bà Rịa Vũng Tàu",
                Description = "thứ 6 từ 18h30 - 20h00, thứ 7 từ 15h00 đến 16h30",
                Address =
                    "hẻm 271 Phan Đăng Lưu, tổ 8, khu phố Hương Điền, phường Long Hương, tp Bà Rịa, Bà Rịa Vũng Tàu",
                SubjectId = chemistry.Id, SessionPerWeek = 2, NumberOfStudent = 1,
                LearningMode = LearningMode.Offline, StudentGender = Gender.Male, Fee = 250, ChargeFee = 500,
                IsDeleted = false, TutorId = tutor2.Id
            },
            new ClassInformation
            {
                Title = "Cần Gia Sư Môn Nhảy Shuffle Dance Tại Củ Chi, Hồ Chí Minh",
                Description = "hoặc học tại Vũ Duy Chí, khu phố 2, tt Củ Chi",
                Address = "đường Tỉnh Lộ 7, ấp Chợ Cũ, Tỉnh Lộ 7, Củ Chi, Hồ Chí Minh",
                SubjectId = dance.Id, SessionPerWeek = 2, NumberOfStudent = 6, MinutePerSession = 120,
                LearningMode = LearningMode.Offline, StudentGender = Gender.Male, Fee = 900, ChargeFee = 1800,
                IsDeleted = false, TutorId = tutor2.Id
            },
        };


        ClassInformations = classInfos;
    }
}