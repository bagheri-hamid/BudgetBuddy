﻿using MediatR;

namespace Core.Domain.Queries.Category;

public record GetAllCategoriesQuery(string? Name, int Offset = 0, int Limit = 20) : IRequest<List<Entities.Category>>;