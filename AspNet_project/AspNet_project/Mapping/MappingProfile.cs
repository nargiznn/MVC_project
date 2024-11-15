using System;
using AspNet_project.Models;
using AspNet_project.ViewModels;
using AspNet_project.ViewModels.Admin.Product;
using AspNet_project.ViewModels.Admin.Slider;
using AutoMapper;
using static System.Net.Mime.MediaTypeNames;

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
                        .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                        .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.ProductImages.Select(pi => new ProductImageVM
                        {
                            Image = pi.Image,
                            IsMain = pi.IsMain
                        }).ToList()));
            CreateMap<ProductVM, Product>()
        .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
        .ForMember(dest => dest.ProductImages, opt => opt.MapFrom(src => src.ImagePaths));

            CreateMap<ProductImageVM, ProductImage>();

        }
    }
}

