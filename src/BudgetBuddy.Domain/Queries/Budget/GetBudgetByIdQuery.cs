﻿using MediatR;

namespace BudgetBuddy.Domain.Queries.Budget;

public record GetBudgetByIdQuery(Guid Id) : IRequest<Budgets.Budget>;