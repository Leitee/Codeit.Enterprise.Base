using Codeit.NetStdLibrary.Base.Abstractions.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Codeit.NetStdLibrary.Base.DomainModel
{
    public class RequestDtoBase : IDto
    {
        public Guid Id { get; set; }
        public bool WSNotificationRequired { get; set; }
        public WebSocketSession ClientSession { get; set; }
    }

    public class WebSocketSession
    { 
        public string ConnectionId { get; set; }
        public string CallbackName { get; set; }            
    }
}
