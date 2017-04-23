using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Components;
using MonoGame.Extended.Particles;

namespace Demo.LessThanNormal.Components
{
    [EntityComponent]
    public class ParticleEffectComponent : EntityComponent
    {
        public bool Trigger { get; set; }
        public ParticleEffect Effect { get; set; }
    }
}