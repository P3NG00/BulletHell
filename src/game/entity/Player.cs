using BulletHell.Input;
using BulletHell.Scenes;
using BulletHell.Utils;
using Microsoft.Xna.Framework;

namespace BulletHell.Game.Entities
{
    public sealed class Player : AbstractEntity
    {
        private const float PLAYER_LIFE = 3f;
        private const float PLAYER_SPEED = 5f;

        public const float PLAYER_RADIUS = 16f;

        private static readonly int InvincibilityResetTicks = GameManager.SecondsToTicks(1f);

        private static DrawData PlayerDrawData => new(Textures.Circle, new(255, 0, 0));
        private static DrawData PlayerInvincibleDrawData => new(Textures.Circle, new(255, 128, 0));
        private static Color PlayerHealthColor => new(0, 255, 0);

        protected sealed override DrawData DrawData => InvincibilityTicks % 2 == 1 ? PlayerInvincibleDrawData : base.DrawData;

        public int InvincibilityTicks { get; private set; } = 0;

        private bool IsInvincible => InvincibilityTicks > 0;

        public Player() : base(Vector2.Zero, PLAYER_RADIUS, PLAYER_SPEED, PLAYER_LIFE, PlayerDrawData, healthColor: PlayerHealthColor) {}

        public sealed override void Tick()
        {
            if (IsInvincible)
                InvincibilityTicks--;
            // reset velocity
            RawVelocity = Vector2.Zero;
            // handle movement input
            if (Keybinds.MoveLeft.Held)
                RawVelocity.X--;
            if (Keybinds.MoveRight.Held)
                RawVelocity.X++;
            if (Keybinds.MoveUp.Held)
                RawVelocity.Y++;
            if (Keybinds.MoveDown.Held)
                RawVelocity.Y--;
            // base call
            base.Tick();
        }

        public sealed override void Damage(float damage = 1f)
        {
            if (IsInvincible)
                return;
            InvincibilityTicks = InvincibilityResetTicks;
            base.Damage(damage);
        }

        protected sealed override void OnDeath() => SceneManager.Scene = new GameEndScene();
    }
}
