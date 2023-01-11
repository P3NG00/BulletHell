using BulletHell.Utils;

namespace BulletHell.Weapon
{
    public sealed class Weapon
    {
        public readonly string Name;
        public readonly int ClipSize;
        public readonly int ID;
        public readonly int ShotTicks;
        public readonly int ReloadTicks;
        public readonly int SwitchToTicks;

        // TODO different projectile types

        public Weapon(string name, int clipSize, float fireRateSeconds, float reloadSeconds, float switchToSeconds,  int id)
        {
            Name = name;
            ClipSize = clipSize;
            ID = id;
            ShotTicks = GameManager.SecondsToTicks(fireRateSeconds);
            ReloadTicks = GameManager.SecondsToTicks(reloadSeconds);
            SwitchToTicks = GameManager.SecondsToTicks(switchToSeconds);
        }
    }
}
