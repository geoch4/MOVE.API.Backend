using AutoMapper;
using MediatR;
using MOVE.Application.DTOs;
using MOVE.Domain.Interfaces;

namespace MOVE.Application.Categories.Queries;

public record GetCategoryByIdQuery(int Id) : IRequest<CategoryDto?>;

public class GetCategoryByIdQueryHandler
	: IRequestHandler<GetCategoryByIdQuery, CategoryDto?>
{
	private readonly ICategoryRepository _repository;
	private readonly IMapper _mapper;

	public GetCategoryByIdQueryHandler(ICategoryRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<CategoryDto?> Handle(
		GetCategoryByIdQuery request,
		CancellationToken cancellationToken)
	{
		var category = await _repository.GetByIdAsync(request.Id);
		return category == null ? null : _mapper.Map<CategoryDto>(category);
	}
}