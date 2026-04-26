using MediatR;
using MOVE.Domain.Interfaces;

namespace MOVE.Application.Products.Commands;

public record DeleteProductCommand(int Id) : IRequest<bool>;

public class DeleteProductCommandHandler
	: IRequestHandler<DeleteProductCommand, bool>
{
	private readonly IProductRepository _repository;

	public DeleteProductCommandHandler(IProductRepository repository)
	{
		_repository = repository;
	}

	public async Task<bool> Handle(
		DeleteProductCommand request,
		CancellationToken cancellationToken)
	{
		var product = await _repository.GetByIdAsync(request.Id);

		if (product == null) return false;

		await _repository.DeleteAsync(request.Id);
		return true;
	}
}