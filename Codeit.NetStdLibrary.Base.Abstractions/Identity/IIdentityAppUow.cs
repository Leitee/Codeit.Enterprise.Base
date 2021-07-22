using Codeit.NetStdLibrary.Base.Abstractions.DataAccess;
using Codeit.NetStdLibrary.Base.Abstractions.Identity;

namespace Codeit.NetStdLibrary.Base.Abstractions
{
    public interface IIdentityAppUow : IApplicationUow
    {
        IUserRepository Users { get; }
        IRoleRepository Roles { get; }

        T GetIdentityRepo<T>() where T : class;
    }
}
