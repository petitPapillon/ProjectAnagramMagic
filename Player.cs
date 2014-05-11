using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domasno
{
    public class Player
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public string bestWord { get; set; }

        public Player(string Name,int Score,string bestWord)
        {
             this.Name = Name;
             this.Score = Score;
             this.bestWord = bestWord;
        }

        public override string ToString()
        {
            return string.Format("{0,-15}{1,-15}{2,-15}", Name, Score, bestWord);
        }
    }
}
