/// <summary>
/// 
/// </summary>
namespace Codeit.Enterprise.Base.DomainModel
{
    using System;

    public class AuditableEntity : EFEntity
    {
        public string CreatedBy { get; set; }

        public DateTime Created { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime? LastModified { get; set; }
    }
}
