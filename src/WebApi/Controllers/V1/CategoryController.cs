using AutoMapper;
using Core.Domain.Commands.Category;
using Core.Domain.Enums;
using Core.Domain.Extensions;
using Core.Domain.Queries.Category;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;
using WebApi.ViewModels;

namespace WebApi.Controllers.V1;

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