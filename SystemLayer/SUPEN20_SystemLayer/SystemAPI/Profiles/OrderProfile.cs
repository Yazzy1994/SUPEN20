using AutoMapper;
using SUPEN20DB.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemAPI.Models;

namespace SystemAPI.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>() //This mapp from Order entity to OrderDTo
            .ReverseMap();//By calling ReverseMap, AutoMapper creates a reverse mapping configuration.


            CreateMap<OrderItem, OrderItemDto>() //This mapp from OrderItem to OrderItemDto
            .ForMember(o => o.ProductId, ex => ex.MapFrom(o => o.Product.ProductId))
            .ForMember(o => o.ProductTitle, ex => ex.MapFrom(o => o.Product.Title))
            .ForMember(o => o.ProductDescription, ex => ex.MapFrom(o => o.Product.Description))
            .ForMember(o => o.ProductPrice, ex => ex.MapFrom(o => o.Product.Price))
            .ForMember(o => o.Quantity, ex => ex.MapFrom(o => o.Product.Quantity))
            .ReverseMap(); //By calling ReverseMap, AutoMapper creates a reverse mapping configuration.


            CreateMap<Product, ProductsDto>() //This mapp from Product to ProductDto
                .ReverseMap(); //By calling ReverseMap, AutoMapper creates a reverse mapping configuration.

        }
    }
}
