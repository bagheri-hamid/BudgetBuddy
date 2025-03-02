using Core.Domain.Enums;

namespace WebApi.ViewModels;

public record TransactionViewModel(
    long Amount,
    string Description,
    TransactionType Type,
    DateTime Date,
    Guid CategoryId,
    Guid AccountId,
    Guid UserId,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);