using BulletHell.Utils;
using Microsoft.Xna.Framework;

namespace BulletHell.Game.Entities
{
    public abstract class AbstractCreatureEntity : AbstractEntity
    {
        private const float TEXT_HEALTH_SCALE = 2f;
        private const float DASH_ROTATION_LERP = 0.05f;

        public bool CanDash => DashCooldownTicks <= 0;
        public bool IsDashing => DashTicks > 0;

        public int DashTicks { get; private set; } = 0;
        public int DashCooldownTicks { get; private set; } = 0;

        protected sealed override float MoveSpeed => IsDashing ? _dashSpeed : base.MoveSpeed;

        protected sealed override Vector2 RawVelocity
        {
            get => base.RawVelocity;
            set => base.RawVelocity = IsDashing ? Vector2.Lerp(base.RawVelocity, value, DASH_ROTATION_LERP) : value;
        }

        private readonly float _dashSpeed;
        private readonly int _dashResetTicks;
        private readonly int _dashCooldownResetTicks;

        protected AbstractCreatureEntity(
            Vector2 position,
            float radius,
            float moveSpeed,
            float life,
            DrawData drawData,
            Vector2? velocity = null,
            float dashMultiplier = 0f,
            float dashSeconds = 0f,
            float dashCooldownSeconds = 0) :
        base(
            position: position,
            radius: radius,
            moveSpeed: moveSpeed,
            life: life,
            drawData: drawData,
            velocity: velocity)
        {
            _dashSpeed = moveSpeed * dashMultiplier;
            _dashResetTicks = GameManager.SecondsToTicks(dashSeconds);
            _dashCooldownResetTicks = GameManager.SecondsToTicks(dashCooldownSeconds);
        }

        public override void Tick()
        {
            // tick dashing
            if (!CanDash)
                DashCooldownTicks--;
            if (IsDashing)
                DashTicks--;
            // base call
            base.Tick();
        }

        public override void Draw()
        {
            if (!Alive)
                return;
            // base draw
            base.Draw();
            // draw health
            var drawScale = new Vector2(TEXT_HEALTH_SCALE);
            FontType.type_writer.DrawOffsetStringCentered(DrawPosition, Life.ToString(), Colors.Health, drawScale);
        }

        protected void Dash()
        {
            if (_dashSpeed <= 0f || !CanDash || RawVelocity == Vector2.Zero)
                return;
            DashTicks = _dashResetTicks;
            DashCooldownTicks = _dashCooldownResetTicks;
        }
    }
}
