using BulletHell.Utils;

namespace BulletHell.Game.Weapon
{
    public struct WeaponInfo
    {
        public readonly string Name;
        public readonly int ClipSize;
        public readonly int SwitchTicks;
        public readonly int ReloadTicks;
        public readonly int ShotTicks;
        public readonly int ProjectilesPerShot;
        public readonly float ProjectileSpreadRadians;

        public WeaponInfo(
            string name,
            int clipSize,
            float fireRateSeconds,
            float reloadSeconds,
            float switchSeconds,
            int projectilesPerShot = 1,
            float projectileSpread = 0f)
        {
            Name = name;
            ClipSize = clipSize;
            ShotTicks = GameManager.SecondsToTicks(fireRateSeconds);
            ReloadTicks = GameManager.SecondsToTicks(reloadSeconds);
            SwitchTicks = GameManager.SecondsToTicks(switchSeconds);
            ProjectilesPerShot = projectilesPerShot;
            ProjectileSpreadRadians = projectileSpread;
        }
    }
}
