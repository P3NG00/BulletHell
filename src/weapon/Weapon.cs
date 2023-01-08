using BulletHell.Utils;

namespace BulletHell.Weapon
{
    public sealed class Weapon
    {
        public readonly string Name;
        public readonly int ShotTicks;

        // TODO max clip amount
        // TODO reload time
        // TODO different projectile types

        public Weapon(string name, float fireRateSeconds)
        {
            Name = name;
            ShotTicks = GameManager.TicksFromSeconds(fireRateSeconds);
        }
    }
}
