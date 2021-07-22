/// <summary>
/// 
/// </summary>
namespace Codeit.NetStdLibrary.Base.Abstractions.DataAccess
{
    using Microsoft.EntityFrameworkCore;

    public interface IPersistenceBuider
    {
        void ConfigurePersistence(DbContextOptionsBuilder options);
    }
}
