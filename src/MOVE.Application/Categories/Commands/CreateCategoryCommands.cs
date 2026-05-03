using AutoMapper;
using MediatR;
using MOVE.Application.DTOs;
using MOVE.Application.Interfaces;
using MOVE.Domain.Common;
using MOVE.Domain.Entities;

namespace MOVE.Application.Categories.Commands;

public record CreateCategoryCommand(
	string Name,
	string Description) : IRequest<OperationResult<CategoryDto>>;

public class CreateCategoryCommandHandler
	: IRequestHandler<CreateCategoryCommand, OperationResult<CategoryDto>>
{
	private readonly ICategoryRepository _repository;
	private readonly IMapper _mapper;

	public CreateCategoryCommandHandler(
		ICategoryRepository repository,
		IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<OperationResult<CategoryDto>> Handle(
		CreateCategoryCommand request,
		CancellationToken cancellationToken)
	{
		var category = new Category
		{
			Name = request.Name,
			Description = request.Description
		};

		await _repository.AddAsync(category);

		var dto = _mapper.Map<CategoryDto>(category);
		return OperationResult<CategoryDto>.SuccessResult(dto);
	}
}