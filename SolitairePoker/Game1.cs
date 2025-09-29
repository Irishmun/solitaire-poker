using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using SolitairePoker.Poker;
using System.Diagnostics;
using SolitairePoker.Background;

namespace SolitairePoker
{
    public class Game1 : Core
    {
        private bool _clearScreen = true;
        private bool _cHeld = false;

        private int cardIndex = 0;

        private DeckLoader _deck;
        private Board _backGround;

        private SpriteFont _font;
        private int deckSize = 10;
        private int maxDeckHeight = 10;


        public Game1() : base("Solitaire Poker", 640, 480, false)
        {

            //IntPtr hWnd = Window.Handle;
            //System.Windows.Forms.Control ctrl = System.Windows.Forms.Control.FromHandle(hWnd);
            //System.Windows.Forms.Form form = ctrl.FindForm();
            //form.TransparencyKey = System.Drawing.Color.Magenta;

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _deck = new DeckLoader();
            _backGround = new Board();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here
            //_deck.LoadDeckIntoMemory(Content, "Decks/Bicycle/Bicycle.dck");
            _deck.LoadDeckIntoMemory(Content, "Decks/Kenney/Kenney.dck");
            _font = Content.Load<SpriteFont>("Font");
            _backGround.LoadBoard();


            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            if (Keyboard.GetState().IsKeyUp(Keys.C) && _cHeld == true)
            {
                //Debug.WriteLine("Toggling ClearScreen to: " + !_clearScreen);
                //_clearScreen = !_clearScreen;
                if (deckSize == 0)
                {
                    return;
                }
                deckSize -= 1;
                _cHeld = false;
            }
            _cHeld = Keyboard.GetState().IsKeyDown(Keys.C);
            // TODO: Add your update logic here

            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            if (_clearScreen)
            {
                //GraphicsDevice.Clear(Color.Purple);
                GraphicsDevice.Clear(Color.Green);
            }

            SpriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointWrap);
            _backGround.DrawBoard(SpriteBatch);

            SpriteBatch.DrawString(_font,$"Loaded Deck \"{_deck.LoadedDeckName}\"...",Vector2.Zero,Color.White);
            SpriteBatch.DrawString(_font,$"Loaded Deck \"{_deck.LoadedDeckName}\"...",Vector2.One,Color.Black);
            // TODO: Add your drawing code here

            //spread out cards
            //Vector2 pos = Vector2.Zero;
            //for (int i = 0; i < _deck.LoadedCards.Length; i++)
            //{
            //    if (pos.X + _deck.LoadedCards[i].Texture.Width > Graphics.PreferredBackBufferWidth)
            //    {
            //        pos.X = 0;
            //        pos.Y += _deck.LoadedCards[i].Texture.Height;
            //    }
            //    _deck.LoadedCards[i].Texture.Draw(SpriteBatch, pos);
            //
            //    pos.X += _deck.LoadedCards[i].Texture.Width;
            //}
            //_deck.CardBackTex.Draw(SpriteBatch, pos);

            Vector2 pos = _backGround.DeckFieldPos;
            int height = deckSize > maxDeckHeight ? maxDeckHeight : deckSize;
            bool alt = height % 2 == 0;
            for (int i = 0; i < height; i++)
            {
                Color col = Color.White;
                if (alt)
                {
                    col = Color.Gray;
                }
                alt = !alt;
                _deck.CardBackTex.Draw(SpriteBatch, pos, col, 0, Vector2.Zero, 2, SpriteEffects.None, 1f - (((float)height - i) / (float)height));
                pos.Y--;
            }

            //cycle through cards
            //Texture2D toDraw;
            //if (cardIndex >= _deck.LoadedCards.Length)
            //{
            //    toDraw = _deck.CardBackTex;
            //    cardIndex = 0;
            //}
            //else
            //{
            //    toDraw = _deck.LoadedCards[cardIndex].Texture;
            //}
            //SpriteBatch.Draw(toDraw, new Vector2((Graphics.PreferredBackBufferWidth - 64) * 0.5f, (Graphics.PreferredBackBufferHeight - 64)) * 0.5f, Color.White);
            //cardIndex += 1;

            SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
