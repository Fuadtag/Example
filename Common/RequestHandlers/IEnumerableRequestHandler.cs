using System.Collections.Generic;
using Common.Requests;
using MediatR;

namespace Common.RequestHandlers
{
    public interface IEnumerableRequestHandler<TIn, TOut> : IRequestHandler<TIn, IEnumerable<TOut>>
        where TIn : IEnumerableRequest<TOut>
    {
    }
}