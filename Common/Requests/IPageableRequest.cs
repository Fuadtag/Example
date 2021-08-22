using Common.Utils;
using MediatR;

namespace Common.Requests
{
    public interface IPageableRequest<TOut> : IRequest<Paging<TOut>>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}