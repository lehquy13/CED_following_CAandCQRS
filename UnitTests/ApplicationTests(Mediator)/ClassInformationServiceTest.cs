using CED.Application.Services.ClassInformations.Commands;
using CED.Application.Services.ClassInformations.Queries;
using CED.Contracts.ClassInformations;
using CED.Domain.ClassInformations;
using CED.Domain.Subjects;
using MapsterMapper;
using Moq;

namespace UnitTests.ApplicationTests
{
    public class ClassInformationServiceTest
    {
        private readonly Mock<IClassInformationRepository> _mockClassInformationRepo = new();
        private readonly Mock<ISubjectRepository> _mockSubjectRepo = new();
        private readonly Mock<IMapper> _mockMapper = new();

        private readonly Guid _sampleId = Guid.NewGuid();
        private readonly Guid _sampleId2 = Guid.NewGuid();
        private readonly Guid _sampleId3 = Guid.NewGuid();

        //sample values
        List<ClassInformation> ClassInformations;
        List<ClassInformationDto> ClassInformationDtos;
        ClassInformation ClassInformation;
        ClassInformationDto ClassInformationDto;

        [SetUp]
        public void Setup()
        {
            ClassInformation = new ClassInformation { Id = _sampleId, Description = "Description Sample", Title = "ClassInformation's Name Sample" };
            ClassInformationDto = new ClassInformationDto { Id = _sampleId, Description = "Description Sample", Title = "ClassInformation's Name Sample" };

            ClassInformations = new List<ClassInformation>{ ClassInformation,
                new ClassInformation { Id = _sampleId2, Description = "Description Sample 2", Title = "ClassInformation's Name Sample 2" },
                new ClassInformation { Id = _sampleId3, Description = "Description Sample 3", Title = "ClassInformation's Name Sample 3" }

            };

            ClassInformationDtos = new List<ClassInformationDto>{ ClassInformationDto,
                new ClassInformationDto { Id = _sampleId2, Description = "Description Sample 2", Title = "ClassInformation's Name Sample 2" },
                new ClassInformationDto { Id = _sampleId3, Description = "Description Sample 3", Title = "ClassInformation's Name Sample 3" }

            };

            //Getbyid
            _mockClassInformationRepo
              .Setup(x => x.GetById(_sampleId))
              .ReturnsAsync(ClassInformation);
            _mockMapper
                .Setup(x => x.Map<ClassInformationDto>(ClassInformation))
                .Returns(new ClassInformationDto { Id = ClassInformation.Id, Description = ClassInformation.Description, Title = ClassInformation.Title });
            _mockMapper
                .Setup(x => x.Map<ClassInformation>(ClassInformationDto))
                .Returns(new ClassInformation { Id = ClassInformation.Id, Description = ClassInformation.Description, Title = ClassInformation.Title });
         

            // GetAll
            _mockClassInformationRepo
               .Setup(x => x.GetAllList())
               .ReturnsAsync(ClassInformations);
            _mockMapper
                .Setup(x => x.Map<List<ClassInformationDto>>(ClassInformations))
                .Returns(ClassInformationDtos);

            //Delete
            _mockClassInformationRepo
                .Setup(x => x.DeleteById(_sampleId))
                .ReturnsAsync(true);
        }

        [Test]
        public async Task GetClassInformationById()
        {
            var query = new GetClassInformationQuery { Id = _sampleId };
            var handler = new GetClassInformationQueryHandler(_mockClassInformationRepo.Object,_mockSubjectRepo.Object, _mockMapper.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
        }

        [Test]
        public async Task GetAllClassInformations()
        {
           
           
            var query = new GetAllClassInformationsQuery { };
            var handler = new GetAllClassInformationsQueryHandler(_mockClassInformationRepo.Object, _mockSubjectRepo.Object , _mockMapper.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
        }

        [Test]
        public async Task CreateClassInformation()
        {
            var command = new CreateUpdateClassInformationCommand { ClassInformationDto = ClassInformationDto };
            var handler = new CreateUpdateClassInformationCommandHandler(_mockClassInformationRepo.Object,_mockMapper.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result);
        }

        [Test]
        public async Task DeleteClassInformation()
        {
            var command = new DeleteClassInformationCommand ( _sampleId );
            var handler = new DeleteClassInformationCommandHandler(_mockClassInformationRepo.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result);
        }
    }
}