namespace BudgetBuddy.Api.ViewModels;

public record CategoryViewModel(
    Guid Id,
    string Name,
    Guid? ParentCategoryId,
    Guid UserId,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);