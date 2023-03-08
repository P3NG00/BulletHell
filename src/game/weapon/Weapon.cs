using BulletHell.Utils;

namespace BulletHell.Game.Weapon
{
    public sealed class Weapon : GameObject
    {
        public readonly ProjectileInfo ProjectileInfo;
        public readonly string Name;
        public readonly int ClipSize;
        public readonly int SwitchTicks;
        public readonly int ReloadTicks;
        public readonly int ShotTicks;

        // TODO make more verbose and readable
        public Weapon(ProjectileInfo projectileInfo, string name, int clipSize, float fireRateSeconds, float reloadSeconds, float switchSeconds, int id) : base(id)
        {
            ProjectileInfo = projectileInfo;
            Name = name;
            ClipSize = clipSize;
            ShotTicks = GameManager.SecondsToTicks(fireRateSeconds);
            ReloadTicks = GameManager.SecondsToTicks(reloadSeconds);
            SwitchTicks = GameManager.SecondsToTicks(switchSeconds);
        }
    }
}
