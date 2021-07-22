/// <summary>
/// 
/// </summary>
namespace Codeit.NetStdLibrary.Base.BusinessLogic
{
    using Codeit.NetStdLibrary.Abstractions.BusinessLogic;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    public class BLResponse : IBLResponse
    {
        public List<string> Errors { get; set; }

        public bool HasError { get { return Errors.Any(); } }

        public int ErrorsCount { get { return Errors.Count; } }

        public HttpStatusCode ResponseCode { get; set; }

        public BLResponse()
        {
            Errors = new List<string>();
        }

        public static IBLResponse GetNoDataResponse(HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new BLResponse() { ResponseCode = statusCode };
        }
    }

    public class BLSingleResponse<TDto> : BLResponse, IBLSingleResponse<TDto>
    {
        public TDto Payload { get; set; }
        public new static IBLSingleResponse<TDto> GetNoDataResponse(HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new BLSingleResponse<TDto>() { ResponseCode = statusCode };
        }
    }

    public class BLListResponse<TDto> : BLResponse, IBLListResponse<TDto>
    {
        public IEnumerable<TDto> Payloads { get; set; }
        public new static IBLListResponse<TDto> GetNoDataResponse(HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new BLListResponse<TDto>() { ResponseCode = statusCode };
        }
    }

    public class BLPagedResponse<TDto> : BLListResponse<TDto>, IBLPagedResponse<TDto>
    {
        public int CurrentPage { get; set; }

        public int RowsPerPage { get; set; }

        public int CollectionLength { get; set; }

        public double TotalPages { get; set; }
    }

}
