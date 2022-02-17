/// <summary>
/// Codeit Corp
/// </summary>
namespace Codeit.Enterprise.Base.Abstractions.DataAccess
{
    using Microsoft.EntityFrameworkCore;

    public interface IPersistenceBuilder
    {
        void BuildConfiguration(DbContextOptionsBuilder options);
    }
}
