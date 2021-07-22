/// <summary>
/// 
/// </summary>
namespace Codeit.NetStdLibrary.Base.Application
{
    using Microsoft.AspNetCore.Mvc;
    using Codeit.NetStdLibrary.Base.Abstractions.BusinessLogic;
    using Codeit.NetStdLibrary.Base.BusinessLogic;
    using System.Linq;
    using System.Net;

    public static class ResponseExtensions
    {
        public static IActionResult ToHttpResponse(this IBLResponse response)
        {
            if (response == null)
                response = BLResponse.GetNoDataResponse(HttpStatusCode.InternalServerError);
            else
                response.ResponseCode = response.HasError ? HttpStatusCode.InternalServerError : HttpStatusCode.OK;

            return new ObjectResult(response)
            {
                StatusCode = (int)HttpStatusCode.OK
            };
        }

        public static IActionResult ToHttpResponse<TModel>(this IBLSingleResponse<TModel> response)
        {
            if (response == null)
                response = BLSingleResponse<TModel>.GetNoDataResponse(HttpStatusCode.NoContent);
            else if (response.HasError)
                response.ResponseCode = HttpStatusCode.InternalServerError;
            else if (response.Payload == null)
                response.ResponseCode = HttpStatusCode.NotFound;
            else
                response.ResponseCode = HttpStatusCode.OK;

            return new ObjectResult(response)
            {
                StatusCode = (int)HttpStatusCode.OK
            };
        }

        public static IActionResult ToHttpResponse<TModel>(this IBLListResponse<TModel> response)
        {
            if (response == null)
                response = BLListResponse<TModel>.GetNoDataResponse(HttpStatusCode.NoContent);
            else if (response.HasError)
                response.ResponseCode = HttpStatusCode.InternalServerError;
            else if (response.Payloads == null || !response.Payloads.Any())
                response.ResponseCode = HttpStatusCode.NoContent;
            else
                response.ResponseCode = HttpStatusCode.OK;

            return new ObjectResult(response)
            {
                StatusCode = (int)HttpStatusCode.OK
            };
        }
    }
}
