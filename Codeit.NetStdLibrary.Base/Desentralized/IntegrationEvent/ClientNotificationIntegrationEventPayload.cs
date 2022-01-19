/// <summary>
/// 
/// </summary>
namespace Codeit.NetStdLibrary.Base.Desentralized.IntegrationEvent
{
    using Newtonsoft.Json;
    using Codeit.NetStdLibrary.Base.Abstractions.BusinessLogic;
    using Codeit.NetStdLibrary.Base.Abstractions.Desentralized;
    using Codeit.NetStdLibrary.Base.DomainModel;

    public class ClientNotificationIntegrationEventPayload : IntegrationEventPayload
    {
        [JsonProperty]
        public string WSConnectionId { get; set; }
        [JsonProperty]
        public string CallBackMethod { get; set; }
        [JsonProperty]
        public ClientNotificationResult Payload { get; set; }

        [JsonConstructor]
        public ClientNotificationIntegrationEventPayload(string connectionId, string callbackMethod, CrudOperationEnum operationType, bool succeeded) : base()
        {
            WSConnectionId = connectionId;
            CallBackMethod = callbackMethod;
            Payload = new ClientNotificationResult(operationType, succeeded);
        }
    }
}
