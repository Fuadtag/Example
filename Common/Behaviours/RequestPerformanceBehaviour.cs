using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
// using Common.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Common.Behaviours
{
    public class RequestPerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        // private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<TRequest> _logger;
        private readonly Stopwatch _timer;

        public RequestPerformanceBehaviour(ILogger<TRequest> logger)
        {
            _timer = new Stopwatch();

            _logger = logger;
            // _currentUserService = currentUserService;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();

            var response = await next();

            _timer.Stop();

            if (_timer.ElapsedMilliseconds > 500)
            {
                var declaringType = typeof(TRequest).DeclaringType;
                if (declaringType is not null)
                {
                    var requestName = declaringType.Name;

                    _logger.LogWarning("Slow Request: {Name} from User {UserId} took {ElapsedMilliseconds} ms", _timer.ElapsedMilliseconds);
                }
            }

            return response;
        }
    }
}