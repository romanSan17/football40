using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace football40
{
    internal class SoccerGoal
    {
        public double X { get; }
        public double Y { get; }
        public double Width { get; }
        public double Height { get; }

        public SoccerGoal(double x, double y, double width, double height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public bool IsBallInGoal(Ball ball)
        {
            var (ballX, ballY) = ball.GetBallPosition();
            return ballX >= X && ballX <= X + Width && ballY >= Y && ballY <= Y + Height;
        }
    }
}

