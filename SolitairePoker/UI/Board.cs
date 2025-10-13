using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;

namespace SolitairePoker.UI
{
    public class Board
    {
        private const int MAX_HAND_CARDS = 5;
        public static readonly Vector2 HAND_CENTER = new Vector2(253, 391);
        public static readonly Vector2 DECK_POS = new Vector2(529, 332);
        public static readonly Vector2 DISCARD_POS = new Vector2(89, 146);

        private TextureRegion _boardTex;
        private Rectangle _deckRect;
        // private Vector2 _handFieldPos;
        private Rectangle _handRect;

        private PlayHandButton _buttonPlayHand;
        private DiscardHandButton _buttonDiscard;
        public void LoadBoard()
        {
            _boardTex = new TextureRegion(Core.Content.Load<Texture2D>("UI/board"), 0, 0, 640, 480);
            //_deckFieldPos = new Vector2(529, 332);
            _deckRect = new Rectangle((int)DECK_POS.X, (int)DECK_POS.Y, 96, 128);
            //_handFieldPos = new Vector2(28, 332);
            _handRect = new Rectangle(28, 332, 460, 128);

            Texture2D buttonTex = Core.Content.Load<Texture2D>("UI/buttons");
            TextureRegion buttonReleased = new TextureRegion(buttonTex, 0, 0, 96, 32);
            TextureRegion buttonPressed = new TextureRegion(buttonTex, 0, 32, 96, 32);
            _buttonPlayHand = new PlayHandButton(buttonTex);
            _buttonPlayHand.Sprite.AddToggleRegion(false, buttonReleased);
            _buttonPlayHand.Sprite.AddToggleRegion(true, buttonPressed);
            _buttonPlayHand.Sprite.Position = new Vector2(523, 232);
            TextureRegion buttonDiscardReleased = new TextureRegion(buttonTex, 96, 0, 96, 32);
            TextureRegion buttonDiscardPressed = new TextureRegion(buttonTex, 96, 32, 96, 32);
            _buttonDiscard = new DiscardHandButton(buttonTex);
            _buttonDiscard.Sprite.AddToggleRegion(false, buttonDiscardReleased);
            _buttonDiscard.Sprite.Position = new Vector2(523, 272);
            _buttonDiscard.Sprite.AddToggleRegion(true, buttonDiscardPressed);
        }

        public void DrawBoard(SpriteBatch batch)
        {
            _boardTex.Draw(batch, Vector2.Zero);
            _buttonDiscard.Sprite.Draw(batch, _buttonDiscard.Sprite.Position);
            _buttonPlayHand.Sprite.Draw(batch, _buttonPlayHand.Sprite.Position);
        }

        public void TryClickButtons(Point pos, bool mouseDown)
        {
            _buttonDiscard.Sprite.TryClick(pos, mouseDown);
            _buttonPlayHand.Sprite.TryClick(pos, mouseDown);
        }

        public ButtonBase TryGetSelectedButton(Point pos)
        {
            if (_buttonDiscard.Sprite.ContainsPoint(pos))
            {
                return _buttonDiscard;
            }

            if (_buttonPlayHand.Sprite.ContainsPoint(pos))
            {
                return _buttonPlayHand;
            }
            return null;
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
    }
}
