﻿/// <summary>
/// 
/// </summary>
namespace Codeit.Enterprise.Base.DomainModel
{
    using Codeit.Enterprise.Base.Abstractions.BusinessLogic;
    using Codeit.Enterprise.Base.Common;

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
