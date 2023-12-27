namespace BulletHell.Game
{
    public abstract class GameObject
    {
        public readonly int ID;

        protected GameObject(int id) => ID = id;
    }
}
