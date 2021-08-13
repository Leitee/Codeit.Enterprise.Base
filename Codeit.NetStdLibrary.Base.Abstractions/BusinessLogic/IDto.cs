/// <summary>
/// Codeit Corp
/// </summary>
namespace Codeit.NetStdLibrary.Base.Abstractions.BusinessLogic
{
    public interface IDto
    {
    }

    public interface IDto<TId> : IDto
    {
        TId Id { get; set; }
    }

    public interface IRequestDto
    {
        public bool WSNotificationRequired { get; set; }
        public string ConnectionId { get; set; }
        public string CallbackName { get; set; }
    }
}
