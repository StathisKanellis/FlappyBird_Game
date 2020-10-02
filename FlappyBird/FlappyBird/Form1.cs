using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlappyBird
{
    public partial class FlappyBirdGame : Form
    {
        int pipeSpeed = 8;
        int gravity = 7;
        int score = 0;
        string text = "Score: ";

        private bool _gameReady;

        public FlappyBirdGame()
        {
            InitializeComponent();
        }

        //game loaded
        private void FlappyBirdGame_Load(object sender, EventArgs e)
        {
            //start with game stopped
            gameTimer.Enabled = false;
            _gameReady = true;

            MessageBox.Show("Press space to begin moving, press R to restart.");
        }

        private void gameTimerEvent(object sender, EventArgs e)
        {
            flappyBird.Top += gravity;
            pipeBottom.Left -= pipeSpeed;
            pipeTop.Left -= pipeSpeed;
            scoreText.Text = text + score.ToString();

            if (pipeBottom.Left < -100)
            {
                pipeBottom.Left = 800;
                score++;
            }

            if (pipeTop.Left < -100)
            {
                pipeTop.Left = 950;
                score++;
            }

            if (flappyBird.Bounds.IntersectsWith(pipeBottom.Bounds) ||
                flappyBird.Bounds.IntersectsWith(pipeTop.Bounds) ||
                flappyBird.Bounds.IntersectsWith(ground.Bounds) || 
                flappyBird.Top < -25)
            {
                endGame();
            }

            if (score > 7)
            {
                pipeSpeed = 13;
            }
        }

        private void gameKeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Space))
            {
                gravity = -5;

                //unpause game
                if (_gameReady)
                {
                    gameTimer.Enabled = true;
                    _gameReady = false;
                }
            }
            else if(e.KeyCode.Equals(Keys.R))
            {
                ResetGame();
            }
        }

        private void gameKeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Space))
            {
                gravity = 5;
            }
        }

        private void endGame()
        {
            gameTimer.Stop();
            scoreText.Text += " - Game Over!!!";
        }

        //reset the game
        private void ResetGame()
        {
            //stop timer
            gameTimer.Enabled = false;

            //reset sprites
            pipeBottom.Location = new Point(327, 291);
            pipeTop.Location = new Point(477, -1);
            flappyBird.Location = new Point(59, 150);

            score = 0;

            _gameReady = true;
        }
    }
}
