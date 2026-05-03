using MediatR;
using MOVE.Application.Interfaces;
using MOVE.Domain.Common;

namespace MOVE.Application.Products.Commands;

public record DeleteProductCommand(int Id) : IRequest<OperationResult>;

public class DeleteProductCommandHandler
	: IRequestHandler<DeleteProductCommand, OperationResult>
{
	private readonly IProductRepository _repository;

	public DeleteProductCommandHandler(IProductRepository repository)
	{
		_repository = repository;
	}

	public async Task<OperationResult> Handle(
		DeleteProductCommand request,
		CancellationToken cancellationToken)
	{
		var product = await _repository.GetByIdAsync(request.Id);

		if (product == null)
			return OperationResult.FailureResult("Product not found");

		await _repository.DeleteAsync(request.Id);

		return OperationResult.SuccessResult();
	}
}