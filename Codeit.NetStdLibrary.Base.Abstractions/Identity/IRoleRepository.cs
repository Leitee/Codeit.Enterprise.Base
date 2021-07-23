/// <summary>
/// 
/// </summary>
namespace Codeit.NetStdLibrary.Base.Abstractions.Identity
{
    public interface IRoleRepository : IRoleRepository<IApplicationRole>
    {

    }

    public interface IRoleRepository<TRole> : IAccountRepository<TRole> where TRole : IApplicationRole
    {

    }
}
