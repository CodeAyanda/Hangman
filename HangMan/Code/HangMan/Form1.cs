using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace HangMan
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        static string filePath = @"words.txt";
        string wordToGuess = getCurrentWord();
        string secretWord = "";
        string secretWordAct = "";
        string currentDisplayedWord = "";
        int numberOfAttempts = 0;

        public static string getCurrentWord()
        {
            Random rnd = new Random();
            string[] textFromFile = File.ReadAllLines(filePath);
            List<string> wordss = new List<string>();
            foreach (string item in textFromFile)
            {
                wordss.Add(item);
            }
            int index = rnd.Next(wordss.Count);
            string word = wordss[index];


            return word.ToUpper();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile("images/Cover.png");
            panel1.Visible = false;


            for (int i = 0; i < wordToGuess.Length; i++)
            {
                secretWord = secretWord + "_ ";
                secretWordAct = placeHolders(secretWord);

            }
            label1.Text = secretWord;
            currentDisplayedWord = secretWordAct;
        }

        public string placeHolders(string str)
        {
            string WordActually = "";
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == ' ')
                {
                }
                else
                {
                    WordActually += '_';
                }

            }

            return WordActually;
        }

        public string placeHoldersInsert(string str)
        {
            char[] strAsArr = str.ToCharArray();
            string wordWithPlaceHolders = "";
            for (int i = 0; i < strAsArr.Length; i++)
            {
                wordWithPlaceHolders += (strAsArr[i] + " ");

            }

            return wordWithPlaceHolders;

        }

        List<string> guessedChars = new List<string>();


        private void button1_Click(object sender, EventArgs e)
        {
            string guessChar = textBox2.Text.ToUpper();

            if(textBox2.Text.Length == 0 || textBox2.Text.Length > 1)
            {
                label12.Visible = true;
            }
            if (guessedChars.Contains(guessChar) && guessChar != "")
            {
                label3.Visible = true;
                label5.Visible = false;

            }
            if (!wordToGuess.Contains(guessChar) && !guessedChars.Contains(guessChar) && guessChar != "")
            {
                label5.Visible = true;
                numberOfAttempts++;

                switch (numberOfAttempts)
                {
                    case 1:
                        pictureBox1.Image = Image.FromFile("images/OneAttempt.png");
                        break;
                    case 2:
                        pictureBox1.Image = Image.FromFile("images/TwoAttempts.png");
                        break;
                    case 3:
                        pictureBox1.Image = Image.FromFile("images/ThreeAttempts.png");
                        break;
                    case 4:
                        pictureBox1.Image = Image.FromFile("images/FourAttempts.png");
                        break;
                    case 5:
                        pictureBox1.Image = Image.FromFile("images/FiveAttempts.png");
                        break;
                    case 6:
                        pictureBox1.Image = Image.FromFile("images/SixAttempts.png");
                        panel1.Visible = false;
                        label6.Visible = true;
                        label7.Visible = true;
                        label8.Visible = true;
                        label8.Text = wordToGuess;
                        label10.Visible = true;
                        button3.Visible = true;
                        break;
                }


            }


            if (wordToGuess.Contains(guessChar) && guessChar != "" && guessChar.Length ==1)
            {
                checkEntered(char.Parse(guessChar));
                label5.Visible = false;

            }

            while (!guessedChars.Contains(guessChar) && guessChar != "")
            {
                label3.Visible = false;
                guessedChars.Add(guessChar);

            }

            foreach (string item in guessedChars)
            {
                string combinedString = string.Join(", ", guessedChars);
                label9.Text = combinedString;
                label9.Visible = true;
            }

            while (guessedChars.Count == 0)
            {
                label9.Visible = false;
            }

            textBox2.Text = String.Empty;
            textBox2.Focus();

        }

        public void checkEntered(char str)
        {
            char enteredChar = str;
            string newSecretWord = secretWordAct;

            List<int> foundIndex = new List<int>();

            char[] array = wordToGuess.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == str)
                {
                    foundIndex.Add(i);
                }
            }

            char[] arrayy = newSecretWord.ToCharArray();
            foreach (int item in foundIndex)
            {
                arrayy[item] = enteredChar;

            }
            newSecretWord = new string(arrayy);
            currentDisplayedWord = addToCurrentWord(currentDisplayedWord, newSecretWord);

            if (!currentDisplayedWord.Contains("_"))
            {
                panel1.Visible = false;
                label6.Text = "YOU WON!!!";
                label6.Visible = true;
                label7.Visible = true;
                label8.Visible = true;
                label8.Text = wordToGuess;
                label11.Visible = true;
                button3.Visible = true;
            }
            label1.Text = placeHoldersInsert(currentDisplayedWord);
        }

        public string addToCurrentWord(string str, string str2)
        {
            string temp = "";
            char[] tempp = str.ToCharArray();
            char[] arr2 = str2.ToCharArray();
            for (int i = 0; i < tempp.Length; i++)
            {
                for (int j = 0; j < arr2.Length; j++)
                {
                    if (arr2[j] != '_' && i == j)
                    {
                        tempp[i] = arr2[j];
                    }
                }
            }
            temp = new string(tempp);
            return temp;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            label12.Visible = true;
            button2.Visible = false;
            panel1.Visible = true;

            pictureBox1.Image = Image.FromFile("images/ZeroAttempts.png");
            textBox2.Focus();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Restart();
            Environment.Exit(0);
            button3.Visible = false;
        }
    }
}