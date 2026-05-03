using AutoMapper;
using MediatR;
using MOVE.Application.DTOs;
using MOVE.Application.Interfaces;

namespace MOVE.Application.Categories.Queries;

public record GetCategoryByIdQuery(int Id) : IRequest<OperationResult<CategoryDto>>;

public class GetCategoryByIdQueryHandler
	: IRequestHandler<GetCategoryByIdQuery, OperationResult<CategoryDto>>
{
	private readonly ICategoryRepository _repository;
	private readonly IMapper _mapper;

	public GetCategoryByIdQueryHandler(ICategoryRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<OperationResult<CategoryDto>> Handle(
	GetCategoryByIdQuery request,
	CancellationToken cancellationToken)
	{
		var result = await _mediator.Send(new GetCategoryByIdQuery(id));
		if (!result.Success)
			return NotFound(result.FailureMessage);
		return Ok(result.Data);
	}
}