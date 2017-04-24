using Demo.BrickOut.Components;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Components;

namespace Demo.BrickOut.Systems
{
    [Aspect(AspectType.All, typeof(TransformComponent), typeof(BodyComponent))]
    [EntitySystem(GameLoopType.Update, Layer = 0)]
    public class PhysicsSystem : EntityProcessingSystem
    {
        protected override void Process(GameTime gameTime, Entity entity)
        {
            var deltaTime = gameTime.GetElapsedSeconds();
            var transform = entity.Get<TransformComponent>();
            var body = entity.Get<BodyComponent>();

            transform.Position += body.Velocity * deltaTime;

            if (transform.Position.X - body.Size.Width / 2f <= 0)
                body.Velocity.X = -body.Velocity.X;

            if (transform.Position.X + body.Size.Width >= GameMain.VirtualWidth)
                body.Velocity.X = -body.Velocity.X;

            if (transform.Position.Y - body.Size.Height <= 0)
                body.Velocity.Y = -body.Velocity.Y;

            if (transform.Position.Y + body.Size.Height >= GameMain.VirtualHeight)
                body.Velocity.Y = -body.Velocity.Y;
        }
    }
}