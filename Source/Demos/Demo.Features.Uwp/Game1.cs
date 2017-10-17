using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Demo.Features.Uwp
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : GameMain
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1() : base(new PlatformConfig() {  IsFullScreen = true})
        {
        }
    }
}
