using BulletHell.Game.Entities;
using BulletHell.Input;
using BulletHell.Scenes;
using BulletHell.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BulletHell.Game.Weapon
{
    public static class WeaponManager
    {
        private const float PROJECTILE_SPAWN_DISTANCE = Player.PLAYER_RADIUS + Projectile.PROJECTILE_RADIUS;

        public static int SwitchTicks { get; private set; } = 0;
        public static int ReloadTicks { get; private set; } = 0;
        public static int NextShotTicks { get; private set; } = 0;

        public static Weapon Weapon
        {
            get => s_weapon;
            private set
            {
                if (s_weapon == value || IsSwitching || IsFiring)
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

        private static ref int CurrentAmmo => ref s_clipAmounts[s_weapon.ID];

        private static int[] s_clipAmounts;
        private static Weapon s_weapon;

        public static void Update()
        {
            if (Keybinds.Reload.PressedThisFrame && !IsFiring && !IsReloading && AmmoAmount != Weapon.ClipSize)
                ReloadTicks = Weapon.ReloadTicks;
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
            s_clipAmounts = Util.PopulateArray<int>(Weapons.Amount, id => Weapons.FromID(id).ClipSize);
            // reset value to allow weapon switching
            s_weapon = null;
            SwitchTicks = 0;
            ReloadTicks = 0;
            NextShotTicks = 0;
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
            var direction = InputManager.MousePositionOffset - playerPos;
            if (direction == Vector2.Zero)
                direction = Vector2.UnitX;
            else
                direction.Normalize();
            var spawnPos = playerPos + (direction * PROJECTILE_SPAWN_DISTANCE);
            new Projectile(spawnPos, direction);
        }
    }
}
