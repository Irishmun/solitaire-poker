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
        private Vector2 _deckFieldPos;
        private Vector2 _handFieldPos;


        public void LoadBoard()
        {
            _boardTex = new TextureRegion(Core.Content.Load<Texture2D>("UI/board"), 0, 0, 640, 480);
            _deckFieldPos = new Vector2(529, 332);
            //_deckFieldPos = new Vector2(561, 364);
            _handFieldPos = new Vector2(28, 332);
            //_handFieldPos = new Vector2(60, 364);
        }

        public void DrawBoard(SpriteBatch batch)
        {
            _boardTex.Draw(batch, Vector2.Zero);
        }
        public Vector2 DeckFieldPos { get => _deckFieldPos; set => _deckFieldPos = value; }
        public Vector2 HandFieldPos { get => _handFieldPos; set => _handFieldPos = value; }

    }
}
