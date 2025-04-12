using AutoMapper;
using BudgetBuddy.Api.ViewModels;
using BudgetBuddy.Domain.Entities;

namespace BudgetBuddy.Api.Mappings;

/// <inheritdoc />
public class MappingProfile : Profile
{
     /// <inheritdoc />
     public MappingProfile()
     {
          CreateMap<Category, CategoryViewModel>();
          CreateMap<Account, AccountViewModel>();
          CreateMap<Budget, BudgetViewModel>();
     }
}