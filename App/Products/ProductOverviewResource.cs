using App.Mappings;
using AutoMapper;
using Common.Resources;
using Domain.Entities;

namespace App.Products
{
    public class ProductOverviewResource: IBaseResource, IResourceMapper
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public decimal Price { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductOverviewResource>();
        }
    }
}