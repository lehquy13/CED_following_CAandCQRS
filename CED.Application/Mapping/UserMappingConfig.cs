using CED.Contracts.Users;
using CED.Domain.Users;
using Mapster;

namespace CED.Application.Mapping;

public class UserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {

        config.NewConfig<StudentDto, TutorDto>();
            
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
            .Map(des => des.AcademicLevel, src => src.AcademicLevel)
            .Map(des => des.University, src => src.University)
            .Map(des => des.IsVerified, src => src.IsVerified)
            //.Map(des => des.WardId, src => src.WardId)
            .Ignore(des => des.Password)
            ;
           
        

        config.NewConfig<User, TutorDto>();
        config.NewConfig<User, UserDto>();
        config.NewConfig<User, StudentDto>();

    }
}

