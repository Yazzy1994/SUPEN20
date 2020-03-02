﻿using AutoMapper;
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
            .ReverseMap();

            CreateMap<OrderItem, OrderItemDto>() //This mapp from OrderItem to OrderItemDto
            .ForMember(o => o.ProductId, ex => ex.MapFrom(o => o.Product.ProductId))
            .ReverseMap();

            CreateMap<Product, ProductsDto>()
                .ReverseMap(); 

        }
    }
}
