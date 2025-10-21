using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Input;
using SolitairePoker.Poker;
using SolitairePoker.UI;
using System;
using System.Diagnostics;

namespace SolitairePoker
{
    public class Game1 : Core
    {
        private bool _clearScreen = true;

        private InputManager _input;
        private Logic _pokerLogic;


        private DeckLoader _deckLoader;
        private CardDeck _deck;
        private Board _backGround;

        private MenuBar _menuBar;
        private SpriteFont _font;
        private TextSprite _message, _totalScore, _scoredHistory, _handHistory, _deckCount;// _fps;

        public Game1() : base("Solitaire Poker", 640, 480, false)
        {
            IntPtr hWnd = Window.Handle;
            System.Windows.Forms.Control ctrl = System.Windows.Forms.Control.FromHandle(hWnd);
            System.Windows.Forms.Form form = ctrl.FindForm();
            _menuBar = new MenuBar(form);
            IsFixedTimeStep = false;
            Graphics.SynchronizeWithVerticalRetrace = false;
            //form.TransparencyKey = System.Drawing.Color.Magenta;

        }

        protected override void Initialize()
        {
            //Add your initialization logic here
            _menuBar.Initialize();
            _input = new InputManager();
            _deckLoader = new DeckLoader();
            _backGround = new Board();
            _pokerLogic = new Logic();
            _pokerLogic.LoadSettings();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here
            _font = Content.Load<SpriteFont>("Font");
            _message = new TextSprite(_font, true, new Vector2(2, 2));
            _message.Position = new Vector2(8, 28);


            /*_fps = new TextSprite(_font, false);
            _fps.Position = new Vector2(8, 28);
            _fps.Text = "0 FPS";*/

            _totalScore = new TextSprite(_font, false);
            _totalScore.Position = new Vector2(438, 231);
            _scoredHistory = new TextSprite(_font, false);
            _scoredHistory.Position = new Vector2(580, 54);
            _handHistory = new TextSprite(_font, false);
            _handHistory.Position = new Vector2(341, 54);
            _deckCount = new TextSprite(_font, false);
            _deckCount.Position = new Vector2(530, 460);

            _menuBar.AddDecksToDropDown(_deckLoader.GetAllDeckNames(Content));

            StartGame("Inventory/inventory.dck");

            _backGround.LoadBoard();

            _menuBar.TSMI_CloseGame.Click += TSMI_CloseGame_Click;
            _menuBar.TSMI_ChooseDeck.DropDownItemClicked += TSMI_ChooseDeck_DropDownItemClicked;

            base.LoadContent();
        }

        private void StartGame(string deckName)
        {
            _totalScore.Text = string.Empty;
            _scoredHistory.Text = string.Empty;
            _handHistory.Text = string.Empty;

            if (_deckLoader.LoadDeckIntoMemory(Content, "Decks/" + deckName, out _deck))
            {
                _message.Alpha = 4;
                System.Diagnostics.Debug.WriteLine($"Loaded deck \"{_deckLoader.LoadedDeckName}\"...");
                _message.Text = $"Loaded Deck \"{_deckLoader.LoadedDeckName}\"...";
                _deck.ShuffleDeck((int)DateTime.Now.Ticks);
                //_deck.AddCardsToHand(_deck.PickupCards(5));
                _deckCount.Text = string.Format("{0}/{1}", _deck.DeckCount, CardDeck.MAX_DECK_SIZE);
            }
        }

        private void TSMI_ChooseDeck_DropDownItemClicked(object sender, System.Windows.Forms.ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == null)
            { return; }
            Debug.WriteLine(e.ClickedItem);
            StartGame(e.ClickedItem.ToString());
        }

        private void TSMI_CloseGame_Click(object sender, EventArgs e)
        {
            Exit();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here

            _input.Update(gameTime);
            HandleKeyboardInputs();
            HandleMouseInputs();
            _deck.Update(gameTime);

            if (gameTime.ElapsedGameTime.Milliseconds > 0)
            {
                _message.Alpha -= (float)gameTime.ElapsedGameTime.TotalSeconds * 2f;
                //_fps.Text = string.Format("{0} FPS", Math.Round((1f / ((float)gameTime.ElapsedGameTime.TotalMilliseconds * 0.001f)), 0, MidpointRounding.AwayFromZero));
                //_fps.Position = new Vector2(GraphicsDevice.PresentationParameters.BackBufferWidth - _font.MeasureString(_fps.Text).X - 8, 28);
            }
            _deckCount.Text = string.Format("{0}/{1}", _deck.DeckCount, CardDeck.MAX_DECK_SIZE);
            _deckCount.Position = new Vector2(616 - _font.MeasureString(_deckCount.Text).X, 460);

            _totalScore.Text = ScoreBoard.TotalScore.ToString();
            _totalScore.Position = new Vector2(438 - _font.MeasureString(_totalScore.Text).X, 231);
            _handHistory.Text = ScoreBoard.GetFormattedHandHistory();
            _scoredHistory.Text = ScoreBoard.GetFormattedScoreHistory();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (_clearScreen)
            {
                //GraphicsDevice.Clear(Color.Purple);
                GraphicsDevice.Clear(Color.Green);
            }

            SpriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.LinearClamp);
            // TODO: Add your drawing code here

            _backGround.DrawBoard(SpriteBatch);
            _message.Draw(SpriteBatch);
            _totalScore.Draw(SpriteBatch);
            _scoredHistory.Draw(SpriteBatch);
            _handHistory.Draw(SpriteBatch);
            _deckCount.Draw(SpriteBatch);
            //_fps.Draw(SpriteBatch);

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
                    int index = -1;
                    for (int i = 0; i < hand.Length; i++)
                    {
                        if (hand[i].Sprite.ContainsPoint(_input.Mouse.Position))
                        {
                            //Debug.WriteLine("picked the {0} of {1}s", hand[i].Face, hand[i].Suit);
                            if (index == -1 || hand[i].Sprite.LayerDepth > hand[index].Sprite.LayerDepth)
                            {
                                index = i;
                            }
                        }
                    }
                    if (index >= 0)
                    {
                        _deck.SelectCard(index, false);
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
                button?.ClickHandButton(_deck, _pokerLogic);
            }
        }


    }
}
