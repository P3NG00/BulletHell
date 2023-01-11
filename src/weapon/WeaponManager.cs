using System;
using BulletHell.Entities;
using BulletHell.Input;
using BulletHell.Scenes;
using BulletHell.Utils;
using Microsoft.Xna.Framework.Input;

namespace BulletHell.Weapon
{
    public static class WeaponManager
    {
        public static int NextShotTicks { get; private set; } = 0;
        public static int ReloadTicks { get; private set; } = 0;

        public static Weapon Weapon
        {
            get => s_weapon;
            private set
            {
                if (s_weapon == value || IsReloading)
                    return;
                s_weapon = value;
                ReloadTicks = Weapon.SwitchToTicks;
            }
        }

        public static ref int AmmoAmount => ref s_clipAmounts[s_weapon.ID];
        public static bool CanFireWeapon => NextShotTicks == 0;
        public static bool IsReloading => ReloadTicks > 0;
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
            if (IsReloading)
                ReloadTicks--;
            else if (!CanFireWeapon)
                NextShotTicks--;
            else if (Keybinds.MouseLeft.Held && !IsEmpty)
                FireWeapon();
        }

        public static void Reset()
        {
            // reset reload ticks to allow weapon switching
            ReloadTicks = 0;
            // switch to pistol
            Weapon = Weapons.Pistol;
            // reset clip amounts
            s_clipAmounts = Util.Populate<int>(Weapons.Amount, id => Weapons.FromID(id).ClipSize);
        }

        private static void FireWeapon()
        {
            AmmoAmount--;
            if (IsEmpty)
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
