namespace WebApi.ViewModels;

public record CategoryViewModel(
    Guid Id,
    string Name,
    Guid? ParentCategoryId,
    Guid UserId,
    DateTime CreatedAt,
    DateTime UpdatedAt
);