using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Application.Services.Users.Commands;
using CED.Application.Services.Users.Queries;
using CED.Application.Services.Users.Queries.CustomerQueries;
using CED.Application.Services.Users.Queries.Handlers;
using CED.Contracts.Users;
using CED.Domain.Repository;
using CED.Domain.Shared.ClassInformationConsts;
using CED.Domain.Subjects;
using CED.Domain.Users;
using MapsterMapper;
using Moq;

namespace UnitTests.ApplicationTests_Mediator_
{
    public class UserServicesTest
    {
        private readonly Mock<IUserRepository> _mockUserRepo = new();
        private readonly Mock<ISubjectRepository> _mockSubjectRepo = new();
        private readonly Mock<IRepository<TutorMajor>> _mockTutorMajorRepo = new();
        private readonly Mock<IMapper> _mockMapper = new();

        private readonly Guid _sampleId = Guid.NewGuid();
        private readonly Guid _sampleId2 = Guid.NewGuid();
        private readonly Guid _sampleId3 = Guid.NewGuid();

        //sample values
        private List<User>? _users;
        private List<UserDto>? _userDtos;
        private User? _user;

        private UserDto? _userDto;


        private User? _tutor;
        private TutorDto? _tutorDto;
        private List<TutorDto>? _tutorDtos;
        private List<User>? _tutorUsers;

        private User? _student;
        private StudentDto? _studentDto;
        private List<StudentDto>? _studentDtos;
        private List<User>? _studentUsers;




        [SetUp]
        public void Setup()
        {
            _user = new User
            {
                Id = _sampleId,
                Description = "Description Sample",
                LastName = "User's Last Name Sample",
                FirstName = "User's First Name Sample"
            };
            _userDto = new UserDto
            {
                Id = _sampleId,
                Description = "Description Sample",
                LastName = "User's Last Name Sample",
                FirstName = "User's First Name Sample"
            };

            _student = new User
            {
                Id = _sampleId2,
                Description = "Description Sample 2",
                LastName = "User's Last Name Sample 2",
                FirstName = "User's First Name Sample 2",
                Role = UserRole.Student
            };
            _studentDto = new StudentDto
            {
                Id = _sampleId2,
                Description = "Description Sample 2",
                LastName = "User's Last Name Sample 2",
                FirstName = "User's First Name Sample 2",
                Role = UserRole.Student
            };
            _studentDtos = new List<StudentDto> { _studentDto };






            //tutor setup

            _tutor = new User
            {
                Id = _sampleId3,
                Description = "Description Sample 3",
                LastName = "User's Last Name Sample 3",
                FirstName = "User's First Name Sample 3",
                Role = UserRole.Tutor
            };
            _tutorDto = new TutorDto
            {
                Id = _sampleId3,
                Description = "Description Sample 3",
                LastName = "User's Last Name Sample 3",
                FirstName = "User's First Name Sample 3",
                Role = UserRole.Tutor
            };
            _tutorDtos = new List<TutorDto>{
                _tutorDto
            };


            //Getbyid
            #region Mock a user by id
            _mockUserRepo
              .Setup(x => x.GetById(_sampleId))
              .ReturnsAsync(_user);

            _mockMapper
                .Setup(x => x.Map<User>(_userDto))
                .Returns(new User
                {
                    Id = _userDto.Id,
                    Description = _userDto.Description,
                    LastName = _userDto.LastName,
                    FirstName = _userDto.FirstName,
                });

            _mockMapper
               .Setup(x => x.Map<UserDto>(_user))
               .Returns(_userDto);
            #endregion


            #region Mock a tutor info by id
            _mockUserRepo
              .Setup(x => x.GetById(_sampleId3))
              .ReturnsAsync(_tutor);
            _mockMapper
              .Setup(x => x.Map<TutorDto>(_tutor))
              .Returns(_tutorDto);
            #endregion

            #region Mock a student info by id
            _mockUserRepo
              .Setup(x => x.GetById(_sampleId2))
              .ReturnsAsync(_student);
            _mockMapper
              .Setup(x => x.Map<StudentDto>(_student))
              .Returns(_studentDto);
            #endregion

            #region Mock All User
            _users = new List<User>
            {
                _user,
                _student,
                _tutor
            };

            _userDtos = new List<UserDto>{
                _userDto,
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
             .ReturnsAsync(_users);
            _mockMapper
               .Setup(x => x.Map<List<UserDto>>(_users))
               .Returns(_userDtos);

            #endregion

            #region  Mock a list of TutorDTO
            _tutorUsers = new List<User>
            {
                _tutor
            };
            _mockUserRepo
               .Setup(x => x.GetTutors())
               .Returns(_tutorUsers);
            _mockMapper
                .Setup(x => x.Map<List<TutorDto>>(_tutorUsers))
                .Returns(_tutorDtos);
            #endregion

            #region  Mock a list of student
            _studentUsers = new List<User>
            {
                _student
            };
            _mockUserRepo
               .Setup(x => x.GetStudents())
               .Returns(_studentUsers);
            _mockMapper
                .Setup(x => x.Map<List<StudentDto>>(_studentUsers))
                .Returns(_studentDtos);
            #endregion

            #region Mock change user info
            _mockUserRepo
            .Setup(x => x.GetUserByEmail(_userDto.Email))
            .ReturnsAsync(_user);

            _mockUserRepo
             .Setup(x => x.Update(_user))
             .Returns(_user);
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
            var query = new GetObjectQuery<List<UserDto>>(){ };
            var handler = new GetUsersQueryHandler(_mockUserRepo.Object, _mockMapper.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.IsNotNull(result);
        }
        [Test]
        public async Task GetUserById()
        {
            var query = new GetObjectQuery<UserDto>() { Guid = _sampleId };
            var handler = new GetUserByIdQueryHandler(_mockUserRepo.Object,_mockSubjectRepo.Object,_mockTutorMajorRepo.Object, _mockMapper.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.IsNotNull(result);
        }
        [Test]
        public async Task GetTutorById()
        {
            var query = new GetObjectQuery<TutorDto>() { Guid = _sampleId3 };
            var handler = new GetTutorByIdQueryHandler(_mockUserRepo.Object, _mockMapper.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.IsNotNull(result);
        }
        [Test]
        public async Task GetStudentById()
        {
            var query = new GetObjectQuery<StudentDto>() { Guid = _sampleId2 };
            var handler = new GetStudentByIdQueryHandler(_mockUserRepo.Object, _mockMapper.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.IsNotNull(result);
            Assert.That(result.Role == UserRole.Student, Is.True);
        }
        [Test]
        public async Task GetAllTutors()
        {
            var query = new GetAllTutorInformationsAdvancedQuery() { };
            var handler = new GetAllTutorInformationsAdvancedQueryHandler(_mockSubjectRepo.Object,_mockUserRepo.Object,_mockTutorMajorRepo.Object, _mockMapper.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.IsNotNull(result);
            //Assert.That(result?.FirstOrDefault()?.LastName, Is.EqualTo("User's Last Name Sample 3"));
        }
        [Test]
        public async Task GetAllStudents()
        {
            var query = new GetObjectQuery<List<StudentDto>>() { };
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
            _userDto.FirstName = "First name after updated";
            var command = new UserInfoChangingCommand(_userDto);
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
