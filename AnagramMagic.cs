using domasno.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace domasno
{
    public partial class AnagramMagic : Form
    {
        int sec = 8;
        bool tick = false;
        bool play = true;
        bool f = true;
        Pie pie;
        int watch;
        int round;
        List<Char> helper;
        List<Point> points;
        List<Button> keyboard;
        List<string> shuffled;
        LinkedList<Point> answer;
        List<Point> answerFix;
        List<Point> zaNazad;
        Bitmap bunn;
        List<Button> buttons;
        List<bool> flag;
        List<Player> players;
        StringBuilder bestPlayersString;
        string word;
        Words words;
        int currentScore;
        int totalScore;
        bool ChangeLettersPerRound;
        LinkedList<char> used;
        LinkedList<char> unused;
        List<Button> randomButtons;
        RandomLetters randomLetters;
        string bestWord;
        int broiRandom;
        Song song;
        String correctNineLetterWord;
        List<Label> titleEven;
        List<Label> titleOdd;
        bool down;
        string name;
        Bitmap bmp;
        Bitmap wallpaper;
        Bitmap background;
        Star star;
        List<Label> invalidLabels;
        GameScore gameScore;

        public AnagramMagic()
        {
            InitializeComponent();
            gameScore = new GameScore();
            invalidLabels = new List<Label>();
            star = new Star(lblValidStars.Location);
            bmp = new Bitmap(Resources.mute, btnMusic.Size);
            bmp.MakeTransparent();
            btnMusic.BackgroundImage = bmp;
            wallpaper = new Bitmap(Resources.wallpaper, pictureBox1.Size);
            pictureBox1.Image = wallpaper;
            background = new Bitmap(Resources.bukvi, menu.Size);
          //  menu.BackgroundImage = background;
            panel1.BackgroundImage = background;
         //   this.BackgroundImage = background;
            DoubleBuffered = true;
            initTitle();
            down = false;
            broiRandom = 0;
            word = "";
            correctNineLetterWord = "";
            words = new Words();
            randomButtons = new List<Button>();
            currentScore = 0;
            totalScore = 0;
            ChangeLettersPerRound = false;
            used = new LinkedList<char>();
            unused = new LinkedList<char>();
            song = new Song();
            pie = new Pie(100, 70);
            points = new List<Point>();
            buttons = new List<Button>();
            answer = new LinkedList<Point>();
            answerFix = new List<Point>();
            shuffled = new List<string>();
            keyboard = new List<Button>();
            flag = new List<bool>();
            zaNazad = new List<Point>();
            setAnswer();
            initRandomButtons();
            bunn = new Bitmap(Resources.bunny, pictureBox3.Size);
            pictureBox3.Image = bunn;
           // pictureBox3.Visible = false;
            setKeyboard();
            randomLetters = new RandomLetters(keyboard);
            watch = 30;
            round = 0;
            players = new List<Player>();
            bestWord = "";
            bestPlayersString = new StringBuilder();
            bestPlayersString.Append("Best players:\n");

            for (int i = 0; i < 9; i++ )
            {
                flag.Add(false);
            }

            using (CsvFileWriter writer = new CsvFileWriter("Scores.csv", true))
            {
                CsvRow row = new CsvRow();
                row.Add("");
            }   
            invalidL();
            timer2.Start();
           
        }

        /*This method is to add the Labels in two separate
         *lists for the "Anagram Magic" bouncing letters on the mennu
         */
        private void initTitle()
        {
            titleEven = new List<Label>();
            titleOdd = new List<Label>();
            titleOdd.Add(lblTitle1);
            titleEven.Add(lblTitle2);
            titleOdd.Add(lblTitle3);
            titleEven.Add(lblTitle4);
            titleOdd.Add(lblTitle5);
            titleEven.Add(lblTitle6);
            titleOdd.Add(lblTitle7);
            titleEven.Add(lblTitle8);
            titleOdd.Add(lblTitle9);
            titleEven.Add(lblTitle10);
            titleOdd.Add(lblTitle11);
            titleEven.Add(lblTitle12);
        }

        /* This method is for adding the Buttons from the second Panel in list in order to use them
         * in the main panel (the Panel where the action happens)
         */
        public void initRandomButtons()
        {
            randomButtons.Add(randomButton1);
            randomButtons.Add(randomButton2); 
            randomButtons.Add(randomButton3);
            randomButtons.Add(randomButton4);
            randomButtons.Add(randomButton5);
            randomButtons.Add(randomButton6);
            randomButtons.Add(randomButton7);
            randomButtons.Add(randomButton8);
            randomButtons.Add(randomButton9);
        }

        /*This method is used when you click on the Shuffle Button which is on the third 
         * panel so you can shuffle the same 9 letters that you've been given for the
         * current round
         */
        public void shuffleButtons()
        {
            Random r = new Random();
            int n = 9;
            LinkedList<string> pom = new LinkedList<string>();
            
            while (n > 0)
            {
                int k = r.Next(n);
                pom.AddLast(shuffled[k]);
                shuffled.RemoveAt(k);
                n--;
            }
            shuffled = new List<string>();
            foreach (string s in pom)
                shuffled.Add(s);
            for (int i = 0; i < keyboard.Count; i++)
            {
                keyboard.ElementAt(i).Text = shuffled.ElementAt(i);
            }
        }

        /*This method is used to fill the list "points" with the actual points of the nine buttons
         * which cointain a letter in ther Text and the list "keyboard" with the actual nine buttons
         */
        public void setKeyboard()
        {
            points.Add(keyboard1.Location);
            points.Add(keyboard2.Location);
            points.Add(keyboard3.Location);
            points.Add(keyboard4.Location);
            points.Add(keyboard5.Location);
            points.Add(keyboard6.Location);
            points.Add(keyboard7.Location);
            points.Add(keyboard8.Location);
            points.Add(keyboard9.Location);
            keyboard.Add(keyboard1);
            keyboard.Add(keyboard2);
            keyboard.Add(keyboard3);
            keyboard.Add(keyboard4);
            keyboard.Add(keyboard5);
            keyboard.Add(keyboard6);
            keyboard.Add(keyboard7);
            keyboard.Add(keyboard8);
            keyboard.Add(keyboard9);
      
        }

        /* This method is used in the constructor
         * Here we fill three diferent lists with the positions of the blank buttons  
         * We fill them in a different order using the methods Add or AddLast in order to get
         * the effect that we want when pushing the letters back and forth
         */
        public void setAnswer()
        {
            answer.AddLast(letter1.Location);
            answerFix.Add(letter1.Location);
            zaNazad.Add(letter1.Location);
            answer.AddLast(letter2.Location);
            answerFix.Add(letter2.Location);
            zaNazad.Add(letter2.Location);
            answer.AddLast(letter3.Location);
            answerFix.Add(letter3.Location);
            zaNazad.Add(letter3.Location);
            answer.AddLast(letter4.Location);
            answerFix.Add(letter4.Location);
            zaNazad.Add(letter4.Location);
            answer.AddLast(letter5.Location);
            answerFix.Add(letter5.Location);
            zaNazad.Add(letter5.Location);
            answer.AddLast(letter6.Location);
            answerFix.Add(letter6.Location);
            zaNazad.Add(letter6.Location);
            answer.AddLast(letter7.Location);
            answerFix.Add(letter7.Location);
            zaNazad.Add(letter7.Location);
            answer.AddLast(letter8.Location);
            answerFix.Add(letter8.Location);
            zaNazad.Add(letter8.Location);
            answer.AddLast(letter9.Location);
            answerFix.Add(letter9.Location);
            zaNazad.Add(letter9.Location); 
        }
     
        /*The first timer that we use is for the ticking pie
         * the pie ticks for 30 seconds, which is the duration of every round 
         */
        private void timer1_Tick(object sender, EventArgs e)
        {
            pie.Move();
            Invalidate();
            watch -= 1;
            if (watch > 9)
                label14.Text = "00:" + watch.ToString();
            else
                label14.Text = "00:0" + watch.ToString();
            if (watch == 0)
            {
                iAmDone_Click(sender, e);
            }
        }
        
        //We use the paint to draw two objects, stars and a pie
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            pie.Draw(e.Graphics);
            if (valid.Text == "valid")
            {
                if (tick)
                star.Draw(e.Graphics);
            }
            
        }

        private void nextRound_Click(object sender, EventArgs e)
        {
            timer3.Stop();
            invalidL();
            sec = 8;
            lblValidStars.Visible = false;
            tick = false;
            nextRoundMethod();
        }


        //Set the keyboard buttons (the ones with letters on them) on their original location
        public void whenNextRound()
        { 
            for (int i = 0; i < 9; i++)
            {
                keyboard[i].Location = points[i];
                flag[i] = false;
            }
        }


        //use this method when using the fillNineLetterWord and fillButtons
        //here fill the buttons with letters despite of being on round 5 or on round 1,2,3,4
        public void pomosnaZapolnenje(List<char> lista)
        {
            Random r = new Random();
            int k = 0;
            List<char> list = new List<char>();
            for (int i = 0; i < keyboard.Count; i++)
            {
                k = r.Next(0, lista.Count);
                keyboard.ElementAt(i).Text = lista.ElementAt(k).ToString().ToUpper();
                list.Add(lista.ElementAt(k));
                lista.RemoveAt(k);
            }
        }
        //use this method for the fifth round where you guess the nine letter word
        public void fillNineLetterWord()
        {
            correctNineLetterWord = words.getNineLetterWord();
            List<char> lista = new List<char>();
            shuffled = new List<string>();
            helper = new List<char>();
            foreach (char c in correctNineLetterWord)
            {
                lista.Add(c);
                helper.Add(c);
                unused.AddLast(c);
                shuffled.Add(c.ToString());
            }

            pomosnaZapolnenje(lista);
            shuffleButtons();
        }

        //use this method to fill the buttons with 9 random letters if you're not on round 5
        public void fillButtons()
        {
            List<Char> lista = new List<char>();
            List<Char> lista1 = new List<char>();
            List<Char> lista2 = new List<char>();
            lista1 = randomLetters.pickConsonants();
            lista2 = randomLetters.pickVowels();
            shuffled = new List<string>();
            helper = new List<char>();

            foreach (char c in lista1)
            {
                lista.Add(c);
                helper.Add(c);
                unused.AddLast(c);
                shuffled.Add(c.ToString());
            }

            foreach (char c in lista2)
            {
                lista.Add(c);
                helper.Add(c);
                unused.AddLast(c);
                shuffled.Add(c.ToString());
            }
            pomosnaZapolnenje(lista);

        }
        /*use the whenNewRound() method show the buttons where the answer is set
         * fill the "answer" list with the actual points from the answer buttons
         * use the fillButton() method if you're not on round 5 , in other way
         * use the fillNineLetterWord()
         */ 
        public void newRound()
        {
            whenNextRound();
            lblValidStars.Visible = false;
            foreach (Point p in answerFix)
            {
                show(p);
            }
            answer = new LinkedList<Point>();
            foreach (Point p in answerFix)
            {
                answer.AddLast(p);
            }
            foreach (Button b in buttons)
                b.Enabled = true;
            buttons = new List<Button>();
            if (round <= 4)
                fillButtons();
            else
            {
                timer1.Stop();
                timer1.Start();
                lastW.Visible = true;
                fillNineLetterWord();
                // MessageBox.Show(correctNineLetterWord);
            }
            valid.Text = "";
        }

        /*Generally, here we do everything which is necessary for a new round
         * the iAmDone  button is enabled, we restart timer1 (for the ticking pie)
         * we increase the round counter and we also use the newRound() method
         */

        public void nextRoundMethod()
        {
            iAmDone.Enabled = true;
            if (round < 5)
            {
                KeyPreview = true;
                pie = new Pie(100, 70);
                timer1.Start();
                
                round += 1;
                label13.Text = round.ToString() + "/5";
                if (ChangeLettersPerRound)
                    newRound();
                ChangeLettersPerRound = true;
                shuffle.Enabled = true;
            }
            nextRound.Enabled = false;
        }
       
        //when guessing a letter, hide the first visible button from the answer buttons
        public void hide (Point p) 
        {
            if (letter1.Location == p)
                letter1.Visible = false;
            else if (letter2.Location == p)
                letter2.Visible = false;
            else if (letter3.Location == p)
                letter3.Visible = false;
            else if (letter4.Location == p)
                letter4.Visible = false;
            else if (letter5.Location == p)
                letter5.Visible = false;
            else if (letter6.Location == p)
                letter6.Visible = false;
            else if (letter7.Location == p)
                letter7.Visible = false;
            else if (letter8.Location == p)
                letter8.Visible = false;
            else if (letter9.Location == p)
                letter9.Visible = false;

        }

        // we use this method in nextRound in order to show the hidden answer buttons
        public void show(Point p)
        {
            if (letter1.Location == p)
                letter1.Visible = true;
            else if (letter2.Location == p)
                letter2.Visible = true;
            else if (letter3.Location == p)
                letter3.Visible = true;
            else if (letter4.Location == p)
                letter4.Visible = true;
            else if (letter5.Location == p)
                letter5.Visible = true;
            else if (letter6.Location == p)
                letter6.Visible = true;
            else if (letter7.Location == p)
                letter7.Visible = true;
            else if (letter8.Location == p)
                letter8.Visible = true;
            else if (letter9.Location == p)
                letter9.Visible = true;

        }


        /*Very important method
         * We are using it for clicking on the letter buttons when creating a word
         * or when writing with your keyboard
         * change the location on the button weather you want to press backspace
         * or to press on a actual letter that you want to use
         * you can do this with your mouse left click too
         */
        private void keyClick(Button button,int i)
        {
            if (iAmDone.Enabled == true && nextRound.Enabled == false)
            {
                if (!flag[i])
                {
                    button.Location = answer.ElementAt(0);
                    hide(answer.ElementAt(0));
                    zaNazad[i] = answer.ElementAt(0);
                    answer.RemoveFirst();
                    flag[i] = true;
                    used.AddLast(button.Text.ElementAt(0));
                    foreach (char c in unused)
                    {
                        if (c == used.Last())
                        {
                            unused.Remove(c);
                            break;
                        }
                    }
                    if (buttons.Count != 0)
                        foreach (Button b in buttons)
                            b.Enabled = false;
                    buttons.Add(button);
                }
                else
                {
                    answer.AddFirst(button.Location);
                    button.Location = points[i];
                    flag[i] = false;
                    show(zaNazad[i]);
                    char c = used.Last();
                    used.RemoveLast();
                    unused.AddLast(c);
                    buttons.Remove(buttons.Last());
                    if (buttons.Count != 0)
                        buttons.Last().Enabled = true;
                }
            }
        }

        //next nine methods use the keyClick method 
        private void keyboard1_Click(object sender, EventArgs e)
        {
            keyClick(keyboard1, 0);
        }

        private void keyboard7_Click(object sender, EventArgs e)
        {
            keyClick(keyboard7, 6);
        }

        private void keyboard6_Click(object sender, EventArgs e)
        {
            keyClick(keyboard6, 5);
        }

        private void keyboard9_Click(object sender, EventArgs e)
        {
            keyClick(keyboard9, 8);
        }

        private void keyboard5_Click(object sender, EventArgs e)
        {
            keyClick(keyboard5, 4);
        }

        private void keyboard4_Click(object sender, EventArgs e)
        {
            keyClick(keyboard4, 3);
        }

        private void keyboard3_Click(object sender, EventArgs e)
        {
            keyClick(keyboard3, 2);  
        }

        private void keyboard2_Click(object sender, EventArgs e)
        {
            keyClick(keyboard2, 1);
        }

        private void keyboard8_Click(object sender, EventArgs e)
        {
            keyClick(keyboard8, 7);
        }


        //here is where we manage the keyPress part
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            string s = e.KeyChar.ToString().ToUpper();
            if (keyboard1.Text == s && keyboard1.Enabled && !buttons.Contains(keyboard1))
                keyboard1_Click(sender, e);
            else if (keyboard2.Text == s && keyboard2.Enabled && !buttons.Contains(keyboard2))
                keyboard2_Click(sender, e);
            else if (keyboard3.Text == s && keyboard3.Enabled && !buttons.Contains(keyboard3))
                keyboard3_Click(sender, e);
            else if (keyboard4.Text == s && keyboard4.Enabled && !buttons.Contains(keyboard4))
                keyboard4_Click(sender, e);
            else if (keyboard5.Text == s && keyboard5.Enabled && !buttons.Contains(keyboard5))
                keyboard5_Click(sender, e);
            else if (keyboard6.Text == s && keyboard6.Enabled && !buttons.Contains(keyboard6))
                keyboard6_Click(sender, e);
            else if (keyboard7.Text == s && keyboard7.Enabled && !buttons.Contains(keyboard7))
                keyboard7_Click(sender, e);
            else if (keyboard8.Text == s && keyboard8.Enabled && !buttons.Contains(keyboard8))
                keyboard8_Click(sender, e);
            else if (keyboard9.Text == s && keyboard9.Enabled && !buttons.Contains(keyboard9))
                keyboard9_Click(sender, e);
                
            if(buttons.Count != 0)
            if (e.KeyChar == (char)Keys.Back)
            {
                if(keyboard1==buttons.Last())
                    keyboard1_Click(sender, e);
                else if (keyboard2 == buttons.Last())
                    keyboard2_Click(sender, e);
                else if (keyboard3 == buttons.Last())
                    keyboard3_Click(sender, e);
                else if (keyboard4 == buttons.Last())
                    keyboard4_Click(sender, e);
                else if (keyboard5 == buttons.Last())
                    keyboard5_Click(sender, e);
                else if (keyboard6 == buttons.Last())
                    keyboard6_Click(sender, e);
                else if (keyboard7 == buttons.Last())
                    keyboard7_Click(sender, e);
                else if (keyboard8 == buttons.Last())
                    keyboard8_Click(sender, e);
                else if (keyboard9 == buttons.Last())
                    keyboard9_Click(sender, e);
            }
        }

     
        /*
         * We check if the nineLetter word is valid or invalid
         * in the fifth round we use actual nine letter words which are shuffled
         * we use a bouncing bunny which tells you if your word is invalid
         * or stars to tell you that you've done a good job
         * also use another method done() for the other 4 rounds which will be explained
         * start the timer3
         */
        private void iAmDone_Click(object sender, EventArgs e)
        {
            iAmDone.Enabled = false;
            timer1.Stop();
            KeyPreview = false;
            pie = new Pie(100, 70);
            label14.Text = "00:00";
            shuffle.Enabled = false;
            
            if (round == 5)
            {
                lastW.Visible = false;
                Invalidate();
                StringBuilder sb = new StringBuilder();
                if (buttons.Count == 9)
                {
                    for (int i = 0; i < 9; i++)
                    {
                        sb.Append(buttons.ElementAt(i).Text);
                    }
                    if (correctNineLetterWord.Trim() == sb.ToString().Trim())
                    {
                        currentScore += 9;
                        //Тука додај го скорот
                        wordTxt.Text = sb.ToString();
                        scoreTxt.Text = currentScore.ToString();
                        valid.Text = "valid";
                        timer3.Start();
                    }
                    else
                    {
                        valid.Text = "invalid";
                      //  MessageBox.Show("The correct nine letter word is " + correctNineLetterWord);
                        bunnyPanel.Visible = true;
                        timer3.Start();
                    }
                }
                else
                {
                    valid.Text = "invalid";
                    bunnyPanel.Visible = true;
                    timer3.Start();
                }
                    iAmDone.Enabled = false;
                    start.Enabled = false;
                    done();
            }
            else
            {
                done();
            }
        }

        /*Check if the letter you've written is valid
         * if you finished the game, we add you as a new player
         * 
         */

        public void done()
        {
            currentScore = 0;
            word = "";
            if (buttons.Count != 0)
            {
                foreach (Button b in buttons)
                {
                    word += b.Text;
                }
                if (words.IsValid(word) && round <= 4)
                {
                    if (word.Length > bestWord.Length)
                        bestWord = word;
                    currentScore = word.Length;
                    totalScore += currentScore;
                    wordTxt.Text = bestWord;
                    valid.Text = "valid";
                    lblValidStars.Visible = true;
                    timer3.Start();
                    Invalidate();
                }
                else if (words.IsValid(word) && word.Length == 9 && round == 5)
                {
                    if (word.Length > bestWord.Length)
                        bestWord = word;
                    currentScore = word.Length;
                    totalScore += currentScore;
                    wordTxt.Text = bestWord;
                    valid.Text = "valid";
                    lblValidStars.Visible = true;
                    timer3.Start();
                    Invalidate();
                }
                else
                {
                    valid.Text = "invalid";
                    bunnyPanel.Visible = true;
                    timer3.Start();

                }
            }
            timer1.Stop();
            watch = 30;
            if (round <= 4)
            {
                nextRound.Enabled = true;
            }
            else
            {
                Player player = new Player(name, totalScore, bestWord);
              //    MessageBox.Show(player.ToString());
                bool f = false;
                int i = 0;
                if (players.Count != 0)
                    foreach (Player p in players)
                    {
                        if (p.Name == name)
                        {
                            f = true;
                            break;
                        }
                        i++;
                    }
                if (!f)
                {
                    players.Add(player);
                    WriteNewBestplayer(player);
                }
                else
                {
                    if (players.ElementAt(i).Score < totalScore)
                        players.ElementAt(i).Score = totalScore;
                    if (players.ElementAt(i).bestWord.Length < bestWord.Length)
                        players.ElementAt(i).bestWord = bestWord;
                    WriteNewBestplayer(players.ElementAt(i));
                }
                bestWord = "";
            }
            scoreTxt.Text = totalScore.ToString();
        }

        /*Here we reset everything
         * Make the second panel visible so the new player can choose nine letters for the first round
         * Stop the first timer
         * Stop the third timer if needed
         * use the whenNextRound method for seting the answer buttons on the original location
         */
        public void NewGame()
        {

            btnRandom.Enabled = true;
            lastW.Visible = false;
            iAmDone.Enabled = true;
            timer1.Stop();
            pie = new Pie(100, 70);
            scoreTxt.Text = "0";
            valid.Text = "";
            totalScore = 0;
            ChangeLettersPerRound = false;
            round = 0;
            watch = 30;
            label13.Text = "1/5";
            label14.Text = "00:00";
            nextRound.Enabled = true;
            bestWord = "";
            wordTxt.Text = bestWord;
            foreach (Button b in randomButtons)
            {
                b.Visible = false;
            }
            shuffled = new List<string>();
            randomLetters = new RandomLetters(keyboard);
            panel1.Visible = true;
            button1.Enabled = true;
            button2.Enabled = true;
            whenNextRound();

            timer3.Stop();
            sec = 8;
            lblValidStars.Visible = false;
            invalidL();

            foreach (Point p in answerFix)
            {
                show(p);
            }
            answer = new LinkedList<Point>();
            foreach (Point p in answerFix)
            {
                answer.AddLast(p);
            }
            foreach (Button b in buttons)
                b.Enabled = true;
            buttons = new List<Button>();
        }

        //Use NewGame() plus make some changes for the second panel where you choose the letters
        private void newGame_Click(object sender, EventArgs e)
        {
            tbName.Clear();
            tbName.Visible = true;
            lblName.Visible = true;
            wordTxt.Text = "";
            broiRandom = 0;
           
            NewGame();
            start.Enabled = false;
            f = true;
        }

