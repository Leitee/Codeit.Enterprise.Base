/// <summary>
/// 
/// </summary>
namespace Codeit.NetStdLibrary.Base.Identity
{
    using Microsoft.AspNetCore.Identity;
    using Codeit.NetStdLibrary.Base.Abstractions.Identity;
    using System;

    public class ApplicationRole : IdentityRole, IApplicationRole
    {
        public ApplicationRole() { }
        public ApplicationRole(string name, string description = null) : base(name)
        {
            Description = description;
            NormalizedName = name.ToUpper();
        }

        public string Description { get; protected set; }

        public override bool Equals(object obj)
        {
            if (obj is null || obj is not ApplicationRole appRole || GetType() != obj.GetType())
            {
                return false;
            }

            return Id == appRole.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() * 17;
        }

        public override string ToString()
        {
            return $"{Name} - {Description}";
        }
    }
}
