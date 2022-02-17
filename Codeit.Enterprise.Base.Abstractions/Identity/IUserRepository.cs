/// <summary>
/// 
/// </summary>
namespace Codeit.Enterprise.Base.Abstractions.Identity
{
    public interface IUserRepository : IUserRepository<IApplicationUser>
    {

    }

    public interface IUserRepository<TUser> : IAccountRepository<TUser> where TUser : IApplicationUser
    {

    }
}
