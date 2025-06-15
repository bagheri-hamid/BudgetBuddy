using AutoMapper;
using BudgetBuddy.Api.Helpers;
using BudgetBuddy.Api.ViewModels.V1;
using BudgetBuddy.Application.UseCases.Categories.CreateCategory;
using BudgetBuddy.Application.UseCases.Categories.DeleteCategory;
using BudgetBuddy.Application.UseCases.Categories.GetAllCategories;
using BudgetBuddy.Application.UseCases.Categories.GetCategoryById;
using BudgetBuddy.Application.UseCases.Categories.UpdateCategory;
using BudgetBuddy.Domain.Enums;
using BudgetBuddy.Domain.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BudgetBuddy.Api.Controllers.V1;

public class CategoryController(IMediator mediator, IMapper mapper) : AuthorizedController
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryCommand command)
    {
        var category = await mediator.Send(command);
        var categoryViewModel = mapper.Map<CategoryViewModel>(category);

        return ResponseHelper.CreateResponse(201, MessageEnum.CreatedSuccessfully.GetDescription(), true, categoryViewModel);
    }
    
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateCategoryCommand command)
    {
        var category = await mediator.Send(command);
        var categoryViewModel = mapper.Map<CategoryViewModel>(category);

        return ResponseHelper.CreateResponse(200, MessageEnum.UpdatedSuccessfully.GetDescription(), true, categoryViewModel);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await mediator.Send(new DeleteCategoryCommand(id));
        
        return ResponseHelper.CreateResponse<object>(202, MessageEnum.DeletedSuccessfully.GetDescription(), true);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllCategoriesQuery query)
    {
        var categories = await mediator.Send(query);
        var categoriesViewModel = mapper.Map<List<CategoryViewModel>>(categories);

        return ResponseHelper.CreateSuccessResponse(categoriesViewModel);
    }
    
    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var category = await mediator.Send(new GetCategoryByIdQuery(id));
        var categoryViewModel = mapper.Map<CategoryViewModel>(category);

        return ResponseHelper.CreateSuccessResponse(categoryViewModel);
    }
}