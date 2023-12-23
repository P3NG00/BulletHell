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
                new(
                    enemyType: typeof(FollowerEnemy),
                    enemyHealth: 2f,
                    enemyDamage: 0.5f,
                    spawnSeconds: 1.25f
                ),
            },
            id: 0
        );
        private static readonly Wave Wave1 = new(
            waveLengthSeconds: 45f,
            enemyInfos: new EnemyInfo[] {
                new(
                    enemyType: typeof(FollowerEnemy),
                    enemyHealth: 3f,
                    enemyDamage: 0.5f,
                    spawnSeconds: 1f
                ),
                new(
                    enemyType: typeof(MoverEnemy),
                    enemyHealth: 3f,
                    enemyDamage: 0.75f,
                    spawnSeconds: 1.5f
                ),
            },
            id: 1
        );
        private static readonly Wave Wave2 = new(
            waveLengthSeconds: 45f,
            enemyInfos: new EnemyInfo[] {
                new(
                    enemyType: typeof(MoverEnemy),
                    enemyHealth: 4f,
                    enemyDamage: 0.75f,
                    spawnSeconds: 1.25f
                ),
                new(
                    enemyType: typeof(DashingEnemy),
                    enemyHealth: 3f,
                    enemyDamage: 1f,
                    spawnSeconds: 2f
                ),
            },
            id: 2
        );
        private static readonly Wave Wave3 = new(
            waveLengthSeconds: 45f,
            enemyInfos: new EnemyInfo[] {
                new(
                    enemyType: typeof(MoverEnemy),
                    enemyHealth: 3.5f,
                    enemyDamage: 1f,
                    spawnSeconds: 1.5f
                ),
                new(
                    enemyType: typeof(DashingEnemy),
                    enemyHealth: 5f,
                    enemyDamage: 1.25f,
                    spawnSeconds: 2f
                ),
                new(
                    enemyType: typeof(ProjectileEnemy),
                    enemyHealth: 4f,
                    enemyDamage: 1.5f,
                    spawnSeconds: 4f
                ),
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
