namespace WebApi.ViewModels;

public record AccountViewModel(
    Guid Id,
    string Name,
    string Type,
    long Balance,
    Guid UserId,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);