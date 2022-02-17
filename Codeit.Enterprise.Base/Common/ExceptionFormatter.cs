/// <summary>
/// 
/// </summary>
namespace Codeit.Enterprise.Base.Common
{
    using Microsoft.Extensions.Configuration;
    using Codeit.Enterprise.Base.Abstractions.BusinessLogic;
    using Codeit.Enterprise.Base.BusinessLogic;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    public sealed class ExceptionFormatter
    {
        private readonly BaseSettings _settings;

        public ExceptionFormatter(IConfiguration configuration)
        {
            _settings = BaseSettings.GetSettings(configuration ?? throw new ArgumentNullException(nameof(configuration)));
        }

        public IBLResponse FormatException(Exception pEx)
        {
            var response = BLResponse.GetNoDataResponse(HttpStatusCode.InternalServerError);
            FormatException(ref response, pEx);
            return response;
        }

        public void FormatException(ref IBLResponse pResponse, Exception pEx)
        {
            List<string> errs = new List<string>();
            do
            {
                errs.Add(pEx.Message);
                pEx = pEx.InnerException;

            } while (pEx != null);

            FormatException(ref pResponse, errs.ToArray());
        }

        public void FormatException(ref IBLResponse pResponse, params string[] pErrors)
        {
            if (_settings.IsDevelopment)
            {
                pResponse.Errors.AddRange(pErrors);
            }
            else
                pResponse.Errors.Add(pErrors.ToList().PopAt(pErrors.Length - 1));
        }
    }
}
