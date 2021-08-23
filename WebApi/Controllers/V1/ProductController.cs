using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using App.Products;
using Common.Resources;
using Common.Utils;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    public class ProductController : BaseController
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }
        
        
        /// <summary>
        ///     Get resources paginated
        /// </summary>
        /// <param name="pageIndex">Index of page starting from 1</param>
        /// <param name="pageSize">Number of entities per page starting from 1</param>
        [HttpGet]
        [Route("all-paginated")]
        public async Task<Paging<ProductOverviewResource>> GetPaginated([FromQuery][Required] int pageIndex,
            [FromQuery][Required] int pageSize)
        {
            return await _service.GetPaginated(pageIndex, pageSize);
        }
        
        
        /// <summary>
        ///     Get All Resources
        /// </summary>
        [HttpGet]
        [Route("all")]
        public async Task<IEnumerable<ProductResource>> GetAll()
        {
            return await _service.GetAll();
        }
        
        /// <summary>
        ///     Get by id
        /// </summary>
        /// <param name="id">Entity id</param>
        [HttpGet]
        [Route("{id}")]
        public ProductResource GetById([FromRoute][Required] int id)
        {
            return _service.GetById(id);
        }
        
        /// <summary>
        ///     Insert when no id present, update otherwise
        /// </summary>
        /// <param name="resource">Resource to be updated or inserted</param>
        [HttpPost]
        [Route("upsert")]
        public async Task<UpsertReplyResource> Upsert([FromBody][Required] ProductResource resource)
        {
            return await _service.Upsert(resource);
        }
        
        /// <summary>
        ///     Delete by id
        /// </summary>
        /// <param name="id">Entity id</param>
        [HttpDelete]
        [Route("{id}")]
        public async Task Delete([FromRoute][Required] int id)
        {
            await _service.Delete(id);
        }
    }
}