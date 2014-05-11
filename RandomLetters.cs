using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace domasno
{
    class RandomLetters
    {
       
        public List<Button> keyboard;
        public int k;
        public List<Char> Consonants;
        public List<Char> Vowels;
        public List<char> randomConsonants;
        public List<char> randomVowels;
        public List<char> randomC;
        public List<char> randomV;
        public int cNum;
        public int vNum;
        public int consNum;
        public int vowNum;
        public Pie pie;
        public int sec;

        public RandomLetters( List<Button> keyboard) 
        {
            k = 0;
            sec = 0;
            this.keyboard = new List<Button>(keyboard);
            pie = new Pie(152, 330);
            cNum = 0;
            vNum = 0;
            consNum = 0;
            vowNum = 0;
            char[] c = { 'B', 'C', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'V', 'W', 'X', 'Y', 'Z' };
            char[] v = { 'A', 'E', 'I', 'O', 'U' };
            Consonants = new List<char>(c);
            Vowels = new List<char>(v);
            randomConsonants = new List<char>(pickConsonants());
            randomVowels = new List<char>(pickVowels());
            randomC = new List<char>(pickConsonants());
            randomV = new List<char>(pickVowels());
        }

        public List<Char> pickConsonants()
        {
            List<char> lista = new List<char>();
            Random r = new Random();
            cNum = r.Next(4, 6);
            consNum = cNum;
            for (int i = 0; i < cNum; i++)
            {
                lista.Add(Consonants.ElementAt(r.Next(0, 21)));
            }
            return lista;
        }

        public List<Char> pickVowels()
        {
            List<char> lista = new List<char>();
            Random r = new Random();
            vNum = 9 - cNum;
            vowNum = vNum;
            for (int i = 0; i < vNum; i++)
            {
                lista.Add(Vowels.ElementAt(r.Next(0, 5)));
            }
            return lista;
        }

        public void fillRandom(Panel panela , List<string> shuffle)
        {
            List<char> allLetters = new List<char>();
            allLetters.AddRange(randomC);
            allLetters.AddRange(randomV);
            Random r = new Random();
            int k = 0;
            for (int i = 0; i < keyboard.Count; i++)
            {
                k = r.Next(allLetters.Count);
                keyboard.ElementAt(i).Text = allLetters.ElementAt(k).ToString().ToUpper();
                allLetters.RemoveAt(k);
            }

            for (int i = 0; i < keyboard.Count; i++)
            {
                shuffle.Add(keyboard.ElementAt(i).Text);
            }
                Thread.Sleep(500); 
        }

        public char getVowel()
        {
            Random r = new Random();
            return Vowels.ElementAt(r.Next(5));
        }

        public char getCons()
        {
            Random r = new Random();
            return Consonants.ElementAt(r.Next(21));
        }
    }
}
