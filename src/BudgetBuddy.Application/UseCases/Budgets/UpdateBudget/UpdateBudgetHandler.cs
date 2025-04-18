﻿using BudgetBuddy.Application.Interfaces;
using BudgetBuddy.Domain.Budgets;
using BudgetBuddy.Domain.Exceptions;
using MediatR;

namespace BudgetBuddy.Application.UseCases.Budgets.UpdateBudget;

public class UpdateBudgetHandler(IBudgetRepository budgetRepository, ITokenHelper tokenHelper) : IRequestHandler<UpdateBudgetCommand, Unit>
{
    public async Task<Unit> Handle(UpdateBudgetCommand request, CancellationToken cancellationToken)
    {
        if (request.Amount < 1)
            throw new CanNotBeLessThanZeroException();

        if (request.StartDate == default || request.EndDate == default)
            throw new InvalidDateException();

        var budget = await budgetRepository.FindOneAsync(b => b.Id == request.Id && b.UserId == tokenHelper.GetUserId());

        if (budget == null)
            throw new ObjectNotFoundException("Budget");
        
        budget.Amount = request.Amount;
        budget.Description = request.Description;
        budget.StartDate = request.StartDate;
        budget.EndDate = request.EndDate;
        budget.UpdatedAt = DateTime.UtcNow;

        await budgetRepository.SaveChangesAsync();
        
        return Unit.Value;
    }
}