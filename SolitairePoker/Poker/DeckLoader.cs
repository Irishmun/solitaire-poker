using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SolitairePoker.Poker
{
    internal class DeckLoader
    {
        //pokernight cards have .75 width
        private readonly Vector2 DesiredCardSize = new Vector2(84, 120);
        private string _loadedDeckName = "no deck";
        private string _cardBack;
        private Sprite _cardBackSprite;
        private Vector2 _origin = new Vector2(-1);//top-left corner of the card
        private Vector2 _size = Vector2.Zero;//size of the texture to be used

        public bool LoadDeckIntoMemory(ContentManager content, string deckFile, out CardDeck deck)
        {
            deck = new CardDeck();
            Card[] cards;
            try
            {
                cards = GetCardNames(deckFile, content.RootDirectory);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Failed to get card names: " + e.Message);
                return false;
            }

            for (int i = 0; i < cards.Length; i++)
            {
                try
                {
                    cards[i].Sprite = LoadTexture(cards[i].FileName);
                    cards[i].Sprite.CenterOrigin();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine($"Failed to create card sprite for card {cards[i].FileName}: " + e.Message);
                    return false;
                }
            }

            try
            {
                _cardBackSprite = LoadTexture(_cardBack);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to create card sprite for card back: " + e.Message);
                return false;
            }

            deck.SetDeck(cards,_cardBackSprite);

            Sprite LoadTexture(string name)
            {
                Texture2D tex = content.Load<Texture2D>(name);
                _origin = _origin.X < 0 ? Vector2.Zero : _origin;
                _size = _size == Vector2.Zero ? new Vector2(tex.Width, tex.Height) : _size;
                Sprite spr = new Sprite(new TextureRegion(tex, (int)_origin.X, (int)_origin.Y, (int)_size.X, (int)_size.Y));
                spr.Scale = new Vector2(DesiredCardSize.X / _size.X, DesiredCardSize.Y / _size.Y);
                return spr;
            }
            return true;
        }

        private Card[] GetCardNames(string deckFile, string rootDir = "Content")
        {
            List<Card> cards = new List<Card>();
            using (Stream stream = TitleContainer.OpenStream(Path.Combine(rootDir, deckFile)))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string[] lines = reader.ReadToEnd().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (string.IsNullOrWhiteSpace(lines[i]))
                        { continue; }
                        //get name
                        string[] cardKey = lines[i].Split(':', 2, StringSplitOptions.TrimEntries);
                        if (cardKey[0].Length == 1)
                        {
                            switch (cardKey[0][0])
                            {
                                case 's': //texture size
                                    string[] sizeVec = cardKey[1].Split(',');
                                    if (sizeVec.Length >= 2 && int.TryParse(sizeVec[0], out int sizeVecX) && int.TryParse(sizeVec[1], out int sizeVecY))
                                    {
                                        _size = new Vector2(sizeVecX, sizeVecY);
                                        Debug.WriteLine($"card size: {_size}");
                                    }
                                    else
                                    {
                                        Debug.WriteLine($"Failed to parse size vector {cardKey[1]}");
                                    }
                                    break;
                                case 'o':
                                    string[] orignVec = cardKey[1].Split(',');
                                    if (orignVec.Length >= 2 && int.TryParse(orignVec[0], out int originVecX) && int.TryParse(orignVec[1], out int originVecY))
                                    {
                                        _origin = new Vector2(originVecX, originVecY);
                                        Debug.WriteLine($"card origin: {_origin}");
                                    }
                                    else
                                    {
                                        Debug.WriteLine($"Failed to parse origin vector {cardKey[1]}");
                                    }
                                    break;
                                case 'n': //deckName
                                    _loadedDeckName = cardKey[1];
                                    break;
                                case 'b': //cardBack
                                    _cardBack = Path.GetDirectoryName(deckFile) + "\\" + cardKey[1];
                                    break;
                                default:
                                    break;
                            }
                            continue;
                        }
                        //get card
                        Card c = new Card();
                        c.FileName = Path.GetDirectoryName(deckFile) + "\\" + cardKey[1];
                        c.Face = ParseFace(cardKey[0][1]);
                        c.Suit = ParseSuit(cardKey[0][0]);

                        cards.Add(c);
                    }
                }

            }
            return cards.ToArray();
        }

        private FaceEnum ParseFace(char code)
        {
            switch (code)
            {
                case 'a':
                    return FaceEnum.FACE_ACE;
                case '2':
                    return FaceEnum.FACE_TWO;
                case '3':
                    return FaceEnum.FACE_THREE;
                case '4':
                    return FaceEnum.FACE_FOUR;
                case '5':
                    return FaceEnum.FACE_FIVE;
                case '6':
                    return FaceEnum.FACE_SIX;
                case '7':
                    return FaceEnum.FACE_SEVEN;
                case '8':
                    return FaceEnum.FACE_EIGHT;
                case '9':
                    return FaceEnum.FACE_NINE;
                case '0':
                    return FaceEnum.FACE_TEN;
                case 'j':
                    return FaceEnum.FACE_JACK;
                case 'q':
                    return FaceEnum.FACE_QUEEN;
                case 'k':
                    return FaceEnum.FACE_KING;
                default:
                    return FaceEnum.NONE;
            }
            return FaceEnum.NONE;
        }

        private SuitEnum ParseSuit(char code)
        {
            switch (code)
            {
                case 'c':
                default:
                    return SuitEnum.SUIT_CLUBS;
                case 'h':
                    return SuitEnum.SUIT_HEARTS;
                case 's':
                    return SuitEnum.SUIT_SPADE;
                case 'd':
                    return SuitEnum.SUIT_DIAMONDS;
            }
        }

        public string LoadedDeckName { get => _loadedDeckName; set => _loadedDeckName = value; }
        public Sprite CardBackTex { get => _cardBackSprite; set => _cardBackSprite = value; }
    }
}
