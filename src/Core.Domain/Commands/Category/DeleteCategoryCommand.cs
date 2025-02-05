using MediatR;

namespace Core.Domain.Commands.Category;

public class DeleteCategoryCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}