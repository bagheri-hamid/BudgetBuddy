﻿using AutoMapper;
using Core.Domain.Commands.Account;
using Core.Domain.Enums;
using Core.Domain.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;
using WebApi.ViewModels;

namespace WebApi.Controllers.V1;

public class AccountController(IMediator mediator, IMapper mapper) : AuthorizedController
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAccountCommand command)
    {
        var account = await mediator.Send(command);
        var accountViewModel = mapper.Map<AccountViewModel>(account);

        return ResponseHelper.CreateResponse(201, MessageEnum.CreatedSuccessfully.GetDescription(), true, accountViewModel);
    }
}