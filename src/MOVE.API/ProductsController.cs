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
		var products = await _mediator.Send(new GetAllProductsQuery());
		return Ok(products);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetById(int id)
	{
		var product = await _mediator.Send(new GetProductByIdQuery(id));
		if (product == null) return NotFound();
		return Ok(product);
	}

	[HttpPost]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
	{
		var product = await _mediator.Send(command);
		return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
	}

	[HttpPut("{id}")]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> Update(int id, [FromBody] UpdateProductCommand command)
	{
		if (id != command.Id) return BadRequest();
		var result = await _mediator.Send(command);
		if (!result) return NotFound();
		return NoContent();
	}

	[HttpDelete("{id}")]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> Delete(int id)
	{
		var result = await _mediator.Send(new DeleteProductCommand(id));
		if (!result) return NotFound();
		return NoContent();
	}
}