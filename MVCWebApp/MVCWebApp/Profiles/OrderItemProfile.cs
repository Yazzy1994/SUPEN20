using AutoMapper;
using MVCWebApp.Models;

namespace MVCWebApp.Profiles
{
    public class OrderItemProfile : Profile
    {
        public OrderItemProfile()
        {
            CreateMap<ProductModel, OrderItemModel>().ReverseMap(); 
        }
    }
}
