using CED.Application.Services.Users.Queries;
using CED.Domain.Shared.ClassInformationConsts;
using CED.Contracts.Users;
using CED.Domain.Users;
using MapsterMapper;
using Moq;
using CED.Application.Services.Users.Queries.Handlers;
using CED.Application.Services.Users.Commands;

namespace UnitTests.ApplicationTests
{
    public class UserServicesTest
    {
        private readonly Mock<IUserRepository> _mockUserRepo = new();
        private readonly Mock<IMapper> _mockMapper = new();

        private readonly Guid _sampleId = Guid.NewGuid();
        private readonly Guid _sampleId2 = Guid.NewGuid();
        private readonly Guid _sampleId3 = Guid.NewGuid();

        //sample values
        List<User> users;
        List<UserDto> userDtos;
        User user;

        UserDto userDto;


        User tutor;
        TutorDto tutorDto;
        List<TutorDto> tutorDtos;
        List<User> tutorUsers;

        User student;
        StudentDto studentDto;
        List<StudentDto> studentDtos;
        List<User> studentUsers;




        [SetUp]
        public void Setup()
        {
            user = new User
            {
                Id = _sampleId,
                Description = "Description Sample",
                LastName = "User's Last Name Sample",
                FirstName = "User's First Name Sample"
            };
            userDto = new UserDto
            {
                Id = _sampleId,
                Description = "Description Sample",
                LastName = "User's Last Name Sample",
                FirstName = "User's First Name Sample"
            };

            student = new User
            {
                Id = _sampleId2,
                Description = "Description Sample 2",
                LastName = "User's Last Name Sample 2",
                FirstName = "User's First Name Sample 2",
                Role = UserRole.Student
            };
            studentDto = new StudentDto
            {
                Id = _sampleId2,
                Description = "Description Sample 2",
                LastName = "User's Last Name Sample 2",
                FirstName = "User's First Name Sample 2",
                Role = UserRole.Student
            };
            studentDtos = new List<StudentDto> { studentDto };






            //tutor setup

            tutor = new User
            {
                Id = _sampleId3,
                Description = "Description Sample 3",
                LastName = "User's Last Name Sample 3",
                FirstName = "User's First Name Sample 3",
                Role = UserRole.Tutor
            };
            tutorDto = new TutorDto
            {
                Id = _sampleId3,
                Description = "Description Sample 3",
                LastName = "User's Last Name Sample 3",
                FirstName = "User's First Name Sample 3",
                Role = UserRole.Tutor
            };
            tutorDtos = new List<TutorDto>{
                tutorDto
            };


            //Getbyid
            #region Mock a user by id
            _mockUserRepo
              .Setup(x => x.GetById(_sampleId))
              .ReturnsAsync(user);

            _mockMapper
                .Setup(x => x.Map<User>(userDto))
                .Returns(new User
                {
                    Id = userDto.Id,
                    Description = userDto.Description,
                    LastName = userDto.LastName,
                    FirstName = userDto.FirstName,
                });

            _mockMapper
               .Setup(x => x.Map<UserDto>(user))
               .Returns(userDto);
            #endregion


            #region Mock a tutor info by id
            _mockUserRepo
              .Setup(x => x.GetById(_sampleId3))
              .ReturnsAsync(tutor);
            _mockMapper
              .Setup(x => x.Map<TutorDto>(tutor))
              .Returns(tutorDto);
            #endregion

            #region Mock a student info by id
            _mockUserRepo
              .Setup(x => x.GetById(_sampleId2))
              .ReturnsAsync(student);
            _mockMapper
              .Setup(x => x.Map<StudentDto>(student))
              .Returns(studentDto);
            #endregion

            #region Mock All User
            users = new List<User>
            {
                user,
                student,
                tutor
            };

            userDtos = new List<UserDto>{
                userDto,
                new UserDto
                {
                    Id = _sampleId2,
                    Description = "Description Sample 2",
                     LastName = "User's Last Name Sample 2",
                    FirstName = "User's First Name Sample 2"
                },
                new UserDto
                {
                    Id = _sampleId3,
                    Description = "Description Sample 3",
                    LastName = "User's Last Name Sample 3",
                    FirstName = "User's First Name Sample 3",
                    Role = UserRole.Tutor
                }

            };

            _mockUserRepo
             .Setup(x => x.GetAllList())
             .ReturnsAsync(users);
            _mockMapper
               .Setup(x => x.Map<List<UserDto>>(users))
               .Returns(userDtos);

            #endregion

            #region  Mock a list of TutorDTO
            tutorUsers = new List<User>
            {
                tutor
            };
            _mockUserRepo
               .Setup(x => x.GetTutors())
               .Returns(tutorUsers);
            _mockMapper
                .Setup(x => x.Map<List<TutorDto>>(tutorUsers))
                .Returns(tutorDtos);
            #endregion

            #region  Mock a list of student
            studentUsers = new List<User>
            {
                student
            };
            _mockUserRepo
               .Setup(x => x.GetStudents())
               .Returns(studentUsers);
            _mockMapper
                .Setup(x => x.Map<List<StudentDto>>(studentUsers))
                .Returns(studentDtos);
            #endregion

            #region Mock change user info
            _mockUserRepo
            .Setup(x => x.GetUserByEmail(userDto.Email))
            .ReturnsAsync(user);

            _mockUserRepo
             .Setup(x => x.Update(user))
             .Returns(user);
            #endregion


            //Delete
            _mockUserRepo
                .Setup(x => x.DeleteById(_sampleId))
                .ReturnsAsync(true);
        }

