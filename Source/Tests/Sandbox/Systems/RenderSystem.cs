using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Components;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.TextureAtlases;
using Sandbox.Components;

namespace Sandbox.Systems
{
    [Aspect(AspectType.All, typeof(SpriteComponent), typeof(TransformComponent))]
    [EntitySystem(GameLoopType.Draw, Layer = 0)]
    public class RenderSystem : EntityProcessingSystem
    {
        private ITextureRegionService _textureRegionService;
        private SpriteBatch _spriteBatch;
        
        public override void LoadContent()
        {
            _textureRegionService = Services.GetService<ITextureRegionService>();
            _spriteBatch = Services.GetService<SpriteBatch>();
        }

        protected override void Begin(GameTime gameTime)
        {
            _spriteBatch.Begin(blendState: BlendState.AlphaBlend, samplerState: SamplerState.PointClamp);
        }

        protected override void Process(GameTime gameTime, Entity entity)
        {
            var sprite = entity.Get<SpriteComponent>();
            var transform = entity.Get<TransformComponent>();
            var region = _textureRegionService.GetTextureRegion(sprite.RegionName);

            _spriteBatch.Draw(region, transform.Position, sprite.Color, transform.Rotation, sprite.Origin * region.Size, transform.Scale, sprite.Effects, sprite.Depth);
        }

        protected override void End(GameTime gameTime)
        {
            _spriteBatch.End();
        }
    }
}
