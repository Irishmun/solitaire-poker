using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Util;
using SolitairePoker.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SolitairePoker.Poker
{
    public class CardDeck
    {
        private const int MAX_HAND_SIZE = 7;
        private const int MAX_SELECT_SIZE = 5;
        public const int MAX_DECK_SIZE = 52;//TODO: place this in a logic zone instead
        public const int MAX_DECK_HEIGHT = 10;

        private List<Card> _deck;
        private List<Card> _hand;
        private List<Card> _markedForDiscard;
        private List<Card> _discard;
        private Sprite _cardBack;
        private Sprite _slideCard;
        private float _backT = 0, _discardT = 0;
        private float _slideTime = .5f;
        private int _selectedCount = 0;
        Random rng;

        public void SetDeck(Card[] cards, Sprite cardBack)
        {
            _deck = new List<Card>(cards);
            _discard = new List<Card>(cards.Length);
            _cardBack = cardBack;
            _slideCard = cardBack;
            _hand = new List<Card>(MAX_HAND_SIZE);
            _markedForDiscard = new List<Card>(cards.Length);
        }

        public void Update(GameTime time)
        {
            float delta = (float)time.ElapsedGameTime.TotalSeconds;
            if (_markedForDiscard.Count > 0)
            {
                if (_discardT < _slideTime)
                {
                    _discardT += delta;
                    for (int i = 0; i < _markedForDiscard.Count; i++)
                    {
                        Card c = _markedForDiscard[i];
                        c.Sprite.Position = Vector2.Lerp(c.Sprite.Position, Board.DISCARD_POS, _discardT / _slideTime);
                    }
                }
                if (_discardT >= _slideTime)
                {
                    DiscardCards(_markedForDiscard.ToArray());
                    _discardT = 0;
                }
            }
            if (_hand.Count < MAX_HAND_SIZE && _deck.Count > 0)
            {
                if (_backT < _slideTime)
                {
                    _backT += delta;
                    _slideCard.Position = Vector2.Lerp(Board.DECK_POS, new Vector2(Board.HAND_CENTER.X, Board.DECK_POS.Y), EasingUtil.EaseInQuad(_backT / _slideTime));
                }
                else
                {
                    AddCardToHand(PickupCard());
                    for (int i = 0; i < _hand.Count; i++)
                    {
                        SelectCard(i, true);
                    }
                    _backT = 0;
                }
                //slide cardback from deck to hand field
                //when there, add card to hand
            }
            else
            {
                _slideCard.Position = new Vector2(-200, -200);
            }
        }

        public void ShuffleDeck(int seed = 0)
        {
            rng = new Random(seed);
            int n = _deck.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Card val = _deck[k];
                _deck[k] = _deck[n];
                _deck[n] = val;
            }
        }

        public void AddCardsToHand(Card[] cards)
        {
            _hand.AddRange(cards);
            SetCardPositions(Board.HAND_CENTER);
        }
        public void AddCardToHand(Card? card)
        {
            if (card == null)
            {
                return;
            }
            _hand.Add((Card)card);
            SetCardPositions(Board.HAND_CENTER);
        }

        public Card? PickupCard()
        {
            Card[] c = PickupCards(1);
            if (c.Length <= 0)
            {
                return null;
            }
            return c[0];
        }

        public Card[] PickupCards(int cards)
        {
            if (_hand.Count >= MAX_HAND_SIZE)
            {//don't draw card
                return null;
            }
            List<Card> drawn = new List<Card>(cards);
            int toDraw = cards >= _deck.Count ? _deck.Count : cards;
            for (int i = 0; i < toDraw; i++)
            {
                drawn.Add(_deck[i]);
            }
            _deck.RemoveRange(0, toDraw);
            return drawn.ToArray();
        }

        public bool DiscardCard(Card card)
        {
            if (!_markedForDiscard.Contains(card))
            {
                return false;
            }
            AddToDiscard(card);
            _markedForDiscard.Remove(card);
            return true;
        }
        public bool DiscardCards(Card[] cards)
        {
            int oldCount = _markedForDiscard.Count;

            for (int i = 0; i < cards.Length; i++)
            {
                if (!_markedForDiscard.Contains(cards[i]))
                {
                    System.Diagnostics.Debug.WriteLine($"Couldn't discard {cards[i]}");
                    continue;
                }
                AddToDiscard(cards[i]);
                _markedForDiscard.Remove(cards[i]);
            }

            if (_markedForDiscard.Count == oldCount)
            {
                return false;
            }

            return true;
        }

        public void MarkForDiscard(Card card)
        {
            _markedForDiscard.Add(card);
            _hand.Remove(card);
        }

        public void MarkForDiscard(Card[] cards)
        {
            _markedForDiscard.AddRange(cards);
            for (int i = 0; i < cards.Length; i++)
            {
                if (!_hand.Contains(cards[i]))
                { continue; }
                _hand.Remove(cards[i]);
            }
        }

        private void AddToDiscard(Card card)
        {
            if (rng == null)
            {
                rng = new Random();
            }
            card.Sprite.Rotation = MathHelper.ToRadians(rng.Next(-10, 11));
            card.Sprite.Position = Board.DISCARD_POS;
            _discard.Add(card);
        }

        public void SelectCard(int cardIndex, bool forceUnselect = false)
        {
            if (cardIndex < 0 || cardIndex >= _hand.Count)
            { return; }

            Card selected = _hand[cardIndex];
            bool select = false;

            if (forceUnselect || selected.Selected == true)
            {
                select = false;
                _selectedCount -= 1;
                if (_selectedCount < 0)
                {
                    _selectedCount = 0;
                }
            }
            else
            {
                select = true;
                if (_selectedCount >= MAX_SELECT_SIZE)
                { return; }
                _selectedCount += 1;
            }
            selected.Selected = select;

            System.Diagnostics.Debug.WriteLine($"Select card: {selected}, {selected.Selected}");

            Vector2 pos = selected.Sprite.Position;
            pos.Y = Board.HAND_CENTER.Y;
            if (select)
            {
                pos.Y -= 32;
                selected.Sprite.Position = pos;
            }
            else
            {
                selected.Sprite.Position = pos;
            }
            _hand[cardIndex] = selected;
        }

        public void DrawDeck(SpriteBatch batch, Vector2 startPos)
        {
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

            _slideCard.Draw(batch, _slideCard.Position, 1f);

            Vector2 pos = startPos;// _backGround.DeckFieldPos;
            int height = _deck.Count > MAX_DECK_HEIGHT ? MAX_DECK_HEIGHT : _deck.Count;
            bool alt = height % 2 == 0;
            for (int i = 0; i < height; i++)
            {
                Color col = Color.White;
                if (alt)
                {
                    col = Color.Gray;
                }
                alt = !alt;
                _cardBack.Color = col;
                _cardBack.LayerDepth = 1f - (((float)height - i) / (float)height);
                _cardBack.Draw(batch, pos);
                pos.Y--;
                pos.X++;
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
        }

        internal void DrawHand(SpriteBatch spriteBatch)
        {
            if (_hand.Count == 0)
            {
                return;
            }

            for (int i = 0; i < _hand.Count; i++)
            {
                _hand[i].Sprite.Draw(spriteBatch, _hand[i].Sprite.Position);
            }
        }
        public void DrawDiscard(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < _discard.Count; i++)
            {
                _discard[i].Sprite.Draw(spriteBatch, _discard[i].Sprite.Position, ((float)i) / (float)_discard.Count);
            }
            for (int i = 0; i < _markedForDiscard.Count; i++)
            {
                _markedForDiscard[i].Sprite.Draw(spriteBatch, _markedForDiscard[i].Sprite.Position, ((float)i) / (float)_markedForDiscard.Count);

            }
        }

        private void SetCardPositions(Vector2 handFieldCenter)
        {
            //https://gamedev.stackexchange.com/a/203507
            if (_hand.Count > 0)
            {
                for (int i = 0; i < _hand.Count; i++)
                {
                    float depth = (i + 0.5f) / (float)_hand.Count;
                    float xPos = float.Lerp(Board.HAND_CENTER.X - Board.HAND_WIDTH, Board.HAND_CENTER.X + Board.HAND_WIDTH, depth);
                    Vector2 pos = new Vector2(xPos, Board.HAND_CENTER.Y);
                    _hand[i].Sprite.Position = pos;
                    _hand[i].Sprite.LayerDepth = depth;
                }
            }
        }

        public Card[] GetHand() => _hand.ToArray();
        public Card[] GetSelectedCards() => _hand.Where(x => x.Selected == true).ToArray();
        public int DeckCount => _deck.Count;
    }
}
