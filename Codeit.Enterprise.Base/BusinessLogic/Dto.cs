/// <summary>
/// Codeit Corp
/// </summary>
namespace Codeit.Enterprise.Base.BusinessLogic
{
    using Codeit.Enterprise.Base.Abstractions.BusinessLogic;
    using System;

    public class Dto : IDto<Guid>
    {
        public Guid Id { get; set; }
    }
}
