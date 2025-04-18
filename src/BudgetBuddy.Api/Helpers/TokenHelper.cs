﻿using System.Security.Claims;
using BudgetBuddy.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace BudgetBuddy.Api.Helpers;

public class TokenHelper(IHttpContextAccessor httpContextAccessor) : ITokenHelper, IScopedDependency
{
    public Guid GetUserId()
    {
        var userIdStr = GetClaim(ClaimTypes.NameIdentifier);
        
        if (!Guid.TryParse(userIdStr, out var userId))
            throw new UnauthorizedAccessException();

        return userId;
    }

    private ClaimsPrincipal User => httpContextAccessor.HttpContext?.User ?? throw new UnauthorizedAccessException();
    private string GetClaim(string claimType) => User.FindFirst(claimType)?.Value ?? throw new UnauthorizedAccessException();
}