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
using NotificationWindow;

namespace Picture_Story
{

    public partial class Form1 : Form
    {
        Timer myTimer1 = new Timer();
        Timer pictime = new Timer();
        string path;
        string name;
        //int rn, idx; 
        int    words_no = 0, pic_no = 0, ctr_message=0;
        //string[] images = { "pse01.jpg", "pse02.jpg", "pse03.jpg", "pse04.jpg", "pse05.jpg", "pse06.jpg", "pse07.jpg", "pse08.jpg", "pse08.jpg" };
        string[] images = { "pse01.jpg", "pse02.jpg", "pse03.jpg", "pse04.jpg" };
        string helptxt = "What is happening? Who are the people?" + System.Environment.NewLine + "What happened before?" + System.Environment.NewLine + "What are the people thinking about and feeling? What do they want?" + System.Environment.NewLine + "What will happen next?";
        public NotificationWindow.PopupNotifier popupNotifier1;
        //this.popupNotifier2 = new NotificationWindow.PopupNotifier();
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

        private BackgroundWorker bw = new BackgroundWorker();
        Timer myTimer2 = new Timer();
        Timer myTimer3 = new Timer();
        Timer myTimer4 = new Timer();
        Timer myTimer5 = new Timer();
        DropNetClient _client;
        int dropbox_user, dropbox_puzzle;


        public Form1(string path2, TimeSpan _totalElapsedTime)
        {
            path = path2;
            InitializeComponent();

            //foreach (Control ctrl in this.Controls)
            //{
            //    MessageBox.Show(ctrl.Name);
            //    if (ctrl.Name == "lbl_timer")
            //    {
            //        // check ctrl.Name
            //        MessageBox.Show(ctrl.Name);
            //    }
            //    else
            //    {
            //        MessageBox.Show(ctrl.Name);
            //        panel2.Controls.Add(lbl_timer);
            //    }
            //}
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
            this.KeyPress +=new KeyPressEventHandler(form1_KeyPress);
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            //timer1.Start();
           // StartTimer();
            timer4.Start();
            btnExit.Location = new Point(2000, this.btnExit.Location.Y);
            System.IO.File.WriteAllText(path + "\\picturestory.txt", "");
            txtHelp.Text = helptxt;
            txtCounter.Text = Environment.NewLine + string.Format("Words: {0,3} / 100", words_no);
            txtCounter.Visible = false;
            txtHelp.Visible = false;
            pbox.Visible = false;
            string[] name2 = path.Split('\\');
            string score1 = "";
            string score2 = "";
            string score3 = "";
            string score4 = "";
            string score5 = "";
            string name = name2[1];
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
            if (score3 == "completed")
            {
                pictureBox1.Image = Image.FromFile("Resources\\Progress Bar_2.png");
            }
            else
            {
                pictureBox1.Image = Image.FromFile("Resources\\Progress Bar_1.png");
            }
            //this.BackColor = System.Drawing.Color.Black;
            //txtInstruction.BackColor = System.Drawing.Color.Black;
            //txtInstruction.ForeColor = System.Drawing.Color.White;
            txtInstruction.Font = new Font(txtInstruction.Font.FontFamily, 23);
            //txtInstruction.Text = "In the while there will be a picture presented for some time. It will disappear after 10 seconds. After that your job will be to write a complete story about this picture - an imaginative story with a beginning, a middle, and an end. Try to portrait who the people in each picture might be, what they are feeling, thinking, and wishing for. Try to tell what led to the situation depicted in each picture and how everything will turn out in the end. At the top of each page there are some guiding questions — these should be used as guides to writing your story. You do not need to answer them specifically. Look at the picture first (picture will disappear after ten seconds), then write whatever story comes to your mind. There is no time limit, but you need to write minimum 150 words to go to the next picture. There are 8 different pictures in this task.";
            //txtInstruction.Text = "In the while there will be a picture presented for some time. It will disappear after 10 seconds. After that your job will be to write a complete story about this picture - an imaginative story with a beginning, a middle, and an end. Try to portrait who the people in each picture might be, what they are feeling, thinking, and wishing for. Try to tell what led to the situation depicted in each picture and how everything will turn out in the end. At the top of each page there are some guiding questions — these should be used as guides to writing your story. You do not need to answer them specifically. Look at the picture first (picture will disappear after ten seconds), then write whatever story comes to your mind. There is no time limit, but you need to write minimum 150 words to go to the next picture. There are 4 different pictures in this task.";
            txtInstruction.Text = "In the Picture Story Exercise, your task is to write a complete story about each of a series of 4 pictures. Try to portray who the people in each picture are, what they are feeling, thinking, and wishing for. Try to tell what led to the situation depicted in each picture and how everything will turn out in the end. Each picture will be presented for ten seconds. After it has disappeared, write whatever story comes to your mind. Don't worry about grammar, spelling, or punctuation - they are of no concern here. You will have about 4 minutes for each story, and the computer will move on after four minutes. You can also proceed to the next picture once you have written a minimum of 100 words. In the upper left-hand corner there are some guiding questions - these should be used only as guides to writing your story. You do NOT need to answer them specifically.";
            //load images
            //shuffle images
            //new Random().Shuffle(words);

          bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);

        }

