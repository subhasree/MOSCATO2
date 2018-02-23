using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using DropNet;
using System.IO;

namespace Words_Category
{

    public partial class Form1 : Form
    {
        Timer myTimer1, myTimer2, myTimer3, myTimer4, myTimer5, myTimer6 = new Timer();
        System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
        int rn, idx, word_no = 0, answer = 0, odp = 0, setno = 0;
        string path;
        string name;
        int[] indexes1 = new int[8];
        int[] indexes2 = new int[16];
        int[] indexes_ = new int[56];
        float[] times1 = new float[56];
        float[] times2 = new float[56];
        float[] times3 = new float[56];
        float[] times4 = new float[56];
        float[] times5 = new float[56];
        float[] times6 = new float[56];
        int[] scores1 = new int[56];
        int[] scores2 = new int[56];
        int[] scores3 = new int[56];
        int[] scores4 = new int[56];
        int[] scores5 = new int[56];
        int[] scores6 = new int[56];
        string[] wordsQ1 = { "I", "My", "Own", "Self" };
        string[] wordsQ2 = { "They", "Them", "Your", "You" };
        string[] wordsA1 = { "Extraverted", "Outgoing", "Sociable", "Lively" };
        string[] wordsA2 = { "Introverted", "Reserved", "Inhibited", "Withdrawn" };
        string[] wordsB1 = { "Organized", "Thorough", "Hardworking", "Efficient" };
        string[] wordsB2 = { "Irresponsible", "Careless", "Disorganized", "Reckless" };
        string[] wordsC1 = { "Sincere", "Honest", "Trustworthy", "Giving" };
        string[] wordsC2 = { "Conceited", "Snobbish", "Egotistical", "Superficial" };
        string[] wordsD1 = { "Agreeable", "Calm", "Peaceful", "Patient" };
        string[] wordsD2 = { "Quick-tempered", "Hot-tempered", "Short-tempered", "Aggressive" };
        string[] wordsE1 = { "Emotional", "Feminine", "Sensitive", "Sentimental" };
        string[] wordsE2 = { "Masculine", "Fearless", "Unemotional", "Rugged" };
        string[] wordsF1 = { "Philosophical", "Insightful", "Complex", "Deep" };
        string[] wordsF2 = { "Simple", "Conservative", "Conventional", "Narrow-minded" };
        string[] wordsA12, wordsB12, wordsC12, wordsD12, wordsE12, wordsF12, words12, words1, words2, words3, words4, words5, words6;

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

        private BackgroundWorker bw = new BackgroundWorker();
        Timer myTi2 = new Timer();
        Timer myTi3 = new Timer();
        Timer myTi4 = new Timer();
        Timer myTi5 = new Timer();
        DropNetClient _client;
        int dropbox_user, dropbox_puzzle;

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime datetime = DateTime.Now;
            this.lbl_timer.Text = datetime.ToString("HH:mm:ss");
        }

        private void txtCategoryA_TextChanged(object sender, EventArgs e)
        {

        }

        private void lbl_timer_Click(object sender, EventArgs e)
        {

        }

