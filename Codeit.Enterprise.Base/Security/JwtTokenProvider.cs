/// <summary>
/// 
/// </summary>
namespace Codeit.Enterprise.Base.Security
{
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using Codeit.Enterprise.Base.Abstractions;
    using Codeit.Enterprise.Base.Application;
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Text;

    public class JwtTokenProvider //: IJwtTokenProvider
    {
        //protected readonly AppSettings _settings;

        //public JwtTokenProvider(IConfiguration config)
        //{
        //    _settings = AppSettings.GetSettings(config ?? throw new ArgumentNullException(nameof(config)));
        //}

        //public virtual TokenResponse GenerateToken<TUser, TRole>(TUser pUser, TRole pRole) where TUser : ApplicationUser where TRole : ApplicationRole
        //{
        //    IEnumerable<Claim> claims = new[] {
        //        new Claim("userdata", JsonConvert.SerializeObject(pUser).ToLower()),//TODO: get rid ToLower and fix Json parsing
        //        new Claim("userrole", JsonConvert.SerializeObject(pRole).ToLower()),
        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())//to generate random token
        //    };

        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.JwtSecretKey));
        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        //    var expiration = _settings.IsDevelopment ? DateTime.UtcNow.AddHours(12) : DateTime.UtcNow.AddMonths(1);

        //    var objToken = new JwtSecurityToken(
        //        issuer: _settings.JwtValidIssuer,
        //        audience: _settings.JwtValidAudience,
        //        claims: claims,
        //        expires: expiration,
        //        signingCredentials: creds
        //    );

        //    return new TokenResponse(new JwtSecurityTokenHandler().WriteToken(objToken), expiration);
        //}
    }
}
