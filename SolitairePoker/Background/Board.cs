using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;

namespace SolitairePoker.Background
{
    public class Board
    {
        private const int MAX_HAND_CARDS = 5;

        private TextureRegion _boardTex;
        private Rectangle _deckRect;
        // private Vector2 _handFieldPos;
        private Vector2 _handFieldCenter;
        private Rectangle _handRect;

        private ToggleSprite _buttonPlayHand,_buttonDiscard;

        public void LoadBoard()
        {
            _boardTex = new TextureRegion(Core.Content.Load<Texture2D>("UI/board"), 0, 0, 640, 480);
            _handFieldCenter = new Vector2(253, 391);
            //_deckFieldPos = new Vector2(529, 332);
            _deckRect = new Rectangle(529, 332, 96, 128);
            //_handFieldPos = new Vector2(28, 332);
            _handRect = new Rectangle(28, 332, 460, 128);
        }

        public void DrawBoard(SpriteBatch batch)
        {
            _boardTex.Draw(batch, Vector2.Zero);
        }

        public bool IsPointInHandField(Point pos)
        {
            return _handRect.Contains(pos);
        }
        public bool IsPointInDeckField(Point pos)
        {
            return _deckRect.Contains(pos);
        }
        public Vector2 DeckFieldPos { get => _deckRect.Location.ToVector2(); set => _deckRect.Location = value.ToPoint(); }
        //public Vector2 HandFieldPos { get => _handFieldPos; set => _handFieldPos = value; }
        public Vector2 HandFieldCenter { get => _handFieldCenter; set => _handFieldCenter = value; }
    }
}