        public Form1(string path2, TimeSpan _totalElapsedTime)
        {
            path = path2;
            int width = 1300; //Screen.PrimaryScreen.Bounds.Width;
            int height = 900; //Screen.PrimaryScreen.Bounds.Height;
            this.Height = 900;
            this.Width = 1300;

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

            //this.Size = new System.Drawing.Size(width, height);
            //Self - Others
            //Extraversion - Introversion
            //Self / Extraversion - Others / Introversion
            //Introversion - Extraversion
            //Self / Introversion - Others / Extraversion

            words1 = wordsQ1.Concat(wordsQ2).ToArray();
            words1 = words1.Concat(wordsA1).ToArray();
            words1 = words1.Concat(wordsA2).ToArray();
            words1 = words1.Concat(wordsQ1).ToArray();
            words1 = words1.Concat(wordsA1).ToArray();
            words1 = words1.Concat(wordsQ2).ToArray();
            words1 = words1.Concat(wordsA2).ToArray();
            words1 = words1.Concat(wordsA2).ToArray();
            words1 = words1.Concat(wordsA1).ToArray();
            words1 = words1.Concat(wordsQ1).ToArray();
            words1 = words1.Concat(wordsA2).ToArray();
            words1 = words1.Concat(wordsQ2).ToArray();
            words1 = words1.Concat(wordsA1).ToArray();

            words2 = wordsQ1.Concat(wordsQ2).ToArray();
            words2 = words2.Concat(wordsB1).ToArray();
            words2 = words2.Concat(wordsB2).ToArray();
            words2 = words2.Concat(wordsQ1).ToArray();
            words2 = words2.Concat(wordsB1).ToArray();
            words2 = words2.Concat(wordsQ2).ToArray();
            words2 = words2.Concat(wordsB2).ToArray();
            words2 = words2.Concat(wordsB2).ToArray();
            words2 = words2.Concat(wordsB1).ToArray();
            words2 = words2.Concat(wordsQ1).ToArray();
            words2 = words2.Concat(wordsB2).ToArray();
            words2 = words2.Concat(wordsQ2).ToArray();
            words2 = words2.Concat(wordsB1).ToArray();

            words3 = wordsQ1.Concat(wordsQ2).ToArray();
            words3 = words3.Concat(wordsC1).ToArray();
            words3 = words3.Concat(wordsC2).ToArray();
            words3 = words3.Concat(wordsQ1).ToArray();
            words3 = words3.Concat(wordsC1).ToArray();
            words3 = words3.Concat(wordsQ2).ToArray();
            words3 = words3.Concat(wordsC2).ToArray();
            words3 = words3.Concat(wordsC2).ToArray();
            words3 = words3.Concat(wordsC1).ToArray();
            words3 = words3.Concat(wordsQ1).ToArray();
            words3 = words3.Concat(wordsC2).ToArray();
            words3 = words3.Concat(wordsQ2).ToArray();
            words3 = words3.Concat(wordsC1).ToArray();

            words4 = wordsQ1.Concat(wordsQ2).ToArray();
            words4 = words4.Concat(wordsD1).ToArray();
            words4 = words4.Concat(wordsD2).ToArray();
            words4 = words4.Concat(wordsQ1).ToArray();
            words4 = words4.Concat(wordsD1).ToArray();
            words4 = words4.Concat(wordsQ2).ToArray();
            words4 = words4.Concat(wordsD2).ToArray();
            words4 = words4.Concat(wordsD2).ToArray();
            words4 = words4.Concat(wordsD1).ToArray();
            words4 = words4.Concat(wordsQ1).ToArray();
            words4 = words4.Concat(wordsD2).ToArray();
            words4 = words4.Concat(wordsQ2).ToArray();
            words4 = words4.Concat(wordsD1).ToArray();

            words5 = wordsQ1.Concat(wordsQ2).ToArray();
            words5 = words5.Concat(wordsE1).ToArray();
            words5 = words5.Concat(wordsE2).ToArray();
            words5 = words5.Concat(wordsQ1).ToArray();
            words5 = words5.Concat(wordsE1).ToArray();
            words5 = words5.Concat(wordsQ2).ToArray();
            words5 = words5.Concat(wordsE2).ToArray();
            words5 = words5.Concat(wordsE2).ToArray();
            words5 = words5.Concat(wordsE1).ToArray();
            words5 = words5.Concat(wordsQ1).ToArray();
            words5 = words5.Concat(wordsE2).ToArray();
            words5 = words5.Concat(wordsQ2).ToArray();
            words5 = words5.Concat(wordsE1).ToArray();

            words6 = wordsQ1.Concat(wordsQ2).ToArray();
            words6 = words6.Concat(wordsF1).ToArray();
            words6 = words6.Concat(wordsF2).ToArray();
            words6 = words6.Concat(wordsQ1).ToArray();
            words6 = words6.Concat(wordsF1).ToArray();
            words6 = words6.Concat(wordsQ2).ToArray();
            words6 = words6.Concat(wordsF2).ToArray();
            words6 = words6.Concat(wordsF2).ToArray();
            words6 = words6.Concat(wordsF1).ToArray();
            words6 = words6.Concat(wordsQ1).ToArray();
            words6 = words6.Concat(wordsF2).ToArray();
            words6 = words6.Concat(wordsQ2).ToArray();
            words6 = words6.Concat(wordsF1).ToArray();

            //Conscientious - Unconscientious
            //Honesty-Humility - Dishonesty-Egoistic
            //Agreeable - Disagreeable
            //Emotionally unstable – Emotionally stable
            //Open minded – Closed minded

            for (int i = 0; i < 8; i++)
            {
                indexes1[i] = i;
            }
            for (int i = 0; i < 16; i++)
            {
                indexes2[i] = i;
            }

            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            InitializeComponent();
            //timer1.Start();
            btnExit.Location = new Point(2000, this.btnExit.Location.Y);
            txtCategoryA.Visible = false;
            txtCategoryB.Visible = false;
            txtCategoryA.TextAlign = HorizontalAlignment.Center;
            txtCategoryB.TextAlign = HorizontalAlignment.Center;
            //this.BackColor = System.Drawing.Color.Black;
            //txtInstruction.BackColor = System.Drawing.Color.Black;
            //txtInstruction.ForeColor = System.Drawing.Color.White;
            //txtInstruction.Font = new Font(txtInstruction.Font.FontFamily, 21);
            screen_intro();

            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);

        }

        private void txtInstruction_TextChanged(object sender, EventArgs e)
        {

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

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if ((worker.CancellationPending == true))
            {
                e.Cancel = true;
                //break;
            }
            else
            {
                // Perform a time consuming operation and report progress.
                // System.Threading.Thread.Sleep(2000);
                // worker.ReportProgress((i * 10));
                if (dropbox_user == 2)
                {
                    _client = new DropNetClient("xz3lt8k0t83agn3", "xa5elp599sswl06", "wcdvhmgvwgohzm3j", "let25haz5uf4gow");
                    _client.UseSandbox = false;
                    if (dropbox_puzzle == 1)
                    {
                        _client.UploadFile("/PUZZLE/" + "PL02", "puzzle_01.png", File.ReadAllBytes(@"Resources\\config.dat"));
                    }
                    if (dropbox_puzzle == 2)
                    {
                        _client.UploadFile("/PUZZLE/" + "PL02", "puzzle_02.png", File.ReadAllBytes(@"Resources\\packages.esx"));
                    }
                    if (dropbox_puzzle == 3)
                    {
                        _client.UploadFile("/PUZZLE/" + "PL02", "puzzle_03.png", File.ReadAllBytes(@"Resources\\model.nxi"));
                    }
                    if (dropbox_puzzle == 4)
                    {
                        _client.UploadFile("/PUZZLE/" + "PL02", "puzzle_04.png", File.ReadAllBytes(@"Resources\\assembly.dat"));
                        _client.UploadFile("/PUZZLE/" + "PL02", "puzzle_05.png", File.ReadAllBytes(@"Resources\\roboto.otg"));
                    }
                    if (dropbox_puzzle == 5)
                    {
                        _client.UploadFile("/PUZZLE/" + "PL02", "puzzle_06.png", File.ReadAllBytes(@"Resources\\cert.dbx"));
                    }
                }
                if (dropbox_user == 3)
                {
                    _client = new DropNetClient("y9ai6v2t26ltac6", "hixs0l8x6n120bp", "pvydbjvywv7i7cx5", "6kdway1t38pw70o");
                    _client.UseSandbox = false;
                    if (dropbox_puzzle == 1)
                    {
                        _client.UploadFile("/PUZZLE/" + "PL01", "puzzle_01.png", File.ReadAllBytes(@"Resources\\config.dat"));
                    }
                    if (dropbox_puzzle == 2)
                    {
                        _client.UploadFile("/PUZZLE/" + "PL01", "puzzle_02.png", File.ReadAllBytes(@"Resources\\packages.esx"));
                    }
                    if (dropbox_puzzle == 3)
                    {
                        _client.UploadFile("/PUZZLE/" + "PL01", "puzzle_03.png", File.ReadAllBytes(@"Resources\\model.nxi"));
                    }
                    if (dropbox_puzzle == 4)
                    {
                        _client.UploadFile("/PUZZLE/" + "PL01", "puzzle_04.png", File.ReadAllBytes(@"Resources\\assembly.dat"));
                        _client.UploadFile("/PUZZLE/" + "PL01", "puzzle_05.png", File.ReadAllBytes(@"Resources\\roboto.otg"));
                    }
                    if (dropbox_puzzle == 5)
                    {
                        _client.UploadFile("/PUZZLE/" + "PL01", "puzzle_06.png", File.ReadAllBytes(@"Resources\\cert.dbx"));
                    }
                }
                worker.ReportProgress(100);
            }

        }

        void timer_Dropbox2(object sender, EventArgs e)
        {
            if (bw.IsBusy != true)
            {
                dropbox_user = 2;
                dropbox_puzzle = 2;
                bw.RunWorkerAsync();
            }
            myTi2.Stop();
        }

        void timer_Dropbox3(object sender, EventArgs e)
        {
            if (bw.IsBusy != true)
            {
                dropbox_user = 3;
                dropbox_puzzle = 2;
                bw.RunWorkerAsync();
            }
            myTi3.Stop();
        }

        void timer_Dropbox4(object sender, EventArgs e)
        {
            if (bw.IsBusy != true)
            {
                dropbox_user = 2;
                dropbox_puzzle = 4;
                bw.RunWorkerAsync();
            }
            myTi4.Stop();
        }

        void timer_Dropbox5(object sender, EventArgs e)
        {
            if (bw.IsBusy != true)
            {
                dropbox_user = 3;
                dropbox_puzzle = 5;
                bw.RunWorkerAsync();
            }
            myTi5.Stop();
        }



        private void screen_intro()
        {
            //txtInstruction.Text = "In this task, you will be presented with categories and words. There will be a total of 6 sets with 5 blocks each. Each block consists of 8 or 16 trials. There will be 2 categories presented in each trial, and the categories will be at the top left and right of the screen, and the word will appear at the center of the screen. You will be required to click “E” if the word belongs to the left category, and “I” if the word belongs to the right category. You may place your fingers at the two keys throughout this task.";
        }
        private void screen_1(int idx2)
        {

            idx = idx2;

            //Self - Others
            //Extraversion - Introversion
            //Self / Extraversion - Others / Introversion
            //Introversion - Extraversion
            //Self / Introversion - Others / Extraversion

            if (idx == 0)
            {
                txtCategoryA.Text = "Self";
                txtCategoryB.Text = "Others";
            }
            if (idx == 8)
            {
                txtCategoryA.Text = "Extraversion";
                txtCategoryB.Text = "Introversion";
            }
            if (idx == 16)
            {
                txtCategoryA.Text = "Self" + Environment.NewLine + "Extraversion";
                txtCategoryB.Text = "Others" + Environment.NewLine + "Introversion";
            }
            if (idx == 32)
            {
                txtCategoryA.Text = "Introversion";
                txtCategoryB.Text = "Extraversion";
            }
            if (idx == 40)
            {
                txtCategoryA.Text = "Self" + Environment.NewLine + "Introversion";
                txtCategoryB.Text = "Others" + Environment.NewLine + "Extraversion";
            }

            odp = 0;
            txtInstruction.Text = words1[indexes_[idx]];
            scores1[idx] = 0;
            watch.Reset();
            watch.Start();
            //myTimer1.Dispose();
            myTimer1 = new Timer();
            myTimer1.Interval = 1;
            myTimer1.Tick += new EventHandler(timer_action_1);
            myTimer1.Start();
        }

        private void screen_2(int idx2)
        {

            idx = idx2;

            //Conscientious - Unconscientious
            //Honesty-Humility - Dishonesty-Egoistic
            //Agreeable - Disagreeable
            //Emotionally unstable – Emotionally stable
            //Open minded – Closed minded
            //Self - Others
            //Extraversion - Introversion
            //Self / Extraversion - Others / Introversion
            //Introversion - Extraversion
            //Self / Introversion - Others / Extraversion

            if (idx == 0)
            {
                txtCategoryA.Text = "Self";
                txtCategoryB.Text = "Others";
            }
            if (idx == 8)
            {
                txtCategoryA.Text = "Conscientious";
                txtCategoryB.Text = "Unconscientious";
            }
            if (idx == 16)
            {
                txtCategoryA.Text = "Self" + Environment.NewLine + "Conscientious";
                txtCategoryB.Text = "Others" + Environment.NewLine + "Unconscientious";
            }
            if (idx == 32)
            {
                txtCategoryA.Text = "Unconscientious";
                txtCategoryB.Text = "Conscientious";
            }
            if (idx == 40)
            {
                txtCategoryA.Text = "Self" + Environment.NewLine + "Unconscientious";
                txtCategoryB.Text = "Others" + Environment.NewLine + "Conscientious";
            }

            odp = 0;
            txtInstruction.Text = words2[indexes_[idx]];
            scores2[idx] = 0;
            watch.Reset();
            watch.Start();
            //myTimer2.Dispose();
            myTimer2 = new Timer();
            myTimer2.Interval = 1;
            myTimer2.Tick += new EventHandler(timer_action_2);
            myTimer2.Start();
        }

        private void screen_3(int idx2)
        {

            idx = idx2;

            //Honesty-Humility - Dishonesty-Egoistic
            //Agreeable - Disagreeable
            //Emotionally unstable – Emotionally stable
            //Open minded – Closed minded
            //Self - Others
            //Extraversion - Introversion
            //Self / Extraversion - Others / Introversion
            //Introversion - Extraversion
            //Self / Introversion - Others / Extraversion

            if (idx == 0)
            {
                txtCategoryA.Text = "Self";
                txtCategoryB.Text = "Others";
            }
            if (idx == 8)
            {
                txtCategoryA.Text = "Honesty-Humility";
                txtCategoryB.Text = "Dishonesty-Egoistic";
            }
            if (idx == 16)
            {
                txtCategoryA.Text = "Self" + Environment.NewLine + "Honesty-Humility";
                txtCategoryB.Text = "Others" + Environment.NewLine + "Dishonesty-Egoistic";
            }
            if (idx == 32)
            {
                txtCategoryA.Text = "Dishonesty-Egoistic";
                txtCategoryB.Text = "Honesty-Humility";
            }
            if (idx == 40)
            {
                txtCategoryA.Text = "Self" + Environment.NewLine + "Dishonesty-Egoistic";
                txtCategoryB.Text = "Others" + Environment.NewLine + "Honesty-Humility";
            }

            odp = 0;
            txtInstruction.Text = words3[indexes_[idx]];
            scores3[idx] = 0;
            watch.Reset();
            watch.Start();
            //myTimer3.Dispose();
            myTimer3 = new Timer();
            myTimer3.Interval = 1;
            myTimer3.Tick += new EventHandler(timer_action_3);
            myTimer3.Start();
        }

        private void screen_4(int idx2)
        {

            idx = idx2;

            //Agreeable - Disagreeable
            //Emotionally unstable – Emotionally stable
            //Open minded – Closed minded
            //Self - Others
            //Extraversion - Introversion
            //Self / Extraversion - Others / Introversion
            //Introversion - Extraversion
            //Self / Introversion - Others / Extraversion

            if (idx == 0)
            {
                txtCategoryA.Text = "Self";
                txtCategoryB.Text = "Others";
            }
            if (idx == 8)
            {
                txtCategoryA.Text = "Agreeable";
                txtCategoryB.Text = "Disagreeable";
            }
            if (idx == 16)
            {
                txtCategoryA.Text = "Self" + Environment.NewLine + "Agreeable";
                txtCategoryB.Text = "Others" + Environment.NewLine + "Disagreeable";
            }
            if (idx == 32)
            {
                txtCategoryA.Text = "Disagreeable";
                txtCategoryB.Text = "Agreeable";
            }
            if (idx == 40)
            {
                txtCategoryA.Text = "Self" + Environment.NewLine + "Disagreeable";
                txtCategoryB.Text = "Others" + Environment.NewLine + "Agreeable";
            }

            odp = 0;
            txtInstruction.Text = words4[indexes_[idx]];
            scores4[idx] = 0;
            watch.Reset();
            watch.Start();
            //myTimer4.Dispose();
            myTimer4 = new Timer();
            myTimer4.Interval = 1;
            myTimer4.Tick += new EventHandler(timer_action_4);
            myTimer4.Start();
        }

        private void screen_5(int idx2)
        {

            idx = idx2;

            //Emotionally unstable – Emotionally stable
            //Open minded – Closed minded
            //Self - Others
            //Extraversion - Introversion
            //Self / Extraversion - Others / Introversion
            //Introversion - Extraversion
            //Self / Introversion - Others / Extraversion

            if (idx == 0)
            {
                txtCategoryA.Text = "Self";
                txtCategoryB.Text = "Others";
            }
            if (idx == 8)
            {
                txtCategoryA.Text = "Emotionally unstable";
                txtCategoryB.Text = "Emotionally stable";
            }
            if (idx == 16)
            {
                txtCategoryA.Text = "Self" + Environment.NewLine + "Emotionally unstable";
                txtCategoryB.Text = "Others" + Environment.NewLine + "Emotionally stable";
            }
            if (idx == 32)
            {
                txtCategoryA.Text = "Emotionally stable";
                txtCategoryB.Text = "Emotionally unstable";
            }
            if (idx == 40)
            {
                txtCategoryA.Text = "Self" + Environment.NewLine + "Emotionally stable";
                txtCategoryB.Text = "Others" + Environment.NewLine + "Emotionally unstable";
            }

            odp = 0;
            txtInstruction.Text = words5[indexes_[idx]];
            scores5[idx] = 0;
            watch.Reset();
            watch.Start();
            //myTimer5.Dispose();
            myTimer5 = new Timer();
            myTimer5.Interval = 1;
            myTimer5.Tick += new EventHandler(timer_action_5);
            myTimer5.Start();
        }

        private void screen_6(int idx2)
        {

            idx = idx2;

            //Open minded – Closed minded
            //Self - Others
            //Extraversion - Introversion
            //Self / Extraversion - Others / Introversion
            //Introversion - Extraversion
            //Self / Introversion - Others / Extraversion

            if (idx == 0)
            {
                txtCategoryA.Text = "Self";
                txtCategoryB.Text = "Others";
            }
            if (idx == 8)
            {
                txtCategoryA.Text = "Open minded";
                txtCategoryB.Text = "Closed minded";
            }
            if (idx == 16)
            {
                txtCategoryA.Text = "Self" + Environment.NewLine + "Open minded";
                txtCategoryB.Text = "Others" + Environment.NewLine + "Closed minded";
            }
            if (idx == 32)
            {
                txtCategoryA.Text = "Closed minded";
                txtCategoryB.Text = "Open minded";
            }
            if (idx == 40)
            {
                txtCategoryA.Text = "Self" + Environment.NewLine + "Closed minded";
                txtCategoryB.Text = "Others" + Environment.NewLine + "Open minded";
            }

            odp = 0;
            txtInstruction.Text = words6[indexes_[idx]];
            scores6[idx] = 0;
            watch.Reset();
            watch.Start();
            //myTimer6.Dispose();
            myTimer6 = new Timer();
            myTimer6.Interval = 1;
            myTimer6.Tick += new EventHandler(timer_action_6);
            myTimer6.Start();
        }

        private void randomize()
        {
            new Random().Shuffle(indexes1);
            new Random().Shuffle(indexes2);

            for (int i = 0; i < 8; i++)
            {
                indexes_[0 + i] = 0 + indexes1[i];
            }
            for (int i = 0; i < 8; i++)
            {
                indexes_[8 + i] = 8 + indexes1[i];
            }
            for (int i = 0; i < 16; i++)
            {
                indexes_[16 + i] = 16 + indexes2[i];
            }
            for (int i = 0; i < 8; i++)
            {
                indexes_[32 + i] = 32 + indexes1[i];
            }
            for (int i = 0; i < 16; i++)
            {
                indexes_[40 + i] = 40 + indexes2[i];
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Visible = false;
            txtInstruction.TextAlign = HorizontalAlignment.Center;
            txtInstruction.Location = new Point(354, 380);
            txtInstruction.Font = new Font(txtInstruction.Font.FontFamily, 50);
            txtCategoryA.Visible = true;
            txtCategoryB.Visible = true;
            Cursor.Hide();

            myTi2.Dispose();
            myTi2 = new Timer();
            myTi2.Interval = 7000;
            myTi2.Tick += new EventHandler(timer_Dropbox2);
            myTi2.Start();

            myTi3.Dispose();
            myTi3 = new Timer();
            myTi3.Interval = 21000;
            myTi3.Tick += new EventHandler(timer_Dropbox3);
            myTi3.Start();

            myTi4.Dispose();
            myTi4 = new Timer();
            myTi4.Interval = 39000;
            myTi4.Tick += new EventHandler(timer_Dropbox4);
            myTi4.Start();

            myTi5.Dispose();
            myTi5 = new Timer();
            myTi5.Interval = 58000;
            myTi5.Tick += new EventHandler(timer_Dropbox5);
            myTi5.Start();


            randomize();
            screen_1(0);
        }

        private void btn_Exit(object sender, EventArgs e)
        {
            //System.Environment.Exit(0);
        }

        void timer_action_1(object sender, EventArgs e)
        {
            if (answer != 0)
            {
                myTimer1.Stop();
                myTimer1.Dispose();
                watch.Stop();
                times1[idx] = (float)watch.Elapsed.TotalMilliseconds;

                scores1[idx] = answer;
                //using (BinaryWriter writer2 = new BinaryWriter(File.OpenWrite("Users\\" + this.name + "\\log.txt")))
                //char[] delimiterChars = {' ', '\\' };
                string[] words = path.Split('\\');
                //MessageBox.Show(words[0]);
                using (System.IO.StreamWriter writer2 = File.AppendText(path + "\\log.txt"))
                {
                    DateTime now = DateTime.Now;
                    //Console.WriteLine(now);
                    string date1 = "";
                    date1 = date1 + now.ToString("hh.mm.ss.ffffff");
                    writer2.Write(date1 + " ");
                    //writer2.Write(" ");
                    writer2.Write(words[1] + " ");
                    writer2.Write("words " );
                    writer2.Write(answer + " " );
                    writer2.Write(System.Environment.NewLine);
                    writer2.Close();
                }

                using (System.IO.StreamWriter writer2 = File.AppendText(path + "\\log.csv"))
                {
                    DateTime now = DateTime.Now;
                    //Console.WriteLine(now);
                    string date1 = "";
                    date1 = date1 + now.ToString("hh.mm.ss.ffffff");
                    writer2.Write(date1 + ",");
                    //writer2.Write(" ");
                    writer2.Write(words[1] + ",");
                    writer2.Write("words " + "," );
                    writer2.Write(answer + ",");
                    writer2.Write(System.Environment.NewLine);
                    writer2.Close();
                }
                //this.Close();
                answer = 0;

                if (idx == 55)
                {
                    //myTimer1.Stop();
                    //myTimer1.Dispose();
                    //answer = 0;
                    //watch.Stop();
                    //times1[idx] = (float)watch.Elapsed.TotalMilliseconds;
                    using (System.IO.StreamWriter writer = new System.IO.StreamWriter(path + "\\words1.txt"))
                    {
                        foreach (var value in indexes_)
                        {
                            writer.WriteLine(value);
                        }
                        foreach (var value in times1)
                        {
                            writer.WriteLine(value);
                        }
                        foreach (var value in scores1)
                        {
                            writer.WriteLine(value);
                        }
                    }
                    //System.IO.File.AppendAllText(@"results.txt", times.ToString());
                    //System.IO.File.AppendAllText(@"results.txt", scores.ToString());
                    //MessageBox.Show("NEXT!");
                    ////new Random().Shuffle(indexes);
                    ////idx = 0;
                    ////screen_CD(0);
                    answer = 0;
                    setno += 1;
                    randomize();
                    screen_2(0);
                    return;
                    //screen_AB(0);
                    //Cursor.Show();
                    //this.Close();
                    //System.Environment.Exit(0);
                }
                if (idx < 55)
                {
                    screen_1(idx + 1);
                }

            }
        }

        // CHANGE EVERYTHING BELOW !!!!!!
        // REPAIR THE DROPBOX !!!!!!!
        // FIND SCREENSHOT APP !!!!!!

        void timer_action_2(object sender, EventArgs e)
        {
            if (answer != 0)
            {
                myTimer2.Stop();
                myTimer2.Dispose();
                watch.Stop();
                times2[idx] = (float)watch.Elapsed.TotalMilliseconds;

                scores2[idx] = answer;
                //using (BinaryWriter writer2 = new BinaryWriter(File.OpenWrite("Users\\" + this.name + "\\log.txt")))
                string[] words = path.Split('\\');
                using (System.IO.StreamWriter writer2 = File.AppendText(path + "\\log.txt"))
                {
                    DateTime now = DateTime.Now;
                    //Console.WriteLine(now);
                    string date1 = "";
                    date1 = date1 + now.ToString("hh.mm.ss.ffffff");
                    writer2.Write(date1 + " " );
                    writer2.Write(words[1] + " " );
                    writer2.Write("words" + " " );
                    writer2.Write(answer+ " " );
                    writer2.Write(System.Environment.NewLine);
                    writer2.Close();
                }

                using (System.IO.StreamWriter writer2 = File.AppendText(path + "\\log.csv"))
                {
                    DateTime now = DateTime.Now;
                    //Console.WriteLine(now);
                    string date1 = "";
                    date1 = date1 + now.ToString("hh.mm.ss.ffffff");
                    writer2.Write(date1 + ",");
                    writer2.Write(words[1] + ",");
                    writer2.Write("words" + ",");
                    writer2.Write(answer + ",");
                    writer2.Write(System.Environment.NewLine);
                    writer2.Close();
                }
                answer = 0;

                if (idx == 55)
                {
                    //myTimer2.Stop();
                    //myTimer2.Dispose();
                    //answer = 0;
                    //watch.Stop();
                    //times2[idx] = (float)watch.Elapsed.TotalMilliseconds;
                    using (System.IO.StreamWriter writer = new System.IO.StreamWriter(path + "\\words2.txt"))
                    {
                        foreach (var value in indexes_)
                        {
                            writer.WriteLine(value);
                        }
                        foreach (var value in times2)
                        {
                            writer.WriteLine(value);
                        }
                        foreach (var value in scores2)
                        {
                            writer.WriteLine(value);
                        }
                    }
                    //System.IO.File.AppendAllText(@"results.txt", times.ToString());
                    //System.IO.File.AppendAllText(@"results.txt", scores.ToString());
                    //MessageBox.Show("NEXT!");
                    ////new Random().Shuffle(indexes);
                    ////idx = 0;
                    ////screen_CD(0);
                    answer = 0;
                    setno += 1;
                    randomize();
                    screen_3(0);
                    return;
                    //screen_AB(0);
                    //Cursor.Show();
                    //this.Close();
                    //System.Environment.Exit(0);
                }
                if (idx < 55)
                {
                    screen_2(idx + 1);
                }

            }
        }

        void timer_action_3(object sender, EventArgs e)
        {
            if (answer != 0)
            {
                myTimer3.Stop();
                myTimer3.Dispose();
                watch.Stop();
                times3[idx] = (float)watch.Elapsed.TotalMilliseconds;

                scores3[idx] = answer;
                //using (BinaryWriter writer2 = new BinaryWriter(File.OpenWrite("Users\\" + this.name + "\\log.txt")))
                string[] words = path.Split('\\');
                using (System.IO.StreamWriter writer2 = File.AppendText(path + "\\log.txt"))
                {
                    DateTime now = DateTime.Now;
                    //Console.WriteLine(now);
                    string date1 = "";
                    date1 = date1 + now.ToString("hh.mm.ss.ffffff");
                    writer2.Write(date1 + " ");
                    writer2.Write(words[1] + " ");
                    writer2.Write("words" + " ");
                    writer2.Write(answer + " ");
                    writer2.Write(System.Environment.NewLine);
                    writer2.Close();
                }

                using (System.IO.StreamWriter writer2 = File.AppendText(path + "\\log.csv"))
                {
                    DateTime now = DateTime.Now;
                    //Console.WriteLine(now);
                    string date1 = "";
                    date1 = date1 + now.ToString("hh.mm.ss.ffffff");
                    writer2.Write(date1 + ",");
                    writer2.Write(words[1] + ",");
                    writer2.Write("words" + ",");
                    writer2.Write(answer + ",");
                    writer2.Write(System.Environment.NewLine);
                    writer2.Close();
                }
                answer = 0;

                if (idx == 55)
                {
                    //myTimer3.Stop();
                    //myTimer3.Dispose();
                    //answer = 0;
                    //watch.Stop();
                    //times3[idx] = (float)watch.Elapsed.TotalMilliseconds;
                    using (System.IO.StreamWriter writer = new System.IO.StreamWriter(path + "\\words3.txt"))
                    {
                        foreach (var value in indexes_)
                        {
                            writer.WriteLine(value);
                        }
                        foreach (var value in times3)
                        {
                            writer.WriteLine(value);
                        }
                        foreach (var value in scores3)
                        {
                            writer.WriteLine(value);
                        }
                    }
                    //System.IO.File.AppendAllText(@"results.txt", times.ToString());
                    //System.IO.File.AppendAllText(@"results.txt", scores.ToString());
                    //MessageBox.Show("NEXT!");
                    ////new Random().Shuffle(indexes);
                    ////idx = 0;
                    ////screen_CD(0);
                    answer = 0;
                    setno += 1;
                    randomize();
                    screen_4(0);
                    return;
                    //screen_AB(0);
                    //Cursor.Show();
                    //this.Close();
                    //System.Environment.Exit(0);
                }
                if (idx < 55)
                {
                    screen_3(idx + 1);
                }

            }
        }

        void timer_action_4(object sender, EventArgs e)
        {
            if (answer != 0)
            {
                myTimer4.Stop();
                myTimer4.Dispose();
                watch.Stop();
                times4[idx] = (float)watch.Elapsed.TotalMilliseconds;

                scores4[idx] = answer;
                // using (BinaryWriter writer2 = new BinaryWriter(File.OpenWrite("Users\\" + this.name + "\\log.txt")))
                string[] words = path.Split('\\');
                using (System.IO.StreamWriter writer2 = File.AppendText(path + "\\log.txt"))
                {
                    DateTime now = DateTime.Now;
                    //Console.WriteLine(now);
                    string date1 = "";
                    date1 = date1 + now.ToString("hh.mm.ss.ffffff");
                    writer2.Write(date1 + " ");
                    writer2.Write(words[1] + " ");
                    writer2.Write("words" + " ");
                    writer2.Write(answer + " ");
                    writer2.Write(System.Environment.NewLine);
                    writer2.Close();
                }
                using (System.IO.StreamWriter writer2 = File.AppendText(path + "\\log.csv"))
                {
                    DateTime now = DateTime.Now;
                    //Console.WriteLine(now);
                    string date1 = "";
                    date1 = date1 + now.ToString("hh.mm.ss.ffffff");
                    writer2.Write(date1 + ",");
                    writer2.Write(words[1] + ",");
                    writer2.Write("words" + ",");
                    writer2.Write(answer + ",");
                    writer2.Write(System.Environment.NewLine);
                    writer2.Close();
                }
                answer = 0;

                if (idx == 55)
                {
                    //myTimer4.Stop();
                    //myTimer4.Dispose();
                    //answer = 0;
                    //watch.Stop();
                    //times4[idx] = (float)watch.Elapsed.TotalMilliseconds;
                    using (System.IO.StreamWriter writer = new System.IO.StreamWriter(path + "\\words4.txt"))
                    {
                        foreach (var value in indexes_)
                        {
                            writer.WriteLine(value);
                        }
                        foreach (var value in times4)
                        {
                            writer.WriteLine(value);
                        }
                        foreach (var value in scores4)
                        {
                            writer.WriteLine(value);
                        }
                    }
                    //System.IO.File.AppendAllText(@"results.txt", times.ToString());
                    //System.IO.File.AppendAllText(@"results.txt", scores.ToString());
                    //MessageBox.Show("NEXT!");
                    ////new Random().Shuffle(indexes);
                    ////idx = 0;
                    ////screen_CD(0);
                    answer = 0;
                    setno += 1;
                    randomize();
                    screen_5(0);
                    return;
                    //screen_AB(0);
                    //Cursor.Show();
                    //this.Close();
                    //System.Environment.Exit(0);
                }
                if (idx < 55)
                {
                    screen_4(idx + 1);
                }

            }
        }

        void timer_action_5(object sender, EventArgs e)
        {
            if (answer != 0)
            {
                myTimer5.Stop();
                myTimer5.Dispose();
                watch.Stop();
                times5[idx] = (float)watch.Elapsed.TotalMilliseconds;

                scores5[idx] = answer;
                //using (BinaryWriter writer2 = new BinaryWriter(File.OpenWrite("Users\\" + this.name + "\\log.txt")))
                string[] words = path.Split('\\');
                using (System.IO.StreamWriter writer2 = File.AppendText(path + "\\log.txt"))
                {
                    DateTime now = DateTime.Now;
                    //Console.WriteLine(now);
                    string date1 = "";
                    date1 = date1 + now.ToString("hh.mm.ss.ffffff");
                    writer2.Write(date1 + " ");
                    writer2.Write(words[1] + " ");
                    writer2.Write("words" + " ");
                    writer2.Write(answer + " ");
                    writer2.Write(System.Environment.NewLine);
                    writer2.Close();
                }

                using (System.IO.StreamWriter writer2 = File.AppendText(path + "\\log.csv"))
                {
                    DateTime now = DateTime.Now;
                    //Console.WriteLine(now);
                    string date1 = "";
                    date1 = date1 + now.ToString("hh.mm.ss.ffffff");
                    writer2.Write(date1 + ",");
                    writer2.Write(words[1] + ",");
                    writer2.Write("words" + ",");
                    writer2.Write(answer + ",");
                    writer2.Write(System.Environment.NewLine);
                    writer2.Close();
                }
                answer = 0;

                if (idx == 55)
                {
                    //myTimer5.Stop();
                    //myTimer5.Dispose();
                    //answer = 0;
                    //watch.Stop();
                    //times5[idx] = (float)watch.Elapsed.TotalMilliseconds;
                    using (System.IO.StreamWriter writer = new System.IO.StreamWriter(path + "\\words5.txt"))
                    {
                        foreach (var value in indexes_)
                        {
                            writer.WriteLine(value);
                        }
                        foreach (var value in times5)
                        {
                            writer.WriteLine(value);
                        }
                        foreach (var value in scores5)
                        {
                            writer.WriteLine(value);
                        }
                    }
                    //System.IO.File.AppendAllText(@"results.txt", times.ToString());
                    //System.IO.File.AppendAllText(@"results.txt", scores.ToString());
                    //MessageBox.Show("NEXT!");
                    ////new Random().Shuffle(indexes);
                    ////idx = 0;
                    ////screen_CD(0);
                    answer = 0;
                    setno += 1;
                    randomize();
                    screen_6(0);
                    return;
                    //screen_AB(0);
                    //Cursor.Show();
                    //this.Close();
                    //System.Environment.Exit(0);
                }
                if (idx < 55)
                {
                    screen_5(idx + 1);
                }

            }
        }

        void timer_action_6(object sender, EventArgs e)
        {
            if (answer != 0)
            {
                myTimer6.Stop();
                myTimer6.Dispose();
                watch.Stop();
                times6[idx] = (float)watch.Elapsed.TotalMilliseconds;

                scores6[idx] = answer;
                //using (BinaryWriter writer2 = new BinaryWriter(File.OpenWrite("Users\\" + this.name + "\\log.txt")))
                string[] words = path.Split('\\');
                using (System.IO.StreamWriter writer2 = File.AppendText(path + "\\log.txt"))
                {
                    DateTime now = DateTime.Now;
                    //Console.WriteLine(now);
                    string date1 = "";
                    date1 = date1 + now.ToString("hh.mm.ss.ffffff");
                    writer2.Write(date1 + " ");
                    writer2.Write(words[1] + " ");
                    writer2.Write("words" + " ");
                    writer2.Write(answer + " ");
                    writer2.Write(System.Environment.NewLine);
                    writer2.Close();
                }

                using (System.IO.StreamWriter writer2 = File.AppendText(path + "\\log.csv"))
                {
                    DateTime now = DateTime.Now;
                    //Console.WriteLine(now);
                    string date1 = "";
                    date1 = date1 + now.ToString("hh.mm.ss.ffffff");
                    writer2.Write(date1 + ",");
                    writer2.Write(words[1] + ",");
                    writer2.Write("words" + ",");
                    writer2.Write(answer + ",");
                    writer2.Write(System.Environment.NewLine);
                    writer2.Close();
                }
                answer = 0;

                if (idx == 55)
                {
                    //myTimer6.Stop();
                    //myTimer6.Dispose();
                    //answer = 0;
                    //watch.Stop();
                    //times6[idx] = (float)watch.Elapsed.TotalMilliseconds;
                    using (System.IO.StreamWriter writer = new System.IO.StreamWriter(path + "\\words6.txt"))
                    {
                        foreach (var value in indexes_)
                        {
                            writer.WriteLine(value);
                        }
                        foreach (var value in times6)
                        {
                            writer.WriteLine(value);
                        }
                        foreach (var value in scores6)
                        {
                            writer.WriteLine(value);
                        }
                    }
                    //System.IO.File.AppendAllText(@"results.txt", times.ToString());
                    //System.IO.File.AppendAllText(@"results.txt", scores.ToString());
                    //MessageBox.Show("NEXT!");
                    ////new Random().Shuffle(indexes);
                    ////idx = 0;
                    ////screen_CD(0);
                    answer = 0;
                    setno += 1;
                    //return;
                    //screen_AB(0);
                    Cursor.Show();
                    this.Close();
                    var result = MessageBox.Show("You have received two puzzle pieces. Do you want to go to folder?", "Puzzle Reward", MessageBoxButtons.OK);
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
                        this.SendToBack();
                        // Closes the parent form.
                        //this.Close();
                    }
                    //MessageBox.Show("You have received two puzzle pieces. Congratulations!");
                    return;
                    //System.Environment.Exit(0);
                }
                if (idx < 55)
                {
                    screen_6(idx + 1);
                }

            }
        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.E)
            {
                answer = 1;
            }
            if (e.KeyCode == Keys.I)
            {
                answer = 2;
            }

            e.Handled = false;
        }

    }

    static class RandomExtensions
    {
        public static void Shuffle<T>(this Random rng, T[] array)
        {
            int n = array.Length;
            while (n > 1)
            {
                int k = rng.Next(n--);
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }
    }

}
