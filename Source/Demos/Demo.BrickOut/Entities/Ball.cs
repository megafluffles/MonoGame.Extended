using Demo.BrickOut.Components;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Components;
using MonoGame.Extended.Serialization;

namespace Demo.BrickOut.Entities
{
    [EntityTemplate(nameof(Ball))]
    public class Ball : EntityTemplate
    {
        protected override void Build(Entity entity)
        {
            var textureRegionService = Services.GetService<ITextureRegionService>();
            var textureRegion = textureRegionService.GetTextureRegion("ballBlue_10");

            entity.Attach<TransformComponent>(c => c.Position = new Vector2(200, 200));
            entity.Attach<BodyComponent>(c =>
            {
                c.Size = textureRegion.Bounds.Size;
                c.Velocity = new Vector2(-200, -300);
            });
            entity.Attach<SpriteComponent>(c =>
            {
                c.TextureRegion = textureRegion;
                c.Origin = new Vector2(textureRegion.Width / 2f, textureRegion.Height / 2f);
            });
        }
    }
}
