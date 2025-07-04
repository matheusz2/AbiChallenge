using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Models;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Users.GetAllUsers;

/// <summary>
/// Handler for GetAllUsersCommand
/// </summary>
public class GetAllUsersHandler : IRequestHandler<GetAllUsersCommand, GetAllUsersResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of GetAllUsersHandler
    /// </summary>
    /// <param name="userRepository">The user repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public GetAllUsersHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetAllUsersCommand request
    /// </summary>
    /// <param name="command">The GetAllUsers command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The result containing the list of users</returns>
    public async Task<GetAllUsersResult> Handle(GetAllUsersCommand command, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync(command.Page, command.PageSize, command.Order, command.Filter, cancellationToken);
        var totalItems = await GetTotalCountAsync(command.Filter, cancellationToken);
        var totalPages = (int)System.Math.Ceiling((double)totalItems / command.PageSize);

        return new GetAllUsersResult
        {
            Data = _mapper.Map<List<GetAllUsersItemResult>>(users),
            TotalItems = totalItems,
            CurrentPage = command.Page,
            TotalPages = totalPages
        };
    }

    private async Task<int> GetTotalCountAsync(UserFilter? filter, CancellationToken cancellationToken)
    {
        var allUsers = await _userRepository.GetAllAsync(1, int.MaxValue, null, filter, cancellationToken);
        return allUsers.Count;
    }
} 