/// <summary>
/// 
/// </summary>
namespace Codeit.Enterprise.Base.Abstractions.BusinessLogic
{
    using System.ComponentModel;

    public enum CrudOperationEnum
    {
        [Description("Creation")]
        CREATE,
        [Description("Edition")]
        UPDATE,
        [Description("Deletion")]
        DELETE,
        [Description("Consumption")]
        READ
    }
}
