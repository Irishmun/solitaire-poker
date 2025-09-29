using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameLibrary.Graphics
{
    public class TextureRegion
    {
        /// <summary>Creates a new texture region</summary>
        public TextureRegion() { }

        /// <summary>Creates a new texture region using the specified source texture.</summary>
        /// <param name="texture">The texture to use as the source texture for this texture region.</param>
        /// <param name="x">The x-coordinate position of the upper-left corner of this texture region relative to the upper-left corner of the source texture.</param>
        /// <param name="y">The y-coordinate position of the upper-left corner of this texture region relative to the upper-left corner of the source texture.</param>
        /// <param name="width">The width, in pixels, of this texture region.</param>
        /// <param name="height">The height, in pixels, of this texture region.</param>
        public TextureRegion(Texture2D texture, int x, int y, int width, int height)
        {
            Texture = texture;
            SourceRectangle = new Rectangle(x, y, width, height);
        }

        public void Draw(SpriteBatch batch, Vector2 position)
        {
            Draw(batch, position, Color.White);
        }

        public void Draw(SpriteBatch batch, Vector2 position, float layerDepth)
        {
            Draw(batch, position, Color.White, layerDepth);
        }

        public void Draw(SpriteBatch batch, Vector2 position, Color color, float layerDepth)
        {
            Draw(batch, position, color, 0, Vector2.Zero, 1, SpriteEffects.None, layerDepth);
        }

        public void Draw(SpriteBatch batch, Vector2 position, Color color)
        {
            Draw(batch, position, color, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
        }

        public void Draw(SpriteBatch batch, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
        {
            Draw(batch, position, color, rotation, origin, new Vector2(scale, scale), effects, layerDepth);
        }

        public void Draw(SpriteBatch batch, Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
        {
            batch.Draw(Texture, position, SourceRectangle, color, rotation, origin, scale, effects, layerDepth);
        }

        public Texture2D Texture { get; set; }
        public Rectangle SourceRectangle { get; set; }

        public int Width => SourceRectangle.Width;
        public int Height => SourceRectangle.Height;
    }
}
