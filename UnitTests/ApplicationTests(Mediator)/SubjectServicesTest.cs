using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Application.Services.Subjects.Commands;
using CED.Application.Services.Subjects.Queries;
using CED.Contracts;
using CED.Contracts.Subjects;
using CED.Domain.Repository;
using CED.Domain.Subjects;
using LazyCache;
using MapsterMapper;
using Moq;

namespace UnitTests.ApplicationTests_Mediator_
{
    public class SubjectServicesTest
    {
        private readonly Mock<ISubjectRepository> _mockSubjectRepo = new();
        private readonly Mock<IRepository<TutorMajor>> _mockTutorMajorRepo = new();
        private readonly Mock<IMapper> _mockMapper = new();
        private readonly Mock<IAppCache> _mockAppCache = new();

        private readonly Guid _sampleId = Guid.NewGuid();
        private readonly Guid _sampleId2 = Guid.NewGuid();
        private readonly Guid _sampleId3 = Guid.NewGuid();

        //sample values
        private List<Subject>? _subjects;
        private List<SubjectDto>? _subjectDtos;
        private Subject? _subject;
        private SubjectDto? _subjectDto;

        [SetUp]
        public void Setup()
        {
            _subject = new Subject { Id = _sampleId, Description = "Description Sample", Name = "Subject's Name Sample" };
            _subjectDto = new SubjectDto { Id = _sampleId, Description = "Description Sample", Name = "Subject's Name Sample" };

            _subjects = new List<Subject>
            { _subject,
                new Subject { Id = _sampleId2, Description = "Description Sample 2", Name = "Subject's Name Sample 2" },
                new Subject { Id = _sampleId3, Description = "Description Sample 3", Name = "Subject's Name Sample 3" }

            };

            _subjectDtos = new List<SubjectDto>
            { _subjectDto,
                new SubjectDto { Id = _sampleId2, Description = "Description Sample 2", Name = "Subject's Name Sample 2" },
                new SubjectDto { Id = _sampleId3, Description = "Description Sample 3", Name = "Subject's Name Sample 3" }

            };

            //Getbyid
            _mockSubjectRepo
              .Setup(x => x.GetById(_sampleId))
              .ReturnsAsync(_subject);
            _mockMapper
                .Setup(x => x.Map<Subject>(_subjectDto))
                .Returns(new Subject { Id = _subjectDto.Id, Description = _subjectDto.Description, Name = _subjectDto.Name });
             _mockMapper
                .Setup(x => x.Map<SubjectDto>(_subject))
                .Returns(new SubjectDto { Id = _subject.Id, Description = _subject.Description, Name = _subject.Name });

            // GetAll
            _mockSubjectRepo
               .Setup(x => x.GetAllList())
               .ReturnsAsync(_subjects);
            _mockSubjectRepo
               .Setup(x => x.GetAll())
               .Returns(_subjects.AsQueryable());
            _mockMapper
                .Setup(x => x.Map<List<SubjectDto>>(_subjects))
                .Returns(_subjectDtos);

            //Delete
            _mockSubjectRepo
                .Setup(x => x.DeleteById(_sampleId))
                .ReturnsAsync(true);
        }

        [Test]
        public async Task GetSubjectById()
        {
            var query = new GetObjectQuery<SubjectDto>() { Guid = _sampleId };
            var handler = new GetSubjectQueryHandler(_mockSubjectRepo.Object, _mockMapper.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
        }

        [Test]
        public async Task GetAllSubjects()
        {
            var query = new GetObjectQuery<PaginatedList<SubjectDto>>();
            var handler = new GetAllSubjectsQueryHandler(_mockSubjectRepo.Object,_mockTutorMajorRepo.Object, _mockMapper.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
        }

        [Test]
        public async Task CreateSubject()
        {
            var command = new CreateUpdateSubjectCommand { SubjectDto = _subjectDto };
            var handler = new CreateUpdateSubjectCommandHandler(_mockSubjectRepo.Object,_mockAppCache.Object, _mockMapper.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result);
        }

        [Test]
        public async Task DeleteSubject()
        {
            var command = new DeleteSubjectCommand ( _sampleId );
            var handler = new DeleteSubjectCommandHandler(_mockSubjectRepo.Object,_mockAppCache.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result);
        }
    }
}