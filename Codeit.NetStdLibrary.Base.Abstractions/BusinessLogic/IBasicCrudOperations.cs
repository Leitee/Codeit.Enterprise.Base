/// <summary>
/// 
/// </summary>
namespace Codeit.NetStdLibrary.Base.Abstractions.BusinessLogic
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IBasicCrudOperations<TDto>
    {
        Task<IBLListResponse<TDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IBLSingleResponse<TDto>> GetByIdAsync(Guid pId);
        Task<IBLSingleResponse<Guid>> CreateAsync(TDto pDto);
        Task<IBLSingleResponse<bool>> UpdateAsync(TDto pDto);
        Task<IBLSingleResponse<bool>> DeleteAsync(TDto pDto);
    }
}
