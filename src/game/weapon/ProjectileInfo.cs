using BulletHell.Utils;

namespace BulletHell.Game.Weapon
{
    public readonly struct ProjectileInfo
    {
        public readonly float Damage;
        public readonly float Speed;
        public readonly float Radius;
        public readonly int LifeTicks;

        public ProjectileInfo(float damage, float speed, float radius, float lifeSeconds)
        {
            Damage = damage;
            Speed = speed;
            Radius = radius;
            LifeTicks = GameManager.SecondsToTicks(lifeSeconds);
        }
    }
}
