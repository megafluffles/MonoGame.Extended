using Demo.LessThanNormal.Components;
using Demo.LessThanNormal.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Components;

namespace Demo.LessThanNormal.Systems
{
    [Aspect(AspectType.All, typeof(ShipComponent), typeof(TransformComponent), typeof(BodyComponent))]
    [EntitySystem(GameLoopType.Update, Layer = 0)]
    public class ShipControlSystem : EntityProcessingSystem
    {
        private KeyboardState _previousKeyboardState;

        public ShipControlSystem()
        {
        }

        protected override void Process(GameTime gameTime, Entity entity)
        {
            var deltaTime = gameTime.GetElapsedSeconds();
            var transform = entity.Get<TransformComponent>();
            var body = entity.Get<BodyComponent>();
            var ship = entity.Get<ShipComponent>();
            var acceleration = Vector2.Zero;

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

            if (keyboardState.IsKeyDown(Keys.Space) && !_previousKeyboardState.IsKeyDown(Keys.Space))
                FireMissile(transform, body, direction);

            acceleration += direction * ship.LeftThrust + direction * ship.RightThrust;
            body.Velocity += acceleration;

            _previousKeyboardState = keyboardState;
        }

        private void FireMissile(TransformComponent transform, BodyComponent body, Vector2 direction)
        {
            var missile = EntityManager.CreateEntityFromTemplate(nameof(Missile));
            var missileTransform = missile.Get<TransformComponent>();
            var missileBody = missile.Get<BodyComponent>();

            missileTransform.Position = transform.Position;
            missileTransform.Rotation = transform.Rotation;
            missileBody.Velocity = body.Velocity + direction * 800;
        }
    }
}