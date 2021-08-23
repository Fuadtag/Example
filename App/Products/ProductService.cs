using System.Linq;
using System.Threading.Tasks;
using App.Common;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common;
using Common.Utils;
using Domain.Entities;

namespace App.Products
{
    public interface IProductService:ICrudService<ProductResource>, IPageableService<ProductOverviewResource>
    {
        
    }

    public class ProductService : CrudService<ProductResource, Product>, IProductService
    {
        private IAppDbContext _dbContext;
        private IMapper _mapper;
        public ProductService(IAppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Paging<ProductOverviewResource>> GetPaginated(int pageIndex, int pageSize)
        {
            var queryable = DbContext.GetDbSet<Product>()
                .OrderBy(p => p.Name)
                .ProjectTo<ProductOverviewResource>(Mapper.ConfigurationProvider);

            return await Paging<ProductOverviewResource>.CreateAsync(queryable, pageIndex, pageSize);
        }
    }
}