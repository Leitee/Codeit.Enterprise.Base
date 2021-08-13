/// <summary>
/// Codeit Corp
/// </summary>
namespace Codeit.NetStdLibrary.Base.BusinessLogic
{
    using Codeit.NetStdLibrary.Base.Abstractions.BusinessLogic;
    using System;

    public class Dto : IDto<Guid>
    {
        public Guid Id { get; set; }
    }
}
