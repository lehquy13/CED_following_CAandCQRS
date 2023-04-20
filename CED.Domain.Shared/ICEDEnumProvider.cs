namespace CED.Domain.Shared;
public interface ICEDEnumProvider
{

    static List<string>? Roles { get;  }
    static List<string>? Genders { get;  }
    static List<string>? AcademicLevels { get;  }

}

