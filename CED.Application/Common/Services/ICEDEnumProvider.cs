namespace CED.Application.Common.Services;
public interface ICEDEnumProvider
{

    public List<string> GetRoles();
    public List<string> GetGenders();
    public List<string> GetAcademicLevels();

}

