using System.Collections.Generic;
using MediatR;

namespace Common.Requests
{
    public interface IEnumerableRequest<TOut> : IRequest<IEnumerable<TOut>>
    {
    }
}