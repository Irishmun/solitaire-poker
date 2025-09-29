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
        private readonly Vector2 AssumedCardSize = new Vector2(42,60);
        private string _loadedDeckName = "no deck";
        private string _cardBack;
        private TextureRegion _cardBackTex;
        private Vector2 _origin = new Vector2(-1);//top-left corner of the card
        private Vector2 _size = Vector2.Zero;//size of the texture to be used
        private Vector2 _scale = Vector2.One;//how to apply scale before any other scale should be applied
        private Card[] _loadedCards;

        public void LoadDeckIntoMemory(ContentManager content, string deckFile)
        {
            Card[] cards = GetCardNames(deckFile, content.RootDirectory);

            Texture2D tex;

            for (int i = 0; i < cards.Length; i++)
            {
                cards[i].Texture = LoadTexture(cards[i].FileName);
            }

            _cardBackTex = LoadTexture(_cardBack);

            _loadedCards = cards;
            System.Diagnostics.Debug.WriteLine($"Loaded deck \"{_loadedDeckName}\"...");

            TextureRegion LoadTexture(string name)
            {
                tex = content.Load<Texture2D>(name);
                _origin = _origin.X < 0 ? new Vector2(tex.Width * 0.5f, tex.Height * 0.5f):_origin;
                _size = _size == Vector2.Zero ? new Vector2(tex.Width, tex.Height) : _size;
                return new TextureRegion(tex, (int)_origin.X, (int)_origin.Y, (int)_size.X, (int)_size.Y);
            }
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
                                case 'z':
                                    string[] zoomVec = cardKey[1].Split(',');
                                    if (zoomVec.Length >= 2 && float.TryParse(zoomVec[0], out float zoomVecX) && float.TryParse(zoomVec[1], out float zoomVecY))
                                    {
                                        _origin = new Vector2(zoomVecX, zoomVecY);
                                        Debug.WriteLine($"card zoom: {_origin}");
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
        public TextureRegion CardBackTex { get => _cardBackTex; set => _cardBackTex = value; }
        public Card[] LoadedCards { get => _loadedCards; set => _loadedCards = value; }
    }
}