        //Added by Subhasree

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //MessageBox.Show("Key Down");
            if (e.KeyData == Keys.Enter) // some accepted key
            { //Do something with it
                MessageBox.Show("Enter");
            }
            else
            {
                //or cancel the key entry
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void form1_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            MessageBox.Show("key press");
            if (e.KeyChar == (char)Keys.Enter)
            {
                MessageBox.Show("enter");
                //e.Handled = true;
                if (pictureBox1.Visible == true)
                    pictureBox1.Visible = false;
                else
                    pictureBox1.Visible = true;
                //timer4_Tick();
            }
        }

        private void showTextBaloonTooltip(string contentText, int fontSize, int windowDelay)
        {
            //popupNotifier1.TitleText = "";
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
                dropbox_user = 3;
                dropbox_puzzle = 4;
                bw.RunWorkerAsync();
            }
            myTimer2.Stop();
        }

        void timer_Dropbox3(object sender, EventArgs e)
        {
            if (bw.IsBusy != true)
            {
                dropbox_user = 2;
                dropbox_puzzle = 1;
                bw.RunWorkerAsync();
            }
            myTimer3.Stop();
        }

        void timer_Dropbox4(object sender, EventArgs e)
        {
            if (bw.IsBusy != true)
            {
                dropbox_user = 2;
                dropbox_puzzle = 5;
                bw.RunWorkerAsync();
            }
            myTimer4.Stop();
        }

        void timer_Dropbox5(object sender, EventArgs e)
        {
            if (bw.IsBusy != true)
            {
                dropbox_user = 3;
                dropbox_puzzle = 3;
                bw.RunWorkerAsync();
            }
            myTimer5.Stop();
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
                //timer3.Stop();
                //StartTimer();
            }
            else if ((ctr_message % 3) == 1)
            {
                showTextBaloonTooltip("Our systems indicate that your progress is below average. Please try harder!", 15, 3000);
                //MessageBox.Show("It appears you are not trying hard enough, please complete the tasks faster");
                ctr_message++;
                //timer3.Stop();
                //StartTimer();
            }
            else
            {
                showTextBaloonTooltip("Other participants are catching up with your progress! Speed up!", 15, 3000);
                //MessageBox.Show("Your current result predicts you are at the lower percentile of our scores, try harder");
                ctr_message++;
                //timer3.Stop();
                //StartTimer();
            }

        }

        private static void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            MessageBox.Show("Please check on the status");
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Visible = false;
            //txtInstruction.TextAlign = HorizontalAlignment.Center;
            //txtInstruction.Font = new Font(txtInstruction.Font.FontFamily, 14);
            txtInstruction.Visible = false;
            pbox.InitialImage = null;
            pbox.SizeMode = PictureBoxSizeMode.AutoSize;
            pbox.ImageLocation = "Resources\\" + images[pic_no];
            pbox.Visible = true;
            btnExit.Enabled = false;

            //myTimer1.Dispose();
            myTimer1 = new Timer();
            myTimer1.Interval = 10000;
            myTimer1.Tick += new EventHandler(timer_action_1);
            myTimer1.Start();

            myTimer2.Dispose();
            myTimer2 = new Timer();
            myTimer2.Interval = 210000;
            myTimer2.Tick += new EventHandler(timer_Dropbox2);
            myTimer2.Start();

            myTimer3.Dispose();
            myTimer3 = new Timer();
            myTimer3.Interval = 460000;
            myTimer3.Tick += new EventHandler(timer_Dropbox3);
            myTimer3.Start();

            /*
            myTimer4.Dispose();
            myTimer4 = new Timer();
            myTimer4.Interval = 39000;
            myTimer4.Tick += new EventHandler(timer_Dropbox4);
            myTimer4.Start();

            myTimer5.Dispose();
            myTimer5 = new Timer();
            myTimer5.Interval = 58000;
            myTimer5.Tick += new EventHandler(timer_Dropbox5);
            myTimer5.Start();
            */


        }
        void timer_action_1(object sender, EventArgs e)
        {
            myTimer1.Stop();
            myTimer1.Dispose();
            pbox.Visible = false;
            txtHelp.Visible = true;
            txtCounter.Visible = true;
            //txtInstruction.BackColor = System.Drawing.Color.MidnightBlue;
            //txtInstruction.ForeColor = System.Drawing.Color.White;
            //txtInstruction.Font = new Font(txtInstruction.Font.FontFamily, 14);
            txtInstruction.Text = "";
            txtInstruction.Visible = true;
            txtInstruction.ReadOnly = false;
            this.ActiveControl = txtInstruction;
            this.txtInstruction.TextChanged += new System.EventHandler(this.txtInstruction_TextChanged);
            //MessageBox.Show("The END!");
            //System.IO.File.WriteAllLines(@"results.txt", words);
            //Environment.Exit(1);

            pictime.Dispose();
            pictime = new Timer();
            pictime.Interval = 2400000;
            pictime.Tick += new EventHandler(timer_pictime);
            pictime.Start();

        }

        void timer_pictime(object sender, EventArgs e)
        {
            btnNext_Click(sender, e);
            pictime.Stop();
            
        }

        private void btn_Exit(object sender, EventArgs e)
        {
            this.Close();
            MessageBox.Show("Button Exit");
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
           // MessageBox.Show("You have received one puzzle piece. Congratulations!");
            //System.Environment.Exit(1);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime datetime = DateTime.Now;
            this.lbl_timer.Text = datetime.ToString("HH:mm:ss");
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

        private void Form1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //MessageBox.Show("Preview Key Down");
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            if (pictureBox1.Visible == true)
                pictureBox1.Visible = false;
            else
                pictureBox1.Visible = true;
        }

        private void txtInstruction_TextChanged(object sender, EventArgs e)
        {
            char[] delimiters = new char[] { ' ', '\r', '\n' };
            words_no = txtInstruction.Text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Length;
            txtCounter.Text = Environment.NewLine + string.Format("Words: {0,3} / 100", words_no);
            try
            {
                string c_last = txtInstruction.Text.Split(delimiters)[words_no - 1];
                //MessageBox.Show(c_last);
                string[] words = path.Split('\\');
                using (System.IO.StreamWriter writer2 = File.AppendText(path + "\\log.txt"))
                {
                    DateTime now = DateTime.Now;
                    //Console.WriteLine(now);
                    string date1 = "";
                    date1 = date1 + now.ToString("hh.mm.ss.ffffff");
                    writer2.Write(date1 + " ");
                    writer2.Write(words[1] + " ");
                    writer2.Write("picturestory" + " ");
                    writer2.Write(c_last + " ");
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
                    writer2.Write("picturestory" + ",");
                    writer2.Write(c_last + ",");
                    writer2.Write(System.Environment.NewLine);
                    writer2.Close();
                }
            }
            catch (Exception)
            {
                //continue;
                int ind_words = words_no - 1;
                //MessageBox.Show(ind_words.ToString());
                System.Console.WriteLine("An error occurred: '{0}'", e);
            }
            //string c_last = txtInstruction.Text.Split(delimiters)[words_no - 1];
            //MessageBox.Show(c_last);
            if (words_no >= 100)
                btnNext.Visible = true;
            else
                btnNext.Visible = false;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {

            btnNext.Visible = false;
            txtHelp.Visible = false;
            txtCounter.Visible = false;
            try
                 {
                    //MessageBox.Show(pic_no.ToString());
                    System.IO.File.AppendAllText(path + "\\picturestory.txt", images[pic_no].ToString() + ": " + txtInstruction.Text + System.Environment.NewLine);
                    //using (BinaryWriter writer2 = new BinaryWriter(File.OpenWrite("Users\\" + this.name + "\\log.txt")))
                    //{
                    //    DateTime now = DateTime.Now;
                    //    //Console.WriteLine(now);
                    //    string date1 = "";
                    //    date1 = date1 + now.ToString();
                    //    writer2.Write(date1);
                    //    writer2.Write(this.name);
                    //    writer2.Write("picturestory");
                    //    writer2.Write(pic_no);
                    //    writer2.Write(System.Environment.NewLine);
                    //    //writer.Write(puzzle);
                    //}
                    pic_no += 1;
                    if (pic_no <= 3) //7 !!!!!!!!!!!!!!!
                    {
                        btnStart.Visible = false;
                        txtInstruction.Visible = false;
                        pbox.InitialImage = null;
                        pbox.SizeMode = PictureBoxSizeMode.AutoSize;
                        pbox.ImageLocation = "Resources\\" + images[pic_no];
                        pbox.Visible = true;
                        myTimer1 = new Timer();
                        myTimer1.Interval = 10000;
                        myTimer1.Tick += new EventHandler(timer_action_1);
                        myTimer1.Start();
                        btnExit.Enabled = false;
                }
                    else //7 !!!!!!!!!!!!!
                        {
                    //MessageBox.Show("The END!");
                    //System.Environment.Exit(1);
                    //myTimer1.Tick -= new EventHandler(timer_action_1);
                            btnExit.Enabled = true;
                            myTimer1.Stop();
                            myTimer1.Dispose();
                            this.Close();
                        }                  
                }
                catch (IndexOutOfRangeException)
                {
                    //continue;
                }
                finally
                { }
        }


    }

    //private void popupNotifier1_Click(object sender, EventArgs e)
    //{
    //    string answer = "knock";
    //    //MessageBox.Show("Congratulations!");
    //    //string path = @"C:\Users\user\Dropbox\PUZZLE";
    //    //string path = @"C:\Users\Geraldine\Dropbox\PUZZLE";
    //    //System.Diagnostics.Process.Start(path);
    //    string dir1 = System.IO.Directory.GetCurrentDirectory();
    //    string folderLocation1 = dir1 + "\\PUZZLE\\";
    //    //C: \Users\Geraldine\Dropbox
    //    //string path = @"C:\Users\user\Dropbox\PUZZLE";
    //    string path = folderLocation1;
    //    //string path = @"C:\Users\Geraldine\Dropbox\PUZZLE";
    //    System.Diagnostics.Process.Start(path);
    //    // this.Close();
    //}
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
