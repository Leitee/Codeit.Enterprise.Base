/// <summary>
/// 
/// </summary>
namespace Codeit.NetStdLibrary.Base.Abstractions.Identity
{
    public interface IUserRepository : IUserRepository<IApplicationUser>
    {

    }

    public interface IUserRepository<TUser> : IAccountRepository<TUser> where TUser : IApplicationUser
    {

    }
}
