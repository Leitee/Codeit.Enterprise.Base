/// <summary>
/// 
/// </summary>
namespace Codeit.NetStdLibrary.Base.DomainModel
{
    using Codeit.NetStdLibrary.Abstractions.BusinessLogic;
    using Codeit.NetStdLibrary.Base.Common;

    public class ClientNotificationResult
    {
        public ClientNotificationResult(CrudOperationEnum clientOperation, bool succeeded)
        {
            ClientOperation = clientOperation;
            Succeeded = succeeded;
        }

        public CrudOperationEnum ClientOperation { get; set; }
        public bool Succeeded { get; set; }
        public string Result => Succeeded ? "SUCCEEDED" : "FAILED";

        public override string ToString()
        {
            return $"Operation '{ClientOperation.GetDescription()}' was {Result}";
        }
    }
}
