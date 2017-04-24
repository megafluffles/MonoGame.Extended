using System;
using Demo.LessThanNormal.Components;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Components;
using MonoGame.Extended.Particles;
using MonoGame.Extended.Particles.Modifiers;
using MonoGame.Extended.Particles.Profiles;
using MonoGame.Extended.Serialization;

namespace Demo.LessThanNormal.Entities
{
    [EntityTemplate(nameof(Missile))]
    public class Missile : EntityTemplate
    {
        protected override void Build(Entity entity)
        {
            var textureRegionService = Services.GetService<ITextureRegionService>();
            var textureRegion = textureRegionService.GetTextureRegion("Missiles/spaceMissiles_013");

            entity.Attach<TransformComponent>();
            entity.Attach<BodyComponent>();
            entity.Attach<SpriteComponent>(c =>
            {
                c.TextureRegion = textureRegion;
                c.Origin = new Vector2(c.TextureRegion.Width / 2f, c.TextureRegion.Height / 2f);
            });
            entity.Attach<ParticleEffectComponent>(c =>
            {
                var region = textureRegionService.GetTextureRegion("Effects/spaceEffects_008");

                c.Trigger = true;
                c.Effect = new ParticleEffect
                {
                    Emitters = new[]
                    {
                        new ParticleEmitter(region, 256, TimeSpan.FromSeconds(1.0f), Profile.Circle(1, Profile.CircleRadiation.Out), autoTrigger: false)
                        {
                            Parameters = new ParticleReleaseParameters
                            {
                                Color = Color.DarkGray.ToHsl(),
                                Quantity = 3,
                                Rotation = new Range<float>(-MathHelper.Pi, MathHelper.Pi),
                                Speed = new Range<float>(15, 20),
                                Scale = new Range<float>(region.Width * 0.6f, region.Width * 0.6f)
                            },
                            Modifiers = new IModifier[]
                            {
                                new OpacityFastFadeModifier(0.5f)
                            }
                        }
                    }
                };
            });
        }
    }
}