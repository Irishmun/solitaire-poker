using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolitairePoker
{
    public static class ScoreBoard
    {
        private const int MAX_HISTORY_LENGTH = 9;
        private static int _totalScore = 0;
        private static List<string> _handHistory = new List<string>();
        private static List<int> _scoreHistory = new List<int>();


        public static void AddScore(string name, int score)
        {
            _totalScore += score;
            if (_scoreHistory.Count >= MAX_HISTORY_LENGTH)
            {
                _scoreHistory.RemoveRange(0, _scoreHistory.Count - MAX_HISTORY_LENGTH + 1);//have one free slot
                _handHistory.RemoveRange(0, _handHistory.Count - MAX_HISTORY_LENGTH + 1);
            }
            _scoreHistory.Add(score);
            _handHistory.Add(name);

        }

        public static string GetFormattedHandHistory()
        {
            return string.Join('\n', _handHistory);
        }

        public static string GetFormattedScoreHistory()
        {
            return string.Join('\n', _scoreHistory);
        }

        public static void ResetScore()
        {
            _scoreHistory.Clear();
            _handHistory.Clear();
            _totalScore = 0;
        }

        public static int TotalScore { get => _totalScore; set => _totalScore = value; }
    }
}
