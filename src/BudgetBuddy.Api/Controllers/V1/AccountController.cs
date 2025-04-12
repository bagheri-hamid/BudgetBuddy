using AutoMapper;
using BudgetBuddy.Api.Helpers;
using BudgetBuddy.Api.ViewModels;
using BudgetBuddy.Api.ViewModels.V1;
using BudgetBuddy.Application.UseCases.Accounts.CreateAccount;
using BudgetBuddy.Application.UseCases.Accounts.DeleteAccount;
using BudgetBuddy.Application.UseCases.Accounts.GetAccountById;
using BudgetBuddy.Application.UseCases.Accounts.GetAllAccounts;
using BudgetBuddy.Application.UseCases.Accounts.UpdateAccount;
using BudgetBuddy.Domain.Enums;
using BudgetBuddy.Domain.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BudgetBuddy.Api.Controllers.V1;

public class AccountController(IMediator mediator, IMapper mapper) : AuthorizedController
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAccountCommand command)
    {
        var account = await mediator.Send(command);
        var accountViewModel = mapper.Map<AccountViewModel>(account);

        return ResponseHelper.CreateResponse(201, MessageEnum.CreatedSuccessfully.GetDescription(), true, accountViewModel);
    }
    
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateAccountCommand command)
    {
        var account = await mediator.Send(command);
        var accountViewModel = mapper.Map<AccountViewModel>(account);

        return ResponseHelper.CreateResponse(201, MessageEnum.UpdatedSuccessfully.GetDescription(), true, accountViewModel);
    }
    
    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await mediator.Send(new DeleteAccountCommand(id));
        
        return ResponseHelper.CreateResponse<object>(202, MessageEnum.DeletedSuccessfully.GetDescription(), true);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllAccountsQuery query)
    {
        var accounts = await mediator.Send(query);
        var accountsViewModel = mapper.Map<List<AccountViewModel>>(accounts);

        return ResponseHelper.CreateSuccessResponse(accountsViewModel);
    }
    
    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var account = await mediator.Send(new GetAccountByIdQuery(id));
        var accountViewModel = mapper.Map<AccountViewModel>(account);

        return ResponseHelper.CreateSuccessResponse(accountViewModel);
    }
}