/// <summary>
/// 
/// </summary>
namespace Codeit.Enterprise.Base.DomainModel
{
    using Codeit.Enterprise.Base.Abstractions.DomainModel;
    using System;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Entity<T> : IEntity<T>
    {
        public T Id { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is null || obj is not Entity<T> efEntity || GetType() != obj.GetType())
            {
                return false;
            }

            return Id.Equals(efEntity.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() * 17;
        }

        public override string ToString()
        {
            return $"Entity Id: {Id}";
        }
    }
    public class EFEntity : Entity<Guid>
    {
        
    }

}
