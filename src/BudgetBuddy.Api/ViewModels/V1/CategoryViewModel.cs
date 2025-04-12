namespace BudgetBuddy.Api.ViewModels.V1;

public record CategoryViewModel(
    Guid Id,
    string Name,
    Guid? ParentCategoryId,
    Guid UserId,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);