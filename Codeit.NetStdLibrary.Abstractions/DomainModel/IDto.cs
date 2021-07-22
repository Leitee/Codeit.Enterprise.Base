using System;
/// <summary>
/// 
/// </summary>
namespace Codeit.NetStdLibrary.Base.Abstractions.DomainModel
{
    public interface IDto<TEntity> : IDto where TEntity : IEntity
    {
        public Guid Id { get; set; }
    }

    public interface IDto
    {
        
    }

    public interface IRequestDto
    {
        public bool WSNotificationRequired { get; set; }
        public string ConnectionId { get; set; }
        public string CallbackName { get; set; }
    }
}
