using Alza.Product.Application.Dtos;
using AutoMapper;

namespace Alza.Product.Application.Mapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ReadModel.Product, ProductDto>();
        }
    }
}
