/// <summary>
/// 
/// </summary>
namespace Codeit.NetStdLibrary.Base.Application
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using Codeit.NetStdLibrary.Base.BusinessLogic;
    using System.Linq;

    public sealed class ValidateModelStateFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
                return;

            var validationErrors = context.ModelState
                .Keys
                .SelectMany(k => context.ModelState[k].Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            var failures = context.ModelState
                .Values
                .SelectMany(result => result.Errors)
                .Select(v => v.ErrorMessage)
                .Where(f => f != null)
                .ToList();

            //new ValidationFailure()
            throw new ValidationException(validationErrors);

            //string msg = "There were some validation errors. ";
            //_logger.LogError(msg, validationErrors);

            //var response = BLResponse.GetNoDataResponse(HttpStatusCode.BadRequest);
            //response.Errors.Add(msg);
            //response.Errors.AddRange(validationErrors);

            //context.HttpContext.Response.ContentType = "application/json";
            //context.Result = new ObjectResult(response);
        }
    }
}
