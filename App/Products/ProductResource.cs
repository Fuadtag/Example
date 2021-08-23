using App.Mappings;
using AutoMapper;
using Common.Resources;
using Domain.Entities;
using FluentValidation;

namespace App.Products
{
    public class ProductResource:IBaseResource,IResourceMapper
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public decimal Price { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductResource>().ReverseMap();
        }

        public class ProductResourceValidator : AbstractValidator<ProductResource>
        {
            public ProductResourceValidator()
            {
                RuleFor(p => p.Name).NotEmpty().MaximumLength(255);
                RuleFor(p => p.Barcode).NotEmpty();
                RuleFor(p => p.Price).InclusiveBetween(0, decimal.MaxValue);
            }
        }
    }
}