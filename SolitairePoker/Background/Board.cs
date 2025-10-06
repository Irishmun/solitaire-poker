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

        private ToggleSprite _buttonPlayHand, _buttonDiscard;

        public void LoadBoard()
        {
            _boardTex = new TextureRegion(Core.Content.Load<Texture2D>("UI/board"), 0, 0, 640, 480);
            _handFieldCenter = new Vector2(253, 391);
            //_deckFieldPos = new Vector2(529, 332);
            _deckRect = new Rectangle(529, 332, 96, 128);
            //_handFieldPos = new Vector2(28, 332);
            _handRect = new Rectangle(28, 332, 460, 128);

            Texture2D buttonTex = Core.Content.Load<Texture2D>("UI/buttons");
            TextureRegion buttonReleased = new TextureRegion(buttonTex, 0, 0, 96, 32);
            TextureRegion buttonPressed = new TextureRegion(buttonTex, 0, 32, 96, 32);
            _buttonPlayHand = new ToggleSprite(buttonTex);
            _buttonPlayHand.AddToggleRegion(false, buttonReleased);
            _buttonPlayHand.AddToggleRegion(true, buttonPressed);
            _buttonPlayHand.Position = new Vector2(524, 232);
            TextureRegion buttonDiscardReleased = new TextureRegion(buttonTex, 96, 0, 96, 32);
            TextureRegion buttonDiscardPressed = new TextureRegion(buttonTex, 96, 32, 96, 32);
            _buttonDiscard = new ToggleSprite(buttonTex);
            _buttonDiscard.AddToggleRegion(false, buttonDiscardReleased);
            _buttonDiscard.Position = new Vector2(524, 272);
            _buttonDiscard.AddToggleRegion(true, buttonDiscardPressed);
        }

        public void DrawBoard(SpriteBatch batch)
        {
            _boardTex.Draw(batch, Vector2.Zero);
            _buttonDiscard.Draw(batch, _buttonDiscard.Position);
            _buttonPlayHand.Draw(batch, _buttonPlayHand.Position);
        }

        public void TryClickButtons(Point pos, bool mouseDown)
        {
            _buttonDiscard.TryClick(pos, mouseDown);
            _buttonPlayHand.TryClick(pos, mouseDown);
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
