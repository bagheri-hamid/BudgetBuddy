using AutoMapper;
using Core.Domain.Commands.Budget;
using Core.Domain.Enums;
using Core.Domain.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;
using WebApi.ViewModels;

namespace WebApi.Controllers.V1;

public class BudgetController(IMediator mediator, IMapper mapper) : AuthorizedController
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBudgetCommand command)
    {
        var budget = await mediator.Send(command);
        var budgetViewModel = mapper.Map<BudgetViewModel>(budget);
        
        return ResponseHelper.CreateResponse(201,  MessageEnum.CreatedSuccessfully.GetDescription(), true, budgetViewModel);
    }
    
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateBudgetCommand command)
    {
        await mediator.Send(command);
        return ResponseHelper.CreateResponse<object>(204,  MessageEnum.UpdatedSuccessfully.GetDescription(), true);
    }
}