using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Prometheus;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace TweetsAPI
{

    public class RequestMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public RequestMiddleware(
            RequestDelegate next
            , ILoggerFactory loggerFactory
            )
        {
            this._next = next;
            this._logger = loggerFactory.CreateLogger<RequestMiddleware>();
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var path = httpContext.Request.Path.Value;
            var method = httpContext.Request.Method;

            var counter = Metrics.CreateCounter("TwitterAPI", "HTTP Requests Total", new CounterConfiguration
            {
                LabelNames = new[] { "path", "method", "status" }
            });

            var statusCode = 200;

            //var sw = Stopwatch.StartNew();
            //await _next(httpContext);
            //sw.Stop();

            //HistogramConfiguration configuration = new HistogramConfiguration
            //{
            //    LabelNames = new string[] { "0", "1", "2", "3", "4", "5", "6", "7" }
            //};

            //var histogram = Metrics.CreateHistogram("tweets_api_time_seconds", "TweetsAPI Response Time in seconds");

            //histogram
            //    .WithLabels(httpContext.Request.Method, httpContext.Request.Path)
            //    .Observe(sw.Elapsed.TotalSeconds);

            try
            {
                await _next.Invoke(httpContext);
            }
            catch (Exception)
            {
                statusCode = 500;
                counter.Labels(path, method, statusCode.ToString()).Inc();

                throw;
            }

            if (path != "/metrics")
            {
                statusCode = httpContext.Response.StatusCode;
                counter.Labels(path, method, statusCode.ToString()).Inc();
            }
        }
    }

    public static class RequestMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestMiddleware>();
        }
    }

}
