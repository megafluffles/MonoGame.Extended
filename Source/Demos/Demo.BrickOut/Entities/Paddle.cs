using Demo.BrickOut.Components;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Components;
using MonoGame.Extended.Serialization;

namespace Demo.BrickOut.Entities
{
    [EntityTemplate(nameof(Paddle))]
    public class Paddle : EntityTemplate
    {
        protected override void Build(Entity entity)
        {
            var textureRegionService = Services.GetService<ITextureRegionService>();
            var textureRegion = textureRegionService.GetTextureRegion("paddle_05");

            entity.Attach<PaddleComponent>();
            entity.Attach<TransformComponent>(c => c.Position = new Vector2(GameMain.VirtualWidth / 2f, GameMain.VirtualHeight - 50));
            entity.Attach<BodyComponent>(c => c.Size = textureRegion.Bounds.Size);
            entity.Attach<SpriteComponent>(c =>
            {
                c.TextureRegion = textureRegion;
                c.Origin = new Vector2(textureRegion.Width / 2f, textureRegion.Height / 2f);
            });
        }
    }
}