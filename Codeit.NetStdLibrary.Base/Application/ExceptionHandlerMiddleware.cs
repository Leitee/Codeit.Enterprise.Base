using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Codeit.NetStdLibrary.Base.BusinessLogic;
using Codeit.NetStdLibrary.Base.Common;
using Codeit.NetStdLibrary.Base.DataAccess;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Codeit.NetStdLibrary.Base.Application
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ExceptionFormatter _formatter;

        public ExceptionHandlerMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _formatter = new ExceptionFormatter(configuration ?? throw new BusinessLogicTierException(nameof(configuration)));
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext pCtx, Exception pEx)
        {
            var code = HttpStatusCode.InternalServerError;
            var response = BLResponse.GetNoDataResponse(code);

            switch (pEx)
            {
                case DataAccessTierException dataAccessTierException:
                    _formatter.FormatException(ref response, dataAccessTierException);
                    break;
                case BusinessLogicTierException businessLogicTierException:
                    _formatter.FormatException(ref response, businessLogicTierException);
                    break;
                case ValidationException validationException:
                    response.ResponseCode = HttpStatusCode.BadRequest;
                    response.Errors.AddRange(validationException.Errors);
                    break;
                default:
                    _formatter.FormatException(ref response, pEx);
                    break;
                //case BadRequestException badRequestException:
                //    code = HttpStatusCode.BadRequest;
                //    result = badRequestException.Message;
                //    break;
                //case NotFoundException _:
                //    code = HttpStatusCode.NotFound;
                //    break;
            }


            pCtx.Response.ContentType = "application/json";
            pCtx.Response.StatusCode = (int)code;

            return pCtx.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }

    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
