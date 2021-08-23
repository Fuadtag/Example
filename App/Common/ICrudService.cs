using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Resources;

namespace App.Common
{
    public interface ICrudService<TResource> where TResource : IBaseResource
    {
        TResource GetById(int id);
        Task<IEnumerable<TResource>> GetAll();
        Task<UpsertReplyResource> Upsert(TResource resource);
        Task Delete(int id);
    }
}