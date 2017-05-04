using System.Reflection;
using Demo.LessThanNormal.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.TextureAtlases;
using MonoGame.Extended.ViewportAdapters;

namespace Demo.LessThanNormal
{
    public class GameMain : Game
    {
        // ReSharper disable once NotAccessedField.Local
        private GraphicsDeviceManager _graphicsDeviceManager;
        private ViewportAdapter _viewportAdapter;

        public const int VirtualWidth = 1024;
        public const int VirtualHeight = 768;

        private readonly EntityComponentSystem _entityComponentSystem;
        private readonly EntityManager _entityManager;

        public GameMain()
        {
            _graphicsDeviceManager = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = VirtualWidth,
                PreferredBackBufferHeight = VirtualHeight
            };
            Content.RootDirectory = "Content";
            IsMouseVisible = false;

            _entityComponentSystem = new EntityComponentSystem(this);
            _entityManager = _entityComponentSystem.EntityManager;

            // scan for components and systems in provided assemblies
            _entityComponentSystem.Scan(Assembly.GetExecutingAssembly());
        }

        protected override void Initialize()
        {
            base.Initialize();

            Mouse.SetPosition(VirtualWidth / 2, VirtualHeight / 2);
        }

        protected override void LoadContent()
        {
            Services.AddService(new SpriteBatch(GraphicsDevice));

            _viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, VirtualWidth, VirtualHeight);
            var camera = new Camera2D(_viewportAdapter) {Zoom = 0.5f};
            camera.LookAt(Vector2.Zero);
            Services.AddService(camera);

            var textureAtlas = Content.Load<TextureAtlas>("space-shooter-ext-atlas");
            Services.AddService<ITextureRegionService>(new TextureRegionService(textureAtlas));

            _entityComponentSystem.Initialize();

            _entityManager.CreateEntityFromTemplate(nameof(PlayerShip));
        }

        protected override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            _entityComponentSystem.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _entityComponentSystem.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
