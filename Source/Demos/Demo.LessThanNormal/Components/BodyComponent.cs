using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Components;

namespace Demo.LessThanNormal.Components
{
    [EntityComponent]
    public class BodyComponent : EntityComponent
    {
        public Vector2 Velocity { get; set; }
    }
}