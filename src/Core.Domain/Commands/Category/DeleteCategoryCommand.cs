using MediatR;

namespace Core.Domain.Commands.Category;

public record DeleteCategoryCommand(Guid Id) : IRequest<Unit>;