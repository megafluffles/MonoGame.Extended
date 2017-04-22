using Demo.LessThanNormal.Components;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Entities;

namespace Demo.LessThanNormal.Systems
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
        }
    }
}