using AutoMapper;
using BudgetBuddy.Api.Helpers;
using BudgetBuddy.Api.ViewModels.V1;
using BudgetBuddy.Application.UseCases.Transactions.CreateTransaction;
using BudgetBuddy.Application.UseCases.Transactions.DeleteTransaction;
using BudgetBuddy.Application.UseCases.Transactions.GetAllTransactions;
using BudgetBuddy.Application.UseCases.Transactions.GetTransactionById;
using BudgetBuddy.Application.UseCases.Transactions.UpdateTransaction;
using BudgetBuddy.Domain.Enums;
using BudgetBuddy.Domain.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BudgetBuddy.Api.Controllers.V1;

public class TransactionController(IMediator mediator, IMapper mapper) : AuthorizedController
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTransactionCommand command)
    {
        var transaction = await mediator.Send(command);
        var transactionViewModel = mapper.Map<TransactionViewModel>(transaction);

        return ResponseHelper.CreateResponse(201, MessageEnum.CreatedSuccessfully.GetDescription(), true, transactionViewModel);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateTransactionCommand command)
    {
        var transaction = await mediator.Send(command);
        var transactionViewModel = mapper.Map<TransactionViewModel>(transaction);
        return ResponseHelper.CreateResponse(200, MessageEnum.UpdatedSuccessfully.GetDescription(), true, transactionViewModel);
    }
    
    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await mediator.Send(new DeleteTransactionCommand(id));
        
        return ResponseHelper.CreateResponse<object>(202, MessageEnum.DeletedSuccessfully.GetDescription(), true);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllTransactionsQuery query)
    {
        var transactions = await mediator.Send(query);
        var transactionViewModels = mapper.Map<List<TransactionViewModel>>(transactions);

        return ResponseHelper.CreateSuccessResponse(transactionViewModels);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var transaction = await mediator.Send(new GetTransactionByIdQuery(id));
        var transactionViewModel = mapper.Map<TransactionViewModel>(transaction);
        
        return ResponseHelper.CreateSuccessResponse(transactionViewModel);
    }
}