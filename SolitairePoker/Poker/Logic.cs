using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace SolitairePoker.Poker
{
    public class Logic
    {
        private const string SCORE_FILE = "Content/scores.psv";
        private Dictionary<string, int> _handScore = new Dictionary<string, int>();

        /// <summary>
        /// Get score from the <see cref="SCORE_FILE"/>
        /// </summary>
        public void LoadSettings()
        {
            using (Stream stream = TitleContainer.OpenStream(SCORE_FILE))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string[] lines = reader.ReadToEnd().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (lines[i].StartsWith(";;"))
                        { continue; }

                        string[] data = lines[i].Split('|', StringSplitOptions.TrimEntries);

                        if (data.Length < 4)
                        { continue; }

                        _handScore.Add(data[0], int.Parse(data[3]));
                    }
                }
            }
        }

        /// <summary>
        /// Apply score to the scoreboard
        /// </summary>
        public void ApplyScore(string scoredHand)
        {
            ScoreBoard.AddScore(scoredHand, GetHandScore(scoredHand));
        }

        /// <summary>
        /// Get Score by hand name.
        /// </summary>
        /// <returns>score if hand name is valid, 0 if not</returns>
        public int GetHandScore(string name)
        {
            return _handScore.ContainsKey(name) ? _handScore[name] : 0;
        }

        /// <summary>
        /// Evaluate played hand for any valid plays
        /// </summary>
        /// <returns><see cref="string.Empty"/> if invalid hand. hand name if valid</returns>
        public string EvaluateHand(Card[] hand)
        {

            if (hand.Length == 0) //<= 1)
            {
                return string.Empty;
            }

            int same4 = GetXSame(4, hand);
            int same3 = GetXSame(3, hand);
            int same2 = GetXSame(2, hand);
            int straight = GetStraight(hand);
            int flush = GetFlush(hand);
            int high = GetHighCard(hand);

            if (flush > 0 && straight > 0)
            {
                return "Straight Flush";
            }

            if (same4 > 0)
            {
                return "Four of a kind";
            }

            if (same3 > 0 && same2 > 0)
            {
                return "Full House";
            }

            if (flush > 0)
            {
                return "Flush";
            }

            if (straight > 0)
            {
                return "Straight";
            }

            if (same3 > 0)
            {
                return "Three of a kind";
            }

            if (same2 == 2)
            {
                return "Two Pair";
            }

            if (same2 > 0)
            {
                return "Pair";
            }

            if (high > 0)
            {
                return "High Card";
            }

            return string.Empty;
        }

        private int GetXSame(int desiredSame, Card[] hand)
        {
            /*
             * List<card[]> samecards
             * for i = handCount, i>1, --i
             *  List<card> curr = new list
             *  curr.add(hand[i])
             *  for handcount (start 1, j)
             *      if hand[i].face == hand[j].face && i != j
             *          curr.add(hand[j]
             *  if curr.count == num //handle three of a kind
             *      sameCards.add(curr.toarray)
             * return samesCount
            */
            List<Card[]> sameCards = new List<Card[]>();

            for (int i = hand.Length - 1; i >= 0; i--)
            {
                List<Card> curr = new List<Card>();
                curr.Add(hand[i]);
                for (int j = 0; j < hand.Length; j++)
                {
                    if (hand[i].Face == hand[j].Face && i != j)
                    {
                        curr.Add(hand[j]);
                    }
                }
                if (curr.Count == desiredSame)
                {
                    sameCards.Add(curr.ToArray());
                }
            }

            return sameCards.Count;
        }

        private int GetStraight(Card[] hand)
        {
            /*
             * int straightLength
             * if handCount < 5
             *  return 0
             * sort hand by face value
             * lowest = hand[0].face
             * bool aceking = (last hand card == king && lowest == ace)
             * if aceking true
             *  lowest = hand[1].face
             * for handCount (start lowest +1)
             *  if (lowest+1 != hand[i].face)
             *      return 0
             *  lowest = hand[i].face
             *  straightLength ++;
             * if straightLength == 4 && aceking
             *  straightLength = 5
             * if straightLength == 5
             *  return 1
             * return 0
             */
            int straightLength = 1;
            bool kingAce;
            if (hand.Length < 5)
            { return 0; }//straight needs 5 cards, assumes there can be no more than 5 cards in a (played) hand

            Card[] sortedHand = hand;
            Array.Sort(sortedHand, new Comparison<Card>((x, y) => Compare((byte)x.Face, (byte)y.Face)));

            Card lowest = sortedHand[0];
            kingAce = sortedHand[sortedHand.Length - 1].Face == FaceEnum.FACE_KING && lowest.Face == FaceEnum.FACE_ACE;

            if (kingAce)
            {
                lowest = sortedHand[1];
            }

            for (int i = kingAce ? 2 : 1; i < sortedHand.Length; i++)
            {
                FaceEnum nextFace = (FaceEnum)((byte)lowest.Face + (byte)1);
                if (nextFace != sortedHand[i].Face)
                {
                    return 0;
                }
                lowest = sortedHand[i];
                straightLength++;
            }

            if (straightLength == 4 && kingAce)
            {//in case Ace is past King (10,J,Q,K,A)
                straightLength = 5;
            }

            if (straightLength >= 5)
            {
                return 1;
            }
            return 0;
        }

        private int GetFlush(Card[] hand)
        {
            /*
             * SuitEnum suit
             * if handcount <5
             *  return 0
             * suit = hand[0].suit
             * for handcount (start 1)
             * if hand[i].suit != suit
             *  return 0
             * return 1
             */
            SuitEnum suit;
            if (hand.Length < 5)
            {//flush needs at least 5 cards
                return 0;
            }
            suit = hand[0].Suit;//assumes there'll be no more than 5 cards in (played) hand
            for (int i = 1; i < hand.Length; i++)
            {
                if (hand[i].Suit != suit)
                {//if ANY suit isn't the first one, there can be no flush
                    return 0;
                }
            }
            return 1;
        }

        private int GetHighCard(Card[] hand)
        {//would always return 1 if hand is not empty
            /*
             * sort hand by face
             * take last (highest) card
             * return 1
             */
            if (hand.Length == 0)
            { return 0; }

            return 1;
        }

        private sbyte Compare(byte a, byte b)
        {
            if (a < b)
            {
                return -1;
            }
            if (a > b)
            {
                return 1;
            }
            return 0;
        }


    }
}
