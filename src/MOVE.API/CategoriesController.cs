using MediatR;
using Microsoft.AspNetCore.Mvc;
using MOVE.Application.Categories.Commands;
using MOVE.Application.Categories.Queries;

namespace MOVE.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
	private readonly IMediator _mediator;

	public CategoriesController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		var categories = await _mediator.Send(new GetAllCategoriesQuery());
		return Ok(categories);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetById(int id)
	{
		var category = await _mediator.Send(new GetCategoryByIdQuery(id));
		if (category == null) return NotFound();
		return Ok(category);
	}

	[HttpPost]
	public async Task<IActionResult> Create([FromBody] CreateCategoryCommand command)
	{
		var category = await _mediator.Send(command);
		return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryCommand command)
	{
		if (id != command.Id) return BadRequest();
		var result = await _mediator.Send(command);
		if (!result) return NotFound();
		return NoContent();
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(int id)
	{
		var result = await _mediator.Send(new DeleteCategoryCommand(id));
		if (!result) return NotFound();
		return NoContent();
	}
}