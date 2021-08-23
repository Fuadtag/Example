using System.Threading.Tasks;
using Common.Resources;
using Common.Utils;

namespace App.Common
{
    public interface IPageableService<TResource> where TResource : IBaseResource
    {
        Task<Paging<TResource>> GetPaginated(int pageIndex, int pageSize);
    }
}