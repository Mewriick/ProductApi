using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Alza.Product.Application.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> logger;
        private readonly Stopwatch stopwatch;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.stopwatch = new Stopwatch();
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {

            logger.LogInformation($"Start of handling request [{typeof(TRequest).Name}]");
            stopwatch.Restart();

            try
            {
                var response = await next();

                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Handling of request [{typeof(TRequest).Name}] failed.");

                throw;
            }
            finally
            {
                logger.LogInformation($"Request[{typeof(TRequest).Name}] duration {stopwatch.ElapsedMilliseconds}ms");
            }
        }
    }
}
