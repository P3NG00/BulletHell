namespace BulletHell.Input
{
    public abstract class AbstractInput<T>
    {
        protected readonly T InputType;

        public AbstractInput(T t) => InputType = t;

        public abstract bool PressedThisFrame { get; }

        public abstract bool ReleasedThisFrame { get; }

        public abstract bool Held { get; }
    }
}
