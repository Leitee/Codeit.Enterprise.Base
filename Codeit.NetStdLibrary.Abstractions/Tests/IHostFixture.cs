/// <summary>
/// 
/// </summary>
namespace Codeit.NetStdLibrary.Base.Abstractions.Tests
{
    using Microsoft.AspNetCore.Hosting;
    using System;

    public interface IHostFixture : IDisposable
    {
        IWebHost Host { get; }
    }
}
