using System;
using Newtonsoft.Json;

namespace Codeit.NetStdLibrary.Base.Abstractions.Desentralized
{
    public abstract class IntegrationEventPayload
    {
        protected IntegrationEventPayload() 
            : this(Guid.NewGuid(), DateTime.UtcNow)
        {

        }

        [JsonConstructor]
        protected IntegrationEventPayload(Guid id, DateTime createDate)
        {
            Id = id;
            CreationDate = createDate;
        }

        [JsonProperty]
        public Guid Id { get; private set; }

        [JsonProperty]
        public DateTime CreationDate { get; private set; }
    }
}
