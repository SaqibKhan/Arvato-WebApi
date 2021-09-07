using System;
using System.IO;
using System.Threading.Tasks;
using GateWayApi.DAL.Entity;
using GateWayApi.Services.LoggerRepo;
using Microsoft.AspNetCore.Http;

namespace GateWayApi.Services.Middlewares
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService LogRepo;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILoggerService repo)
        {
            this._next = next;
            this.LogRepo = repo;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var log = new LogItem
            {
                Path = context.Request.Path,
                Method = context.Request.Method,
                QueryString = context.Request.QueryString.ToString()
            };

            await LogRequest(context,log);
            await LogResponse(context,log);
            
            LogRepo.AddToLogs(log);
            
            await _next.Invoke(context);
        }

        private async Task LogRequest(HttpContext context, LogItem log)
        {

            context.Request.EnableBuffering();
            var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
            context.Request.Body.Position = 0;
            log.Payload = body;
            log.RequestedOn = DateTime.Now;
        }

        private async Task LogResponse(HttpContext context, LogItem log)
        {
            using (Stream originalRequest = context.Response.Body)
            {
                try
                {
                    var memStream = new MemoryStream();
                    context.Response.Body = memStream;
                    memStream.Position = 0;

                    await _next(context);
                    context.Response.Body.Seek(0, SeekOrigin.Begin);
                    var response = await new StreamReader(context.Response.Body).ReadToEndAsync();
                    context.Response.Body.Seek(0, SeekOrigin.Begin);

                    log.Response = response;
                    log.ResponseCode = context.Response.StatusCode.ToString();
                    log.IsSuccessStatusCode =(context.Response.StatusCode == 200 || context.Response.StatusCode == 201);
                    log.RespondedOn = DateTime.Now;
                    await memStream.CopyToAsync(originalRequest);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    // assign the response body to the actual context
                    context.Response.Body = originalRequest;
                }
            }
            
        }
    }
}
