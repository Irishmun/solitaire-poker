using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using SolitairePoker.Poker;
using System.Diagnostics;
using SolitairePoker.UI;
using MonoGameLibrary.Input;
using System;
using System.Linq;

namespace SolitairePoker
{
    public class Game1 : Core
    {
        private bool _clearScreen = true;

        private InputManager _input;

        private int cardIndex = 0;

        private DeckLoader _deckLoader;
        private CardDeck _deck;
        private Board _backGround;

        private MenuBar _menuBar;
        private SpriteFont _font;
        private TextSprite _message;

        public Game1() : base("Solitaire Poker", 640, 480, false)
        {
            IntPtr hWnd = Window.Handle;
            System.Windows.Forms.Control ctrl = System.Windows.Forms.Control.FromHandle(hWnd);
            System.Windows.Forms.Form form = ctrl.FindForm();
            _menuBar = new MenuBar(form);
            //form.TransparencyKey = System.Drawing.Color.Magenta;

        }

        protected override void Initialize()
        {
            //Add your initialization logic here
            _menuBar.Initialize();
            _input = new InputManager();
            _deckLoader = new DeckLoader();
            _backGround = new Board();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _font = Content.Load<SpriteFont>("Font");
            _message = new TextSprite(_font, true, new Vector2(2, 2));
            _message.Position = new Vector2(8, 28);
            _message.Alpha = 4;
            // TODO: use this.Content to load your game content here
            _menuBar.AddDecksToDropDown(_deckLoader.GetAllDeckNames(Content));
            //if (_deckLoader.LoadDeckIntoMemory(Content, "Decks/Bicycle/Bicycle.dck", out _deck))
            //if (_deckLoader.LoadDeckIntoMemory(Content, "Decks/Kenney/Kenney.dck", out _deck))
            if (_deckLoader.LoadDeckIntoMemory(Content, "Decks/TF2/tf2.dck", out _deck))
            {
                System.Diagnostics.Debug.WriteLine($"Loaded deck \"{_deckLoader.LoadedDeckName}\"...");
                _message.Text = $"Loaded Deck \"{_deckLoader.LoadedDeckName}\"...";
                _deck.ShuffleDeck((int)DateTime.Now.Ticks);
                //_deck.AddCardsToHand(_deck.PickupCards(5));
            }
            _backGround.LoadBoard();

            _menuBar.TSMI_CloseGame.Click += TSMI_CloseGame_Click;
            _menuBar.TSMI_ChooseDeck.DropDownItemClicked += TSMI_ChooseDeck_DropDownItemClicked;

            base.LoadContent();
        }

        private void TSMI_ChooseDeck_DropDownItemClicked(object sender, System.Windows.Forms.ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == null)
            { return; }
            Debug.WriteLine(e.ClickedItem);
            if (_deckLoader.LoadDeckIntoMemory(Content, "Decks/" + e.ClickedItem, out _deck))
            {
                _message.Text = $"Loaded Deck \"{_deckLoader.LoadedDeckName}\"...";
                _message.Alpha = 4;
            }
        }

        private void TSMI_CloseGame_Click(object sender, EventArgs e)
        {
            Exit();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            _input.Update(gameTime);
            HandleKeyboardInputs();
            HandleMouseInputs();
            _deck.Update(gameTime);

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
            _message.Draw(SpriteBatch);
            _deck.DrawDeck(SpriteBatch, _backGround.DeckFieldPos);
            _deck.DrawHand(SpriteBatch);
            _deck.DrawDiscard(SpriteBatch);

            SpriteBatch.End();
            base.Draw(gameTime);
        }


        private void HandleKeyboardInputs()
        {
            if (_input.Keyboard.WasKeyJustReleased(Keys.C))
            {
                Debug.WriteLine("Toggling ClearScreen to: " + !_clearScreen);
                _clearScreen = !_clearScreen;
            }
        }

        private void HandleMouseInputs()
        {
            _backGround.TryClickButtons(_input.Mouse.Position, _input.Mouse.IsButtonDown(MouseButton.Left));
            if (_input.Mouse.WasButtonJustPressed(MouseButton.Left))
            {
                Card[] hand = _deck.GetHand();

                if (hand != null && hand.Length > 0)
                {
                    for (int i = 0; i < hand.Length; i++)
                    {
                        if (hand[i].Sprite.ContainsPoint(_input.Mouse.Position))
                        {
                            Debug.WriteLine("picked the {0} of {1}s", hand[i].Face, hand[i].Suit);
                            _deck.SelectCard(i, false);
                            //click card
                            break;
                        }
                    }
                }
                /*else if (_backGround.IsPointInDeckField(_input.Mouse.Position))
                {
                    Card[] pickedUpCards = _deck.PickupCards(1);
                    if (pickedUpCards != null)
                    {
                        _deck.AddCardsToHand(pickedUpCards);
                    }
                }*/
            }
            if (_input.Mouse.WasButtonJustReleased(MouseButton.Left))
            {
                //get selected button, do that one
                ButtonBase button = _backGround.TryGetSelectedButton(_input.Mouse.Position);
                Debug.WriteLine($"Selected button: " + button);
                button?.ClickHandButton(_deck);
            }
        }
    }
}
