namespace SolitairePoker
{
    partial class GameOverScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new System.Windows.Forms.Label();
            BT_PlayAgain = new System.Windows.Forms.Button();
            BT_Exit = new System.Windows.Forms.Button();
            LB_Score = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            LB_HighScore = new System.Windows.Forms.Label();
            LB_HighScoreDate = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            label1.Location = new System.Drawing.Point(12, 9);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(261, 15);
            label1.TabIndex = 0;
            label1.Text = "Game over, no more valid moves.";
            label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // BT_PlayAgain
            // 
            BT_PlayAgain.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            BT_PlayAgain.DialogResult = System.Windows.Forms.DialogResult.Yes;
            BT_PlayAgain.Location = new System.Drawing.Point(146, 108);
            BT_PlayAgain.Name = "BT_PlayAgain";
            BT_PlayAgain.Size = new System.Drawing.Size(128, 23);
            BT_PlayAgain.TabIndex = 1;
            BT_PlayAgain.Text = "Play again";
            BT_PlayAgain.UseVisualStyleBackColor = true;
            BT_PlayAgain.Click += BT_PlayAgain_Click;
            // 
            // BT_Exit
            // 
            BT_Exit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            BT_Exit.DialogResult = System.Windows.Forms.DialogResult.No;
            BT_Exit.Location = new System.Drawing.Point(12, 108);
            BT_Exit.Name = "BT_Exit";
            BT_Exit.Size = new System.Drawing.Size(128, 23);
            BT_Exit.TabIndex = 2;
            BT_Exit.Text = "Exit";
            BT_Exit.UseVisualStyleBackColor = true;
            // 
            // LB_Score
            // 
            LB_Score.AutoSize = true;
            LB_Score.Location = new System.Drawing.Point(57, 38);
            LB_Score.Name = "LB_Score";
            LB_Score.Size = new System.Drawing.Size(62, 15);
            LB_Score.TabIndex = 4;
            LB_Score.Text = "9999 chips";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(12, 38);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(39, 15);
            label2.TabIndex = 3;
            label2.Text = "Score:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(12, 60);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(63, 15);
            label3.TabIndex = 5;
            label3.Text = "Best score:";
            // 
            // LB_HighScore
            // 
            LB_HighScore.AutoSize = true;
            LB_HighScore.Location = new System.Drawing.Point(81, 60);
            LB_HighScore.Name = "LB_HighScore";
            LB_HighScore.Size = new System.Drawing.Size(62, 15);
            LB_HighScore.TabIndex = 6;
            LB_HighScore.Text = "9999 chips";
            // 
            // LB_HighScoreDate
            // 
            LB_HighScoreDate.AutoSize = true;
            LB_HighScoreDate.Location = new System.Drawing.Point(160, 60);
            LB_HighScoreDate.Name = "LB_HighScoreDate";
            LB_HighScoreDate.Size = new System.Drawing.Size(95, 15);
            LB_HighScoreDate.TabIndex = 7;
            LB_HighScoreDate.Text = "Date: 10/30/2025";
            // 
            // label4
            // 
            label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            label4.Location = new System.Drawing.Point(12, 90);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(261, 15);
            label4.TabIndex = 8;
            label4.Text = "Play again?";
            label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // GameOverScreen
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(285, 143);
            Controls.Add(label4);
            Controls.Add(LB_HighScoreDate);
            Controls.Add(LB_HighScore);
            Controls.Add(label3);
            Controls.Add(LB_Score);
            Controls.Add(label2);
            Controls.Add(BT_Exit);
            Controls.Add(BT_PlayAgain);
            Controls.Add(label1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "GameOverScreen";
            SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            Text = "GameOverScreen";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BT_PlayAgain;
        private System.Windows.Forms.Button BT_Exit;
        private System.Windows.Forms.Label LB_Score;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label LB_HighScore;
        private System.Windows.Forms.Label LB_HighScoreDate;
        private System.Windows.Forms.Label label4;
    }
}