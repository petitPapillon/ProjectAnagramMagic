using domasno.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace domasno
{
    class Words
    {
         string dataBase = Resources.a;
        HashSet<string> set = new HashSet<string>();
        SortedDictionary<string, HashSet<string>> map = new SortedDictionary<string, HashSet<string>>();
        List<string> nineLetterWords;
        public Words()
        {
            char[] separator = { '\n' };
           string [] words = dataBase.Split(separator);
           nineLetterWords = new List<string>();
            foreach (string word in words)
            {
                string lower = word.ToLower();
                string firstLetter = word.ElementAt(0).ToString().ToLower();
                if (lower.Length == 10 && !lower.Contains("-") && !lower.Contains(" ") && !lower.Contains("'"))
                    nineLetterWords.Add(lower);
                if (map.ContainsKey(firstLetter))
                {
                    map.TryGetValue(firstLetter, out set);
                    set.Add(lower);
                }
                else
                {
                    set = new HashSet<string>();
                    map.Add(firstLetter, set);
                }
            }
        }

        public string getNineLetterWord(){
            List<char> l = new List<char>();
            Random r = new Random();
            int idx = r.Next(0, nineLetterWords.Count);
            return nineLetterWords.ElementAt(idx).ToUpper();
        }
        public List<char> getNineLetter(string word)
        {
            List<char> l = new List<char>();
            Random r = new Random();
            int i = 0;
            while (word.Length != 0)
            {
                i = r.Next(0,word.Length);
                l.Add(word.ElementAt(i));
                word.Remove(i);
            }
            return l;
        }

        public bool IsValid(string word)
        {
            word = word.ToLower();
            string firstLetter = word.ElementAt(0).ToString();
            HashSet<string> l;
            map.TryGetValue(firstLetter, out l);
            foreach (string s in l)
            {
                if (s.Trim() == word.Trim())
                {
                    return true;
                }
            }
            return false;
        }

    }
    
}
