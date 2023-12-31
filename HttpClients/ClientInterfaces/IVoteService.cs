﻿using Assignment1.DTOs;
using Assignment1.Models;

namespace HttpClients.ClientInterfaces;

public interface IVoteService
{
    Task CreateAsync(VoteCreationDto dto);
    Task<ICollection<Vote>> GetAsync( int? postId, string? username);
    Task UpdateAsync(UpdateVoteDto dto);
    Task DeleteAsync(int voteId);
}