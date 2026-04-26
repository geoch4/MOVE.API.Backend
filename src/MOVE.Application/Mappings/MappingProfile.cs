using AutoMapper;
using MOVE.Application.DTOs;
using MOVE.Domain.Entities;

namespace MOVE.Application.Mappings;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		// Product → ProductDto
		CreateMap<Product, ProductDto>()
			.ForMember(dest => dest.CategoryName,
				opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : string.Empty));

		// Category → CategoryDto
		CreateMap<Category, CategoryDto>()
			.ForMember(dest => dest.ProductCount,
				opt => opt.MapFrom(src => src.Products != null ? src.Products.Count : 0));
	}
}