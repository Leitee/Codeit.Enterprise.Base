/// <summary>
/// 
/// </summary>
namespace Codeit.NetStdLibrary.Base.Identity
{
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AccountManagerFacade : AccountManagerFacade<ApplicationUser, ApplicationRole>
    {
        public AccountManagerFacade(
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILoggerFactory loggerFactory,
            IAuthenticationSchemeProvider schemes)
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, loggerFactory, schemes)
        {
        }
    }

    public class AccountManagerFacade<TUser, TRole> where TUser : ApplicationUser where TRole : ApplicationRole, new()
    {
        private readonly SignInManager<TUser> _signInManager;

        public AccountManagerFacade(
            UserManager<TUser> userManager,
            IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<TUser> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILoggerFactory loggerFactory,
            IAuthenticationSchemeProvider schemes)
        {
            _signInManager = new SignInManager<TUser>(userManager, contextAccessor, claimsFactory, optionsAccessor, loggerFactory.CreateLogger<SignInManager<TUser>>(), schemes);
            UserManager = userManager;
        }

        public virtual UserManager<TUser> UserManager { get; }
        public HttpContext Context { get { return _signInManager.Context; } }

        public virtual async Task<IdentityResult> SignUpAsync(TUser user, string pPassword)
        {
            user.PasswordHash = new PasswordHasher<TUser>().HashPassword(user, pPassword);
            return await UserManager.CreateAsync(user);
        }

        public virtual async Task<IdentityResult> SignUpAsync(TUser user, string pPassword, IEnumerable<TRole> roles)
        {
            user.PasswordHash = new PasswordHasher<TUser>().HashPassword(user, pPassword);
            await UserManager.AddToRolesAsync(user, roles.Select(x => x.Name));
            return await UserManager.CreateAsync(user);
        }

        public virtual async Task<SignInResult> SignInAsync(string pUsername, string pPassword, bool pRememberMe)
        {
            var signResul = await PasswordSignInAsync(pUsername, pPassword, pRememberMe);
            if(signResul.Succeeded)
                await _signInManager.SignInAsync(await UserManager.FindByNameAsync(pUsername), pRememberMe);
            return signResul;
        }

        public virtual async Task SignInAsync(TUser pUsername, bool pRememberMe)
        {
            await _signInManager.SignInAsync(pUsername, pRememberMe);
        }

        public Task SignOutAsync()
        {
            return _signInManager.SignOutAsync();
        }

        public virtual async Task<string> GetEmailConfirmTokenAsync(TUser user)
        {
            return await UserManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public virtual async Task<IdentityResult> ConfirmEmailAsync(TUser user, string token)
        {
            return await UserManager.ConfirmEmailAsync(user, token);
        }

        public virtual async Task<TRole> GetRoleByUserAsync(TUser user)
        {
            return (await GetRolesByUserAsync(user)).FirstOrDefault();
        }

        public async Task<IEnumerable<TRole>> GetRolesByUserAsync(TUser user)
        {
            var rolesStr = await UserManager.GetRolesAsync(user);
            return rolesStr.Select(x => new TRole() { Name = x });
        }

        public async Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent)
        {
            return await _signInManager.PasswordSignInAsync(userName, password, isPersistent, false);
        }

        public AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl)
        {
            return _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        }

        public async Task<ExternalLoginInfo> GetExternalLoginInfoAsync()
        {
            return await _signInManager.GetExternalLoginInfoAsync();
        }

        public async Task<SignInResult> ExternalLoginSignInAsync(string loginProvider, string providerKey, bool isPersistent)
        {
            return await _signInManager.ExternalLoginSignInAsync(loginProvider, providerKey, isPersistent);
        }
    }
}
