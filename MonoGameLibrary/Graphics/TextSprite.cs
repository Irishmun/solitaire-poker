using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonoGameLibrary.Graphics
{
    public class TextSprite
    {
        private SpriteFont _font;
        private bool _useShadow;
        private Vector2 _shadowOffset = Vector2.One;
        private string _text;
        private Color _foreColor = Color.White;
        private Color _backColor = Color.Black;
        private float _alpha = 1;
        private Vector2 _position;

        public TextSprite()
        { }

        public TextSprite(SpriteFont font, bool useShadow = true) : this()
        {
            _font = font;
            _useShadow = useShadow;
        }

        public TextSprite(SpriteFont font, bool useShadow, Vector2 offset) : this(font, useShadow)
        {
            _shadowOffset = offset;
        }

        public void Draw(SpriteBatch batch)
        {
            Draw(batch, _position);
        }

        public void Draw(SpriteBatch batch, Vector2 position)
        {
            float alpha = Math.Clamp(Math.Min(_alpha, 1), 0, 1);
            batch.DrawString(_font, _text, position, Color.Lerp(Color.Transparent, _foreColor, alpha));// _foreColor);
            if (_useShadow)
            {
                batch.DrawString(_font, _text, position + _shadowOffset, Color.Lerp(Color.Transparent, _backColor, alpha));
            }
        }


        public string Text { get => _text; set => _text = value; }
        public Color Color { get => _foreColor; set => _foreColor = value; }
        public float Alpha { get => _alpha; set => _alpha = value; }
        public Vector2 Position { get => _position; set => _position = value; }
    }
}
