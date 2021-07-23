/// <summary>
/// 
/// </summary>
namespace Codeit.NetStdLibrary.Base.Abstractions
{
    using Codeit.NetStdLibrary.Base.Abstractions.Identity;

    public interface IJwtTokenProvider
    {
        object GenerateToken<TUser, TRole>(TUser pUser, TRole pRole) where TUser : IApplicationUser where TRole : IApplicationRole;
    }
}
