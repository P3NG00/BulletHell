using BulletHell.Entities;
using BulletHell.Input;
using BulletHell.Scenes;

namespace BulletHell.Weapon
{
    public static class WeaponManager
    {
        public static int NextShotTicks { get; private set; } = 0;
        public static Weapon Weapon
        {
            get => s_weapon;
            set
            {
                s_weapon = value;
                Reset();
            }
        }

        private static bool CanFireWeapon => NextShotTicks == 0;

        private static Weapon s_weapon = Weapons.Pistol;

        public static void Tick()
        {
            if (!CanFireWeapon)
                NextShotTicks--;
            else if (Keybinds.MouseLeft.Held)
                FireWeapon();
        }

        public static void Reset() => NextShotTicks = 0;

        private static void FireWeapon()
        {
            NextShotTicks = Weapon.ShotTicks;
            var playerPos = GameScene.Player.Position;
            var direction = InputManager.MousePositionOffset;
            direction.Y *= -1f;
            direction -= playerPos;
            GameScene.AddEntity(new Projectile(playerPos, direction));
        }
    }
}
