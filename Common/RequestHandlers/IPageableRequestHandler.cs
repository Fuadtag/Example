using Common.Requests;
using Common.Utils;
using MediatR;

namespace Common.RequestHandlers
{
    public interface IPageableRequestHandler<TIn, TOut> : IRequestHandler<TIn, Paging<TOut>> where TIn : IPageableRequest<TOut>
    {
    }
}