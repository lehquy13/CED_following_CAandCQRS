using System.Linq.Dynamic.Core;
using CED.Application.Services.Abstractions.CommandHandlers;
using CED.Domain.Interfaces.Services;
using CED.Domain.Repository;
using CED.Domain.Shared.ClassInformationConsts;
using CED.Domain.Subjects;
using CED.Domain.Users;
using MapsterMapper;
using Microsoft.Extensions.Logging;

namespace CED.Application.Services.Users.Tutor.ChangeInfo;

public class TutorInfoChangingCommandHandler : CreateUpdateCommandHandler<TutorInfoChangingCommand>
{
    private readonly ITutorRepository _tutorRepository;
    private readonly ICloudinaryFile _cloudinaryFile;

    private readonly IRepository<TutorMajor> _tutorMajorRepository;
    private readonly IRepository<TutorVerificationInfo> _tutorVerificationInfoRepository;

    public TutorInfoChangingCommandHandler(ITutorRepository tutorRepository,
        IRepository<TutorMajor> tutorMajorRepository,
        ICloudinaryFile cloudinaryFile,
        IRepository<TutorVerificationInfo> tutorVerificationInfoRepository,
        ILogger<TutorInfoChangingCommandHandler> logger, IMapper mapper) : base(logger, mapper)
    {
        _tutorRepository = tutorRepository;
        _cloudinaryFile = cloudinaryFile;
        _tutorMajorRepository = tutorMajorRepository;
        _tutorVerificationInfoRepository = tutorVerificationInfoRepository;
    }

    public override async Task<bool> Handle(TutorInfoChangingCommand command, CancellationToken cancellationToken)
    {
        //Check if the user existed
        var tutor = await _tutorRepository.GetById(command.TutorDto.Id);
        if (tutor is null)
        {
            throw new Exception("User doesn't exist");
        }

        var newMajorUpdate = command.SubjectIds.DistinctBy(x => x).ToList();
        var currentMajor = _tutorMajorRepository.GetAll().Where(x => x.TutorId.Equals(command.TutorDto.Id)).ToList();

        // check the subject changes
        foreach (var major in currentMajor)
        {
            if (!newMajorUpdate.Contains(major.SubjectId))
            {
                _tutorMajorRepository.Delete(major);
                _logger.LogDebug("Remove subject {0} from tutor's major", major.SubjectId);
            }
            else
            {
                var removeResult = newMajorUpdate.Remove(major.SubjectId);
                if (removeResult)
                {
                    _logger.LogDebug("Remove subject {0} from newMajorUpdateList", major.SubjectId);
                }
                else
                {
                    _logger.LogError("fail to remove the subject {0} from newMajorUpdateList", major.SubjectId);
                }
            }
        }

        foreach (var newMu in newMajorUpdate)
        {
            await _tutorMajorRepository.Insert(new TutorMajor()
            {
                TutorId = tutor.Id,
                SubjectId = newMu
            });
        }

        //Handle filepath !!! Upgrade
        if (command.FilePaths.Count > 0)
        {
            var verifications = _tutorVerificationInfoRepository
                .GetAll().Where(x => x.TutorId.Equals(tutor.Id)).ToList();
            foreach (var i in verifications)
            {
                _tutorVerificationInfoRepository.Delete(i);
            }


            //verifications = new List<TutorVerificationInfo>();
            foreach (var i in command.FilePaths)
            {
                await _tutorVerificationInfoRepository.Insert(new TutorVerificationInfo
                {
                    Image = _cloudinaryFile.UploadImage(i),
                    TutorId = tutor.Id
                });
            }
        }


        //if (tutor.Role != UserRole.Tutor) return false;
        var mappedTutor = _mapper.Map<Domain.Users.Tutor>(command.TutorDto);
        // check
        tutor.UpdateTutorInformation(mappedTutor);

        var afterUpdatedUser = _tutorRepository.Update(tutor);

        if (afterUpdatedUser is null)
        {
            return false;
        }

        return true;
    }
}