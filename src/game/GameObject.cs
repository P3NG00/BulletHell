using System;

namespace BulletHell.Game
{
    public abstract class GameObject : IEquatable<GameObject>
    {
        public readonly int ID;

        public GameObject(int id) => ID = id;

        public bool Equals(GameObject other)
        {
            if (ReferenceEquals(other, null))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return ID == other.ID;
        }

        public sealed override bool Equals(object obj) => Equals(obj as GameObject);

        public sealed override int GetHashCode() => ID.GetHashCode() ^ base.GetHashCode();

        public static bool operator ==(GameObject a, GameObject b)
        {
            bool aNull = ReferenceEquals(a, null);
            bool bNull = ReferenceEquals(b, null);
            if (aNull && bNull)
                return true;
            if (aNull || bNull)
                return false;
            if (ReferenceEquals(a, b))
                return true;
            return a.Equals(b);
        }

        public static bool operator !=(GameObject a, GameObject b) => !(a == b);
    }
}
