/// <summary>
/// 
/// </summary>
namespace Codeit.NetStdLibrary.Abstractions.BusinessLogic
{
    using System.Collections.Generic;
    using System.Net;

    public interface IBLResponse
    {
        List<string> Errors { get; set; }

        public bool HasError { get; }

        public int ErrorsCount { get; }

        public HttpStatusCode ResponseCode { get; set; }
    }

    public interface IBLSingleResponse<TDto> : IBLResponse
    {
        TDto Payload { get; set; }
    }

    public interface IBLListResponse<TDto> : IBLResponse
    {
        IEnumerable<TDto> Payloads { get; set; }
    }

    public interface IBLPagedResponse<TDto> : IBLListResponse<TDto>
    {
        int CurrentPage { get; set; }

        int RowsPerPage { get; set; }

        int CollectionLength { get; set; }

        double TotalPages { get; set; }
    }
}
