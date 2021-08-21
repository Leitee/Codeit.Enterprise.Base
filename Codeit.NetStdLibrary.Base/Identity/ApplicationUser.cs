/// <summary>
/// 
/// </summary>
namespace Codeit.NetStdLibrary.Base.Identity
{
    using Microsoft.AspNetCore.Identity;
    using Codeit.NetStdLibrary.Base.Abstractions.Identity;
    using Codeit.NetStdLibrary.Base.Abstractions.DomainModel;
    using System;

    public class ApplicationUser : IdentityUser, IDeleteableEntity, IApplicationUser
    {
        public ApplicationUser() : base() { }
        public ApplicationUser(string pUsername, string pEmail, string pFirstName, string pLastName) : base( pUsername )
        {
            Email = pEmail;
            FirstName = pFirstName;
            LastName = pLastName;
            JoinDate = DateTime.UtcNow;
            NormalizedUserName = UserName.ToUpper();
            NormalizedEmail = Email.ToUpper();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime JoinDate { get; set; }

        public string FullName { get { return $"{LastName.ToUpper()} {FirstName}"; } }

        public bool Deleted { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is null || obj is not ApplicationUser appUser || this.GetType() != obj.GetType())
            {
                return false;
            }

            return Id == appUser.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() * 17;
        }

        public override string ToString()
        {
            return $"{UserName} - {FullName} - {Email}";
        }
    }
}
