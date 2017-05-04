using System;
using Demo.LessThanNormal.Components;
using Demo.LessThanNormal.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Components;

namespace Demo.LessThanNormal.Systems
{
    [Aspect(AspectType.All, typeof(ShipComponent), typeof(TransformComponent), typeof(BodyComponent), typeof(ParticleEffectComponent))]
    [EntitySystem(GameLoopType.Update, Layer = 0)]
    public class ShipControlSystem : EntityProcessingSystem
    {
        private float _fireDelay;

        public ShipControlSystem()
        {
        }

        
        protected override void Process(GameTime gameTime, Entity entity)
        {
            var deltaTime = gameTime.GetElapsedSeconds();
            var transform = entity.Get<TransformComponent>();
            var body = entity.Get<BodyComponent>();
            var ship = entity.Get<ShipComponent>();
            var particle = entity.Get<ParticleEffectComponent>();
            var acceleration = Vector2.Zero;

            var mouseState = Mouse.GetState();
            var mx = mouseState.X - GameMain.VirtualWidth / 2;
            var my = mouseState.Y - GameMain.VirtualHeight / 2;

            var d = new Vector2(mx, my);
            
            if (d != Vector2.Zero)
            {
                var dd = d;
                transform.Position += dd;
                transform.Rotation = (dd * 100).ToAngle();
            }

            Mouse.SetPosition(GameMain.VirtualWidth / 2, GameMain.VirtualHeight / 2);


            var keyboardState = Keyboard.GetState();
            var direction = Vector2.UnitY.Rotate(transform.Rotation).NormalizedCopy();

            ship.LeftThrust = 0;
            ship.RightThrust = 0;

            if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up))
            {
                ship.LeftThrust = ship.ForwardThrust;
                ship.RightThrust = ship.ForwardThrust;
            }

            if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down))
            {
                ship.LeftThrust = -ship.ReverseThrust;
                ship.RightThrust = -ship.ReverseThrust;
            }

            if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
            {
                ship.LeftThrust = ship.ForwardThrust;
                transform.Rotation -= deltaTime * ship.AngularAcceleration;
            }

            if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
            {
                ship.RightThrust = ship.ForwardThrust;
                transform.Rotation += deltaTime * ship.AngularAcceleration;
            }

            if (keyboardState.IsKeyDown(Keys.Space) && _fireDelay <= 0)
            {
                FireMissile(transform, body, direction, new Vector2(25, -40));
                FireMissile(transform, body, direction, new Vector2(-25, -40));
                _fireDelay = 0.2f;
            }

            particle.Trigger = ship.LeftThrust > 0 || ship.RightThrust > 0;

            acceleration += direction * ship.LeftThrust + direction * ship.RightThrust;
            body.Velocity += acceleration;

            if (_fireDelay > 0)
                _fireDelay -= deltaTime;
        }

        private void FireMissile(TransformComponent transform, BodyComponent body, Vector2 direction, Vector2 offset)
        {
            var missile = EntityManager.CreateEntityFromTemplate(nameof(Missile));
            var missileTransform = missile.Get<TransformComponent>();
            var missileBody = missile.Get<BodyComponent>();

            missileTransform.Rotation = transform.Rotation + MathHelper.Pi;
            missileTransform.Position = transform.Position + offset.Rotate(missileTransform.Rotation);
            missileBody.Velocity = body.Velocity + direction * 600;
        }
    }
}