        #region GetAll and GetByID
        [Test]
        public async Task GetAllUser()
        {
            var query = new GetUsersQuery<UserDto> { };
            var handler = new GetUsersQueryHandler(_mockUserRepo.Object, _mockMapper.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.IsNotNull(result);
        }
        [Test]
        public async Task GetUserById()
        {
            var query = new GetUserByIdQuery<UserDto> { Id = _sampleId };
            var handler = new GetUserByIdQueryHandler(_mockUserRepo.Object, _mockMapper.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.IsNotNull(result);
        }
        [Test]
        public async Task GetTutorById()
        {
            var query = new GetUserByIdQuery<TutorDto> { Id = _sampleId3 };
            var handler = new GetTutorByIdQueryHandler(_mockUserRepo.Object, _mockMapper.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.IsNotNull(result);
        }
        [Test]
        public async Task GetStudentById()
        {
            var query = new GetUserByIdQuery<StudentDto> { Id = _sampleId2 };
            var handler = new GetStudentByIdQueryHandler(_mockUserRepo.Object, _mockMapper.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.IsNotNull(result);
            Assert.That(result.Role == UserRole.Student, Is.True);
        }
        [Test]
        public async Task GetAllTutors()
        {
            var query = new GetUsersQuery<TutorDto> { };
            var handler = new GetAllTutorsQueryHandler(_mockUserRepo.Object, _mockMapper.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.IsNotNull(result);
            //Assert.That(result?.FirstOrDefault()?.LastName, Is.EqualTo("User's Last Name Sample 3"));
        }
        [Test]
        public async Task GetAllStudents()
        {
            var query = new GetUsersQuery<StudentDto> { };
            var handler = new GetAllStudentsQueryHandler(_mockUserRepo.Object, _mockMapper.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.IsNotNull(result);
            //Assert.That(result?.FirstOrDefault()?.LastName, Is.EqualTo("User's Last Name Sample 3"));
        }

        #endregion


        //Update user infor
        [Test]
        public async Task ChangeUserInfo()
        {
            userDto.FirstName = "First name after updated";
            var command = new UserInfoChangingCommand(userDto);
            var handler = new UserInfoChangingCommandHandler(_mockUserRepo.Object, _mockMapper.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsTrue(result);

        }

        //[Test]
        //public async Task GetUserById()
        //{
        //    var query = new GetUserQuery { id = _sampleId };
        //    var handler = new GetUserQueryHandler(_mockUserRepo.Object, _mockMapper.Object);
        //    var result = await handler.Handle(query, CancellationToken.None);

        //    Assert.NotNull(result);
        //}

        //[Test]
        //public async Task GetAllUsers()
        //{


        //    var query = new GetAllUsersQuery { };
        //    var handler = new GetAllUsersQueryHandler(_mockUserRepo.Object, _mockMapper.Object);
        //    var result = await handler.Handle(query, CancellationToken.None);

        //    Assert.NotNull(result);
        //}

        //[Test]
        //public async Task CreateUser()
        //{
        //    var command = new CreateUpdateUserCommand { UserDto = userDto };
        //    var handler = new CreateUserCommandHandler(_mockUserRepo.Object, _mockMapper.Object);
        //    var result = await handler.Handle(command, CancellationToken.None);

        //    Assert.True(result);
        //}

        //[Test]
        //public async Task DeleteUser()
        //{
        //    var command = new DeleteUserCommand { id = _sampleId };
        //    var handler = new DeleteUserCommandHandler(_mockUserRepo.Object);
        //    var result = await handler.Handle(command, CancellationToken.None);

        //    Assert.True(result);
        //}
    }
}
