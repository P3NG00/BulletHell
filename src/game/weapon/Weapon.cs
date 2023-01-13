using BulletHell.Utils;

namespace BulletHell.Game.Weapon
{
    public sealed class Weapon : GameObject
    {
        public readonly string Name;
        public readonly int ClipSize;
        public readonly int SwitchTicks;
        public readonly int ReloadTicks;
        public readonly int ShotTicks;

        // TODO different projectile types
        // TODO different projectile velocities

        public Weapon(string name, int clipSize, float fireRateSeconds, float reloadSeconds, float switchSeconds, int id) : base(id)
        {
            Name = name;
            ClipSize = clipSize;
            ShotTicks = GameManager.SecondsToTicks(fireRateSeconds);
            ReloadTicks = GameManager.SecondsToTicks(reloadSeconds);
            SwitchTicks = GameManager.SecondsToTicks(switchSeconds);
        }
    }
}
