/// <summary>
/// 
/// </summary>
namespace Codeit.Enterprise.Base.Identity
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Codeit.Enterprise.Base.Abstractions.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserManagerFacade : UserManagerFacade<ApplicationUser>, IUserRepository
    {
        public UserManagerFacade(
            IUserStore<ApplicationUser> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<ApplicationUser> passwordHasher,
            IEnumerable<IUserValidator<ApplicationUser>> userValidators,
            IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILoggerFactory loggerFactory) 
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, loggerFactory)
        {
        }

        public Task<IdentityResult> CreateAsync(IApplicationUser identity)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(IApplicationUser identity)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(IApplicationUser identity)
        {
            throw new NotImplementedException();
        }

        Task<IApplicationUser> IAccountRepository<IApplicationUser>.FindByIdAsync(int identityId)
        {
            throw new NotImplementedException();
        }

        Task<IApplicationUser> IAccountRepository<IApplicationUser>.FindByNameAsync(string identityName)
        {
            throw new NotImplementedException();
        }

        Task<IQueryable<IApplicationUser>> IAccountRepository<IApplicationUser>.GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }

    public class UserManagerFacade<TUser> : UserManager<TUser>, IUserRepository<TUser> where TUser : ApplicationUser
    {
        public UserManagerFacade(
            IUserStore<TUser> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<TUser> passwordHasher,
            IEnumerable<IUserValidator<TUser>> userValidators,
            IEnumerable<IPasswordValidator<TUser>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILoggerFactory loggerFactory) 
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, loggerFactory.CreateLogger<UserManager<TUser>>())
        {
        }

        public async Task<IdentityResult> CreateAsync<TRole>(TUser user, IEnumerable<TRole> roles) where TRole : ApplicationRole
        {
            IdentityResult result = await CreateAsync(user);
            if (!result.Succeeded) return result;
            //var a = roles.ToList().ConvertAll(x => x.Name);
            return await AddToRolesAsync(await FindByNameAsync(user.UserName), roles.ToList().ConvertAll(x => x.Name));
        }

        public virtual async Task<IdentityResult> DeleteAsync(int UserId)
        {
            var user = await FindByIdAsync(UserId);
            return await DeleteAsync(user);
        }

        public virtual async Task<TUser> FindByIdAsync(int userId)
        {
            return await FindByIdAsync(userId.ToString());
        }

        public virtual async Task<IQueryable<TUser>> GetAllAsync()
        {
            return await Task.Run(() =>
            {
                return Users;
            });
        }
    }
}
