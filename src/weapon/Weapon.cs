using BulletHell.Utils;

namespace BulletHell.Weapon
{
    public struct Weapon
    {
        public readonly int ShotTicks;

        // TODO max clip amount
        // TODO reload time
        // TODO different projectile types

        public Weapon(float fireRateSeconds) => ShotTicks = GameManager.TicksFromSeconds(fireRateSeconds);
    }
}
