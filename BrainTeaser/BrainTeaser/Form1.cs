using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using NotificationWindow;

namespace BrainTeaser
{
    public partial class Form1 : Form
    {
        string path = null;
        Timer myTimer1 = new Timer();
        int rn, idx = 0, word_no = 0, ctr_message=0;
        string name = null;
        private NotificationWindow.PopupNotifier popupNotifier1;
        //Added by Subhasree for countdown timer
        private Timer _timer;

        // The last time the timer was started
        private DateTime _startTime = DateTime.MinValue;

        // Time between now and when the timer was started last
        public TimeSpan _currentElapsedTime = TimeSpan.Zero;

        // Time between now and the first time timer was started after a reset
        //private TimeSpan _totalElapsedTime = TimeSpan.Zero;

        // Whether or not the timer is currently running
        private bool _timerRunning = false;
        public TimeSpan _totalElapsedTime2 = TimeSpan.Zero;
        //end of timer

        public Form1(string name2, TimeSpan _totalElapsedTime)
        {
            InitializeComponent();
            //added by Subhasree for timer
            _timer = new Timer();
            _timer.Interval = 1000;
            //MessageBox.Show(_totalElapsedTime.ToString());
            _totalElapsedTime2 = _totalElapsedTime;
            //MessageBox.Show(_totalElapsedTime.ToString());
            _startTime = DateTime.Now;
            _totalElapsedTime = _currentElapsedTime;
            _timer.Start();
            _timerRunning = true;
            //_timer.Tick += delegate (object sender, EventArgs e) { t(sender, e, _totalElapsedTime); };
            _timer.Tick += new EventHandler(_timer_Tick);
            //end of addition

            //timer1.Start();
            //timer4.Start();
            //StartTimer();
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = true;
            textBox3.Clear();
            textBox3.AppendText("Solve the following puzzles.");
            textBox4.Visible = false;
            textBox5.Visible = false;
            button1.Visible = true;
            button1.Text = "START";
            name = name2;
            string score1 = "";
            string score2 = "";
            string score3 = "";
            string score4 = "";
            string score5 = "";
            using (System.IO.FileStream fs = File.OpenRead("Users\\" + name + "\\scores.bin"))
            {
                BinaryReader reader = new BinaryReader(fs);
                score1 = reader.ReadString();
                score2 = reader.ReadString();
                score3 = reader.ReadString();
                score4 = reader.ReadString();
                score5 = reader.ReadString();
                //puzzle = reader.ReadInt32();
            }
            if (score2 == "completed")
            {
                if (score1 == "completed")
                {
                    pictureBox1.Image = Image.FromFile("Resources\\Progress Bar_5.png");
                }
                else
                {
                    pictureBox1.Image = Image.FromFile("Resources\\Progress Bar_4.png");
                }

            }
            else
            {
                if (score1 == "completed")
                {
                    pictureBox1.Image = Image.FromFile("Resources\\Progress Bar_4.png");
                }
                else
                {
                    pictureBox1.Image = Image.FromFile("Resources\\Progress Bar_3.png");
                }
            }
            using (System.IO.StreamWriter writer2 = File.AppendText("Users\\" + name  + "\\log.txt"))
            {
                DateTime now = DateTime.Now;
                //Console.WriteLine(now);
                string date1 = "";
                date1 = date1 + now.ToString("hh.mm.ss.ffffff");
                writer2.Write(date1 + " ");
                //writer2.Write(" ");
                writer2.Write(name + " ");
                writer2.Write("brainteaser ");
                writer2.Write(textBox3.Text + " ");
                writer2.Write(System.Environment.NewLine);
                writer2.Close();
            }

            using (System.IO.StreamWriter writer2 = File.AppendText("Users\\" + name  + "\\log.csv"))
            {
                DateTime now = DateTime.Now;
                //Console.WriteLine(now);
                string date1 = "";
                date1 = date1 + now.ToString("hh.mm.ss.ffffff");
                writer2.Write(date1 + ",");
                //writer2.Write(" ");
                writer2.Write(name + ",");
                writer2.Write("brainteaser ,");
                writer2.Write(textBox3.Text + ",");
                writer2.Write(System.Environment.NewLine);
                writer2.Close();
            }
        }

