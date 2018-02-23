using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Brain_Teaser
{
    public partial class Form1 : Form
    {
        string path = null;
        Timer myTimer1 = new Timer();
        int rn, idx=0, word_no = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            Size size = TextRenderer.MeasureText(textBox3.Text, textBox3.Font);
            textBox3.Width = size.Width;
            textBox3.Height = size.Height;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            idx = idx + 1;
            load_puzzle(idx);
        }

        public void load_puzzle(int idx)
        {
            switch (idx)
            {
                case 1:
                    textBox2.Text = "1";
                    textBox3.AppendText("Replace the missing vowels to give a well-known proverb. What is it?");
                    textBox3.AppendText(Environment.NewLine);
                    textBox3.AppendText(Environment.NewLine);
                    textBox3.AppendText("T F S G H T, T F M N D");
                    
                    break;
                case 2:
                    textBox2.Text = "2";
                    textBox3.Clear();
                    textBox3.AppendText("Replace each group of dashes in the following sentence with a five letter word.");
                    textBox3.AppendText(Environment.NewLine);
                    textBox3.AppendText("The same five letters must be used for both words. What are the words ?");
                    textBox3.AppendText(Environment.NewLine);
                    textBox3.AppendText(Environment.NewLine);
                    textBox3.AppendText("The supporters in the _ _ _ _ _ stand at the sports event always _ _ _ _ _ the loudest.?");
                    textBox3.AppendText(Environment.NewLine);
                   
                    break;
                case 3:
                    textBox2.Text = "3";
                    textBox3.Clear();
                    textBox3.AppendText("Rearrange the following letters to give one word. All eight letters must be used.");
                    textBox3.AppendText(Environment.NewLine);
                    textBox3.AppendText("What is the word?");
                    textBox3.AppendText(Environment.NewLine);
                    textBox3.AppendText(Environment.NewLine);
                    textBox3.AppendText("A A B E E L M N");
                    textBox3.AppendText(Environment.NewLine);
                    break;
                case 4:
                    textBox2.Text = "4";
                    textBox3.Clear();
                    textBox3.AppendText("The alphabet is written here but some letters are missing. Arrange the missing letters to give a word.");
                    textBox3.AppendText(Environment.NewLine);
                    textBox3.AppendText("What is it?");
                    textBox3.AppendText(Environment.NewLine);
                    textBox3.AppendText(Environment.NewLine);
                    textBox3.AppendText("A   B   C   F   H   J   K   M   O   P   Q   R   T   V   W   X   Y   Z");
                    textBox3.AppendText(Environment.NewLine);
                    break;
                case 5:
                    textBox2.Text = "5";
                    textBox3.Clear();
                    textBox3.AppendText("Which three letters can be attached to the beginning of all the following words to give six longer words?");
                    //textBox3.AppendText(Environment.NewLine);
                    //textBox3.AppendText("What is it?");
                    textBox3.AppendText(Environment.NewLine);
                    textBox3.AppendText(Environment.NewLine);
                    textBox3.AppendText("ANGLE  RAP   IRE   WINE   ICE");
                    textBox3.AppendText(Environment.NewLine);
                    break;
                case 6:
                    textBox2.Text = "6";
                    textBox3.Clear();
                    textBox3.AppendText("Provide a three-letter word that can be attached to the end of the given words to form five longer words.");
                    textBox3.AppendText(Environment.NewLine);
                    textBox3.AppendText("What is the word?");
                    textBox3.AppendText(Environment.NewLine);
                    textBox3.AppendText(Environment.NewLine);
                    textBox3.AppendText("CROSS DRAW HANDLE SAND CROW");
                    textBox3.AppendText(Environment.NewLine);
                    break;
                case 7:
                    textBox2.Text = "7";
                    textBox3.Clear();
                    textBox3.AppendText("Rearrange the following letters to give one word. All eight letters must be used.");
                    textBox3.AppendText(Environment.NewLine);
                    textBox3.AppendText("What is the word?");
                    textBox3.AppendText(Environment.NewLine);
                    textBox3.AppendText(Environment.NewLine);
                    textBox3.AppendText("C  G  I  M  N  N  O  O");
                    textBox3.AppendText(Environment.NewLine);
                    break;
                case 8:
                    textBox2.Text = "8";
                    textBox3.Clear();
                    textBox3.AppendText("Insert the same two letters into the exact centre of each of the following words, so that five longer words can be read.");
                    textBox3.AppendText(Environment.NewLine);
                    textBox3.AppendText("What are they?");
                    textBox3.AppendText(Environment.NewLine);
                    textBox3.AppendText(Environment.NewLine);
                    textBox3.AppendText("MALE    CUED    HEAL    GALE    VEAL");
                    textBox3.AppendText(Environment.NewLine);
                    break;
                case 9:
                    textBox2.Text = "9";
                    textBox3.Clear();
                    textBox3.AppendText("Rearrange the letters below to give three words. All seven letters must be used in each word.");
                    textBox3.AppendText(Environment.NewLine);
                    textBox3.AppendText("What are the three words?");
                    textBox3.AppendText(Environment.NewLine);
                    textBox3.AppendText(Environment.NewLine);
                    textBox3.AppendText("A E I M N R S");
                    textBox3.AppendText(Environment.NewLine);
                    break;
                default:
                    break;
            }
        }
    }
}
