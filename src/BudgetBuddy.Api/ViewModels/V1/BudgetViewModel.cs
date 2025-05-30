﻿namespace BudgetBuddy.Api.ViewModels.V1;

public record BudgetViewModel(
    Guid Id,
    long Amount,
    string? Description,
    DateTime StartDate,
    DateTime EndDate,
    Guid CategoryId,
    DateTime CreatedAt,
    DateTime UpdatedAt
);