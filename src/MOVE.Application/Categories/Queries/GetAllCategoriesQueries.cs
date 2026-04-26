using AutoMapper;
using MediatR;
using MOVE.Application.DTOs;
using MOVE.Domain.Interfaces;

namespace MOVE.Application.Categories.Queries;

public record GetAllCategoriesQuery() : IRequest<IEnumerable<CategoryDto>>;

public class GetAllCategoriesQueryHandler
	: IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryDto>>
{
	private readonly ICategoryRepository _repository;
	private readonly IMapper _mapper;

	public GetAllCategoriesQueryHandler(ICategoryRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<IEnumerable<CategoryDto>> Handle(
		GetAllCategoriesQuery request,
		CancellationToken cancellationToken)
	{
		var categories = await _repository.GetAllAsync();
		return _mapper.Map<IEnumerable<CategoryDto>>(categories);
	}
}