//Similar to newGame_Click
        private void playAgain_Click(object sender, EventArgs e)
        {
            lblName.Visible = false;
            tbName.Visible = false;
            ChangeLettersPerRound = false;
            broiRandom = 0;
            start.Enabled = false;
            lblValidStars.Visible = false;
            NewGame();
        }

        //Here we show the players, their scores and the best word they made
        private void bestPlayers_Click(object sender, EventArgs e)
        {
            GameScore gameScore = new GameScore();
            bestPlayersString = new StringBuilder();
            int i = 1;
            players = ReadAllPlayers().OrderByDescending(x => x.Score).ToList();
            foreach (Player p in players)
            {
                bestPlayersString.Append(i + ". " + p.ToString() + "\n");
                i++;
            }
            gameScore.AddScores(bestPlayersString.ToString());
            gameScore.ShowDialog();
        }

        //Use shuffleButtons to shuffle the buttons
        private void shuffle_Click(object sender, EventArgs e)
        {
            if (buttons.Count == 0)
                shuffleButtons();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
             
            e.Handled = false;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Left || keyData == Keys.Right || keyData == Keys.Up || keyData == Keys.Down)
            {
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        //Generate nine Random letters in the second label

        private void btnRandom_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            shuffled = new List<string>();
            randomLetters.fillRandom(panel1,shuffled);
            for (int i = 0; i < 9; i++ )
            {
                randomButtons[i].Text = shuffled[i];
                randomButtons[i].Font = new Font("SketchFlow Print", 14, FontStyle.Bold);
               randomButtons[i].Visible = true;
            }
            start.Enabled = true; 
        }
        //button1 is used to generate a random consonant
        private void button1_Click(object sender, EventArgs e)
        {
            btnRandom.Enabled = false;
            if (broiRandom < 9)
            {
                randomButtons.ElementAt(broiRandom).Visible = true;
                string c = randomLetters.getCons().ToString().ToUpper();
                randomButtons.ElementAt(broiRandom).Text = c;
                randomButtons.ElementAt(broiRandom).Font = new Font("SketchFlow Print", 14, FontStyle.Bold);
                broiRandom++;
            }

            if (broiRandom == 9)
            {
                for (int i = 0; i < randomButtons.Count; i++)
                {
                    randomLetters.keyboard.ElementAt(i).Text = randomButtons.ElementAt(i).Text.ToUpper();
                    shuffled.Add(randomLetters.keyboard.ElementAt(i).Text);
                }
                button2.Enabled = false;
                button1.Enabled = false;
                start.Enabled = true;
            }
        }

        //button2 is used to generate a random vowel
        private void button2_Click(object sender, EventArgs e)
        {
            btnRandom.Enabled = false;
            if (broiRandom < 9)
            {
                randomButtons.ElementAt(broiRandom).Visible = true;
                string c = randomLetters.getVowel().ToString().ToUpper();
                randomButtons.ElementAt(broiRandom).Text = c;
                randomButtons.ElementAt(broiRandom).Font = new Font("SketchFlow Print", 14, FontStyle.Bold);
                broiRandom++;
            }

            if (broiRandom == 9)
            {
                for (int i = 0; i < randomButtons.Count; i++)
                {
                    randomLetters.keyboard.ElementAt(i).Text = randomButtons.ElementAt(i).Text.ToUpper();
                    shuffled.Add(randomLetters.keyboard.ElementAt(i).Text);
                }
                button2.Enabled = false;
                button1.Enabled = false;
                start.Enabled = true;
            }
        }


        //Start(Play) is on the second panel when we want to start with the game
        private void start_Click(object sender, EventArgs e)
        {
            if (ValidateChildren())
            {
                panel1.Visible = false;
                nextRoundMethod();
                plName.Text = name;
                start.Enabled = false;
                timer1.Start();
            }
        }

        private void tbName_Enter(object sender, EventArgs e)
        {
            KeyPreview = false;
        }

        private void tbName_Leave(object sender, EventArgs e)
        {
            if (f)
            {
                name = tbName.Text;
                f = false;
            }

            KeyPreview = true;
            
        }


        //the on/off button on the main label if you're not in the mood for music, just click the button :)
        private void music_Click(object sender, EventArgs e)
        {
            if (music.Text == "ON")
            {
                music.Text = "OFF";
                song.playSong();
            }
            else
            {
                music.Text = "ON";
                song.stopSong();
            }
        }

        private void btnHowToPlay_Click(object sender, EventArgs e)
        {
            Instruction f2 = new Instruction();
            f2.Show();
        }

        private void btnPlayGame_Click(object sender, EventArgs e)
        {
            menu.Visible = false;
            timer2.Stop();
            f = true;
        }

        private void btnQuitGame_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Quit?", "Quit?", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                this.Close();
            }
        }

        //timer for the bouncing letters on the menu panel

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (down)
            {
                foreach (Label l in titleEven)
                {
                    Point p = l.Location;
                    p = new Point(p.X, p.Y + 5);
                    l.Location = p;
                }
                foreach (Label l in titleOdd)
                {
                    Point p = l.Location;
                    p = new Point(p.X, p.Y - 5);
                    l.Location = p;
                }
            }
            else
            {
                foreach (Label l in titleEven)
                {
                    Point p = l.Location;
                    p = new Point(p.X, p.Y - 5);
                    l.Location = p;
                }
                foreach (Label l in titleOdd)
                {
                    Point p = l.Location;
                    p = new Point(p.X, p.Y + 5);
                    l.Location = p;
                }
            }
            down = !down;
        }

        private void btnHowToPlay_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void btnHowToPlay_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void btnPlayGame_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void btnPlayGame_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void btnQuitGame_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void btnQuitGame_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        
        private void tbName_Validating(object sender, CancelEventArgs e)
        {
            if (tbName.Text.Trim() == "")
            {
                errorProvider.SetError(tbName,"Enter name!");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(tbName, null);
            }
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void btnRandom_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void btnRandom_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }


        //button for the music on the menu panel
        private void btnMusic_Click_1(object sender, EventArgs e)
        {
            if (play)
            {
                bmp = new Bitmap(Resources.unmute, new Size(btnMusic.Width - 5, btnMusic.Height));
                bmp.MakeTransparent();
                btnMusic.BackgroundImage = bmp;
                song.stopSong();
            }
            else
            {
                bmp = new Bitmap(Resources.mute, btnMusic.Size);
                bmp.MakeTransparent();
                btnMusic.BackgroundImage = bmp;
                song.playSong();
            }
            play = !play;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Location = new Point(250,170);
        }

        private void start_MouseHover(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void start_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Arrow;
        }

        //this is the button "Back" to go back to the menu if you want to read the instructions
        private void button3_Click(object sender, EventArgs e)
        {
            menu.Visible = true;
            timer2.Start();
            timer1.Stop();
            lblName.Visible = true;
            tbName.Visible = true;
            tbName.Clear();
            start.Enabled = false;
            button1.Enabled = true;
            button2.Enabled = true;
            for (int i = 0; i < randomButtons.Count; i++)
            {
                randomButtons[i].Visible = false;
            }
            broiRandom = 0;
        }

        private void btnBack_MouseHover(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void btnBack_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Arrow;
        }

        //The third timer is used for the bunny or the stars
        //for bunny to hop, or the stars to shine
        private void timer3_Tick(object sender, EventArgs e)
        {
            if (valid.Text == "valid")
            {
                if (sec > 0)
                {
                    lblValidStars.Visible = !lblValidStars.Visible;
                    tick = !tick;
                    Invalidate();
                    sec--;
                }
                else
                {
                    timer3.Stop();
                    sec = 8;
                    lblValidStars.Visible = false;
                  
                }
            }
            else
            {
                Label l = null;
                if (invalidLabels.Count > 0)
                {
                    l = invalidLabels.ElementAt(invalidLabels.Count - 1);
                    l.Visible = true;
                    invalidLabels.Remove(invalidLabels.ElementAt(invalidLabels.Count - 1));
                    if (invalidLabels.Count != 0)
                    {
                        l = invalidLabels.ElementAt(invalidLabels.Count - 1);
                        pictureBox3.Location = new Point(l.Location.X, l.Location.Y - 15);
                    }
                    else
                    {
                        pictureBox3.Location = new Point(pictureBox3.Location.X - pictureBox3.Size.Width, pictureBox3.Location.Y);

                        if (round == 5)
                        {
                            MessageBox.Show("The correct nine letter word is " + correctNineLetterWord);
                        }
                    }
                }
                else
                {

                    pictureBox3.Location = new Point(pictureBox3.Location.X - pictureBox3.Size.Width, pictureBox3.Location.Y);

                    //invalidL();
                }

            }
        }

        //reset the INVALID letters that the bunny reveals
        public void invalidL()
        {
            invalidLabels = new List<Label>();
            invalidLabels.Add(label1);
            invalidLabels.Add(label2);
            invalidLabels.Add(label3);
            invalidLabels.Add(label4);
            invalidLabels.Add(label5);
            invalidLabels.Add(label6);
            invalidLabels.Add(label7);

            foreach (Label l in invalidLabels)
                l.Visible = false;

            bunnyPanel.Visible = false;
            pictureBox3.Location = label7.Location;

        }

        //// read write functions
        void WriteNewBestplayer(Player p)
        {
            // Write sample data to CSV file
            using (CsvFileWriter writer = new CsvFileWriter("Scores.csv", true))
            {

                CsvRow row = new CsvRow();
                row.Add(p.Name);
                row.Add(p.Score.ToString());
                row.Add(p.bestWord);
                writer.WriteRow(row);
            }
        }

        List<Player> ReadAllPlayers()
        {
            // Read sample data from CSV file
            using (CsvFileReader reader = new CsvFileReader("Scores.csv", true))
            {
                CsvRow row = new CsvRow();
                List<Player> lstPlayers = new List<Player>();
                while (reader.ReadRow(row))
                {
                    int br = 0;
                    String name = "";
                    String word = "";
                    int score = 0;
                    foreach (string s in row)
                    {
                        if (br == 0)
                            name = s;
                        else if (br == 1)
                            score = int.Parse(s);
                        else
                            word = s;
                        br++;
                    }
                    Player p = new Player(name, score, word);
                    lstPlayers.Add(p);
                }
                return lstPlayers;
            }
        }

        private void menu_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
