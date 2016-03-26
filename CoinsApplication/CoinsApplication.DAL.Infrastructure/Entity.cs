namespace CoinsApplication.DAL.Infrastructure
{
    public class Entity<T> : IEntity
        where T : class, IEntity
    {
        private int? _oldHashCode;

        public virtual int Id { get; set; }

        public override bool Equals(object obj)
        {
            T other = obj as T;
            if (other == null)
                return false;

            // handle the case of comparing two NEW objects
            var otherIsTransient = Equals(other.Id, 0);
            var thisIsTransient = Equals(Id, 0);

            if (otherIsTransient && thisIsTransient)
                return ReferenceEquals(other, this);

            return other.Id.Equals(Id);
        }

        public override int GetHashCode()
        {
            // Once we have a hash code we'll never change it
            if (_oldHashCode.HasValue)
                return _oldHashCode.Value;

            bool thisIsTransient = Equals(Id, 0);

            // When this instance is transient, we use the base GetHashCode()
            // and remember it, so an instance can NEVER change its hash code.
            if (thisIsTransient)
            {
                _oldHashCode = base.GetHashCode();
                return _oldHashCode.Value;
            }
            return Id.GetHashCode();
        }

        public static bool operator ==(Entity<T> x, Entity<T> y)
        {
            return Equals(x, y);
        }

        public static bool operator !=(Entity<T> x, Entity<T> y)
        {
            return !(x == y);
        }
    }
}