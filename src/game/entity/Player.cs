using BulletHell.Input;
using BulletHell.Utils;
using Microsoft.Xna.Framework;

namespace BulletHell.Game.Entities
{
    public sealed class Player : AbstractCreatureEntity
    {
        private const float PLAYER_LIFE = 3f;
        private const float PLAYER_SPEED = 5f;
        private const float PLAYER_DASH_MULT = 2.5f;
        private const float PLAYER_DASH_SECONDS = 0.25f;
        private const float PLAYER_DASH_COOLDOWN_SECONDS = 2f;

        public const float PLAYER_RADIUS = 16f;

        private static readonly int InvincibilityResetTicks = GameManager.SecondsToTicks(1f);

        private static DrawData PlayerDrawData => new(Textures.Circle, Colors.Player);
        private static DrawData PlayerInvincibleDrawData => new(Textures.Circle, Colors.PlayerInvincible);

        protected sealed override DrawData DrawData => !InvincibilityTicks.IsEven() ? PlayerInvincibleDrawData : base.DrawData;

        public int InvincibilityTicks { get; private set; } = 0;

        private bool IsInvincible => InvincibilityTicks > 0;

        public Player() : base(Vector2.Zero, PLAYER_RADIUS, PLAYER_SPEED, PLAYER_LIFE, PlayerDrawData, healthColor: Colors.PlayerHealth, dashMultiplier: PLAYER_DASH_MULT, dashSeconds: PLAYER_DASH_SECONDS, dashCooldownSeconds: PLAYER_DASH_COOLDOWN_SECONDS) {}

        public void HandleInput()
        {
            // check dash keybind
            if (Keybinds.MoveDash.PressedThisFrame)
                Dash();
        }

        public sealed override void Tick()
        {
            // tick invincibility
            if (IsInvincible)
                InvincibilityTicks--;
            // handle movement input
            var direction = Vector2.Zero;
            if (Keybinds.MoveLeft.Held)
                direction.X--;
            if (Keybinds.MoveRight.Held)
                direction.X++;
            if (Keybinds.MoveUp.Held)
                direction.Y++;
            if (Keybinds.MoveDown.Held)
                direction.Y--;
            RawVelocity = direction;
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
    }
}
