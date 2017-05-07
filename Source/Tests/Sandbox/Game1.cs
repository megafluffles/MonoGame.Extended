using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Components;
using MonoGame.Extended.Gui;
using MonoGame.Extended.Gui.Controls;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.TextureAtlases;
using MonoGame.Extended.ViewportAdapters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sandbox.Components;

// The Sandbox project is used for experiementing outside the normal demos.
// Any code found here should be considered experimental work in progress.
namespace Sandbox
{
    public class TextureRegionService : ITextureRegionService
    {
        private readonly TextureAtlas _textureAtlas;

        public TextureRegionService(TextureAtlas textureAtlas)
        {
            _textureAtlas = textureAtlas;
        }

        public TextureRegion2D GetTextureRegion(string name)
        {
            return _textureAtlas.GetRegion(name);
        }
    }

    public class Game1 : Game
    {
        // ReSharper disable once NotAccessedField.Local
        private GraphicsDeviceManager _graphicsDeviceManager;
        private ViewportAdapter _viewportAdapter;
        private Camera2D _camera;
        private GuiSystem _guiSystem;
        private SpriteBatch _spriteBatch;

        private readonly EntityComponentSystemManager _entityComponentSystem;
        private EntityManager _entityManager;

        public Game1()
        {
            _graphicsDeviceManager = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";

            _entityComponentSystem = new EntityComponentSystemManager(this);
            _entityManager = _entityComponentSystem.EntityManager;

            // scan for components and systems in provided assemblies
            _entityComponentSystem.Scan(Assembly.GetExecutingAssembly());
        }

        protected override void LoadContent()
        {
            IsMouseVisible = false;

            _viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, 800, 480);
            _camera = new Camera2D(_viewportAdapter);

            var skin = GuiSkin.FromFile(Content, @"Content/adventure-gui-skin.json");
            var guiRenderer = new GuiSpriteBatchRenderer(GraphicsDevice, _camera.GetViewMatrix);

            _guiSystem = new GuiSystem(_viewportAdapter, guiRenderer)
            {
                Screen = new GuiScreen(skin)
                {
                    Controls =
                    {
                        new GuiLabel { Text = "Hello World" }
                    }
                }
            };

            Services.AddService(new SpriteBatch(GraphicsDevice));
            Services.AddService(typeof(ITextureRegionService), new TextureRegionService(Content.Load<TextureAtlas>("adventure-gui-atlas")));

            var entity = CreateEntityFromFile("Entities/test.entity");
            entity.Get<TransformComponent>().Position = new Vector2(_viewportAdapter.ViewportWidth / 4f, _viewportAdapter.VirtualHeight / 4f);
        }

        private Entity CreateEntityFromFile(string path)
        {
            var entity = _entityManager.CreateEntity();
            var componentTypes = new Dictionary<string, Type>(StringComparer.CurrentCultureIgnoreCase)
            {
                {"transform", typeof(TransformComponent)},
                {"sprite", typeof(SpriteComponent)}
            };

            using (var stream = TitleContainer.OpenStream(path))
            using (var streamReader = new StreamReader(stream))
            {
                var json = streamReader.ReadToEnd();
                var components = JsonConvert.DeserializeObject<JObject>(json);

                foreach (var componentData in components)
                {
                    var key = componentData.Key;
                    var componentType = componentTypes[key];
                    var component = entity.Attach(componentType);
                    var valueJson = componentData.Value.ToString();

                    JsonConvert.PopulateObject(valueJson, component);
                }
            }

            return entity;
        }

        protected override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            _entityComponentSystem.Update(gameTime);
            _guiSystem.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _entityComponentSystem.Draw(gameTime);
            _guiSystem.Draw(gameTime);
        }
    }
}
