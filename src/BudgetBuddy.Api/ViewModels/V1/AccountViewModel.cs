namespace BudgetBuddy.Api.ViewModels.V1;

public record AccountViewModel(
    Guid Id,
    string Name,
    string Type,
    long Balance,
    Guid UserId,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);