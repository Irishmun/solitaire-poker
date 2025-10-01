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

        private DeckLoader _deckLoader;
        private CardDeck _deck;
        private Board _backGround;

        private SpriteFont _font;
        private TextSprite _message;



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
            _deckLoader = new DeckLoader();
            _backGround = new Board();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _font = Content.Load<SpriteFont>("Font");
            _message = new TextSprite(_font, true, new Vector2(2, 2));
            _message.Alpha = 4;
            // TODO: use this.Content to load your game content here

            //if (_deck.LoadDeckIntoMemory(Content, "Decks/Bicycle/Bicycle.dck",out _deck))
            if (_deckLoader.LoadDeckIntoMemory(Content, "Decks/Kenney/Kenney.dck", out _deck))
            //if (_deck.LoadDeckIntoMemory(Content, "Decks/TF2/tf2.dck",out _deck))
            {
                System.Diagnostics.Debug.WriteLine($"Loaded deck \"{_deckLoader.LoadedDeckName}\"...");
                _message.Text = $"Loaded Deck \"{_deckLoader.LoadedDeckName}\"...";
                _deck.ShuffleDeck();
                _deck.AddCardsToHand(_deck.PickupCards(5));
            }
            _backGround.LoadBoard();

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            if (Keyboard.GetState().IsKeyUp(Keys.C) && _cHeld == true)
            {
                Debug.WriteLine("Toggling ClearScreen to: " + !_clearScreen);
                _clearScreen = !_clearScreen;
                _cHeld = false;
            }
            _cHeld = Keyboard.GetState().IsKeyDown(Keys.C);
            if (gameTime.ElapsedGameTime.Milliseconds > 0)
            {
                _message.Alpha -= (float)gameTime.ElapsedGameTime.TotalSeconds * 2f;
            }
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
            // TODO: Add your drawing code here
            _backGround.DrawBoard(SpriteBatch);
            _message.Draw(SpriteBatch, new Vector2(8, 4));
            _deck.DrawDeck(SpriteBatch, _backGround.DeckFieldPos);
            _deck.DrawHand(SpriteBatch, _backGround.HandFieldCenter);

            SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
