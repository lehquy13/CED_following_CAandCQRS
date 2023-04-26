using CED.Application.Services.Subjects.Commands;
using CED.Application.Services.Subjects.Queries;
using CED.Contracts.Subjects;
using CED.Domain.Subjects;
using MapsterMapper;
using Moq;

namespace UnitTests.ApplicationTests
{
    public class SubjectServicesTest
    {
        private readonly Mock<ISubjectRepository> _mockSubjectRepo = new();
        private readonly Mock<IMapper> _mockMapper = new();

        private readonly Guid _sampleId = Guid.NewGuid();
        private readonly Guid _sampleId2 = Guid.NewGuid();
        private readonly Guid _sampleId3 = Guid.NewGuid();

        //sample values
        List<Subject> subjects;
        List<SubjectDto> subjectDtos;
        Subject subject;
        SubjectDto subjectDto;

        [SetUp]
        public void Setup()
        {
            subject = new Subject { Id = _sampleId, Description = "Description Sample", Name = "Subject's Name Sample" };
            subjectDto = new SubjectDto { Id = _sampleId, Description = "Description Sample", Name = "Subject's Name Sample" };

            subjects = new List<Subject>{ subject,
                new Subject { Id = _sampleId2, Description = "Description Sample 2", Name = "Subject's Name Sample 2" },
                new Subject { Id = _sampleId3, Description = "Description Sample 3", Name = "Subject's Name Sample 3" }

            };

            subjectDtos = new List<SubjectDto>{ subjectDto,
                new SubjectDto { Id = _sampleId2, Description = "Description Sample 2", Name = "Subject's Name Sample 2" },
                new SubjectDto { Id = _sampleId3, Description = "Description Sample 3", Name = "Subject's Name Sample 3" }

            };

            //Getbyid
            _mockSubjectRepo
              .Setup(x => x.GetById(_sampleId))
              .ReturnsAsync(subject);
            _mockMapper
                .Setup(x => x.Map<Subject>(subjectDto))
                .Returns(new Subject { Id = subjectDto.Id, Description = subjectDto.Description, Name = subjectDto.Name });
             _mockMapper
                .Setup(x => x.Map<SubjectDto>(subject))
                .Returns(new SubjectDto { Id = subject.Id, Description = subject.Description, Name = subject.Name });

            // GetAll
            _mockSubjectRepo
               .Setup(x => x.GetAllList())
               .ReturnsAsync(subjects);
            _mockMapper
                .Setup(x => x.Map<List<SubjectDto>>(subjects))
                .Returns(subjectDtos);

            //Delete
            _mockSubjectRepo
                .Setup(x => x.DeleteById(_sampleId))
                .ReturnsAsync(true);
        }

        [Test]
        public async Task GetSubjectById()
        {
            var query = new GetSubjectQuery { Id = _sampleId };
            var handler = new GetSubjectQueryHandler(_mockSubjectRepo.Object, _mockMapper.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
        }

        [Test]
        public async Task GetAllSubjects()
        {
           
           
            var query = new GetAllSubjectsQuery { };
            var handler = new GetAllSubjectsQueryHandler(_mockSubjectRepo.Object, _mockMapper.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
        }

        [Test]
        public async Task CreateSubject()
        {
            var command = new CreateUpdateSubjectCommand { SubjectDto = subjectDto };
            var handler = new CreateSubjectCommandHandler(_mockSubjectRepo.Object, _mockMapper.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result);
        }

        [Test]
        public async Task DeleteSubject()
        {
            var command = new DeleteSubjectCommand ( _sampleId );
            var handler = new DeleteSubjectCommandHandler(_mockSubjectRepo.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result);
        }
    }
}