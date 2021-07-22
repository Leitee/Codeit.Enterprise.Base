/// <summary>
/// 
/// </summary>
namespace Codeit.NetStdLibrary.Base.DomainModel
{
    using Codeit.NetStdLibrary.Base.Abstractions.DomainModel;
    using System;

    public class EFEntity : IEntity
    {
        public Guid Id { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is null || obj is not EFEntity efEntity || GetType() != obj.GetType())
            {
                return false;
            }

            return Id == efEntity.Id;
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

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Entity<T> : IEntity<T>
    {
        public T Id { get; set; }
    }
}
