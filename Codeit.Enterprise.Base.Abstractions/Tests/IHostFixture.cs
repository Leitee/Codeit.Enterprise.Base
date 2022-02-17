/// <summary>
/// 
/// </summary>
namespace Codeit.Enterprise.Base.Abstractions.Tests
{
    using Microsoft.AspNetCore.Hosting;
    using System;

    public interface IHostFixture : IDisposable
    {
        IWebHost Host { get; }
    }
}
