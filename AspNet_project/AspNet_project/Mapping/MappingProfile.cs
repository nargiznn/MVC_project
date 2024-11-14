using System;
using AspNet_project.Models;
using AspNet_project.ViewModels.Admin.Product;
using AspNet_project.ViewModels.Admin.Slider;
using AutoMapper;

namespace AspNet_project.Mapping
{
	public class MappingProfile: Profile
    {
        public MappingProfile()
        {

            CreateMap<Product, ProductVM>()
                    .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                    .ForMember(dest => dest.MainImage, opt => opt.MapFrom(src => src.ProductImages.FirstOrDefault(m => m.IsMain).Image));

            CreateMap<Product, SingleProductVM>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
        }
    }
}

