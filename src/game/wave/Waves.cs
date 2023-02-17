using BulletHell.Game.Entities.Enemies;

namespace BulletHell.Game.Waves
{
    public sealed class Waves : ObjectManager<Wave>
    {
        private static ObjectManager<Wave> s_instance;

        public static int Amount => s_instance.ObjectAmount;

        // TODO make waves more challenging
        protected sealed override Wave[] ObjectArray => new Wave[] {
            new(25f, 0, new WaveInfo(typeof(FollowerEnemy), 2f, 0.75f)),
            new(25f, 1, new WaveInfo(typeof(FollowerEnemy), 3f, 1f)),
            new(50f, 2, new WaveInfo(typeof(FollowerEnemy), 5f, 1.5f), new WaveInfo(typeof(TeleportEnemy), 4f, 0.75f)),
        };

        public Waves() : base(ref s_instance) {}

        public static Wave FromID(int id) => s_instance.ObjectFromID(id);
    }
}
