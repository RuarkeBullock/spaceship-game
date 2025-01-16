using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace spaceship_game
{
    public partial class Form1 : Form
    {
        // PLayer scores
        private int scorePlayer1 = 0;
        private int scorePlayer2 = 0;

        Random randGen = new Random();

        // Create players 
        Rectangle player1 = new Rectangle(200, 270, 20, 20);
        Rectangle player2 = new Rectangle(400, 270, 20, 20); // Moved player 2 right

        // Hero speed
        int heroSpeed = 10;

        // List of balls
        List<Rectangle> balls = new List<Rectangle>();
        List<int> ballSpeeds = new List<int>();  // ball speeds
        List<int> ballSizes = new List<int>();   // ball sizes

        // Button presses
        bool upPressed = false;
        bool downPressed = false;
        bool wPressed = false;
        bool sPressed = false;

        // Brushes
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush greenBrush = new SolidBrush(Color.Green);
        SolidBrush blackBrush = new SolidBrush(Color.Black);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    upPressed = true;
                    break;
                case Keys.Down:
                    downPressed = true;
                    break;
                case Keys.W:
                    wPressed = true;
                    break;
                case Keys.S:
                    sPressed = true;
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    upPressed = false;
                    break;
                case Keys.Down:
                    downPressed = false;
                    break;
                case Keys.W:
                    wPressed = false;
                    break;
                case Keys.S:
                    sPressed = false;
                    break;
            }

        }

        private void gameTime_Tick(object sender, EventArgs e)
        {
            

            // Move Player 1 (up/down)
            if (upPressed && player1.Y > 0) player1.Y -= heroSpeed;
            if (downPressed && player1.Y < this.Height - player1.Height) player1.Y += heroSpeed;
            

            // Move Player 2 (w/s) 
            if (wPressed && player2.Y > 0) player2.Y -= heroSpeed;
            if (sPressed && player2.Y < this.Height - player2.Height) player2.Y += heroSpeed;
            

            // Check if Player 1 reaches the top
            if (player1.Y <= 0) { scorePlayer1++; player1.Y = this.Height - player1.Height; }
            scorePlayer1++;
            
            // Check if Player 2 reaches the top
            if (player2.Y <= 0) { scorePlayer2++; player2.Y = this.Height - player2.Height; }
            scorePlayer2++;
            

            // Create balls randomly
            if (randGen.Next(0, 101) < 15)  // Reduced frequency of ball creation
            {
                int y = randGen.Next(0, this.Height - 200);
                int size = randGen.Next(5, 15);  // Random size 
                int speed = randGen.Next(5, 10);  // Random speed 
                balls.Add(new Rectangle(0, y, size, size));
                ballSpeeds.Add(speed);
                ballSizes.Add(size);
            }
            else if (randGen.Next(0, 101) < 15)
            {
                int y = randGen.Next(0, this.Height);
                int size = randGen.Next(5, 15);  // Random size 
                int speed = randGen.Next(5, 10);  // Random speed 
                balls.Add(new Rectangle(this.Width, y, size, size));  // Ball starts from right
                ballSpeeds.Add(-speed);
                ballSizes.Add(size);
            }

            // Move balls left or right
            for (int i = 0; i < balls.Count; i++)
            {
                balls[i] = new Rectangle(balls[i].X + ballSpeeds[i], balls[i].Y, ballSizes[i], ballSizes[i]);

                // Change direction when ball hits sides
                if (balls[i].X <= 0 || balls[i].X >= this.Width - balls[i].Width)
                    ballSpeeds[i] = -ballSpeeds[i];
            }

            // Check if Player 1 or Player 2 intersects with balls
            for (int i = 0; i < balls.Count; i++)
            {
                if (player1.IntersectsWith(balls[i]))
                {

                    balls.RemoveAt(i);
                    ballSpeeds.RemoveAt(i);
                    ballSizes.RemoveAt(i);
                    
                    player1.Y = this.Height - player1.Height - 50;  // Reset Player 1 to bottom
                }

                if (player2.IntersectsWith(balls[i]))
                {

                    balls.RemoveAt(i);
                    ballSpeeds.RemoveAt(i);
                    ballSizes.RemoveAt(i);
                    
                    player2.Y = this.Height - player2.Height - 50;  // Reset Player 2 to bottom
                }
            }

            // Refresh the screen
            Refresh();
        }


        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //draw balls
            for (int i = 0; i < balls.Count; i++)
            {
                e.Graphics.FillEllipse(greenBrush, balls[i]);
            }

            //draw hero1
            e.Graphics.FillRectangle(whiteBrush, player1);

            //draw hero2
            e.Graphics.FillRectangle(whiteBrush, player2);
        }
    }
}
