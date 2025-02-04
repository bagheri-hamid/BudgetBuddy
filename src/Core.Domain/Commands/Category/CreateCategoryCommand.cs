using System.Text.Json.Serialization;
using MediatR;

namespace Core.Domain.Commands.Category;

/// <summary>
/// Command for creating a new category
/// </summary>
public record CreateCategoryCommand(string Name, Guid ParentCategoryId) : IRequest<Entities.Category>;