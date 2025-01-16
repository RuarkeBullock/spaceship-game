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
        Rectangle hero = new Rectangle(280, 540, 40, 40);
        int heroSpeed = 10;

        //Ball variables
        int ballSize = 10;
        int ballSpeed = 8;

        //List of balls
        List<Rectangle> balls = new List<Rectangle>();

        int score = 0;
        int time = 500;

        bool upPressed = false;
        bool downPressed = false;
        bool wPressed = false;
        bool sPressed = false;

        SolidBrush greenBrush = new SolidBrush(Color.Green);
        SolidBrush whiteBrush = new SolidBrush(Color.White);

        Random randGen = new Random();
        int randValue = 0;

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

            //move hero
            if (upPressed == true && hero.X > 0)
            {
                hero.X -= heroSpeed;
            }

            if (downPressed == true && hero.X < this.Width - hero.Width)
            {
                hero.X += heroSpeed;
            }


            //create ball
            randValue = randGen.Next(0, 101);

            if (randValue < 50)
            {
                int y = randGen.Next(0, this.Height);
                Rectangle newBall = new Rectangle(0, y, ballSize, ballSize);
                balls.Add(newBall);
            }

            //check for points
            for (int i = 0; i < balls.Count; i++)
            {
                int x = balls[i].X + ballSpeed;
                balls[i] = new Rectangle(x, balls[i].Y, ballSize, ballSize);

            }

            //move ball
            for (int i = 0; i < balls.Count; i++)
            {
                if (hero.IntersectsWith(balls[i]))
                {
                    score += 10;
                    balls.RemoveAt(i);
                }
            }

            //redraw the screen
            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //draw balls
            for (int i = 0; i < balls.Count; i++)
            {
                e.Graphics.FillEllipse(greenBrush, balls[i]);
            }

            //draw hero
            e.Graphics.FillRectangle(whiteBrush, hero);
        }
    }
}
