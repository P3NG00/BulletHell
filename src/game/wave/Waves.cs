using BulletHell.Game.Entities.Enemies;

namespace BulletHell.Game.Waves
{
    public sealed class Waves : ObjectManager<Wave>
    {
        private static ObjectManager<Wave> s_instance;

        public static int Amount => s_instance.ObjectAmount;

        // TODO make waves more challenging
        protected sealed override Wave[] ObjectArray => new Wave[] {
            new(30f, 0, new WaveInfo(typeof(FollowerEnemy), 2f, 0.75f)),
            new(30f, 1, new WaveInfo(typeof(FollowerEnemy), 2f, 0.75f), new WaveInfo(typeof(TeleportEnemy), 3f, 0.25f)),
            new(60f, 2, new WaveInfo(typeof(FollowerEnemy), 4f, 1f), new WaveInfo(typeof(ProjectileEnemy), 3f, 0.5f)),
            new(60f, 3, new WaveInfo(typeof(ProjectileEnemy), 5f, 1.25f)),
        };

        public Waves() : base(ref s_instance) {}

        public static Wave FromID(int id) => s_instance.ObjectFromID(id);
    }
}
