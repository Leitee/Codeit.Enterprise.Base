using Codeit.Enterprise.Base.Abstractions.DataAccess;
using Codeit.Enterprise.Base.Abstractions.Identity;

namespace Codeit.Enterprise.Base.Abstractions
{
    public interface IIdentityAppUow : IApplicationUow
    {
        IUserRepository Users { get; }
        IRoleRepository Roles { get; }

        T GetIdentityRepo<T>() where T : class;
    }
}
