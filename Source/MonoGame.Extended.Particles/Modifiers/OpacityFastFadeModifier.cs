namespace MonoGame.Extended.Particles.Modifiers
{
    public sealed class OpacityFastFadeModifier : IModifier
    {
        private readonly float _initialOpacity;

        public OpacityFastFadeModifier(float initialOpacity = 1.0f)
        {
            _initialOpacity = initialOpacity;
        }

        public unsafe void Update(float elapsedSeconds, ParticleBuffer.ParticleIterator iterator)
        {
            while (iterator.HasNext)
            {
                var particle = iterator.Next();
                particle->Opacity = _initialOpacity - particle->Age;
            }
        }
    }
}