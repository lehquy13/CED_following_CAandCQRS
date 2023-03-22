using CED.Application.Services.UsersInformations.Queries;
using CED.Domain.Shared.ClassInformationConsts;
using CED.Contracts.Users;
using CED.Domain.Users;
using MapsterMapper;
using Moq;
using NUnit.Framework.Constraints;

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




            users = new List<User>
            {
                user,
                new User
                {
                    Id = _sampleId2,
                    Description = "Description Sample 2",
                    LastName = "User's Last Name Sample 2",
                    FirstName = "User's First Name Sample 2"
                },
                new User
                {
                    Id = _sampleId3,
                    Description = "Description Sample 3",
                    LastName = "User's Last Name Sample 3",
                    FirstName = "User's First Name Sample 3",
                    Role = UserRole.Tutor
                }
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

            // GetAll
            _mockUserRepo
               .Setup(x => x.GetAllList())
               .ReturnsAsync(users);
            _mockUserRepo
               .Setup(x => x.GetAll())
               .Returns(users.AsQueryable());
            _mockMapper
                .Setup(x => x.Map<List<UserDto>>(users))
                .Returns(userDtos);
            //wrong setup
            _mockMapper
                .Setup(x => x.Map<List<TutorDto>>(users))
                .Returns(tutorDtos);
     

            //Delete
            _mockUserRepo
                .Setup(x => x.DeleteById(_sampleId))
                .ReturnsAsync(true);
        }

        [Test]
        public async Task GetAllUser()
        {
            var query = new GetUsersQuery<UserDto> { UserRole = UserRole.All };
            var handler = new GetUsersQueryHandler<UserDto>(_mockUserRepo.Object, _mockMapper.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.IsNotNull(result);
        } 
        [Test]
        public async Task GetUserById()
        {
            var query = new GetUserByIdQuery<UserDto> { Id = _sampleId };
            var handler = new GetUserByIdQueryHandler<UserDto>(_mockUserRepo.Object, _mockMapper.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.IsNotNull(result);
        }
        [Test]
        public async Task GetAllTutors()
        {
            var query = new GetUsersQuery<TutorDto> { UserRole = UserRole.Tutor };
            var handler = new GetUsersQueryHandler<TutorDto>(_mockUserRepo.Object, _mockMapper.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.IsNotNull(result);
            //Assert.That(result?.FirstOrDefault()?.LastName, Is.EqualTo("User's Last Name Sample 3"));
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