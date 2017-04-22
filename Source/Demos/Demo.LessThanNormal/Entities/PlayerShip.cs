using System;
using Demo.LessThanNormal.Components;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.TextureAtlases;

namespace Demo.LessThanNormal.Entities
{
    public static class EntityTemplateServiceTemp
    {
        public static TextureAtlas TextureAtlas { get; set; }
        public static Camera2D Camera { get; set; }
    }

    [EntityTemplate(nameof(PlayerShip))]
    public class PlayerShip : EntityTemplate
    {
        protected override void Build(Entity entity)
        {
            entity.Attach<TransformComponent>();
            entity.Attach<ShipComponent>();
            entity.Attach<BodyComponent>();
            entity.Attach<SpriteComponent>(c =>
            {
                c.TextureRegion = EntityTemplateServiceTemp.TextureAtlas["Ships/spaceShips_004"];
                c.Origin = new Vector2(c.TextureRegion.Width / 2f, c.TextureRegion.Height / 2f);
            });
        }
    }
}
