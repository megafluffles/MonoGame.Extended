using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Components;

namespace Demo.LessThanNormal.Components
{
    [EntityComponent]
    public class ShipComponent : EntityComponent
    {
        public float ForwardThrust { get; set; } = 3;
        public float ReverseThrust { get; set; } = 2;
        public float LeftThrust { get; set; }
        public float RightThrust { get; set; }
        public float AngularAcceleration { get; set; } = MathHelper.PiOver2;
    }
}