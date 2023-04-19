﻿using CED.Domain.Shared.ClassInformationConsts;

namespace CED.Domain.Users;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetUserByEmail(string email);
    List<User> GetTutors();
    List<User> GetStudents();
    List<User> GetUsersByRole(UserRole userRole = UserRole.All);
}
