using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolitairePoker
{
    public partial class GameOverScreen : Form
    {
        public GameOverScreen()
        {
            InitializeComponent();
        }

        private void BT_PlayAgain_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GameOverScreen_Load(object sender, EventArgs e)
        {
            ScoreBoard.ReadScoreFile();

            LB_HighScore.Text = ScoreBoard.GetHighScore(out DateTime date).ToString() + " Chips";
            LB_HighScoreDate.Text = "Date: " + date.ToString(CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern);

            LB_Score.Text = ScoreBoard.TotalScore + " Chips";
        }
    }
}
