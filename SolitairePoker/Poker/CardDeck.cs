using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using System;
using System.Collections.Generic;

namespace SolitairePoker.Poker
{
    public class CardDeck
    {
        private const int MAX_HAND_SIZE = 5;
        public const int MAX_DECK_SIZE = 52;//TODO: place this in a logic zone instead
        public const int MAX_DECK_HEIGHT = 10;

        private List<Card> _deck;
        private List<Card> _hand;
        private Sprite _cardBack;

        public void SetDeck(Card[] cards, Sprite cardBack)
        {
            _deck = new List<Card>(cards);
            _cardBack = cardBack;
            _hand = new List<Card>(MAX_HAND_SIZE);
        }

        public void ShuffleDeck(int seed = 0)
        {
            Random rng = new Random(seed);
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
        }
        public void AddCardToHand(Card card)
        {
            _hand.Add(card);
        }

        public Card PickupCard()
        {
            return PickupCards(1)[0];
        }

        public Card[] PickupCards(int cards)
        {
            if (_hand.Count >= MAX_HAND_SIZE)
            {//don't draw card
                return null;
            }
            List<Card> drawn = new List<Card>(cards);
            int toDraw = cards >= _deck.Count ? _deck.Count - 1 : cards;
            for (int i = 0; i < toDraw; i++)
            {
                drawn.Add(_deck[i]);
            }
            _deck.RemoveRange(0, toDraw);
            return drawn.ToArray();
        }

        public bool DiscardCard(Card card)
        {
            if (!_deck.Contains(card))
            {
                return false;
            }
            _deck.Remove(card);
            return true;
        }
        public bool DiscardCards(Card[] cards)
        {
            int oldCount = _deck.Count;
            for (int i = 0; i < cards.Length; i++)
            {
                if (!_deck.Contains(cards[i]))
                {
                    System.Diagnostics.Debug.WriteLine($"Couldn't discard {cards[i]}");
                    continue;
                }
                _deck.Remove(cards[i]);
            }

            if (_deck.Count == oldCount)
            {
                return false;
            }

            return true;
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

        internal void DrawHand(SpriteBatch spriteBatch, Vector2 handFieldCenter)
        {
            if (_hand.Count == 0)
            {
                return;
            }

            switch (_hand.Count)
            {
                case 1:
                    _hand[0].Sprite.Position = handFieldCenter;
                    _hand[0].Sprite.Draw(spriteBatch, _hand[0].Sprite.Position);
                    break;
                case 2:
                    _hand[0].Sprite.Position = handFieldCenter - new Vector2(_hand[0].Sprite.Width * 0.5f + 4, 0);
                    _hand[1].Sprite.Position = handFieldCenter + new Vector2(_hand[1].Sprite.Width * 0.5f + 4, 0);
                    _hand[0].Sprite.Draw(spriteBatch, _hand[0].Sprite.Position);
                    _hand[1].Sprite.Draw(spriteBatch, _hand[1].Sprite.Position);
                    break;
                case 3:
                    _hand[0].Sprite.Position = handFieldCenter - new Vector2(_hand[0].Sprite.Width + 8, 0);
                    _hand[1].Sprite.Position = handFieldCenter;
                    _hand[2].Sprite.Position = handFieldCenter + new Vector2(_hand[2].Sprite.Width + 8, 0);
                    _hand[0].Sprite.Draw(spriteBatch, _hand[0].Sprite.Position);
                    _hand[1].Sprite.Draw(spriteBatch, _hand[1].Sprite.Position);
                    _hand[2].Sprite.Draw(spriteBatch, _hand[2].Sprite.Position);
                    break;
                case 4:
                    _hand[0].Sprite.Position = handFieldCenter - new Vector2(_hand[0].Sprite.Width * 1.5f + 4, 0);
                    _hand[1].Sprite.Position = handFieldCenter - new Vector2(_hand[1].Sprite.Width * 0.5f + 2, 0);
                    _hand[2].Sprite.Position = handFieldCenter + new Vector2(_hand[2].Sprite.Width * 0.5f + 2, 0);
                    _hand[3].Sprite.Position = handFieldCenter + new Vector2(_hand[3].Sprite.Width * 1.5f + 4, 0);
                    _hand[0].Sprite.Draw(spriteBatch, _hand[0].Sprite.Position);
                    _hand[1].Sprite.Draw(spriteBatch, _hand[1].Sprite.Position);
                    _hand[2].Sprite.Draw(spriteBatch, _hand[2].Sprite.Position);
                    _hand[3].Sprite.Draw(spriteBatch, _hand[3].Sprite.Position);
                    break;
                case 5:
                    _hand[0].Sprite.Position = handFieldCenter - new Vector2(_hand[0].Sprite.Width * 2 + 8, 0);
                    _hand[1].Sprite.Position = handFieldCenter - new Vector2(_hand[2].Sprite.Width + 4, 0);
                    _hand[2].Sprite.Position = handFieldCenter;
                    _hand[3].Sprite.Position = handFieldCenter + new Vector2(_hand[3].Sprite.Width + 4, 0);
                    _hand[4].Sprite.Position = handFieldCenter + new Vector2(_hand[4].Sprite.Width * 2 + 8, 0);
                    _hand[0].Sprite.Draw(spriteBatch, _hand[0].Sprite.Position);
                    _hand[1].Sprite.Draw(spriteBatch, _hand[1].Sprite.Position);
                    _hand[2].Sprite.Draw(spriteBatch, _hand[2].Sprite.Position);
                    _hand[3].Sprite.Draw(spriteBatch, _hand[3].Sprite.Position);
                    _hand[4].Sprite.Draw(spriteBatch, _hand[4].Sprite.Position);
                    break;
                default:
                    break;
            }

            for (int i = 0; i < _hand.Count; i++)
            {

            }
        }

        internal Card[] GetHand() => _hand.ToArray();
    }
}
