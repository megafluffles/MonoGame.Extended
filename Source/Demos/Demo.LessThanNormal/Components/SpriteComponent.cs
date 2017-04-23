using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Components;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;

namespace Demo.LessThanNormal.Components
{
    [EntityComponent]
    public class SpriteComponent : EntityComponent, ISprite
    {
        public TextureRegion2D TextureRegion { get; set; }
        public bool IsVisible { get; set; } = true;
        public Color Color { get; set; } = Color.White;
        public Vector2 Origin { get; set; } = Vector2.One * 0.5f;
        public SpriteEffects Effect { get; set; } = SpriteEffects.None;
    }
}
