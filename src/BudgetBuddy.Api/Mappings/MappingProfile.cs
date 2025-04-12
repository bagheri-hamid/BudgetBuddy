using AutoMapper;
using BudgetBuddy.Api.ViewModels;
using BudgetBuddy.Api.ViewModels.V1;
using BudgetBuddy.Domain.Accounts;
using BudgetBuddy.Domain.Budgets;
using BudgetBuddy.Domain.Categories;

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