using BulletHell.Game.Entities.Enemies;

namespace BulletHell.Game.Waves
{
    public sealed class Waves : ObjectManager<Wave>
    {
        private static ObjectManager<Wave> s_instance;

        public static int Amount => s_instance.ObjectAmount;

        // TODO add more waves to draw out the experience
        private static readonly Wave Wave0 = new(
            waveLengthSeconds: 45f,
            enemyInfos: new EnemyInfo[] {
                new(typeof(FollowerEnemy), 2f, 0.5f, 1.25f)
            },
            id: 0
        );
        private static readonly Wave Wave1 = new(
            waveLengthSeconds: 45f,
            enemyInfos: new EnemyInfo[] {
                new(typeof(FollowerEnemy), 3f, 0.5f, 1f),
                new(typeof(DashingEnemy), 3f, 0.75f, 1.5f)
            },
            id: 1
        );
        private static readonly Wave Wave2 = new(
            waveLengthSeconds: 45f,
            enemyInfos: new EnemyInfo[] {
                new(typeof(DashingEnemy), 4f, 0.75f, 1.25f),
                new(typeof(ProjectileEnemy), 3f, 1f, 2f)
            },
            id: 2
        );
        private static readonly Wave Wave3 = new(
            waveLengthSeconds: 45f,
            enemyInfos: new EnemyInfo[] {
                new(typeof(DashingEnemy), 3.5f, 1f, 1.5f),
                new(typeof(ProjectileEnemy), 5f, 1.25f, 2f),
                new(typeof(TeleportEnemy), 4f, 1.5f, 4f)
            },
            id: 3
        );

        protected sealed override Wave[] ObjectArray => new Wave[] {
            Wave0,
            Wave1,
            Wave2,
            Wave3,
        };

        public Waves() : base(ref s_instance) {}

        public static Wave FromID(int id) => s_instance.ObjectFromID(id);
    }
}
