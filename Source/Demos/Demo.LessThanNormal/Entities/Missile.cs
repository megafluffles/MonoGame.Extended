using Demo.LessThanNormal.Components;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;

namespace Demo.LessThanNormal.Entities
{
    [EntityTemplate(nameof(Missile))]
    public class Missile : EntityTemplate
    {
        protected override void Build(Entity entity)
        {
            entity.Attach<TransformComponent>();
            entity.Attach<BodyComponent>();
            entity.Attach<SpriteComponent>(c =>
            {
                c.TextureRegion = EntityTemplateServiceTemp.TextureAtlas["Missiles/spaceMissiles_013"];
                c.Origin = new Vector2(c.TextureRegion.Width / 2f, c.TextureRegion.Height / 2f);
            });
        }
    }
}