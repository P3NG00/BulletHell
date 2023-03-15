namespace BulletHell.Game.Weapon
{
    public sealed class Weapon : GameObject
    {
        public readonly WeaponInfo WeaponInfo;
        public readonly ProjectileInfo ProjectileInfo;

        public Weapon(
            WeaponInfo weaponInfo,
            ProjectileInfo projectileInfo,
            int id) :
        base(id)
        {
            WeaponInfo = weaponInfo;
            ProjectileInfo = projectileInfo;
        }
    }
}
