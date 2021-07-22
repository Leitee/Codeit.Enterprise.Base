/// <summary>
/// 
/// </summary>
namespace Codeit.NetStdLibrary.Base.Abstractions.Identity
{
    using Codeit.NetStdLibrary.Abstractions.Identity;

    public interface IRoleRepository : IRoleRepository<IApplicationRole>
    {

    }

    public interface IRoleRepository<TRole> : IAccountRepository<TRole> where TRole : IApplicationRole
    {

    }
}
