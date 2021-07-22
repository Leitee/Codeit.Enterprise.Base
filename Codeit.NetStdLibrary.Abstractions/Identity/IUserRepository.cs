/// <summary>
/// 
/// </summary>
namespace Codeit.NetStdLibrary.Base.Abstractions.Identity
{
    using Codeit.NetStdLibrary.Abstractions.Identity;

    public interface IUserRepository : IUserRepository<IApplicationUser>
    {

    }

    public interface IUserRepository<TUser> : IAccountRepository<TUser> where TUser : IApplicationUser
    {

    }
}
