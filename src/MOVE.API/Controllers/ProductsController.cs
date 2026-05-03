using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOVE.Application.Products.Commands;
using MOVE.Application.Products.Queries;

namespace MOVE.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
	private readonly IMediator _mediator;

	public ProductsController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		var result = await _mediator.Send(new GetAllProductsQuery());

		if (!result.Success)
			return BadRequest(result.FailureMessage);

		return Ok(result.Data);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetById(int id)
	{
		var result = await _mediator.Send(new GetProductByIdQuery(id));

		if (!result.Success)
			return NotFound(result.FailureMessage);

		return Ok(result.Data);
	}

	[HttpPost]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
	{
		var result = await _mediator.Send(command);

		if (!result.Success)
			return BadRequest(result.FailureMessage);

		return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result.Data);
	}

	[HttpPut("{id}")]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> Update(int id, [FromBody] UpdateProductCommand command)
	{
		if (id != command.Id) return BadRequest();

		var result = await _mediator.Send(command);

		if (!result.Success)
			return NotFound(result.FailureMessage);

		return NoContent();
	}

	[HttpDelete("{id}")]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> Delete(int id)
	{
		var result = await _mediator.Send(new DeleteProductCommand(id));

		if (!result.Success)
			return NotFound(result.FailureMessage);

		return NoContent();
	}
}