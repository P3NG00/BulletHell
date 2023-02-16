using System;
using BulletHell.Game.Entities.Enemies;

namespace BulletHell.Game.Waves
{
    public sealed class Waves : ObjectManager<Wave>
    {
        private static ObjectManager<Wave> s_instance;

        public static int Amount => s_instance.ObjectAmount;

        // TODO make waves more challenging
        protected sealed override Wave[] ObjectArray => new Wave[] {
            new(25f, 0.75f, CreateEnemyTypes((typeof(FollowerEnemy), 2f)), 0),
            new(25f, 1f, CreateEnemyTypes((typeof(FollowerEnemy), 3f)), 1),
            new(50f, 1.5f, CreateEnemyTypes((typeof(FollowerEnemy), 5f), (typeof(TeleportEnemy), 4f)), 2),
        };

        public Waves() : base(ref s_instance) {}

        private (Type, float)[] CreateEnemyTypes(params (Type, float)[] enemyTypes) => enemyTypes;

        public static Wave FromID(int id) => s_instance.ObjectFromID(id);
    }
}
