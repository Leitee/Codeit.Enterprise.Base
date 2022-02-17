/// <summary>
/// 
/// </summary>
namespace Codeit.Enterprise.Base.Identity
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using Codeit.Enterprise.Base.Abstractions.Identity;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class RoleManagerFacade : RoleManagerFacade<ApplicationRole>, IRoleRepository
    {
        public RoleManagerFacade(
            IRoleStore<ApplicationRole> store,
            IEnumerable<IRoleValidator<ApplicationRole>> roleValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            ILogger<RoleManager<ApplicationRole>> logger) 
            : base(store, roleValidators, keyNormalizer, errors, logger)
        {
        }

        public Task<IdentityResult> CreateAsync(IApplicationRole identity)
        {
            throw new System.NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(IApplicationRole identity)
        {
            throw new System.NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(IApplicationRole identity)
        {
            throw new System.NotImplementedException();
        }

        Task<IApplicationRole> IAccountRepository<IApplicationRole>.FindByIdAsync(int identityId)
        {
            throw new System.NotImplementedException();
        }

        Task<IApplicationRole> IAccountRepository<IApplicationRole>.FindByNameAsync(string identityName)
        {
            throw new System.NotImplementedException();
        }

        Task<IQueryable<IApplicationRole>> IAccountRepository<IApplicationRole>.GetAllAsync()
        {
            throw new System.NotImplementedException();
        }
    }

    public class RoleManagerFacade<TRole> : RoleManager<TRole>, IRoleRepository<TRole> where TRole : ApplicationRole
    {
        public RoleManagerFacade(IRoleStore<TRole> store, IEnumerable<IRoleValidator<TRole>> roleValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, ILogger<RoleManager<TRole>> logger) : base(store, roleValidators, keyNormalizer, errors, logger)
        {
        }

        public virtual async Task<IdentityResult> DeleteAsync(int roleId)
        {
            TRole role = await FindByIdAsync(roleId);
            return await DeleteAsync(role);
        }

        public virtual async Task<TRole> FindByIdAsync(int roleId)
        {
            return await FindByIdAsync(roleId.ToString());
        }

        public virtual async Task<IQueryable<TRole>> GetAllAsync()
        {
            return await Task.Run(() =>
            {
                return Roles;
            });
        }
    }
}
