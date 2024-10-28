using System;
using System.Collections.Generic;
using System.Threading;

namespace football40
{
    class Program
    {
        static void Main(string[] args)
        {
            Stadium stadium = new Stadium(60, 20); 

            SoccerGoal homeGoal = new SoccerGoal(0, 8, 5, 4);
            SoccerGoal awayGoal = new SoccerGoal(55, 8, 5, 4);

            Team homeTeam = new Team("Home");
            Team guestTeam = new Team("Guest");

            for (int i = 0; i < 5; i++)
            {
                homeTeam.AddPlayer(new Player($"H{i + 1}"));
                guestTeam.AddPlayer(new Player($"G{i + 1}"));
            }

            Game game = new Game(homeTeam, guestTeam, stadium);
            game.Start();

   
            DateTime lastBallPosition = DateTime.Now;
            (double lastBallX, double lastBallY) = (game.Ball.GetBallPosition().Item1, game.Ball.GetBallPosition().Item2);
            double stopTime = 0;

            while (true)
            {
                game.Move();

                (double nowBallX, double nowBallY) = (game.Ball.GetBallPosition().Item1, game.Ball.GetBallPosition().Item2);

                if (Math.Abs(nowBallX - lastBallX) < 0.1 && Math.Abs(nowBallY - lastBallY) < 0.1)
                {
                    stopTime += 0.1; 
                }
                else
                {
                    stopTime = 0; 
                }

                lastBallX = nowBallX;
                lastBallY = nowBallY;

                if (stopTime >= 5)
                {
                    game.Ball.SetSpeed(0, 0); 
                    game.Ball.SetPosition(stadium.Width / 2, stadium.Height / 2); 
                    stopTime = 0; 
                }

                DrawGame(stadium, homeTeam, guestTeam, game.Ball, homeGoal, awayGoal, game.Score);

                Thread.Sleep(100); 
            }
        }

        static void DrawGame(Stadium stadium, Team homeTeam, Team awayTeam, Ball ball, SoccerGoal homeGoal, SoccerGoal awayGoal, Score score)
        {
            Console.Clear(); 

            for (int y = 0; y < stadium.Height; y++)
            {
                for (int x = 0; x < stadium.Width; x++)
                {
                    // Проверяем, есть ли мяч на текущих координатах
                    (double ballX, double ballY) = ball.GetBallPosition(); // Получаем позицию мяча
                    if (Math.Abs(ballX - x) < 0.5 && Math.Abs(ballY - y) < 0.5)
                    {
                        Console.Write("O"); // Отображаем мяч
                    }
                    else
                    {
                        bool playerDrawn = false;

                        // Проверяем игроков домашней команды
                        foreach (var player in homeTeam.Players)
                        {
                            if (Math.Abs(player.X - x) < 0.5 && Math.Abs(player.Y - y) < 0.5)
                            {
                                Console.Write("H");
                                playerDrawn = true;
                                break;
                            }
                        }

                        if (!playerDrawn)
                        {
                            foreach (var player in awayTeam.Players)
                            {
                                if (Math.Abs(player.X - x) < 0.5 && Math.Abs(player.Y - y) < 0.5)
                                {
                                    Console.Write("G"); 
                                    playerDrawn = true;
                                    break;
                                }
                            }
                        }


                        if (x >= homeGoal.X && x <= homeGoal.X + homeGoal.Width && y >= homeGoal.Y && y <= homeGoal.Y + homeGoal.Height)
                        {
                            Console.Write("|"); 
                            playerDrawn = true;
                        }

                        if (x >= awayGoal.X && x <= awayGoal.X + awayGoal.Width && y >= awayGoal.Y && y <= awayGoal.Y + awayGoal.Height)
                        {
                            Console.Write("|"); 
                            playerDrawn = true;
                        }

                        if (!playerDrawn)
                        {
                            Console.Write(".");
                        }
                    }
                }
                Console.WriteLine(); 
            }
            score.DisplayScore();
        }
    }
}