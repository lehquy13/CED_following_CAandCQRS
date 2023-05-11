﻿using CED.Domain.Shared.ClassInformationConsts;

namespace CED.Domain.Shared;

public class EnumProvider
{
    public static List<string> Roles { get; } = Enum.GetNames(typeof(UserRole))
                                                    .AsEnumerable()
                                                    .Where(x => x != "All" && x != "Undefined")
                                                    .ToList();

    public static List<string>? Genders { get; } = Enum.GetNames(typeof(Gender))
                                                       .AsEnumerable()
                                                       .Where(x => x != "None")
                                                       .ToList();

    public static List<string>? AcademicLevels { get; } = Enum.GetNames(typeof(AcademicLevel))
                                              .ToList();
    public static List<string>? LearningModes { get; } = Enum.GetNames(typeof(LearningMode))
                                              .ToList();

    public static List<string>? Status { get; } = Enum.GetNames(typeof(Status))
                                              .ToList();

}