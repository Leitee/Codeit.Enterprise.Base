/// <summary>
/// Differents generic roles to be used across application 
/// </summary>
namespace Codeit.Enterprise.Base.Abstractions.Identity
{
    using System.ComponentModel;

    public enum RoleEnum
    {
        [Description(Roles.Debug)]
        DEBUG = -1,
        [Description(Roles.Admin)]
        ADMIN = 1,
        [Description(Roles.User)]
        USER,
        [Description(Roles.Guest)]
        GUEST,
    }

    public static class Roles
    {
        public const string Debug = "debug";
        public const string Admin = "admin";
        public const string User = "user";
        public const string Guest = "guest";
    }
}
