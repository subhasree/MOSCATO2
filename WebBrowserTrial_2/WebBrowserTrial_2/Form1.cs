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
using NotificationWindow;

namespace WebBrowserTrial_2
{
    public partial class Form1 : Form
    {
        //Added by Subhasree for countdown timer
        private Timer _timer;

        // The last time the timer was started
        private DateTime _startTime = DateTime.MinValue;
        private NotificationWindow.PopupNotifier popupNotifier1;
        // Time between now and when the timer was started last
        public TimeSpan _currentElapsedTime = TimeSpan.Zero;

        // Time between now and the first time timer was started after a reset
        //private TimeSpan _totalElapsedTime = TimeSpan.Zero;

        // Whether or not the timer is currently running
        private bool _timerRunning = false;
        public TimeSpan _totalElapsedTime2 = TimeSpan.Zero;
        //end of timer
        int ctr_message = 0;
        String Url = string.Empty;
        public Form1(string url, string name2, TimeSpan _totalElapsedTime)
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
            timer4.Start();
            //StartTimer();
            //Url = "http://www.msn.com";
            //Url = "https://docs.google.com/forms/d/e/1FAIpQLScLOPiY8pl_YeGpLj4DrGd1ZZP-GHNimrREdeqiA_8eDnfnoA/viewform";
            //Url = "C:\\Subhasree\\CODES\\CODES\\moscato\\Resources\\[MOSCATO] Crossword puzzle.xlsx";
            //Url = "http://www.websudoku.com/?select=1&level=2";
            Url = url;
            myBrowser(Url);
            string name = name2;
            string score1 = "";
            string score2 = "";
            string score3 = "";
            string score4 = "";
            string score5 = "";
            if (Url == "https://docs.google.com/forms/d/e/1FAIpQLScLOPiY8pl_YeGpLj4DrGd1ZZP-GHNimrREdeqiA_8eDnfnoA/viewform")
            {
                pictureBox1.Visible = false;
                lbl_timer.Visible = false;
            }
            else
            {
                if (Url == "http://www.websudoku.com/?level=1&set_id=7867973019")
                {
                    txt_Instruction.Visible = true;
                }
                else
                {
                    txt_Instruction.Visible = false;
                }
                //string text = System.IO.File.ReadAllText(@"C:\Users\Public\TestFolder\WriteText.txt");
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
                //MessageBox.Show(score1 + " " + score2 + " " + score3 + " " + score4 + " " + score5);
            }
        }

        //Added by Subhasree
        private void showTextBaloonTooltip(string contentText, int fontSize, int windowDelay)
        {
            popupNotifier1.TitleText = "";
            popupNotifier1.ContentText = contentText;
            popupNotifier1.ShowCloseButton = true;
            popupNotifier1.ShowOptionsButton = false;
            popupNotifier1.ShowGrip = false;
            popupNotifier1.Delay = windowDelay; //5000;
            popupNotifier1.AnimationInterval = 10;
            popupNotifier1.AnimationDuration = 1500;
            popupNotifier1.TitlePadding = new Padding(0);
            popupNotifier1.ContentPadding = new Padding(0);
            popupNotifier1.ImagePadding = new Padding(0);
            popupNotifier1.Scroll = false;
            popupNotifier1.GradientPower = 50; //Gradient of window background color
            //popupNotifier1.HeaderHeight = 1;//9; //Height of window header
            popupNotifier1.Size = new Size(400, 100); //Size of the window
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
            popupNotifier1.Image = null;
            popupNotifier1.Popup();

        }

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
            // MessageBox.Show(_currentElapsedTime.ToString());
            // These are just two Label controls which display the current 
            // elapsed time and total elapsed time
            this.lbl_timer.Text = _currentElapsedTime.ToString();
            //_currentElapsedTimeDisplay.Text = timeSinceStartTime.ToString();
        }

        public void gotoSite(string url)
        {
            System.Diagnostics.Process.Start(url);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripButton1.Enabled = false;
            // toolStripButton2.Enabled = false;
            //gotoSite(Url);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            myBrowser(Url);
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
            if ((ctr_message % 3) == 0)
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

        private static void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            MessageBox.Show("Please check on the status");
        }

        private void myBrowser(string Url)
        {
            //if (toolStripComboBox1.Text != "")
            //    Url = toolStripComboBox1.Text;
            
            webBrowser1.Navigate(Url);
            webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser1.ProgressChanged +=
            new WebBrowserProgressChangedEventHandler(webpage_ProgressChanged);
            webBrowser1.DocumentTitleChanged += new EventHandler(webpage_DocumentTitleChanged);
            webBrowser1.StatusTextChanged += new EventHandler(webpage_StatusTextChanged);
            webBrowser1.Navigated += new WebBrowserNavigatedEventHandler(webpage_Navigated);
            webBrowser1.DocumentCompleted +=
            new WebBrowserDocumentCompletedEventHandler(webpage_DocumentCompleted);
        }

        private void webpage_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser1.CanGoBack) toolStripButton1.Enabled = true;
            else toolStripButton1.Enabled = false;

            //if (webBrowser1.CanGoForward) toolStripButton2.Enabled = true;
            //else toolStripButton2.Enabled = false;
            toolStripStatusLabel1.Text = "Done";
        }

        private void webpage_DocumentTitleChanged(object sender, EventArgs e)
        {
            this.Text = webBrowser1.DocumentTitle.ToString();
        }

        private void webpage_StatusTextChanged(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = webBrowser1.StatusText;
        }

        private void webpage_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            toolStripProgressBar1.Maximum = (int)e.MaximumProgress;
            toolStripProgressBar1.Value = ((int)e.CurrentProgress < 0 ||
            (int)e.MaximumProgress < (int)e.CurrentProgress) ? (int)e.MaximumProgress : (int)e.CurrentProgress;
        }

        private void webpage_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            //toolStripComboBox1.Text = webBrowser1.Url.ToString();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            webBrowser1.Refresh();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            webBrowser1.GoHome();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            webBrowser1.ShowPrintPreviewDialog();
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            webBrowser1.Stop();
            this.Close();
            var result = MessageBox.Show("You have received one puzzle piece. Do you want to go to folder?", "Puzzle Reward", MessageBoxButtons.OK);
            if (result == DialogResult.OK)
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
                // Closes the parent form.
                //this.Close();
            }
            return;
            //MessageBox.Show("You have received one puzzle piece. Congratulations!");
            // Application.Exit();
            
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime datetime = DateTime.Now;
            this.lbl_timer.Text = datetime.ToString("HH:mm:ss");
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            if (pictureBox1.Visible == true)
                pictureBox1.Visible = false;
            else
                pictureBox1.Visible = true;
        }
    }
}
