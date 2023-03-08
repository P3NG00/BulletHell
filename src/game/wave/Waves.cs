using BulletHell.Game.Entities.Enemies;

namespace BulletHell.Game.Waves
{
    public sealed class Waves : ObjectManager<Wave>
    {
        private static ObjectManager<Wave> s_instance;

        public static int Amount => s_instance.ObjectAmount;

        // TODO make waves more even
        protected sealed override Wave[] ObjectArray => new Wave[] {
            new(45f, 0, new WaveInfo(typeof(FollowerEnemy), 2f, 0.75f)),
            new(45f, 1, new WaveInfo(typeof(FollowerEnemy), 3f, 0.5f), new WaveInfo(typeof(DashingEnemy), 3f, 0.4f)),
            new(45f, 2, new WaveInfo(typeof(DashingEnemy), 4f, 1f), new WaveInfo(typeof(ProjectileEnemy), 3f, 0.5f)),
            new(45f, 3, new WaveInfo(typeof(ProjectileEnemy), 3.5f, 0.5f), new WaveInfo(typeof(ProjectileEnemy), 5f, 1.25f), new WaveInfo(typeof(TeleportEnemy), 4f, 0.75f)),
        };

        public Waves() : base(ref s_instance) {}

        public static Wave FromID(int id) => s_instance.ObjectFromID(id);
    }
}
