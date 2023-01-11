using BulletHell.Entities;
using BulletHell.Input;
using BulletHell.Scenes;
using BulletHell.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BulletHell.Weapon
{
    public static class WeaponManager
    {
        public static int SwitchTicks { get; private set; } = 0;
        public static int ReloadTicks { get; private set; } = 0;
        public static int NextShotTicks { get; private set; } = 0;

        private static ref int CurrentAmmo => ref s_clipAmounts[s_weapon.ID];

        public static Weapon Weapon
        {
            get => s_weapon;
            private set
            {
                if (s_weapon == value || IsSwitching)
                    return;
                s_weapon = value;
                SwitchTicks = value.SwitchTicks;
                ReloadTicks = s_clipAmounts[value.ID] <= 0 ? value.ReloadTicks : 0;
            }
        }

        public static int AmmoAmount => CurrentAmmo;
        public static bool IsSwitching => SwitchTicks > 0;
        public static bool IsReloading => ReloadTicks > 0;
        public static bool IsFiring => NextShotTicks > 0;
        public static bool IsEmpty => AmmoAmount <= 0;

        public static readonly int MaxClipSize = GameManager.SecondsToTicks(1f);

        private static int[] s_clipAmounts;
        private static Weapon s_weapon;

        public static void Update()
        {
            for (int i = 0; i < Weapons.Amount; i++)
                if (InputManager.KeyPressedThisFrame(Keys.D1 + i))
                    WeaponManager.Weapon = Weapons.FromID(i);
        }

        public static void Tick()
        {
            if (IsSwitching)
                SwitchTicks--;
            else if (IsReloading)
            {
                ReloadTicks--;
                if (!IsReloading)
                    CurrentAmmo = Weapon.ClipSize;
            }
            else if (IsFiring)
                NextShotTicks--;
            else if (Keybinds.MouseLeft.Held && !IsEmpty)
                FireWeapon();
        }

        public static void Reset()
        {
            // reset clip amounts
            s_clipAmounts = Util.Populate<int>(Weapons.Amount, id => Weapons.FromID(id).ClipSize);
            // reset value to allow weapon switching
            s_weapon = null;
            // switch to pistol
            Weapon = Weapons.Pistol;
        }

        private static void FireWeapon()
        {
            CurrentAmmo--;
            if (IsEmpty)
                ReloadTicks = Weapon.ReloadTicks;
            else
                NextShotTicks = Weapon.ShotTicks;
            var playerPos = GameScene.Player.Position;
            var direction = InputManager.MousePositionOffset;
            direction -= playerPos;
            if (direction.Length() == 0)
                direction = Vector2.UnitX;
            else
                direction.Normalize();
            var spawnPos = playerPos + (direction * (GameScene.Player.Radius * 1.25f));
            GameScene.AddProjectile(new Projectile(spawnPos, direction));
        }
    }
}
