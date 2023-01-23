using BulletHell.Input;
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

        private static DrawData PlayerDrawData => new(Textures.Circle, new(0, 255, 0));
        private static DrawData PlayerInvincibleDrawData => new(Textures.Circle, new(255, 128, 0));

        protected sealed override DrawData DrawData => _invincibilityTicks % 2 == 1 ? PlayerInvincibleDrawData : base.DrawData;

        public int InvincibilityTicks => _invincibilityTicks;

        private bool IsInvincible => _invincibilityTicks > 0;

        private int _invincibilityTicks = 0;

        public Player() :
            base(Vector2.Zero,
                 PLAYER_RADIUS,
                 PLAYER_SPEED,
                 PLAYER_LIFE,
                 PlayerDrawData) {}

        public sealed override void Tick()
        {
            if (IsInvincible)
                _invincibilityTicks--;
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
            _invincibilityTicks = InvincibilityResetTicks;
            base.Damage(damage);
            // TODO go to game end scene when dead (add OnDeath overridable method)
        }
    }
}
