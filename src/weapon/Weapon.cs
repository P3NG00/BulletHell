using BulletHell.Utils;

namespace BulletHell.Weapon
{
    public sealed class Weapon
    {
        public readonly string Name;
        public readonly int ShotTicks;
        public readonly int ClipSize;
        public readonly int ReloadTicks;

        // TODO different projectile types

        public Weapon(string name, int clipSize, float fireRateSeconds, float reloadSeconds)
        {
            Name = name;
            ClipSize = clipSize;
            ShotTicks = GameManager.TicksFromSeconds(fireRateSeconds);
            ReloadTicks = GameManager.TicksFromSeconds(reloadSeconds);
        }
    }
}
