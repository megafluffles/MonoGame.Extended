using Demo.BrickOut.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Components;

namespace Demo.BrickOut.Systems
{
    [Aspect(AspectType.All, typeof(PaddleComponent), typeof(TransformComponent), typeof(BodyComponent))]
    [EntitySystem(GameLoopType.Update, Layer = 0)]
    public class PaddleSystem : EntityProcessingSystem
    {
        protected override void Process(GameTime gameTime, Entity entity)
        {
            var transform = entity.Get<TransformComponent>();
            var body = entity.Get<BodyComponent>();
            var mouseState = Mouse.GetState();
            
            transform.Position = new Vector2(mouseState.Position.X, transform.Position.Y);

            if (transform.Position.X - body.Size.Width / 2f <= 0)
                transform.Position = new Vector2(body.Size.Width / 2f, transform.Position.Y);

            if (transform.Position.X + body.Size.Width / 2f >= GameMain.VirtualWidth)
                transform.Position = new Vector2(GameMain.VirtualWidth - body.Size.Width / 2f, transform.Position.Y);
        }
    }
}