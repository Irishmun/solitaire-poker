using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MonoGameLibrary.Graphics
{
    public class ToggleSprite : Sprite
    {
        private Dictionary<string, TextureRegion> _regions;
        private bool _toggledOn;

        /// <summary>
        /// Gets or Sets the source texture represented by this texture atlas.
        /// </summary>
        public Texture2D Texture { get; set; }

        public ToggleSprite()
        {
            _regions = new Dictionary<string, TextureRegion>();
        }

        public ToggleSprite(Texture2D texture) : this()
        {
            Texture = texture;
        }
        public void AddToggleRegion(bool toggleOn, TextureRegion region)
        {
            if (toggleOn)
            {
                if (_regions.ContainsKey("On"))
                {
                    return;
                }
                _regions.Add("On", region);
                Region = region;
                return;
            }
            else
            {
                if (_regions.ContainsKey("Off"))
                {
                    return;
                }
                _regions.Add("Off", region);
                return;
            }
        }

        public void Toggle(bool toggleOn)
        {
            _toggledOn = toggleOn;
        }

        public void TryClick(Point pos, bool Clicked)
        {
            if (Clicked == false)
            {
                Toggle(false);
            }
            if (ContainsPoint(pos))
            {
                Toggle(true);
            }
        }

        /// <summary>
        /// Submit this sprite for drawing to the current batch.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch instance used for batching draw calls.</param>
        /// <param name="position">The xy-coordinate position to render this sprite at.</param>
        public new void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            TextureRegion reg = _toggledOn ? _regions["On"] : _regions["Off"];
            reg.Draw(spriteBatch, position, Color, Rotation, Origin, Scale, Effects, LayerDepth);
        }
    }
}
