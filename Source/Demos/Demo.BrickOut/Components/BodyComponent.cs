using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Components;

namespace Demo.BrickOut.Components
{
    [EntityComponent]
    public class BodyComponent : EntityComponent
    {
        public Size2 Size;
        public Vector2 Velocity;
    }
}