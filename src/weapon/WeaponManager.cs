using BulletHell.Entities;
using BulletHell.Input;
using BulletHell.Scenes;
using Microsoft.Xna.Framework.Input;

namespace BulletHell.Weapon
{
    public static class WeaponManager
    {
        public static int NextShotTicks { get; private set; } = 0;
        public static int ReloadTicks { get; private set; } = 0;
        public static int ClipAmount { get; private set; } = 0;

        public static Weapon Weapon
        {
            get => s_weapon;
            private set
            {
                if (s_weapon == value)
                    return;
                s_weapon = value;
                NextShotTicks = 0;
                ReloadTicks = 0;
                ClipAmount = value.ClipSize;
            }
        }

        private static bool CanFireWeapon => NextShotTicks == 0;
        private static bool Reloading => ReloadTicks > 0;
        private static bool Empty => ClipAmount == 0;

        private static Weapon s_weapon;

        // TODO disable weapon switching while reloading

        public static void Update()
        {
            for (int i = 0; i < Weapons.Amount; i++)
                if (InputManager.KeyPressedThisFrame(Keys.D1 + i))
                    WeaponManager.Weapon = Weapons.FromID(i);
        }

        public static void Tick()
        {
            if (Reloading)
            {
                ReloadTicks--;
                if (Reloading)
                    return;
                else
                    ClipAmount = Weapon.ClipSize;
            }
            if (!CanFireWeapon)
                NextShotTicks--;
            else if (Keybinds.MouseLeft.Held && !Empty)
                FireWeapon();
        }

        public static void Reset() => Weapon = Weapons.Pistol;

        private static void FireWeapon()
        {
            ClipAmount--;
            if (Empty)
                ReloadTicks = Weapon.ReloadTicks;
            else
                NextShotTicks = Weapon.ShotTicks;
            var playerPos = GameScene.Player.Position;
            var direction = InputManager.MousePositionOffset;
            direction -= playerPos;
            GameScene.AddProjectile(new Projectile(playerPos, direction));
        }
    }
}
