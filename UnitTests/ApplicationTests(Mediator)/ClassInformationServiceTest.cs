using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Application.Services.ClassInformations.Commands;
using CED.Application.Services.ClassInformations.Queries;
using CED.Contracts.ClassInformations;
using CED.Contracts.ClassInformations.Dtos;
using CED.Contracts.Subjects;
using CED.Contracts.Users;
using CED.Domain.ClassInformations;
using CED.Domain.Shared.ClassInformationConsts;
using CED.Domain.Subjects;
using CED.Domain.Users;
using MapsterMapper;
using Moq;

namespace UnitTests.ApplicationTests_Mediator_
{
    public class ClassInformationServiceTest
    {
        private readonly Mock<IClassInformationRepository> _mockClassInformationRepo = new();
        private readonly Mock<ISubjectRepository> _mockSubjectRepo = new();
        private readonly Mock<IUserRepository> _mockUserRepo = new();
        private readonly Mock<ITutorRepository> _mockTutorRepo = new();
        private readonly Mock<IMapper> _mockMapper = new();

        private readonly Guid _sampleId = Guid.NewGuid();
        private readonly Guid _sampleId2 = Guid.NewGuid();
        private readonly Guid _sampleId3 = Guid.NewGuid();

        //sample values
        private List<ClassInformation>? _classInformations;
        private List<ClassInformationDto>? _classInformationDtos;
        private ClassInformation? _classInformation;
        private ClassInformationDto? _classInformationDto;

        //sample values
        private List<Subject>? _subjects;
        private List<SubjectDto>? _subjectDtos;
        private Subject? _subject;
        private SubjectDto? _subjectDto;
        
        //sample values for tutors
        
        private User? _tutor;
        private TutorDto? _tutorDto;
        private List<TutorDto>? _tutorDtos;
        private List<User>? _tutorUsers;
        [SetUp]
        public void Setup()
        {
            _subject = new Subject { Id = _sampleId, Description = "Description Sample", Name = "Subject's Name Sample" };
            _subjectDto = new SubjectDto { Id = _sampleId, Description = "Description Sample", Name = "Subject's Name Sample" };

            _classInformation = new ClassInformation
            {
                Id = _sampleId, Description = "Description Sample", Title = "ClassInformation's Name Sample",
                SubjectId = _sampleId, TutorId = _sampleId
            };
            _classInformationDto = new ClassInformationDto
                { Id = _sampleId, Description = "Description Sample", Title = "ClassInformation's Name Sample",SubjectId = _sampleId,TutorDtoId = _sampleId };

            _classInformations = new List<ClassInformation>{ _classInformation,
                new ClassInformation
                {
                    Id = _sampleId2, Description = "Description Sample 2", Title = "ClassInformation's Name Sample 2",
                    SubjectId = _sampleId,TutorId = _sampleId
                },
                new ClassInformation
                {
                    Id = _sampleId3, Description = "Description Sample 3", Title = "ClassInformation's Name Sample 3",
                    SubjectId = _sampleId, TutorId = _sampleId
                }

            };

            _classInformationDtos = new List<ClassInformationDto>{ _classInformationDto,
                new ClassInformationDto
                {
                    Id = _sampleId2, Description = "Description Sample 2", Title = "ClassInformation's Name Sample 2",
                    SubjectId = _sampleId,TutorDtoId = _sampleId
                },
                new ClassInformationDto
                {
                    Id = _sampleId3, Description = "Description Sample 3", Title = "ClassInformation's Name Sample 3",
                    SubjectId = _sampleId, TutorDtoId = _sampleId
                }

            };
            
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

            
            _subjects = new List<Subject>
            { _subject,
                new Subject { Id = _sampleId2, Description = "Description Sample 2", Name = "Subject's Name Sample 2" },
                new Subject { Id = _sampleId3, Description = "Description Sample 3", Name = "Subject's Name Sample 3" }

            };
           
            //Mock subject
            _mockSubjectRepo
                .Setup(x => x.GetAllList())
                .ReturnsAsync(_subjects);
            _mockSubjectRepo
                .Setup(x => x.GetById(_sampleId))
                .ReturnsAsync(_subject);
            _mockMapper
                .Setup(x => x.Map<Subject>(_subjectDto))
                .Returns(new Subject { Id = _subjectDto.Id, Description = _subjectDto.Description, Name = _subjectDto.Name });
            _mockMapper
                .Setup(x => x.Map<SubjectDto>(_subject))
                .Returns(new SubjectDto { Id = _subject.Id, Description = _subject.Description, Name = _subject.Name });
            //Getbyid
            _mockClassInformationRepo
              .Setup(x => x.GetById(_sampleId))
              .ReturnsAsync(_classInformation);
            _mockMapper
                .Setup(x => x.Map<ClassInformationDto>(_classInformation))
                .Returns(new ClassInformationDto
                {
                    Id = _classInformation.Id, Description = _classInformation.Description,
                    Title = _classInformation.Title,
                    TutorDtoId = _classInformation.TutorId
                });
            _mockMapper
                .Setup(x => x.Map<ClassInformation>(_classInformationDto))
                .Returns(new ClassInformation
                {
                    Id = _classInformation.Id, Description = _classInformation.Description,
                    Title = _classInformation.Title,
                    TutorId = _classInformation.TutorId
                });
         

            // GetAll
            _mockClassInformationRepo
               .Setup(x => x.GetAllList())
               .ReturnsAsync(_classInformations);
            _mockMapper
                .Setup(x => x.Map<List<ClassInformationDto>>(_classInformations))
                .Returns(_classInformationDtos);

            //Delete
            _mockClassInformationRepo
                .Setup(x => x.DeleteById(_sampleId))
                .ReturnsAsync(true);
        }

        [Test]
        public async Task GetClassInformationById()
        {
            var query = new GetObjectQuery<ClassInformationDto>() { Guid = _sampleId };
            var handler = new GetClassInformationQueryHandler(_mockClassInformationRepo.Object,_mockSubjectRepo.Object,_mockUserRepo.Object, _mockMapper.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
        }

        [Test]
        public async Task GetAllClassInformations()
        {
            var query = new GetAllClassInformationsQuery() { };
            var handler = new GetAllClassInformationsQueryHandler(_mockClassInformationRepo.Object,
                _mockSubjectRepo.Object, _mockTutorRepo.Object, _mockMapper.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
        }

        // [Test]
        // public async Task CreateClassInformation()
        // {
        //     var command = new CreateUpdateClassInformationCommand { ClassInformationDto = _classInformationDto };
        //     var handler = new CreateUpdateClassInformationCommandHandler(_mockClassInformationRepo.Object,_mockMapper.Object);
        //     var result = await handler.Handle(command, CancellationToken.None);
        //
        //     Assert.True(result);
        // }
        //
        // [Test]
        // public async Task DeleteClassInformation()
        // {
        //     var command = new DeleteClassInformationCommand ( _sampleId );
        //     var handler = new DeleteClassInformationCommandHandler(_mockClassInformationRepo.Object);
        //     var result = await handler.Handle(command, CancellationToken.None);
        //
        //     Assert.True(result);
        // }
    }
}