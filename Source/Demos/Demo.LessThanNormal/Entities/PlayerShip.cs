using Demo.LessThanNormal.Components;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Serialization;

namespace Demo.LessThanNormal.Entities
{
    [EntityTemplate(nameof(PlayerShip))]
    public class PlayerShip : EntityTemplate
    {
        protected override void Build(Entity entity)
        {
            var textureRegionService = Services.GetService<ITextureRegionService>();

            entity.Attach<TransformComponent>();
            entity.Attach<ShipComponent>();
            entity.Attach<BodyComponent>();
            entity.Attach<SpriteComponent>(c =>
            {
                c.TextureRegion = textureRegionService.GetTextureRegion("Ships/spaceShips_004");
                c.Origin = new Vector2(c.TextureRegion.Width / 2f, c.TextureRegion.Height / 2f);
            });
        }
    }
}
