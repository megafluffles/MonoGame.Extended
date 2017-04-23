using MonoGame.Extended.Serialization;
using MonoGame.Extended.TextureAtlases;

namespace Demo.LessThanNormal
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
}