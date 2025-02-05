using MediatR;

namespace Core.Domain.Commands.Category;

public record UpdateCategoryCommand(Guid Id, string Name) : IRequest<Unit>;