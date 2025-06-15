using AutoMapper;
using BudgetBuddy.Api.Helpers;
using BudgetBuddy.Api.ViewModels.V1;
using BudgetBuddy.Application.UseCases.Budgets.CreateBudget;
using BudgetBuddy.Application.UseCases.Budgets.DeleteBudget;
using BudgetBuddy.Application.UseCases.Budgets.GetAllBudgets;
using BudgetBuddy.Application.UseCases.Budgets.GetBudgetById;
using BudgetBuddy.Application.UseCases.Budgets.UpdateBudget;
using BudgetBuddy.Domain.Enums;
using BudgetBuddy.Domain.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BudgetBuddy.Api.Controllers.V1;

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

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await mediator.Send(new DeleteBudgetCommand(id));
        return ResponseHelper.CreateResponse<object>(202, MessageEnum.DeletedSuccessfully.GetDescription(), true);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllBudgetsQuery query)
    {
        var budgets = await mediator.Send(query);
        var budgetsViewModel = mapper.Map<List<BudgetViewModel>>(budgets);

        return ResponseHelper.CreateSuccessResponse(budgetsViewModel);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAll(Guid id)
    {
        var budget = await mediator.Send(new GetBudgetByIdQuery(id));
        var budgetViewModel = mapper.Map<BudgetViewModel>(budget);

        return ResponseHelper.CreateSuccessResponse(budgetViewModel);
    }
}