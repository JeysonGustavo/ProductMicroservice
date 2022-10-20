using AutoMapper;
using Product.API.Core.Models.Domain;
using Product.API.Core.Models.Request;
using Product.API.Core.Models.Response;

namespace Product.API.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductModel, ProductResponseModel>();
            CreateMap<ProductRequestModel, ProductModel>();
            CreateMap<CategoryModel, CategoryResponseModel>();
        }
    }
}