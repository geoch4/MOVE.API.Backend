using MediatR;
using MOVE.Application.Interfaces;
using MOVE.Domain.Common;

namespace MOVE.Application.Products.Commands;

public record UpdateProductCommand(
	int Id,
	string Name,
	string Description,
	decimal Price,
	int StockQuantity,
	string ImageUrl,
	int CategoryId) : IRequest<OperationResult>;

public class UpdateProductCommandHandler
	: IRequestHandler<UpdateProductCommand, OperationResult>
{
	private readonly IProductRepository _repository;

	public UpdateProductCommandHandler(IProductRepository repository)
	{
		_repository = repository;
	}

	public async Task<OperationResult> Handle(
		UpdateProductCommand request,
		CancellationToken cancellationToken)
	{
		var product = await _repository.GetByIdAsync(request.Id);

		if (product == null)
			return OperationResult.FailureResult("Product not found");

		product.Name = request.Name;
		product.Description = request.Description;
		product.Price = request.Price;
		product.StockQuantity = request.StockQuantity;
		product.ImageUrl = request.ImageUrl;
		product.CategoryId = request.CategoryId;

		await _repository.UpdateAsync(product);

		return OperationResult.SuccessResult();
	}
}