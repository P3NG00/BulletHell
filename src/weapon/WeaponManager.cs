using BulletHell.Entities;
using BulletHell.Input;
using BulletHell.Scenes;
using BulletHell.Utils;

namespace BulletHell.Weapon
{
    public static class WeaponManager
    {
        private const int NEXT_SHOT_TICKS = (int)(GameManager.TICKS_PER_SECOND * 0.5f);

        public static int NextShotTicks { get; private set; } = 0;

        private static bool CanFireWeapon => NextShotTicks == 0;

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
            NextShotTicks = NEXT_SHOT_TICKS;
            var playerPos = GameScene.Player.Position;
            var direction = InputManager.MousePositionOffset;
            direction.Y *= -1f;
            direction -= playerPos;
            GameScene.AddEntity(new Projectile(playerPos, direction));
        }
    }
}
