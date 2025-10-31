using System;
using System.Collections.Generic;
using System.IO;

namespace SolitairePoker
{
    public static class ScoreBoard
    {
        private const string SCORE_FILENAME = "scores.csv";
        private const int MAX_HISTORY_LENGTH = 9;
        private static int _totalScore = 0;
        private static List<string> _handHistory = new List<string>();
        private static List<int> _scoreHistory = new List<int>();
        private static List<ScoreDate> _scores = new List<ScoreDate>();

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

        public static bool WriteScoreToFile()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SCORE_FILENAME);
            DateTime date = DateTime.Now;
            string dateScore = $"{date.ToString("yyyy/MM/dd")},{_totalScore}";

            using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(dateScore);
                }
            }

            return true;
        }

        public static bool ReadScoreFile()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SCORE_FILENAME);
            if (!File.Exists(path))
            {
                return false;
            }

            _scores.Clear();

            using (StreamReader reader = new StreamReader(path))
            {
                string line = reader.ReadLine();
                string[] vals = line.Split(',', 2);

                DateTime date = DateTime.ParseExact(vals[0], "yyyy/MM/dd", null);
                int score = int.Parse(vals[1]);

                _scores.Add(new ScoreDate
                {
                    Date = date,
                    Score = score
                });
            }

            return true;
        }

        public static int GetHighScore(out DateTime date)
        {
            if (_scores.Count == 0)
            {
                date = DateTime.MinValue;
                return 0;
            }

            _scores.Sort((s1, s2) => s1.Score.CompareTo(s2.Score));

            ScoreDate high = _scores[0];

            date = high.Date;
            return high.Score;
        }

        public static int TotalScore { get => _totalScore; set => _totalScore = value; }
    }

    public struct ScoreDate
    {
        public int Score { get; set; }
        public DateTime Date { get; set; }
    }
}