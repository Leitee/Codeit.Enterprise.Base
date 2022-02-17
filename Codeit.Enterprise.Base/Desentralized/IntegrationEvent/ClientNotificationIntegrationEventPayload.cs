/// <summary>
/// 
/// </summary>
namespace Codeit.Enterprise.Base.Desentralized.IntegrationEvent
{
    using Newtonsoft.Json;
    using Codeit.Enterprise.Base.Abstractions.BusinessLogic;
    using Codeit.Enterprise.Base.Abstractions.Desentralized;
    using Codeit.Enterprise.Base.DomainModel;

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
