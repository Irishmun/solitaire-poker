using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolitairePoker.Poker
{
    public class Logic
    {
        private const string SCORE_FILE = "Content/scores.psv";
        private Dictionary<string, int> _handScore = new Dictionary<string, int>();

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

        public bool IsValidHandScore(Card[] hand, out string handName, out int score)
        {
            handName = string.Empty;
            score = 0;

            if (hand.Length <= 1)
            {
                //high card doesn't score?
                return false;
            }
            //check if any card share the same face,
            //if so, check how many (pair, three of a kind, four of a kind)
            //check if multiple pairs (two pair)
            //check if sequential pairing (straight)
            //check if all same suit (flush)
            //check if all same suit & sequential (straight flush)
            //check if all same suit & face cards (royal flush)

            return false;
        }
    }
}
