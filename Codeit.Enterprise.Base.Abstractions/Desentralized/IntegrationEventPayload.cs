/// <summary>
/// Codeit Corp
/// </summary>
namespace Codeit.Enterprise.Base.Abstractions.Desentralized
{
    using System;
    using System.Text.Json.Serialization;

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

        [JsonInclude]
        public Guid Id { get; private set; }

        [JsonInclude]
        public DateTime CreationDate { get; private set; }
    }
}
