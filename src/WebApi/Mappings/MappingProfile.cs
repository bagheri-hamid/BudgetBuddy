using AutoMapper;
using Core.Domain.Entities;
using WebApi.ViewModels;

namespace WebApi.Mappings;

/// <inheritdoc />
public class MappingProfile : Profile
{
     /// <inheritdoc />
     public MappingProfile()
     {
          CreateMap<Category, CategoryViewModel>();
          CreateMap<Account, AccountViewModel>();
     }
}