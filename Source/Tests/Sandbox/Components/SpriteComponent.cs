using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities;

namespace Sandbox.Components
{
    [EntityComponent]
    public class SpriteComponent : EntityComponent
    {
        public string RegionName { get; set; }
        public Color Color { get; set; } = Color.White;
        public Vector2 Origin { get; set; } = Vector2.One * 0.5f;
        public SpriteEffects Effects { get; set; } = SpriteEffects.None;
        public float Depth { get; set; } = 0;
    }
}
