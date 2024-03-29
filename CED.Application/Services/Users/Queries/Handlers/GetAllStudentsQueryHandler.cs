﻿using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts;
using CED.Contracts.Users;
using CED.Domain.Users;
using FluentResults;
using MapsterMapper;

namespace CED.Application.Services.Users.Queries.Handlers;

public class GetAllStudentsQueryHandler : GetAllQueryHandler<GetObjectQuery<PaginatedList<LearnerDto>>, LearnerDto>
{
    private readonly IUserRepository _userRepository;

    public GetAllStudentsQueryHandler(IUserRepository userRepository, IMapper mapper) : base(mapper)
    {
        _userRepository = userRepository;
    }

    public override async Task<Result<PaginatedList<LearnerDto>>> Handle(GetObjectQuery<PaginatedList<LearnerDto>> query,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        try
        {
            var users = await _userRepository.GetStudents();
            var result = _mapper.Map<List<LearnerDto>>(users.Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize).ToList());
            
            return PaginatedList<LearnerDto>.CreateAsync(result, query.PageIndex, query.PageSize, users.Count);
             
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}