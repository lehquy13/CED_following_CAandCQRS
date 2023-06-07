using CED.Contracts.Users;
using CED.Domain.Users;
using Mapster;

namespace CED.Application.Mapping;

public class UserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<LearnerDto, TutorDto>();

        config.NewConfig<TutorDto, User>();
        config.NewConfig<UserDto, User>()
            .Map(des => des.FirstName, src => src.FirstName)
            .Map(des => des.LastName, src => src.LastName)
            .Map(des => des.Gender, src => src.Gender)
            .Map(des => des.BirthYear, src => src.BirthYear)
            .Map(des => des.Address, src => src.Address)
            .Map(des => des.Description, src => src.Description)
            .Map(des => des.Email, src => src.Email)
            .Map(des => des.PhoneNumber, src => src.PhoneNumber)
            .Map(des => des.Role, src => src.Role)
            // .Map(des => des.AcademicLevel, src => src.AcademicLevel)
            // .Map(des => des.University, src => src.University )
            // .Map(des => des.IsVerified, src => src.IsVerified)
            //.Map(des => des.WardId, src => src.WardId)
            .Ignore(des => des.Password)
            ;

        config.NewConfig<TutorDto, Tutor>()
            .Map(des => des.AcademicLevel, src => src.AcademicLevel)
            .Map(des => des.University, src => src.University)
            .Map(des => des.IsVerified, src => src.IsVerified)
            .Map(des => des.Rate, src => src.Rate);
        config.NewConfig<TutorVerificationInfo, TutorVerificationInfo>()
            .Map(des => des.TutorId, src => src.Id)
            .Map(des => des.Image, src => src.Image);
        
        
            

        config.NewConfig< (User,Tutor), TutorDto>()
            .Map(des => des.FirstName, src => src.Item1.FirstName)
            .Map(des => des.LastName, src => src.Item1.LastName)
            .Map(des => des.Gender, src => src.Item1.Gender)
            .Map(des => des.BirthYear, src => src.Item1.BirthYear)
            .Map(des => des.Address, src => src.Item1.Address)
            .Map(des => des.Description, src => src.Item1.Description)
            .Map(des => des.Email, src => src.Item1.Email)
            .Map(des => des.PhoneNumber, src => src.Item1.PhoneNumber)
            .Map(des => des.Role, src => src.Item1.Role)
            .Map(des => des.AcademicLevel, src => src.Item2.AcademicLevel)
            .Map(des => des.University, src => src.Item2.University)
            .Map(des => des.IsVerified, src => src.Item2.IsVerified)
            .Map(des => des.Rate, src => src.Item2.Rate);
        config.NewConfig<User, UserDto>();
        config.NewConfig<User, LearnerDto>();
    }
}