using Demo.LessThanNormal.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Components;
using MonoGame.Extended.Particles;

namespace Demo.LessThanNormal.Systems
{
    [Aspect(AspectType.All, typeof(ParticleEffectComponent), typeof(TransformComponent))]
    [EntitySystem(GameLoopType.Draw, Layer = 0)]
    public class ParticleEffectSystem : EntityProcessingSystem
    {
        private Camera2D _camera;
        private SpriteBatch _spriteBatch;
        
        public override void Initialize()
        {
            _camera = Services.GetService<Camera2D>();
            _spriteBatch = Services.GetService<SpriteBatch>();
        }

        protected override void Begin(GameTime gameTime)
        {
            _spriteBatch.Begin(blendState: BlendState.NonPremultiplied, transformMatrix: _camera.GetViewMatrix());
        }

        protected override void Process(GameTime gameTime, Entity entity)
        {
            var deltaTime = gameTime.GetElapsedSeconds();
            var transform = entity.Get<TransformComponent>();
            var component = entity.Get<ParticleEffectComponent>();

            if (component.Trigger)
                component.Effect.Trigger(transform.Position);

            component.Effect.Update(deltaTime);
            _spriteBatch.Draw(component.Effect);
        }

        protected override void End(GameTime gameTime)
        {
            _spriteBatch.End();
        }
    }
}