using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace football40
{
    internal class Game
    {
        public Team HomeTeam { get; }
        public Team AwayTeam { get; }
        public Stadium Stadium { get; }
        public Ball Ball { get; private set; }
        public Score Score { get; private set; }

        public Game(Team homeTeam, Team awayTeam, Stadium stadium)
        {
            HomeTeam = homeTeam;
            homeTeam.Game = this;
            AwayTeam = awayTeam;
            awayTeam.Game = this;
            Stadium = stadium;
            Score = new Score(); // Инициализируем счет
        }

        public void Start()
        {
            Ball = new Ball(Stadium.Width / 2, Stadium.Height / 2, this);
            HomeTeam.StartGame(Stadium.Width / 2, Stadium.Height);
            AwayTeam.StartGame(Stadium.Width / 2, Stadium.Height);
        }

        private (double, double) GetPositionForAwayTeam(double x, double y)
        {
            return (Stadium.Width - x, Stadium.Height - y);
        }

        public (double, double) GetPositionForTeam(Team team, double x, double y)
        {
            return team == HomeTeam ? (x, y) : GetPositionForAwayTeam(x, y);
        }

        public (double, double) GetBallPositionForTeam(Team team)
        {
            return GetPositionForTeam(team, Ball.X, Ball.Y);
        }

        public void SetBallSpeedForTeam(Team team, double vx, double vy)
        {
            if (team == HomeTeam)
            {
                Ball.SetSpeed(vx, vy);
            }
            else
            {
                Ball.SetSpeed(-vx, -vy);
            }
        }

        public void Move()
        {
            HomeTeam.Move();
            AwayTeam.Move();
            Ball.Move();

            if (HomeTeam.GetClosestPlayerToBall().GetDistanceToBall() < 1.0 && Stadium.IsIn(Ball.X, Ball.Y)) // Примерная проверка на гол
            {
                Score.IncrementHomeScore();
                ResetBall();
            }
            else if (AwayTeam.GetClosestPlayerToBall().GetDistanceToBall() < 1.0 && Stadium.IsIn(Ball.X, Ball.Y)) // Примерная проверка на гол
            {
                Score.IncrementAwayScore();
                ResetBall();
            }
        }

        private void ResetBall()
        {
            Ball.SetPosition(Stadium.Width / 2, Stadium.Height / 2);
            Ball.SetSpeed(0, 0);
        }
    }
}
