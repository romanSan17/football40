using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace football40
{
    internal class Score
    {
        public int HomeScore { get; private set; }
        public int AwayScore { get; private set; }

        public void IncrementHomeScore()
        {
            HomeScore++;
        }

        public void IncrementAwayScore()
        {
            AwayScore++;
        }

        public void DisplayScore()
        {
            Console.WriteLine($"Score: Home {HomeScore}-{AwayScore} Guest");
        }
    }
}