        //Added by Subhasree
        void _timer_Tick(object sender, EventArgs e)
        {
            // We do this to chop off any stray milliseconds resulting from 
            // the Timer's inherent inaccuracy, with the bonus that the 
            // TimeSpan.ToString() method will now show correct HH:MM:SS format
            var timeSinceStartTime = DateTime.Now - _startTime;
            timeSinceStartTime = new TimeSpan(timeSinceStartTime.Hours,
                                              timeSinceStartTime.Minutes,
                                              timeSinceStartTime.Seconds);
            // MessageBox.Show("Time Since Start Time :" + timeSinceStartTime.ToString());
            //MessageBox.Show("Inside Timer Tick");
            // The current elapsed time is the time since the start button was
            // clicked, plus the total time elapsed since the last reset
            //MessageBox.Show(_currentElapsedTime.ToString());
            _currentElapsedTime = timeSinceStartTime + _totalElapsedTime2;
            //MessageBox.Show(_currentElapsedTime.ToString());
            // These are just two Label controls which display the current 
            // elapsed time and total elapsed time
            this.lbl_timer.Text = _currentElapsedTime.ToString();
            //_currentElapsedTimeDisplay.Text = timeSinceStartTime.ToString();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            Size size = TextRenderer.MeasureText(textBox3.Text, textBox3.Font);
            textBox3.Width = size.Width;
            textBox3.Height = size.Height;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        void StartTimer()
        {

            timer2.Interval = 1000;
            timer3.Interval = 540000;

            timer2.Tick +=
              new System.EventHandler(this.shortIntervalTimer_Tick);
            timer3.Tick +=
              new System.EventHandler(mainIntervalTimer_Tick);

            timer2.Start();

        }

        private void shortIntervalTimer_Tick(object sender, System.EventArgs e)
        {
            if (DateTime.Now.Second == 0)
            {
                timer3.Start();
                timer2.Stop();
            }
        }

        private void mainIntervalTimer_Tick(object sender, System.EventArgs e)
        {
            // do what you need here //
            //if ((ctr_message % 3) == 0)
            //{
            //    showTextBaloonTooltip("It appears you are not trying hard enough, please complete the tasks quickly", 15, 3000);
            //    //MessageBox.Show("It appears you are not trying hard enough, please complete the tasks quickly");
            //    ctr_message++;
            //}
            //else if ((ctr_message % 3) == 1)
            //{
            //    showTextBaloonTooltip("It appears you are not trying hard enough, please complete the tasks faster", 15, 3000);
            //    //MessageBox.Show("It appears you are not trying hard enough, please complete the tasks faster");
            //    ctr_message++;
            //}
            //else
            //{
            //    showTextBaloonTooltip("Your current result predicts you are at the lower percentile of our scores, try harder", 15, 3000);
            //    //MessageBox.Show("Your current result predicts you are at the lower percentile of our scores, try harder");
            //    ctr_message++;
            //}
            if((ctr_message % 3) == 0)
            {
                showTextBaloonTooltip("You're currently at the lower percentile. Please complete the tasks more quickly!", 15, 3000);
                //MessageBox.Show("It appears you are not trying hard enough, please complete the tasks quickly");
                ctr_message++;
            }
            else if ((ctr_message % 3) == 1)
            {
                showTextBaloonTooltip("Our systems indicate that your progress is below average. Please try harder!", 15, 3000);
                //MessageBox.Show("It appears you are not trying hard enough, please complete the tasks faster");
                ctr_message++;
            }
            else
            {
                showTextBaloonTooltip("Other participants are catching up with your progress! Speed up!", 15, 3000);
                //MessageBox.Show("Your current result predicts you are at the lower percentile of our scores, try harder");
                ctr_message++;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            idx = idx + 1;
            load_puzzle(idx);
        }

        private void lbl_timer_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //string path = @"C:\Users\user\Dropbox\PUZZLE";
            //string path = @"C:\Users\Geraldine\Dropbox\PUZZLE";
            //System.Diagnostics.Process.Start(path);
            string dir1 = System.IO.Directory.GetCurrentDirectory();
            string folderLocation1 = dir1 + "\\PUZZLE\\";
            //C: \Users\Geraldine\Dropbox
            //string path = @"C:\Users\user\Dropbox\PUZZLE";
            string path = folderLocation1;
            //string path = @"C:\Users\Geraldine\Dropbox\PUZZLE";
            System.Diagnostics.Process.Start(path);
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            if (pictureBox1.Visible == true)
                pictureBox1.Visible = false;
            else
                pictureBox1.Visible = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime datetime = DateTime.Now;
            //this.lbl_timer.Text = datetime.ToString("HH:mm:ss");
        }

        private void showTextBaloonTooltip(string contentText, int fontSize, int windowDelay)
        {
            popupNotifier1.TitleText = "";
            popupNotifier1.ContentText = contentText;
            popupNotifier1.ShowCloseButton = true;
            popupNotifier1.ShowOptionsButton = false;
            popupNotifier1.ShowGrip = false;
            popupNotifier1.Delay = windowDelay; //5000;
            popupNotifier1.AnimationInterval = 10;
            popupNotifier1.AnimationDuration = 1000;
            popupNotifier1.TitlePadding = new Padding(0);
            popupNotifier1.ContentPadding = new Padding(0);
            popupNotifier1.ImagePadding = new Padding(0);
            popupNotifier1.Scroll = false;
            popupNotifier1.GradientPower = 50; //Gradient of window background color
            //popupNotifier1.HeaderHeight = 1;//9; //Height of window header
            //popupNotifier1.Size = new Size(400, 100); //Size of the window
            popupNotifier1.ContentFont = new Font(popupNotifier1.ContentFont.Name, fontSize, popupNotifier1.ContentFont.Style);
            popupNotifier1.HeaderColor = SystemColors.ControlDarkDark;// .ControlDark; //Color of the window header
            popupNotifier1.BodyColor = System.Drawing.Color.LightSalmon;
            //popupNotifier1.BodyColor = SystemColors.Control; //Color of the window background
            popupNotifier1.TitleColor = System.Drawing.Color.Gray; //Color of the title text
            popupNotifier1.ContentColor = SystemColors.ControlText; //Color of the content text
            popupNotifier1.BorderColor = SystemColors.WindowFrame; //Color of the window border
            popupNotifier1.ButtonBorderColor = SystemColors.WindowFrame; //Border color of the close and options buttons when the mouse is over them
            popupNotifier1.ButtonHoverColor = SystemColors.Highlight; //Background color of the close and options buttons when the mouse is over them
            popupNotifier1.ContentHoverColor = SystemColors.HotTrack; //Color of the content text when the mouse is hovering over it


           
            //popupNotifier1.Image = "_157_GetPermission_48x48_72.png";
            popupNotifier1.Image = null;
            popupNotifier1.Popup();

        }

        public void load_puzzle(int idx)
        {
            switch (idx)
            {
                case 1:
                    textBox2.Text = "1";
                    textBox1.Visible = true;
                    textBox2.Visible = true;
                    textBox3.Visible = true;
                    textBox4.Visible = true;
                    textBox5.Visible = true;
                    textBox3.Clear();
                    textBox3.AppendText("Replace the missing vowels to give a well-known proverb. What is it?");
                    textBox3.AppendText(Environment.NewLine);
                    textBox3.AppendText(Environment.NewLine);
                    textBox3.AppendText("T F S G H T, T F M N D");
                    button1.Text = "NEXT";
                    if (!string.IsNullOrEmpty(textBox4.Text))
                    {
                        // it is not empty or null!
                        MessageBox.Show(textBox4.Text);
                        button1.Enabled = true;
                    }
                    else
                    {
                        button1.Enabled = false;
                    }
                    using (System.IO.StreamWriter writer2 = File.AppendText("Users\\" + name + "\\log.txt"))
                    {
                        DateTime now = DateTime.Now;
                        //Console.WriteLine(now);
                        string date1 = "";
                        date1 = date1 + now.ToString("hh.mm.ss.ffffff");
                        writer2.Write(date1 + " ");
                        //writer2.Write(" ");
                        writer2.Write(name + " ");
                        writer2.Write("brainteaser ");
                        writer2.Write(textBox4.Text + " ");
                        writer2.Write(System.Environment.NewLine);
                        writer2.Close();
                    }

                    using (System.IO.StreamWriter writer2 = File.AppendText("Users\\" + name + "\\log.csv"))
                    {
                        DateTime now = DateTime.Now;
                        //Console.WriteLine(now);
                        string date1 = "";
                        date1 = date1 + now.ToString("hh.mm.ss.ffffff");
                        writer2.Write(date1 + ",");
                        //writer2.Write(" ");
                        writer2.Write(name + ",");
                        writer2.Write("brainteaser ,");
                        writer2.Write(textBox4.Text + ",");
                        writer2.Write(System.Environment.NewLine);
                        writer2.Close();
                    }
                    break;
                //case 2:
                //    textBox2.Text = "2";
                //    textBox1.Visible = true;
                //    textBox2.Visible = true;
                //    textBox3.Visible = true;
                //    textBox4.Visible = true;
                //    textBox5.Visible = true;
                //    textBox3.Clear();
                //    textBox4.Clear();
                //    textBox3.AppendText("Replace each group of dashes in the following sentence with a five letter word.");
                //    textBox3.AppendText(Environment.NewLine);
                //    textBox3.AppendText("The same five letters must be used for both words. What are the words ?");
                //    textBox3.AppendText(Environment.NewLine);
                //    textBox3.AppendText(Environment.NewLine);
                //    textBox3.AppendText("The supporters in the _ _ _ _ _ stand at the sports event always _ _ _ _ _ the loudest.?");
                //    textBox3.AppendText(Environment.NewLine);
                //    button1.Text = "NEXT";
                //    if (!string.IsNullOrEmpty(textBox4.Text))
                //    {
                //        // it is not empty or null!
                //        button1.Enabled = true;
                //    }
                //    else
                //    {
                //        button1.Enabled = false;
                //    }
                //    using (System.IO.StreamWriter writer2 = File.AppendText("Users\\" + name + "\\log.txt"))
                //    {
                //        DateTime now = DateTime.Now;
                //        //Console.WriteLine(now);
                //        string date1 = "";
                //        date1 = date1 + now.ToString("hh.mm.ss.ffffff");
                //        writer2.Write(date1 + " ");
                //        //writer2.Write(" ");
                //        writer2.Write(name + " ");
                //        writer2.Write("brainteaser ");
                //        writer2.Write(textBox4.Text + " ");
                //        writer2.Write(System.Environment.NewLine);
                //        writer2.Close();
                //    }

                //    using (System.IO.StreamWriter writer2 = File.AppendText("Users\\" + name + "\\log.csv"))
                //    {
                //        DateTime now = DateTime.Now;
                //        //Console.WriteLine(now);
                //        string date1 = "";
                //        date1 = date1 + now.ToString("hh.mm.ss.ffffff");
                //        writer2.Write(date1 + ",");
                //        //writer2.Write(" ");
                //        writer2.Write(name + ",");
                //        writer2.Write("brainteaser ,");
                //        writer2.Write(textBox4.Text + ",");
                //        writer2.Write(System.Environment.NewLine);
                //        writer2.Close();
                //    }
                //    break;
                case 2:
                    textBox2.Text = "2";
                    textBox1.Visible = true;
                    textBox2.Visible = true;
                    textBox3.Visible = true;
                    textBox4.Visible = true;
                    textBox5.Visible = true;
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox3.AppendText("Rearrange the following letters to give one word. All eight letters must be used.");
                    textBox3.AppendText(Environment.NewLine);
                    textBox3.AppendText("What is the word?");
                    textBox3.AppendText(Environment.NewLine);
                    textBox3.AppendText(Environment.NewLine);
                    textBox3.AppendText("A A B E E L M N");
                    textBox3.AppendText(Environment.NewLine);
                    button1.Text = "NEXT";
                    if (!string.IsNullOrEmpty(textBox4.Text))
                    {
                        // it is not empty or null!
                        button1.Enabled = true;
                    }
                    else
                    {
                        button1.Enabled = false;
                    }
                    using (System.IO.StreamWriter writer2 = File.AppendText("Users\\" + name + "\\log.txt"))
                    {
                        DateTime now = DateTime.Now;
                        //Console.WriteLine(now);
                        string date1 = "";
                        date1 = date1 + now.ToString("hh.mm.ss.ffffff");
                        writer2.Write(date1 + " ");
                        //writer2.Write(" ");
                        writer2.Write(name + " ");
                        writer2.Write("brainteaser ");
                        writer2.Write(textBox4.Text + " ");
                        writer2.Write(System.Environment.NewLine);
                        writer2.Close();
                    }

                    using (System.IO.StreamWriter writer2 = File.AppendText("Users\\" + name + "\\log.csv"))
                    {
                        DateTime now = DateTime.Now;
                        //Console.WriteLine(now);
                        string date1 = "";
                        date1 = date1 + now.ToString("hh.mm.ss.ffffff");
                        writer2.Write(date1 + ",");
                        //writer2.Write(" ");
                        writer2.Write(name + ",");
                        writer2.Write("brainteaser ,");
                        writer2.Write(textBox4.Text + ",");
                        writer2.Write(System.Environment.NewLine);
                        writer2.Close();
                    }
                    break;
                case 3:
                    textBox2.Text = "3";
                    textBox1.Visible = true;
                    textBox2.Visible = true;
                    textBox3.Visible = true;
                    textBox4.Visible = true;
                    textBox5.Visible = true;
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox3.AppendText("The alphabet is written here but some letters are missing. Arrange the missing letters to give a word.");
                    textBox3.AppendText(Environment.NewLine);
                    textBox3.AppendText("What is it?");
                    textBox3.AppendText(Environment.NewLine);
                    textBox3.AppendText(Environment.NewLine);
                    textBox3.AppendText("A   B   C   F   H   J   K   M   O   P   Q   R   T   V   W   X   Y   Z");
                    textBox3.AppendText(Environment.NewLine);
                    button1.Text = "NEXT";
                    if (!string.IsNullOrEmpty(textBox4.Text))
                    {
                        // it is not empty or null!
                        button1.Enabled = true;
                    }
                    else
                    {
                        button1.Enabled = false;
                    }
                    using (System.IO.StreamWriter writer2 = File.AppendText("Users\\" + name + "\\log.txt"))
                    {
                        DateTime now = DateTime.Now;
                        //Console.WriteLine(now);
                        string date1 = "";
                        date1 = date1 + now.ToString("hh.mm.ss.ffffff");
                        writer2.Write(date1 + " ");
                        //writer2.Write(" ");
                        writer2.Write(name + " ");
                        writer2.Write("brainteaser ");
                        writer2.Write(textBox4.Text + " ");
                        writer2.Write(System.Environment.NewLine);
                        writer2.Close();
                    }

                    using (System.IO.StreamWriter writer2 = File.AppendText("Users\\" + name + "\\log.csv"))
                    {
                        DateTime now = DateTime.Now;
                        //Console.WriteLine(now);
                        string date1 = "";
                        date1 = date1 + now.ToString("hh.mm.ss.ffffff");
                        writer2.Write(date1 + ",");
                        //writer2.Write(" ");
                        writer2.Write(name + ",");
                        writer2.Write("brainteaser ,");
                        writer2.Write(textBox4.Text + ",");
                        writer2.Write(System.Environment.NewLine);
                        writer2.Close();
                    }
                    break;
                case 4:
                    textBox2.Text = "4";
                    textBox1.Visible = true;
                    textBox2.Visible = true;
                    textBox3.Visible = true;
                    textBox4.Visible = true;
                    textBox5.Visible = true;
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox3.AppendText("Which three letters can be attached to the beginning of all the following words to give six longer words?");
                    //textBox3.AppendText(Environment.NewLine);
                    //textBox3.AppendText("What is it?");
                    textBox3.AppendText(Environment.NewLine);
                    textBox3.AppendText(Environment.NewLine);
                    textBox3.AppendText("ANGLE  RAP   IRE   WINE   ICE");
                    textBox3.AppendText(Environment.NewLine);
                    button1.Text = "NEXT";
                    if (!string.IsNullOrEmpty(textBox4.Text))
                    {
                        // it is not empty or null!
                        button1.Enabled = true;
                    }
                    else
                    {
                        button1.Enabled = false;
                    }
                    using (System.IO.StreamWriter writer2 = File.AppendText("Users\\" + name + "\\log.txt"))
                    {
                        DateTime now = DateTime.Now;
                        //Console.WriteLine(now);
                        string date1 = "";
                        date1 = date1 + now.ToString("hh.mm.ss.ffffff");
                        writer2.Write(date1 + " ");
                        //writer2.Write(" ");
                        writer2.Write(name + " ");
                        writer2.Write("brainteaser ");
                        writer2.Write(textBox4.Text + " ");
                        writer2.Write(System.Environment.NewLine);
                        writer2.Close();
                    }

                    using (System.IO.StreamWriter writer2 = File.AppendText("Users\\" + name + "\\log.csv"))
                    {
                        DateTime now = DateTime.Now;
                        //Console.WriteLine(now);
                        string date1 = "";
                        date1 = date1 + now.ToString("hh.mm.ss.ffffff");
                        writer2.Write(date1 + ",");
                        //writer2.Write(" ");
                        writer2.Write(name + ",");
                        writer2.Write("brainteaser ,");
                        writer2.Write(textBox4.Text + ",");
                        writer2.Write(System.Environment.NewLine);
                        writer2.Close();
                    }
                    break;
                case 5:
                    textBox2.Text = "5";
                    textBox1.Visible = true;
                    textBox2.Visible = true;
                    textBox3.Visible = true;
                    textBox4.Visible = true;
                    textBox5.Visible = true;
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox3.AppendText( "Provide a three-letter word that can be attached to the end of the given words to form five longer words.");
                    textBox3.AppendText(Environment.NewLine);
                    textBox3.AppendText("What is the word?");
                    textBox3.AppendText(Environment.NewLine);
                    textBox3.AppendText(Environment.NewLine);
                    textBox3.AppendText("CROSS DRAW HANDLE SAND CROW");
                    textBox3.AppendText(Environment.NewLine);
                    button1.Text = "NEXT";
                    if (!string.IsNullOrEmpty(textBox4.Text))
                    {
                        // it is not empty or null!
                        button1.Enabled = true;
                    }
                    else
                    {
                        button1.Enabled = false;
                    }
                    using (System.IO.StreamWriter writer2 = File.AppendText("Users\\" + name + "\\log.txt"))
                    {
                        DateTime now = DateTime.Now;
                        //Console.WriteLine(now);
                        string date1 = "";
                        date1 = date1 + now.ToString("hh.mm.ss.ffffff");
                        writer2.Write(date1 + " ");
                        //writer2.Write(" ");
                        writer2.Write(name + " ");
                        writer2.Write("brainteaser ");
                        writer2.Write(textBox4.Text + " ");
                        writer2.Write(System.Environment.NewLine);
                        writer2.Close();
                    }

                    using (System.IO.StreamWriter writer2 = File.AppendText("Users\\" + name + "\\log.csv"))
                    {
                        DateTime now = DateTime.Now;
                        //Console.WriteLine(now);
                        string date1 = "";
                        date1 = date1 + now.ToString("hh.mm.ss.ffffff");
                        writer2.Write(date1 + ",");
                        //writer2.Write(" ");
                        writer2.Write(name + ",");
                        writer2.Write("brainteaser ,");
                        writer2.Write(textBox4.Text + ",");
                        writer2.Write(System.Environment.NewLine);
                        writer2.Close();
                    }
                    break;
                //case 7:
                //    textBox2.Text = "7";
                //    textBox1.Visible = true;
                //    textBox2.Visible = true;
                //    textBox3.Visible = true;
                //    textBox4.Visible = true;
                //    textBox5.Visible = true;
                //    textBox3.Clear();
                //    textBox4.Clear();
                //    textBox3.AppendText("Rearrange the following letters to give one word. All eight letters must be used.");
                //    textBox3.AppendText(Environment.NewLine);
                //    textBox3.AppendText("What is the word?");
                //    textBox3.AppendText(Environment.NewLine);
                //    textBox3.AppendText(Environment.NewLine);
                //    textBox3.AppendText("C  G  I  M  N  N  O  O");
                //    textBox3.AppendText(Environment.NewLine);
                //    button1.Text = "NEXT";
                //    if (!string.IsNullOrEmpty(textBox4.Text))
                //    {
                //        // it is not empty or null!
                //        button1.Enabled = true;
                //    }
                //    else
                //    {
                //        button1.Enabled = false;
                //    }
                //    using (System.IO.StreamWriter writer2 = File.AppendText("Users\\" + name + "\\log.txt"))
                //    {
                //        DateTime now = DateTime.Now;
                //        //Console.WriteLine(now);
                //        string date1 = "";
                //        date1 = date1 + now.ToString("hh.mm.ss.ffffff");
                //        writer2.Write(date1 + " ");
                //        //writer2.Write(" ");
                //        writer2.Write(name + " ");
                //        writer2.Write("brainteaser ");
                //        writer2.Write(textBox4.Text + " ");
                //        writer2.Write(System.Environment.NewLine);
                //        writer2.Close();
                //    }

                //    using (System.IO.StreamWriter writer2 = File.AppendText("Users\\" + name + "\\log.csv"))
                //    {
                //        DateTime now = DateTime.Now;
                //        //Console.WriteLine(now);
                //        string date1 = "";
                //        date1 = date1 + now.ToString("hh.mm.ss.ffffff");
                //        writer2.Write(date1 + ",");
                //        //writer2.Write(" ");
                //        writer2.Write(name + ",");
                //        writer2.Write("brainteaser ,");
                //        writer2.Write(textBox4.Text + ",");
                //        writer2.Write(System.Environment.NewLine);
                //        writer2.Close();
                //    }
                //    break;
                case 6:
                    textBox2.Text = "6";
                    textBox1.Visible = true;
                    textBox2.Visible = true;
                    textBox3.Visible = true;
                    textBox4.Visible = true;
                    textBox5.Visible = true;
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox3.AppendText("Insert the same two letters into the exact centre of each of the following words, so that five longer words can be read.");
                    textBox3.AppendText(Environment.NewLine);
                    textBox3.AppendText("What are they?");
                    textBox3.AppendText(Environment.NewLine);
                    textBox3.AppendText(Environment.NewLine);
                    textBox3.AppendText("MALE    CUED    HEAL    GALE    VEAL");
                    textBox3.AppendText(Environment.NewLine);
                    button1.Text = "NEXT";
                    if (!string.IsNullOrEmpty(textBox4.Text))
                    {
                        // it is not empty or null!
                        button1.Enabled = true;
                    }
                    else
                    {
                        button1.Enabled = false;
                    }
                    using (System.IO.StreamWriter writer2 = File.AppendText("Users\\" + name + "\\log.txt"))
                    {
                        DateTime now = DateTime.Now;
                        //Console.WriteLine(now);
                        string date1 = "";
                        date1 = date1 + now.ToString("hh.mm.ss.ffffff");
                        writer2.Write(date1 + " ");
                        //writer2.Write(" ");
                        writer2.Write(name + " ");
                        writer2.Write("brainteaser ");
                        writer2.Write(textBox4.Text + " ");
                        writer2.Write(System.Environment.NewLine);
                        writer2.Close();
                    }

                    using (System.IO.StreamWriter writer2 = File.AppendText("Users\\" + name + "\\log.csv"))
                    {
                        DateTime now = DateTime.Now;
                        //Console.WriteLine(now);
                        string date1 = "";
                        date1 = date1 + now.ToString("hh.mm.ss.ffffff");
                        writer2.Write(date1 + ",");
                        //writer2.Write(" ");
                        writer2.Write(name + ",");
                        writer2.Write("brainteaser ,");
                        writer2.Write(textBox4.Text + ",");
                        writer2.Write(System.Environment.NewLine);
                        writer2.Close();
                    }
                    break;
                case 7:
                    textBox2.Text = "7";
                    textBox1.Visible = true;
                    textBox2.Visible = true;
                    textBox3.Visible = true;
                    textBox4.Visible = true;
                    textBox5.Visible = true;
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox3.AppendText("Rearrange the letters below to give three words. All seven letters must be used in each word.");
                    textBox3.AppendText(Environment.NewLine);
                    textBox3.AppendText("What are the three words?");
                    textBox3.AppendText(Environment.NewLine);
                    textBox3.AppendText(Environment.NewLine);
                    textBox3.AppendText("A E I M N R S");
                    textBox3.AppendText(Environment.NewLine);
                    button1.Text = "DONE";
                    if (!string.IsNullOrEmpty(textBox4.Text))
                    {
                        // it is not empty or null!
                        button1.Enabled = true;
                    }
                    else
                    {
                        button1.Enabled = false;
                    }
                    using (System.IO.StreamWriter writer2 = File.AppendText("Users\\" + name + "\\log.txt"))
                    {
                        DateTime now = DateTime.Now;
                        //Console.WriteLine(now);
                        string date1 = "";
                        date1 = date1 + now.ToString("hh.mm.ss.ffffff");
                        writer2.Write(date1 + " ");
                        //writer2.Write(" ");
                        writer2.Write(name + " ");
                        writer2.Write("brainteaser ");
                        writer2.Write(textBox4.Text + " ");
                        writer2.Write(System.Environment.NewLine);
                        writer2.Close();
                    }

                    using (System.IO.StreamWriter writer2 = File.AppendText("Users\\" + name + "\\log.csv"))
                    {
                        DateTime now = DateTime.Now;
                        //Console.WriteLine(now);
                        string date1 = "";
                        date1 = date1 + now.ToString("hh.mm.ss.ffffff");
                        writer2.Write(date1 + ",");
                        //writer2.Write(" ");
                        writer2.Write(name + ",");
                        writer2.Write("brainteaser ,");
                        writer2.Write(textBox4.Text + ",");
                        writer2.Write(System.Environment.NewLine);
                        writer2.Close();
                    }
                    break;
                default:
                    this.Close();
                    MessageBox.Show("Error has occurred, no puzzle piece was awarded, please continue with the other tasks.");
                    break;
            }
        }
    }
}
