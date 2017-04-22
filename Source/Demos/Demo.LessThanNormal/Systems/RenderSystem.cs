using Demo.LessThanNormal.Components;
using Demo.LessThanNormal.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities;
using MonoGame.Extended.TextureAtlases;

namespace Demo.LessThanNormal.Systems
{
    [Aspect(AspectType.All, typeof(SpriteComponent), typeof(TransformComponent))]
    [EntitySystem(GameLoopType.Draw, Layer = 0)]
    public class RenderSystem : EntityProcessingSystem
    {
        private SpriteBatch _spriteBatch;

        public RenderSystem()
        {
        }

        public override void Initialize()
        {
            base.Initialize();
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Begin(GameTime gameTime)
        {
            base.Begin(gameTime);
            _spriteBatch.Begin(blendState: BlendState.NonPremultiplied, transformMatrix: EntityTemplateServiceTemp.Camera.GetViewMatrix());
        }

        protected override void Process(GameTime gameTime, Entity entity)
        {
            base.Process(gameTime, entity);

            var transform = entity.Get<TransformComponent>();
            var sprite = entity.Get<SpriteComponent>();

            _spriteBatch.Draw(sprite.TextureRegion, transform.Position, sprite.Color, transform.Rotation, sprite.Origin, transform.Scale, sprite.Effect, layerDepth: 0);
        }

        protected override void End(GameTime gameTime)
        {
            _spriteBatch.End();
            base.End(gameTime);
        }
    }
}
