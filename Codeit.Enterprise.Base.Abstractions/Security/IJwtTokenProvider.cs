/// <summary>
/// 
/// </summary>
namespace Codeit.Enterprise.Base.Abstractions
{
    using Codeit.Enterprise.Base.Abstractions.Identity;

    public interface IJwtTokenProvider
    {
        object GenerateToken<TUser, TRole>(TUser pUser, TRole pRole) where TUser : IApplicationUser where TRole : IApplicationRole;
    }
}
