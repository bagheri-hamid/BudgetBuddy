using AutoMapper;
using Core.Domain.Commands.Category;
using Core.Domain.Enums;
using Core.Domain.Extensions;
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

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteCategoryCommand command)
    {
        await mediator.Send(command);
        
        return ResponseHelper.CreateResponse<object>(204, MessageEnum.DeletedSuccessfully.GetDescription(), true);
    }
}