using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;

namespace MonoGame.Extended.Sprites
{
    public interface ISprite
    {
        bool IsVisible { get; }
        TextureRegion2D TextureRegion { get; }
        Color Color { get; }
        Vector2 Origin { get; }
        SpriteEffects Effect { get; }
    }

    public interface ISpriteBatchDrawable : ISprite
    {
        Vector2 Position { get; }
        float Rotation { get; }
        Vector2 Scale { get; }
    }
}