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
using System.Net;
using NotificationWindow;
using DropNet;
//using BrainTeaser;



namespace MOSCATO
{
    public partial class Form1 : Form
    {
        public delegate void ClosingEventHandler(object sender, EventArgs e);
        //public event ClosingEventHandler Closing;

        private BackgroundWorker bw = new BackgroundWorker();

        string name, curDir, curDirNoDrive, score1, score2, score3, score4, score5, score6;
        int puzzle = 0,  run_game = 0, arkanoid = 0, new_user = 0, ch_screen =0, ctr_message=0;
        //int nav = 0, arkanoid = 0, new_user = 0;
        int dropbox_user, dropbox_puzzle;
        String Url = string.Empty;
        //private string m_ExcelFileName = "C:\\Subhasree\\CODES\\CODES\\moscato\\Resources\\[MOSCATO] Crossword puzzle.xlsx";

        Timer myTimer = new Timer();
        Timer myTimer2 = new Timer();
        Timer myTimer3 = new Timer();
        Timer myTimer4 = new Timer();
        Timer myTimer5 = new Timer();

        DropNetClient _client;

        private Timer _timer;

        // The last time the timer was started
        private DateTime _startTime = DateTime.MinValue;

        // Time between now and when the timer was started last
        private TimeSpan _currentElapsedTime = TimeSpan.Zero;

        // Time between now and the first time timer was started after a reset
        public TimeSpan _totalElapsedTime = TimeSpan.Zero;

        public TimeSpan timeSinceStartTime = TimeSpan.Zero;
        // Whether or not the timer is currently running
        private bool _timerRunning = false;

        protected override void WndProc(ref Message m)
        {
            const int WM_NCLBUTTONDOWN = 161;
            const int WM_SYSCOMMAND = 274;
            const int HTCAPTION = 2;
            const int SC_MOVE = 61456;
            const int WM_PARENTNOTIFY = 0x210;
            const int WM_DESTROY = 2;

            if ((m.Msg == WM_SYSCOMMAND) && (m.WParam.ToInt32() == SC_MOVE))
            {
                return;
            }

            if ((m.Msg == WM_NCLBUTTONDOWN) && (m.WParam.ToInt32() == HTCAPTION))
            {
                return;
            }

            base.WndProc(ref m);

            if ((m.Msg == WM_PARENTNOTIFY) && (m.WParam.ToInt32() == WM_DESTROY))
            {
                //Closing(this, EventArgs.Empty);
                //DefWndProc(ref m);
                //base.WndProc(ref m);
                return;
            }


        }

        NotifyIcon notifyIcon = null;
        //private FormClosedEventHandler BtForm_FormClosed;

        public Form1()
        {
            InitializeComponent();
           // notifyIcon = new NotifyIcon();
            // Initializing notifyIcon here...
           // notifyIcon.BalloonTipClicked += new EventHandler(notifyIcon_BalloonTipClicked);
            //timer1.Start();
            timer4.Start();
            //StartTimer();
            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Start();
            _timer.Tick += new EventHandler(_timer_Tick);

            //_currentElapsedTime = _currentElapsedTime2;
            // PopMessage();
            curDir = Directory.GetCurrentDirectory().Replace("\\", "/");
            curDirNoDrive = curDir.Substring(Path.GetPathRoot(curDir).Length);
            //int width = 1300; //Screen.PrimaryScreen.Bounds.Width;
            //int height = 900; //Screen.PrimaryScreen.Bounds.Height;
            //this.Size = new System.Drawing.Size(width, height);
            ////this.MinimumSize = new Size(width, height);
            ////this.MaximumSize = new Size(width, height);
            //this.WindowState = FormWindowState.Maximized;
            //this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximumSize = new System.Drawing.Size(1200, 1200);
            //this.MinimumSize = new System.Drawing.Size(1005, 800);
            this.CenterToScreen();
            this.MaximizeBox = true;
            this.MinimizeBox = true;

            panel1.Visible = true;
            panel2.Visible = false;
            //panel3.Visible = false;

            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);

        }

        //void notifyIcon_BalloonTipClicked(object sender, EventArgs e)
        //{
        // Operation you want...
        //   System.Windows.Forms.MessageBox.Show("My message here");
        //}

        void _timer_Tick(object sender, EventArgs e)
        {
            timeSinceStartTime = DateTime.Now - _startTime;
            timeSinceStartTime = new TimeSpan(timeSinceStartTime.Hours,
                                              timeSinceStartTime.Minutes,
                                              timeSinceStartTime.Seconds);

            // The current elapsed time is the time since the start button was
            // clicked, plus the total time elapsed since the last reset
            //MessageBox.Show("Time Since Start Time :" + timeSinceStartTime.ToString());
            _currentElapsedTime = timeSinceStartTime + _totalElapsedTime;

            // These are just two Label controls which display the current 
            // elapsed time and total elapsed time
            //MessageBox.Show(_currentElapsedTime.ToString());
            lbl_timer.Text = timeSinceStartTime.ToString();
            //_totalElapsedTimeDisplay.Text = _currentElapsedTime.ToString();
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
                //if (dropbox_user == 1)
                //{
                //    _client = new DropNetClient("a1h5xkztcn59ps9", "oaci9hj0mjnkh9x", "lp9zbolaczthzmfj", "9v62zp3altu20g4");
                //    _client.UseSandbox = false;

                //    if (dropbox_puzzle == 1)
                //    {
                //        _client.UploadFile("/PUZZLE/" + this.name, "puzzle_01.png", File.ReadAllBytes(@"Resources\\config.dat"));
                //    }
                //    if (dropbox_puzzle == 2)
                //    {
                //        _client.UploadFile("/PUZZLE/" + this.name, "puzzle_02.png", File.ReadAllBytes(@"Resources\\packages.esx"));
                //    }
                //    if (dropbox_puzzle == 3)
                //    {
                //        _client.UploadFile("/PUZZLE/" + this.name, "puzzle_03.png", File.ReadAllBytes(@"Resources\\model.nxi"));
                //    }
                //    if (dropbox_puzzle == 4)
                //    {
                //        _client.UploadFile("/PUZZLE/" + this.name, "puzzle_04.png", File.ReadAllBytes(@"Resources\\assembly.dat"));
                //        _client.UploadFile("/PUZZLE/" + this.name, "puzzle_05.png", File.ReadAllBytes(@"Resources\\roboto.otg"));
                //    }
                //    if (dropbox_puzzle == 5)
                //    {
                //        _client.UploadFile("/PUZZLE/" + this.name, "puzzle_06.png", File.ReadAllBytes(@"Resources\\cert.dbx"));
                //    }

                //}
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

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((e.Cancelled == true))
            {
                //Console.WriteLine("Canceled!");
            }

            else if (!(e.Error == null))
            {
                //Console.WriteLine(("Error: " + e.Error.Message));
            }

            else
            {
                //Console.WriteLine("Done!");
            }
        }
        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //Console.WriteLine((e.ProgressPercentage.ToString() + "%"));
        }



        private void saveScores()
        {
            using (BinaryWriter writer = new BinaryWriter(File.OpenWrite("Users\\" + this.name + "\\scores.bin")))
            {
                writer.Write(score1);
                writer.Write(score2);
                writer.Write(score3);
                writer.Write(score4);
                writer.Write(score5);
                writer.Write(score6);
                writer.Write(puzzle);
            }
            //string curFile = @"c:\temp\test.txt";
           // bool b1 = File.Exists("Users\\" + this.name + "\\log.txt");
            //MessageBox.Show(b1.ToString);
            if (File.Exists("Users\\" + this.name + "\\log.txt"))
            {
                //MessageBox.Show("Exists!");
            }
            else
            {
                using (BinaryWriter writer2 = new BinaryWriter(File.OpenWrite("Users\\" + this.name + "\\log.txt")))
                {
                    DateTime now = DateTime.Now;
                    //Console.WriteLine(now);
                    string date1 = "";
                    date1 = date1 + now.ToString("hh.mm.ss.ffffff");
                    writer2.Write(date1 + " ");
                    writer2.Write(this.name + " ");
                    writer2.Write("login" + " ");
                    writer2.Write(System.Environment.NewLine);
                    //writer.Write(score3);
                    //writer.Write(score4);
                    //writer.Write(score5);
                    //writer.Write(puzzle);
                }
            }

            if (File.Exists("Users\\" + this.name + "\\log.csv"))
            {
                //MessageBox.Show("Exists!");
            }
            else
            {
                using (BinaryWriter writer2 = new BinaryWriter(File.OpenWrite("Users\\" + this.name + "\\log.csv")))
                {
                    DateTime now = DateTime.Now;
                    //Console.WriteLine(now);
                    string date1 = "";
                    date1 = date1 + now.ToString("hh.mm.ss.ffffff");
                    writer2.Write(date1 + ",");
                    writer2.Write(this.name + ",");
                    writer2.Write("login" + ",");
                    writer2.Write(System.Environment.NewLine);
                    //writer.Write(score3);
                    //writer.Write(score4);
                    //writer.Write(score5);
                    //writer.Write(puzzle);
                }
            }
        }

        private void readScores()
        {
            //MessageBox.Show("Inside read");
            using (FileStream fs = File.OpenRead("Users\\" + this.name + "\\scores.bin"))
            {
                BinaryReader reader = new BinaryReader(fs);
                score1 = reader.ReadString();
                score2 = reader.ReadString();
                score3 = reader.ReadString();
                score4 = reader.ReadString();
                score5 = reader.ReadString();
                score6 = reader.ReadString();
                //puzzle = reader.ReadInt32();
            }
         
            if (File.Exists("Users\\" + this.name + "\\log.txt"))
            {
               // MessageBox.Show("Exists!");
                using (System.IO.StreamWriter writer2 = File.AppendText("Users\\" + this.name + "\\log.txt"))
                {
                    DateTime now = DateTime.Now;
                    //Console.WriteLine(now);
                    string date1 = "";
                    date1 = date1 + now.ToString("hh.mm.ss.ffffff");
                    writer2.Write(date1 + " ");
                    writer2.Write(this.name + " ");
                    writer2.Write("login" + " ");
                    writer2.Write(System.Environment.NewLine);
                    //writer.Write(score3);
                    //writer.Write(score4);
                    //writer.Write(score5);
                    //writer.Write(puzzle);
                }
            }
            else
            {
                using (BinaryWriter writer2 = new BinaryWriter(File.OpenWrite("Users\\" + this.name + "\\log.txt")))
                {
                    DateTime now = DateTime.Now;
                    //Console.WriteLine(now);
                    string date1 = "";
                    date1 = date1 + now.ToString("hh.mm.ss.ffffff");
                    writer2.Write(date1 + " " );
                    writer2.Write(this.name + " " );
                    writer2.Write("login" + " ");
                    writer2.Write(System.Environment.NewLine);
                    //writer.Write(puzzle);
                }
            }

            if (File.Exists("Users\\" + this.name + "\\log.csv"))
            {
                // MessageBox.Show("Exists!");
                using (System.IO.StreamWriter writer2 = File.AppendText("Users\\" + this.name + "\\log.csv"))
                {
                    DateTime now = DateTime.Now;
                    //Console.WriteLine(now);
                    string date1 = "";
                    date1 = date1 + now.ToString("hh.mm.ss.ffffff");
                    writer2.Write(date1 + ",");
                    writer2.Write(this.name + ",");
                    writer2.Write("login" + ",");
                    writer2.Write(System.Environment.NewLine);
                    //writer.Write(score3);
                    //writer.Write(score4);
                    //writer.Write(score5);
                    //writer.Write(puzzle);
                }
            }
            else
            {
                using (BinaryWriter writer2 = new BinaryWriter(File.OpenWrite("Users\\" + this.name + "\\log.csv")))
                {
                    DateTime now = DateTime.Now;
                    //Console.WriteLine(now);
                    string date1 = "";
                    date1 = date1 + now.ToString("hh.mm.ss.ffffff");
                    writer2.Write(date1 + ",");
                    writer2.Write(this.name + ",");
                    writer2.Write("login" + ",");
                    writer2.Write(System.Environment.NewLine);
                    //writer.Write(puzzle);
                }
            }
        }

        private void updateScores()
        {
            //lblGame1.Text = string.Format("Crappy Bird: {0}", score1);
            //lblGame2.Text = string.Format("Hockey: {0}", score2);
            //lable1.Text = string.Format("Letters: {0}", score3);
            //lblGame4.Text = string.Format("Words: {0}", score4);
            //label3.Text = string.Format("Picture Story: {0}", score5);
            //MessageBox.Show("Update");
            int a = 0;
            if(score1 == "completed")
            {
                a++;
                dropbox_user = 1;
                dropbox_puzzle = 1;
                if (bw.IsBusy != true)
                {
                    bw.RunWorkerAsync();
                }
            }
            if (score2 == "completed")
            {
                a++;
                dropbox_user = 1;
                dropbox_puzzle = 2;
                if (bw.IsBusy != true)
                {
                    bw.RunWorkerAsync();
                }
            }
            if (score3 == "completed")
            {
                a++;
                dropbox_user = 1;
                dropbox_puzzle = 3;
                if (bw.IsBusy != true)
                {
                    bw.RunWorkerAsync();
                }

            }
            if (score4 == "completed")
            {
                a++;
                a++;
                dropbox_user = 1;
                dropbox_puzzle = 4;
                if (bw.IsBusy != true)
                {
                    bw.RunWorkerAsync();
                }
            }
            if (score5 == "completed")
            {
                a++;
                dropbox_user = 1;
                dropbox_puzzle = 5;
                if(bw.IsBusy != true)
                {
                    bw.RunWorkerAsync();
                }
            }

            puzzle = a;
            //readScores();
            lblPuzzle.Text = string.Format("Puzzle parts: {0}/6", puzzle);
            //MessageBox.Show(score4);
              
            if (score4 == "not completed")
            {
                ch_screen = 1;
            }
            else
            {
                if ((score3 == "not completed") && (score5 == "not completed") && (score1 == "not completed") && (score2 == "not completed"))
                {
                    ch_screen = 2;
                }
                else
                {
                    if ((score3 == "completed") && (score5 == "not completed") && (score1 == "not completed") && (score2 == "not completed"))
                    {
                        ch_screen = 3;
                    }
                    else
                    {
                        if ((score3 == "not completed") && (score5 == "completed") && (score1 == "not completed") && (score2 == "not completed"))
                        {
                            ch_screen = 4;
                        }
                        else
                        {
                            if ((score3 == "completed") && (score5 == "completed") && (score1 == "not completed") && (score2 == "not completed"))
                            {
                                ch_screen = 5;
                            }
                            else
                            {
                                if ((score3 == "completed") && (score5 == "completed") && (score1 == "completed") && (score2 == "not completed"))
                                {
                                    ch_screen = 6;
                                }
                                else
                                {
                                    if ((score3 == "completed") && (score5 == "completed") && (score1 == "not completed") && (score2 == "completed"))
                                    {
                                        ch_screen = 7;
                                    }
                                    else
                                    {
                                        if ((score3 == "completed") && (score5 == "completed") && (score1 == "completed") && (score2 == "completed"))
                                        { ch_screen = 8; }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //String path_dir = System.IO.Directory.GetCurrentDirectory();
            //string ch_Scr = ch_screen;
            switch (ch_screen)
            {
                case 1:
                    //MessageBox.Show(ch_screen.ToString());
                    pictureBox1.Visible = true;
                   // pictureBox1.Image = Image.FromFile("Resources\\Map_One Colour_1.png");
                    pictureBox1.Image = Image.FromFile("Resources\\Map_One Colour_1.png");
                    //pictureBox2.Image = Image.FromFile(@"C:\Subhasree\CODES\CODES\moscato\Resources\Progress Bar_0.png");
                    pictureBox2.Image = Image.FromFile("Resources\\Progress Bar_0.png");
                    //pictureBox3.Image = Image.FromFile("Resources\\puzzle_1N2_Fotor_Collage.png");
                    pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                    button1.BackgroundImage = Image.FromFile("Resources\\Icon_1_Incomplete.png");
                    button1.BackgroundImageLayout = ImageLayout.Stretch;
                    button2.BackgroundImage = Image.FromFile("Resources\\Icon_5_Gray.png");
                    button2.BackgroundImageLayout = ImageLayout.Stretch;
                    button3.BackgroundImage = Image.FromFile("Resources\\Icon_6_Gray.png");
                    button3.BackgroundImageLayout = ImageLayout.Stretch;
                    button_letters.BackgroundImage = Image.FromFile("Resources\\Icon_3_Gray.png");
                    button_letters.BackgroundImageLayout = ImageLayout.Stretch;
                    button_pic_story.BackgroundImage = Image.FromFile("Resources\\Icon_2_Gray.png");
                    button_pic_story.BackgroundImageLayout = ImageLayout.Stretch;
                    button_game4.BackgroundImage = Image.FromFile("Resources\\Icon_4_Gray.png");
                    button_game4.BackgroundImageLayout = ImageLayout.Stretch;
                    button1.Enabled = true;
                    button2.Enabled = false;
                    button3.Enabled = false;
                    button_letters.Enabled = false;
                    button_game4.Enabled = false;
                    button_pic_story.Enabled = false;
                    txtQuestion.Enabled = false;
                    btnAnswer.Enabled = false;
                    break;
                case 2:
                    //MessageBox.Show(ch_screen.ToString());
                    pictureBox1.Visible = true;
                    pictureBox1.Image = Image.FromFile("Resources\\Map_One Colour_2.png");
                    pictureBox2.Image = Image.FromFile("Resources\\Progress Bar_1.png");
                    pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox3.Image = Image.FromFile("Resources\\puzzle_1N6.png");
                    pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
                    button1.BackgroundImage = Image.FromFile("Resources\\Icon_1_Complete.png");
                    button1.BackgroundImageLayout = ImageLayout.Stretch;
                    button2.BackgroundImage = Image.FromFile("Resources\\Icon_5_Gray.png");
                    button2.BackgroundImageLayout = ImageLayout.Stretch;
                    button3.BackgroundImage = Image.FromFile("Resources\\Icon_6_Gray.png");
                    button3.BackgroundImageLayout = ImageLayout.Stretch;
                    button_letters.BackgroundImage = Image.FromFile("Resources\\Icon_3_Incomplete.png");
                    button_letters.BackgroundImageLayout = ImageLayout.Stretch;
                    button_pic_story.BackgroundImage = Image.FromFile("Resources\\Icon_2_Incomplete.png");
                    button_pic_story.BackgroundImageLayout = ImageLayout.Stretch;
                    button_game4.BackgroundImage = Image.FromFile("Resources\\Icon_4_Gray.png");
                    button_game4.BackgroundImageLayout = ImageLayout.Stretch;
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = false;
                    button_letters.Enabled = true;
                    button_game4.Enabled = false;
                    button_pic_story.Enabled = true;
                    break;
                case 3:
                    //MessageBox.Show(ch_screen.ToString());
                    pictureBox1.Image = Image.FromFile("Resources\\Map_One Colour_2.png");
                    pictureBox2.Image = Image.FromFile("Resources\\Progress Bar_2.png");
                    pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox3.Image = Image.FromFile("Resources\\puzzle_1N6N2.png");
                    pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
                    button1.BackgroundImage = Image.FromFile("Resources\\Icon_1_Complete.png");
                    button1.BackgroundImageLayout = ImageLayout.Stretch;
                    button2.BackgroundImage = Image.FromFile("Resources\\Icon_5_Gray.png");
                    button2.BackgroundImageLayout = ImageLayout.Stretch;
                    button3.BackgroundImage = Image.FromFile("Resources\\Icon_6_Gray.png");
                    button3.BackgroundImageLayout = ImageLayout.Stretch;
                    button_letters.BackgroundImage = Image.FromFile("Resources\\Icon_3_Complete.png");
                    button_letters.BackgroundImageLayout = ImageLayout.Stretch;
                    button_pic_story.BackgroundImage = Image.FromFile("Resources\\Icon_2_Incomplete.png");
                    button_pic_story.BackgroundImageLayout = ImageLayout.Stretch;
                    button_game4.BackgroundImage = Image.FromFile("Resources\\Icon_4_Gray.png");
                    button_game4.BackgroundImageLayout = ImageLayout.Stretch;
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = false;
                    button_letters.Enabled = false;
                    button_game4.Enabled = false;
                    button_pic_story.Enabled = true;
                    txtQuestion.Enabled = false;
                    btnAnswer.Enabled = false;
                    break;
                case 4:
                    //MessageBox.Show(ch_screen.ToString());
                    pictureBox1.Image = Image.FromFile("Resources\\Map_One Colour_2.png");
                    pictureBox2.Image = Image.FromFile("Resources\\Progress Bar_2.png");
                    pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox3.Image = Image.FromFile("Resources\\puzzle_1N6N4.png");
                    pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
                    button1.BackgroundImage = Image.FromFile("Resources\\Icon_1_Complete.png");
                    button1.BackgroundImageLayout = ImageLayout.Stretch;
                    button2.BackgroundImage = Image.FromFile("Resources\\Icon_5_Gray.png");
                    button2.BackgroundImageLayout = ImageLayout.Stretch;
                    button3.BackgroundImage = Image.FromFile("Resources\\Icon_6_Gray.png");
                    button3.BackgroundImageLayout = ImageLayout.Stretch;
                    button_letters.BackgroundImage = Image.FromFile("Resources\\Icon_3_Incomplete.png");
                    button_letters.BackgroundImageLayout = ImageLayout.Stretch;
                    button_pic_story.BackgroundImage = Image.FromFile("Resources\\Icon_2_Complete.png");
                    button_pic_story.BackgroundImageLayout = ImageLayout.Stretch;
                    button_game4.BackgroundImage = Image.FromFile("Resources\\Icon_4_Gray.png");
                    button_game4.BackgroundImageLayout = ImageLayout.Stretch;
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = false;
                    button_letters.Enabled = true;
                    button_game4.Enabled = false;
                    button_pic_story.Enabled = false;
                    txtQuestion.Enabled = false;
                    btnAnswer.Enabled = false;
                    break;
                case 5:
                   // MessageBox.Show(ch_screen.ToString());
                    pictureBox1.Image = Image.FromFile("Resources\\Map_One Colour_Full.png");
                    pictureBox2.Image = Image.FromFile("Resources\\Progress Bar_3.png");
                    pictureBox3.Image = Image.FromFile("Resources\\puzzle_1N6N2N4.png");
                    pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                    button1.BackgroundImage = Image.FromFile("Resources\\Icon_1_Complete.png");
                    button1.BackgroundImageLayout = ImageLayout.Stretch;
                    button2.BackgroundImage = Image.FromFile("Resources\\Icon_5_Incomplete.png");
                    button2.BackgroundImageLayout = ImageLayout.Stretch;
                    button3.BackgroundImage = Image.FromFile("Resources\\Icon_6_Incomplete.png");
                    button3.BackgroundImageLayout = ImageLayout.Stretch;
                    button_letters.BackgroundImage = Image.FromFile("Resources\\Icon_3_Complete.png");
                    button_letters.BackgroundImageLayout = ImageLayout.Stretch;
                    button_pic_story.BackgroundImage = Image.FromFile("Resources\\Icon_2_Complete.png");
                    button_pic_story.BackgroundImageLayout = ImageLayout.Stretch;
                    //button_game4.BackgroundImage = Image.FromFile("Resources\\Icon_4_Incomplete.png");
                    //button_game4.BackgroundImageLayout = ImageLayout.Stretch;
                    button1.Enabled = false;
                    button2.Enabled = true;
                    button3.Enabled = true;
                    button_letters.Enabled = false;
                    //button_game4.Enabled = true;
                    //button_pic_story.Enabled = false;
                    if (score6 == "completed")
                    {
                        button_game4.BackgroundImage = Image.FromFile("Resources\\Icon_4_Complete.png");
                        button_game4.BackgroundImageLayout = ImageLayout.Stretch;
                        button_game4.Enabled = false;
                    }
                    else
                    {
                        button_game4.BackgroundImage = Image.FromFile("Resources\\Icon_4_Incomplete.png");
                        button_game4.BackgroundImageLayout = ImageLayout.Stretch;
                        button_game4.Enabled = true;
                    }
                    txtQuestion.Enabled = true;
                    btnAnswer.Enabled = true;
                    break;
                case 6:
                    //MessageBox.Show(ch_screen.ToString());
                    try
                    {
                        pictureBox1.Image = Image.FromFile("Resources\\Map_One Colour_Full.png");
                        pictureBox2.Image = Image.FromFile("Resources\\Progress Bar_4.png");
                        pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                        pictureBox3.Image = Image.FromFile("Resources\\puzzle_1N6N2N4N3.png");
                        pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
                        button1.BackgroundImage = Image.FromFile("Resources\\Icon_1_Complete.png");
                        button1.BackgroundImageLayout = ImageLayout.Stretch;
                        button2.BackgroundImage = Image.FromFile("Resources\\Icon_5_Incomplete.png");
                        button2.BackgroundImageLayout = ImageLayout.Stretch;
                        button3.BackgroundImage = Image.FromFile("Resources\\Icon_6_Complete.png");
                        button3.BackgroundImageLayout = ImageLayout.Stretch;
                        button_letters.BackgroundImage = Image.FromFile("Resources\\Icon_3_Complete.png");
                        button_letters.BackgroundImageLayout = ImageLayout.Stretch;
                        button_pic_story.BackgroundImage = Image.FromFile("Resources\\Icon_2_Complete.png");
                        button_pic_story.BackgroundImageLayout = ImageLayout.Stretch;
                        //button_game4.BackgroundImage = Image.FromFile("Resources\\Icon_4_Incomplete.png");
                        //button_game4.BackgroundImageLayout = ImageLayout.Stretch;
                        button1.Enabled = false;
                        button2.Enabled = true;
                        button3.Enabled = false;
                        button_letters.Enabled = false;
                        //button_game4.Enabled = true;
                        button_pic_story.Enabled = false;
                        if (score6 == "completed")
                        {
                            button_game4.BackgroundImage = Image.FromFile("Resources\\Icon_4_Complete.png");
                            button_game4.BackgroundImageLayout = ImageLayout.Stretch;
                            button_game4.Enabled = false;
                        }
                        else
                        {
                            button_game4.BackgroundImage = Image.FromFile("Resources\\Icon_4_Incomplete.png");
                            button_game4.BackgroundImageLayout = ImageLayout.Stretch;
                            button_game4.Enabled = true;
                        }
                        break;
                    }
                    catch
                    {
                        MessageBox.Show("Exception");
                    }
                    break;
                    
                    
                case 7:
                    //MessageBox.Show(ch_screen.ToString());
                    pictureBox1.Image = Image.FromFile("Resources\\Map_One Colour_Full.png");
                    pictureBox2.Image = Image.FromFile("Resources\\Progress Bar_4.png");
                    pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox3.Image = Image.FromFile("Resources\\puzzle_1N6N2N4N5.png");
                    pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
                    button1.BackgroundImage = Image.FromFile("Resources\\Icon_1_Complete.png");
                    button1.BackgroundImageLayout = ImageLayout.Stretch;
                    button2.BackgroundImage = Image.FromFile("Resources\\Icon_5_Complete.png");
                    button2.BackgroundImageLayout = ImageLayout.Stretch;
                    button3.BackgroundImage = Image.FromFile("Resources\\Icon_6_Incomplete.png");
                    button3.BackgroundImageLayout = ImageLayout.Stretch;
                    button_letters.BackgroundImage = Image.FromFile("Resources\\Icon_3_Complete.png");
                    button_letters.BackgroundImageLayout = ImageLayout.Stretch;
                    button_pic_story.BackgroundImage = Image.FromFile("Resources\\Icon_2_Complete.png");
                    button_pic_story.BackgroundImageLayout = ImageLayout.Stretch;
                    //button_game4.BackgroundImage = Image.FromFile("Resources\\Icon_4_Incomplete.png");
                    //button_game4.BackgroundImageLayout = ImageLayout.Stretch;
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = true;
                    button_letters.Enabled = false;
                    //button_game4.Enabled = true;
                    button_pic_story.Enabled = false;
                    if (score6 == "completed")
                    {
                        button_game4.BackgroundImage = Image.FromFile("Resources\\Icon_4_Complete.png");
                        button_game4.BackgroundImageLayout = ImageLayout.Stretch;
                        button_game4.Enabled = false;
                    }
                    else
                    {
                        button_game4.BackgroundImage = Image.FromFile("Resources\\Icon_4_Incomplete.png");
                        button_game4.BackgroundImageLayout = ImageLayout.Stretch;
                        button_game4.Enabled = true;
                    }
                    break;
                    break;
                case 8:
                    //MessageBox.Show(ch_screen.ToString());
                    pictureBox1.Image = Image.FromFile("Resources\\Map_One Colour_Full.png");
                    pictureBox2.Image = Image.FromFile("Resources\\Progress Bar_5.png");
                    pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox3.Image = Image.FromFile("Resources\\puzzle_all.png");
                    pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
                    button1.BackgroundImage = Image.FromFile("Resources\\Icon_1_Complete.png");
                    button1.BackgroundImageLayout = ImageLayout.Stretch;
                    button2.BackgroundImage = Image.FromFile("Resources\\Icon_5_Complete.png");
                    button2.BackgroundImageLayout = ImageLayout.Stretch;
                    button3.BackgroundImage = Image.FromFile("Resources\\Icon_6_Complete.png");
                    button3.BackgroundImageLayout = ImageLayout.Stretch;
                    button_letters.BackgroundImage = Image.FromFile("Resources\\Icon_3_Complete.png");
                    button_letters.BackgroundImageLayout = ImageLayout.Stretch;
                    button_pic_story.BackgroundImage = Image.FromFile("Resources\\Icon_2_Complete.png");
                    button_pic_story.BackgroundImageLayout = ImageLayout.Stretch;
                    //button_game4.BackgroundImage = Image.FromFile("Resources\\Icon_4_Incomplete.png");
                    //button_game4.BackgroundImageLayout = ImageLayout.Stretch;
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = false;
                    button_letters.Enabled = false;
                    //button_game4.Enabled = true;
                    button_pic_story.Enabled = false;
                    btnSolvePuzzle.Visible = true;
                    btnSolvePuzzle.Enabled = true;
                    if (score6 == "completed")
                    {
                        button_game4.BackgroundImage = Image.FromFile("Resources\\Icon_4_Complete.png");
                        button_game4.BackgroundImageLayout = ImageLayout.Stretch;
                        button_game4.Enabled = false;
                    }
                    else
                    {
                        button_game4.BackgroundImage = Image.FromFile("Resources\\Icon_4_Incomplete.png");
                        button_game4.BackgroundImageLayout = ImageLayout.Stretch;
                        button_game4.Enabled = true;
                    }
                    break;
                default:
                    break;
            }
        }

        public void gotoSite(string url)
        {
            System.Diagnostics.Process.Start(url);
        }


        private void btnStart_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtLogin.Text))
            {
                MessageBox.Show("Login name can not be empty");
                return;
            }
            this.name = txtLogin.Text;
            panel1.Visible = false;
            panel2.Visible = true;
            if (!_timerRunning)
            {
                // Set the start time to Now
                _startTime = DateTime.Now;

                // Store the total elapsed time so far
                _totalElapsedTime = _currentElapsedTime;

                _timer.Start();
                _timerRunning = true;
            }
            else // If the timer is already running
            {
                _timer.Stop();
                _timerRunning = false;
            }
            //panel3.Visible = false ;
            panel2.Controls.Add(lbl_timer);
            ////browser.Navigate("http://www.google.com");
            //Url = "https://docs.google.com/forms/d/e/1FAIpQLScLOPiY8pl_YeGpLj4DrGd1ZZP-GHNimrREdeqiA_8eDnfnoA/viewform";
            //gotoSite(Url);
            //this.SendToBack();
            //string Url = "https://docs.google.com/forms/d/e/1FAIpQLScLOPiY8pl_YeGpLj4DrGd1ZZP-GHNimrREdeqiA_8eDnfnoA/viewform";
            //WebBrowserTrial_2.Form1 WBTrial = new WebBrowserTrial_2.Form1(Url, this.name);
            //WBTrial.FormClosed += new FormClosedEventHandler(WBTrial_FormClosed);
            //this.Visible = false;
            //WBTrial.Show();

            //foreach (Control ctrl in panel2.Controls)
            //{
            //    MessageBox.Show(ctrl.Name);
            //    //if (ctrl.Name == "lbl_timer")
            //    //{
            //    //    // check ctrl.Name
            //    //    MessageBox.Show(ctrl.Name);
            //    //}
            //    //else
            //    //{
            //    //    MessageBox.Show(ctrl.Name);
            //    //    panel2.Controls.Add(lbl_timer);
            //    //}
            //}

            if (!System.IO.Directory.Exists("Users\\" + this.name))
            {
                System.IO.Directory.CreateDirectory("Users\\" + this.name);
                this.new_user = 1;
                score1 = "not completed";
                score2 = "not completed";
                score3 = "not completed";
                score4 = "not completed";
                score5 = "not completed";
                score6 = "not completed";
                saveScores();
            }
            else
            {
                readScores();
            }

            updateScores();
            //System.Windows.Forms.MessageBox.Show("My message here");
            //showTextBaloonTooltip("Collect the puzzle parts. Solve the puzzle and answer the question!", 10, 5000);

            ///////////////// START ///////////////
            /*
            if (bw.IsBusy != true)
            {
                bw.RunWorkerAsync();
            }
            */
            ///////////////// CANCEL //////////////
            /*
            if (bw.WorkerSupportsCancellation == true)
            {
                bw.CancelAsync();
            }
            */
            ///////////////////////////////////////

            dropbox_user = 2;
            dropbox_puzzle = 3;
            myTimer.Dispose();
            myTimer = new Timer();
            myTimer.Interval = 11000;
            myTimer.Tick += new EventHandler(timer_Dropbox);
            myTimer.Start();

        }


        void TimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            showTextBaloonTooltip("Faster, faster!", 12, 3000);
        }
        
        //private void btnPlayGame1_Click(object sender, EventArgs e)
        //{
        //    string tekst = "This is an instruction how to use the game \"Crappy Bird\"." + Environment.NewLine + "Use left mouse button (click several times) to keep a bird in the air. Try to collect as many coins as possible.";
        //    Introduction.Form1 Intro = new Introduction.Form1(tekst);
        //    Intro.FormClosed += new FormClosedEventHandler(Intro_FormClosed1);
        //    this.Visible = false;
        //    Intro.Show();

        //}

        //private void btnPlayGame2_Click(object sender, EventArgs e)
        //{

        //    string tekst = "This is an instruction how to use the game \"Hockey\"." + Environment.NewLine + "You can use up/down arrows to move your stick (on the left side). Try to win with computer.";
        //    Introduction.Form1 Intro = new Introduction.Form1(tekst);
        //    Intro.FormClosed += new FormClosedEventHandler(Intro_FormClosed2);
        //    this.Visible = false;
        //    Intro.Show();

        //}


        private void btnSolvePuzzle_Click(object sender, EventArgs e)
        {
            //string tekst = "Load all collected puzzles and try to put all pieces in the proper order. Read it carefully and answer the question at the main screen.";
            Introduction.Form1 Intro = new Introduction.Form1("User\\" + name);
            Intro.FormClosed += new FormClosedEventHandler(Intro_FormClosed3);
            this.Visible = false;
            Intro.Show();
        }

        //void Intro_FormClosed1(object sender, FormClosedEventArgs e)
        //{
        //    MessageBox.Show("Intro");
        //    Url = "http://www.websudoku.com/?select=1&level=2";
        //    panel1.Visible = false;
        //    panel2.Visible = false;
        //    //panel3.Visible = true;
        //    //webBrowser1.Navigate(Url);
        //    //this.Show();
        //    //this.axWebBrowser1.Navigate(@"http://www.websudoku.com/?select=1&level=2");
        //    //this.axWebBrowser1.Navigate(String.Format(@"file://127.0.0.1/c$/{0}/Games/CrappyBird/index.html", curDirNoDrive));
        //    //this.axWebBrowser1.Navigate(String.Format(@"file://127.0.0.1/c$/{0}/Games/Tron/index.html", curDirNoDrive));
        //    //this.axWebBrowser1.Navigate(String.Format(@"file://127.0.0.1/c$/{0}/Games/Hextris/index.html", curDirNoDrive));
        //    //this.axWebBrowser1.Navigate(String.Format(@"file://127.0.0.1/c$/{0}/Games/Orbium/index.html", curDirNoDrive));
        //    //this.axWebBrowser1.Silent = true;
        //    run_game = 1;
        //    score1 = "completed";
        //    puzzle = 1;

        //    myTimer.Dispose();
        //    myTimer = new Timer();
        //    myTimer.Interval = 5000;
        //    myTimer.Tick += new EventHandler(timer_BaloonTooltip);
        //    myTimer.Start();
        //    arkanoid = 1;
        //}

        void Intro_FormClosed2(object sender, FormClosedEventArgs e)
        {
            score2 = "completed";
            updateScores();

            Pingpong.Form1 Pingpong = new Pingpong.Form1(name);
            Pingpong.FormClosed += new FormClosedEventHandler(Pingpong_FormClosed);
            this.Visible = false;
            Pingpong.Show();

            //this.axWebBrowser1.Navigate(String.Format(@"file://127.0.0.1/c$/{0}/Games/Tron/index.html", curDirNoDrive));
            //this.axWebBrowser1.Navigate(String.Format(@"file://127.0.0.1/c$/{0}/Games/Hextris/index.html", curDirNoDrive));
            //this.axWebBrowser1.Navigate(String.Format(@"file://127.0.0.1/c$/{0}/Games/Orbium/index.html", curDirNoDrive));
            /*
            this.axWebBrowser1.Silent = true;
            run_game = 1;
            score2 = 5;
            puzzle = 2;
            updateScores();
            */

            ///myTimer.Dispose();
            ///myTimer = new Timer();
            ///myTimer.Interval = 10000;
            ///myTimer.Tick += new EventHandler(timer_Dropbox1);
            ///myTimer.Start();

            //myTimer2 = new Timer();
            ///myTimer2.Interval = 30000;
            ///myTimer2.Tick += new EventHandler(timer_Close);
            ///myTimer2.Start();
            ///

            dropbox_user = 3;
            dropbox_puzzle = 1;
            myTimer.Dispose();
            myTimer = new Timer();
            myTimer.Interval = 11000;
            myTimer.Tick += new EventHandler(timer_Dropbox);
            myTimer.Start();

        }

        void Intro_FormClosed3(object sender, FormClosedEventArgs e)
        {
            PictureTiles.Form1 Puzzle = new PictureTiles.Form1(Directory.GetCurrentDirectory() + "\\Users\\" + name);
            //JigsawPuzzle.MainForm Puzzle = new JigsawPuzzle.MainForm();
            Puzzle.FormClosed += new FormClosedEventHandler(Puzzle_FormClosed);
            this.Visible = false;

            Puzzle.Show();
            ///myTimer.Dispose();
            ///myTimer = new Timer();
            ///myTimer.Interval = 11000;
            ///myTimer.Tick += new EventHandler(timer_Dropbox2);
            ///myTimer.Start();
            //this.Show();
        }


        void Pingpong_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
            saveScores();
        }

        void Puzzle_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
            saveScores();
        }

        void Letters_FormClosed(object sender, FormClosedEventArgs e)
        {
            //_client = new DropNetClient("a1h5xkztcn59ps9", "oaci9hj0mjnkh9x", "lp9zbolaczthzmfj", "9v62zp3altu20g4");
            //_client.UseSandbox = false;
            //C:\Subhasree\CODES\MOSCATO2\moscato\bin\Release\PUZZLE
            string dir = System.IO.Directory.GetCurrentDirectory();
            string fileToCopy1 = dir + "\\Resources\\PUZZLE\\puzzle_02.png";
            //string fileToCopy2 = dir + "\\Resources\\PUZZLE\\puzzle_06.png";
            //String server = Environment.UserName;
            string newLocation1 = dir + "\\PUZZLE\\" + this.name + "\\puzzle_02.png";
            //string newLocation2 = dir + "\\PUZZLE\\" + this.name + "\\puzzle_06.png";
            string folderLocation = dir + "\\PUZZLE\\" + this.name;
            //MessageBox.Show(folderLocation);
            bool exists = System.IO.Directory.Exists(folderLocation);

            if (!exists)
            {
                System.IO.Directory.CreateDirectory(folderLocation);
                if (System.IO.File.Exists(fileToCopy1))
                {
                    //MessageBox.Show("file copied");
                    System.IO.File.Copy(fileToCopy1, newLocation1, true);
                    //System.IO.File.Copy(fileToCopy2, newLocation2, true);

                }
                else
                {
                    //MessageBox.Show(fileToCopy1);
                    MessageBox.Show("no such files");

                }
            }
            else
            {
                if (System.IO.File.Exists(fileToCopy1))
                {
                    //MessageBox.Show("file copied");
                    System.IO.File.Copy(fileToCopy1, newLocation1, true);
                    //System.IO.File.Copy(fileToCopy2, newLocation2, true);

                }
                else
                {
                    //MessageBox.Show(fileToCopy1);
                    MessageBox.Show("no such files");

                }
            }

            //File.Copy("Release\\Resources\\PUZZLE\\puzzle_02.png", "Release\\PUZZLE\\"+ this.name);
            //_client.UploadFile("/PUZZLE/" + this.name, "puzzle_02.png", File.ReadAllBytes(@"Resources\\packages.esx"));
            this.Show();
            this.SendToBack();
            //timer2.Start();
            saveScores();
        }

        void Words_FormClosed(object sender, FormClosedEventArgs e)
        {
            //_client = new DropNetClient("a1h5xkztcn59ps9", "oaci9hj0mjnkh9x", "lp9zbolaczthzmfj", "9v62zp3altu20g4");
            //_client.UseSandbox = false;
            //_client.UploadFile("/PUZZLE/" + this.name, "puzzle_01.png", File.ReadAllBytes(@"Resources\\config.dat"));
            //_client.UploadFile("/PUZZLE/" + this.name, "puzzle_06.png", File.ReadAllBytes(@"Resources\\cert.dbx"));
            string dir = System.IO.Directory.GetCurrentDirectory();
            string fileToCopy1 = dir + "\\Resources\\PUZZLE\\puzzle_01.png";
            string fileToCopy2 = dir + "\\Resources\\PUZZLE\\puzzle_06.png";
            //String server = Environment.UserName;
            string newLocation1 = dir + "\\PUZZLE\\" + this.name + "\\puzzle_01.png";
            string newLocation2 = dir + "\\PUZZLE\\" + this.name + "\\puzzle_06.png";
            string folderLocation = dir + "\\PUZZLE\\" + this.name;
            //MessageBox.Show(folderLocation);
            bool exists = System.IO.Directory.Exists(folderLocation);

            if (!exists)
            {
                System.IO.Directory.CreateDirectory(folderLocation);
                if (System.IO.File.Exists(fileToCopy1))
                {
                    //MessageBox.Show("file copied");
                    System.IO.File.Copy(fileToCopy1, newLocation1, true);
                    System.IO.File.Copy(fileToCopy2, newLocation2, true);

                }
                else
                {
                    //MessageBox.Show(fileToCopy1);
                    MessageBox.Show("no such files");

                }
            }

            this.Show();
            this.SendToBack();
            timer2.Start();
            saveScores();
        }

        void Picture_FormClosed(object sender, FormClosedEventArgs e)
        {
            //_client = new DropNetClient("a1h5xkztcn59ps9", "oaci9hj0mjnkh9x", "lp9zbolaczthzmfj", "9v62zp3altu20g4");
            //_client.UseSandbox = false;
            //_client.UploadFile("/PUZZLE/" + this.name, "puzzle_04.png", File.ReadAllBytes(@"Resources\\assembly.dat"));
            string dir = System.IO.Directory.GetCurrentDirectory();
            string fileToCopy1 = dir + "\\Resources\\PUZZLE\\puzzle_04.png";
            //string fileToCopy2 = dir + "\\Resources\\PUZZLE\\puzzle_06.png";
            //String server = Environment.UserName;
            string newLocation1 = dir + "\\PUZZLE\\" + this.name + "\\puzzle_04.png";
            //string newLocation2 = dir + "\\PUZZLE\\" + this.name + "\\puzzle_06.png";
            string folderLocation = dir + "\\PUZZLE\\" + this.name;
            //MessageBox.Show(folderLocation);
            bool exists = System.IO.Directory.Exists(folderLocation);

            if (!exists)
            {
                System.IO.Directory.CreateDirectory(folderLocation);
                if (System.IO.File.Exists(fileToCopy1))
                {
                    //MessageBox.Show("file copied");
                    System.IO.File.Copy(fileToCopy1, newLocation1, true);
                    //System.IO.File.Copy(fileToCopy2, newLocation2, true);

                }
                else
                {
                    //MessageBox.Show(fileToCopy1);
                    MessageBox.Show("no such files");

                }
            }
            else
            {
                if (System.IO.File.Exists(fileToCopy1))
                {
                    //MessageBox.Show("file copied");
                    System.IO.File.Copy(fileToCopy1, newLocation1, true);
                    //System.IO.File.Copy(fileToCopy2, newLocation2, true);

                }
                else
                {
                    //MessageBox.Show(fileToCopy1);
                    MessageBox.Show("no such files");

                }
            }
            this.Show();
            this.SendToBack();
            var result = MessageBox.Show("You have received one puzzle piece. Do you want to go to folder?", "Puzzle Reward", MessageBoxButtons.OK);
            if (result == DialogResult.OK)
            {
                string dir1 = System.IO.Directory.GetCurrentDirectory();
                string folderLocation1 = dir1 + "\\PUZZLE\\" ;
                //C: \Users\Geraldine\Dropbox
                //string path = @"C:\Users\user\Dropbox\PUZZLE";
                string path = folderLocation1;
                //string path = @"C:\Users\Geraldine\Dropbox\PUZZLE";
                System.Diagnostics.Process.Start(path);
                // Closes the parent form.
                //this.Close();
            }
            //timer2.Start();
            saveScores();
        }

        
        void WBTrial_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Refresh();
            //_client = new DropNetClient("a1h5xkztcn59ps9", "oaci9hj0mjnkh9x", "lp9zbolaczthzmfj", "9v62zp3altu20g4");
           // _client.UseSandbox = false;
            //MessageBox.Show("Inside form closed");
            //MessageBox.Show(Url);
            //MessageBox.Show(score2);
            string dir = System.IO.Directory.GetCurrentDirectory();
            string fileToCopy1="";
            string newLocation1="";
            if (score2 == "completed")
            {
              //  MessageBox.Show("Inside sudoku closed");
                //_client.UploadFile("/PUZZLE/" + this.name, "puzzle_05.png", File.ReadAllBytes(@"Resources\\roboto.otg"));
                fileToCopy1 = dir + "\\Resources\\PUZZLE\\puzzle_05.png";
                newLocation1 = dir + "\\PUZZLE\\" + this.name + "\\puzzle_05.png";
            }
            if (score1 == "completed")
            {
                //MessageBox.Show("Inside crossword closed");
                //_client.UploadFile("/PUZZLE/" + this.name, "puzzle_03.png", File.ReadAllBytes(@"Resources\\model.nxi"));
                fileToCopy1 = dir + "\\Resources\\PUZZLE\\puzzle_03.png";
                newLocation1 = dir + "\\PUZZLE\\" + this.name + "\\puzzle_03.png";
            }
            //string fileToCopy2 = dir + "\\Resources\\PUZZLE\\puzzle_06.png";
            //String server = Environment.UserName;
            //string newLocation2 = dir + "\\PUZZLE\\" + this.name + "\\puzzle_06.png";
            string folderLocation = dir + "\\PUZZLE\\" + this.name;
            //MessageBox.Show(folderLocation);
            bool exists = System.IO.Directory.Exists(folderLocation);

            if (!exists)
            {
                System.IO.Directory.CreateDirectory(folderLocation);
                if (System.IO.File.Exists(fileToCopy1))
                {
                    //MessageBox.Show("file copied");
                    System.IO.File.Copy(fileToCopy1, newLocation1, true);
                    //System.IO.File.Copy(fileToCopy2, newLocation2, true);

                }
                else
                {
                    //MessageBox.Show(fileToCopy1);
                    MessageBox.Show("no such files");

                }
            }
            else
            {
                if (System.IO.File.Exists(fileToCopy1))
                {
                    //MessageBox.Show("file copied");
                    System.IO.File.Copy(fileToCopy1, newLocation1, true);
                    //System.IO.File.Copy(fileToCopy2, newLocation2, true);

                }
                else
                {
                    //MessageBox.Show(fileToCopy1);
                    MessageBox.Show("no such files");

                }
            }
            this.Show();
            this.SendToBack();
            //timer2.Start();
            this.Visible = true;
            panel2.Visible = true;
            saveScores();
        }

        private void showTextBaloonTooltip(string contentText, int fontSize, int windowDelay)
        {
            popupNotifier1.TitleText = "";
            //popupNotifier1.
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
            //popupNotifier1.Size = new Size(500, 100); //Size of the window
            popupNotifier1.ContentFont = new Font("Georgia", fontSize, FontStyle.Bold);
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

        private void showTextBaloonToolDropbox(string contentText, int fontSize, int windowDelay)
        {
            popupNotifier1.TitleText = "";
            //popupNotifier1.
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
            //popupNotifier1.Size = new Size(500, 100); //Size of the window
            popupNotifier1.ContentFont = new Font("Georgia", fontSize, FontStyle.Bold);
            //popupNotifier1.ContentFont = new Font(popupNotifier1.ContentFont.Name, fontSize, popupNotifier1.ContentFont.Style);
            popupNotifier1.HeaderColor = SystemColors.ControlDarkDark;// .ControlDark; //Color of the window header
            popupNotifier1.BodyColor = System.Drawing.Color.DarkOliveGreen;
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
        //private void axWebBrowser1_WindowClosing(object sender, AxSHDocVw.DWebBrowserEvents2_WindowClosingEvent e)
        //{

        //}

        void UploadToDropbox(int user, int puzzleno)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime datetime = DateTime.Now;
            this.lbl_timer.Text = datetime.ToString("HH:mm:ss");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            //////e.Cancel = true;
        }

        void StartTimer()
        {

            timer2.Interval = 1000;
            timer3.Interval = 300000;
            //timer5.Interval = 3000;
            timer5.Interval = 1020000;
            timer2.Tick +=
              new System.EventHandler(this.shortIntervalTimer_Tick);
            timer3.Tick +=
              new System.EventHandler(mainIntervalTimer_Tick);
            timer5.Tick += new System.EventHandler(dropbox_Tick);
            timer2.Start();

        }

        private void shortIntervalTimer_Tick(object sender, System.EventArgs e)
        {
            if (DateTime.Now.Second == 0)
            {
                timer3.Start();
                timer5.Start();
                timer2.Stop();
            }
        }
        private void dropbox_Tick(object sender, System.EventArgs e)
        {
            // do what you need here //
            //MessageBox.Show("Dropbox");
            showTextBaloonToolDropbox("PL08 has acquired a new puzzle piece!", 12, 5000);
                //MessageBox.Show("It appears you are not trying hard enough, please complete the tasks quickly

        }

        private void mainIntervalTimer_Tick(object sender, System.EventArgs e)
        {
            // do what you need here //
            if ((ctr_message % 3) == 0)
            {
                showTextBaloonTooltip("You're currently at the lower percentile. Please complete the tasks more quickly!", 12, 5000);
                //MessageBox.Show("It appears you are not trying hard enough, please complete the tasks quickly");
                ctr_message++;
            }
            else if ((ctr_message % 3) == 1)
            {
                showTextBaloonTooltip("Our systems indicate that your progress is below average. Please try harder!", 12, 5000);
                //MessageBox.Show("It appears you are not trying hard enough, please complete the tasks faster");
                ctr_message++;
            }
            else
            {
                showTextBaloonTooltip("Other participants are catching up with your progress! Speed up!", 12, 5000);
                //MessageBox.Show("Your current result predicts you are at the lower percentile of our scores, try harder");
                ctr_message++;
            }

        }

        //public void PopMessage()
        //{
        //    // do your inquiry stuff
        //    ////////////////////////

        //    // Set Timer Interval
        //    //timer2.Interval = new TimeSpan(0, 5, 0); // 5 Minutes
        //    // Set Timer Event
        //    timer2.Tick += new EventHandler(TimerEventProcessor);

        //    // Start timer
        //    timer2.Start();

        //}

        private static void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            MessageBox.Show("Please check on the status");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            score3 = "completed";
            updateScores();
            readScores();

            Wisielec.Form1 Letters = new Wisielec.Form1("Users\\" + name, timeSinceStartTime);
            Letters.FormClosed += new FormClosedEventHandler(Letters_FormClosed);
            this.Visible = false;
            Letters.Show();          

        }

        private void button_game4_Click(object sender, EventArgs e)
        {

            //string tekst = "This is an instruction how to use the game \"Hockey\"." + Environment.NewLine + "You can use up/down arrows to move your stick (on the left side). Try to win with computer.";
            //Brain_Teaser.Form1 btForm = new Brain_Teaser.Form1();
            //btForm.FormClosed += new FormClosedEventHandler(BtForm_FormClosed);
            ////Introduction.Form1 Intro = new Introduction.Form1(tekst);
            ////Intro.FormClosed += new FormClosedEventHandler(Intro_FormClosed2);
            //this.Visible = false;
            //btForm.Show();

            score6 = "completed";
            updateScores();
            StartTimer();
            BrainTeaser.Form1 BtForm = new BrainTeaser.Form1(this.name, timeSinceStartTime);
            BtForm.FormClosed += new FormClosedEventHandler(BtForm_FormClosed);
            this.Visible = false;
            //timer2.Stop();
            BtForm.Show();
        }

        void BtForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            dropbox_user = 3;
            dropbox_puzzle = 1;
            myTimer.Dispose();
            myTimer = new Timer();
            myTimer.Interval = 11000;
            myTimer.Tick += new EventHandler(timer_Dropbox);
            myTimer.Start();

            this.Show();
            //timer2.Start();
            saveScores();

        }


        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //string answer = "knock";
            //MessageBox.Show("Congratulations!");
            //string path = @"C:\Users\user\Dropbox\PUZZLE";
            //string path = @"C:\Users\Geraldine\Dropbox\PUZZLE";
            string dir1 = System.IO.Directory.GetCurrentDirectory();
            string folderLocation1 = dir1 + "\\PUZZLE\\";
            //C: \Users\Geraldine\Dropbox
            //string path = @"C:\Users\user\Dropbox\PUZZLE";
            string path = folderLocation1;
            //string path = @"C:\Users\Geraldine\Dropbox\PUZZLE";
            System.Diagnostics.Process.Start(path);
            //System.Diagnostics.Process.Start(path);
            // this.Close();
        }
        //public void OpenFile(string filename)
        //{
        //    // Check the file exists
        //    if (!System.IO.File.Exists(filename)) throw new Exception();
        //    m_ExcelFileName = filename;
        //    // Load the workbook in the WebBrowser control
        //    //this.webBrowser1.Navigate(filename, false);
        //}

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            score1 = "completed";
            updateScores();
            //panel3.Visible = true;
            StartTimer();
            string Url = "http://tinyurl.com/moscross2";
            //string Url = "https://docs.google.com/spreadsheets/d/147B3VT-UNH0mnRMsr6Dvm8JXrHfwlSncEyOa1NXJAK0/edit#gid=0";
            WebBrowserTrial_2.Form1 WBTrial = new WebBrowserTrial_2.Form1(Url, this.name, timeSinceStartTime);
            WBTrial.FormClosed += new FormClosedEventHandler(WBTrial_FormClosed);
            this.Visible = false;
            WBTrial.Show();

            run_game = 1;
            
            using (System.IO.StreamWriter writer2 = File.AppendText("Users\\" + name + "\\log.txt"))
            {
                DateTime now = DateTime.Now;
                //Console.WriteLine(now);
                string date1 = "";
                date1 = date1 + now.ToString("hh.mm.ss.ffffff");
                writer2.Write(date1 + " ");
                //writer2.Write(" ");
                writer2.Write(name + " ");
                writer2.Write("crossword ");
                writer2.Write("done" + " ");
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
                writer2.Write("crossword ,");
                writer2.Write("done" + ",");
                writer2.Write(System.Environment.NewLine);
                writer2.Close();
            }
            puzzle = 1;
            //OpenFile(m_ExcelFileName);
            //Panel panel4 = new Panel();
            //Form1.controls.add(panel4);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button_letters_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click_2(object sender, EventArgs e)
        {

        }

        private void button_game4_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {

        }
        private bool change = true;
        private void timer4_Tick(object sender, EventArgs e)
        {

            if (pictureBox2.Visible == true)
                pictureBox2.Visible = false;
            else
                pictureBox2.Visible = true;

        }

        private void pictureBox1_Click_2(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button_pic_story_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            //Intro_FormClosed1();
            //string tekst = "This is an instruction how to use the game \"Sudoku\"." + Environment.NewLine + "Use left mouse button (click several times) to keep a bird in the air. Try to collect as many coins as possible.";
            //Introduction.Form1 Intro = new Introduction.Form1(tekst);
            //Intro.FormClosed += new FormClosedEventHandler(Intro_FormClosed1);

            //Web
            StartTimer();
            Url = "http://www.websudoku.com/?level=1&set_id=7867973019";
            WebBrowserTrial_2.Form1 WBTrial = new WebBrowserTrial_2.Form1(Url, this.name, timeSinceStartTime);
            WBTrial.FormClosed += new FormClosedEventHandler(WBTrial_FormClosed);
            this.Visible = false;
            //timer2.Stop();
            WBTrial.Show();

            run_game = 1;
            score2 = "completed";
            updateScores();
            using (System.IO.StreamWriter writer2 = File.AppendText("Users\\" + name + "\\log.txt"))
            {
                DateTime now = DateTime.Now;
                //Console.WriteLine(now);
                string date1 = "";
                date1 = date1 + now.ToString("hh.mm.ss.ffffff");
                writer2.Write(date1 + " ");
                //writer2.Write(" ");
                writer2.Write(name + " ");
                writer2.Write("sudoku ");
                writer2.Write("done" + " ");
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
                writer2.Write("sudoku ,");
                writer2.Write("done" + ",");
                writer2.Write(System.Environment.NewLine);
                writer2.Close();
            }
            puzzle = 1;

            //myTimer.Dispose();
            //myTimer = new Timer();
            //myTimer.Interval = 5000;
            //myTimer.Tick += new EventHandler(timer_BaloonTooltip);
            //myTimer.Start();
            //arkanoid = 1;
            //this.Visible = false;
            //Intro.Show();
        }

        private void button_pic_story_Click(object sender, EventArgs e)
        {
            score5 = "completed";
            updateScores();
            //readScores();
            StartTimer();
            Picture_Story.Form1 Picture = new Picture_Story.Form1("Users\\" + name, timeSinceStartTime);
            Picture.FormClosed += new FormClosedEventHandler(Picture_FormClosed);
            this.Visible = false;
            //timer2.Stop();
            Picture.Show();
        }

        private void button_letters_Click(object sender, EventArgs e)
        {
            score3 = "completed";
            updateScores();
            //readScores();
            StartTimer();
            Wisielec.Form1 Letters = new Wisielec.Form1("Users\\" + name, timeSinceStartTime);
            Letters.FormClosed += new FormClosedEventHandler(Letters_FormClosed);
            this.Visible = false;
            //timer2.Stop();
            Letters.Show();
            

           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            score4 = "completed";
            //MessageBox.Show(score4);
            updateScores();
            //readScores();

            Words_Category.Form1 Words = new Words_Category.Form1("Users\\" + name, timeSinceStartTime);
            Words.FormClosed += new FormClosedEventHandler(Words_FormClosed);
            this.Visible = false;
            timer2.Stop();
            Words.Show();
        }

        private void axWebBrowser1_BeforeNavigate2(object sender, AxSHDocVw.DWebBrowserEvents2_BeforeNavigate2Event e)
        {

                //MessageBox.Show("BeforeNavigate");
                //panel3.Visible = false;
                //panel2.Visible = true;
        }

        private void axWebBrowser1_NavigateError(object sender, AxSHDocVw.DWebBrowserEvents2_NavigateErrorEvent e)
        {
            //showTextBaloonTooltip("My grandmother plays better!", 10, 5000);
            //var tmp = (HtmlDocument)axWebBrowser1.Document;
            //Console.WriteLine(tmp.GetElementById("result").GetAttribute("value"));
            panel2.Visible = true;
            //panel3.Visible = false;
            ///UploadToDropbox();
            updateScores();

        }

        private void axWebBrowser1_DocumentComplete(object sender, AxSHDocVw.DWebBrowserEvents2_DocumentCompleteEvent e)
        {

        }

        void timer_BaloonTooltip(object sender, EventArgs e)
        {
            showTextBaloonTooltip("Faster, faster!", 10, 3000);
            myTimer.Stop();
        }

        void timer_Dropbox(object sender, EventArgs e)
        {
            if (bw.IsBusy != true)
            {
                bw.RunWorkerAsync();
            }
            myTimer.Stop();
        }

        void timer_Close(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = true;
            //panel3.Visible = false;
            //myTimer2.Stop();
        }

        /*
        void timer_Dropbox1(object sender, EventArgs e)
        {
            myTimer.Stop();
            //student_A
            _client1 = new DropNetClient("a1h5xkztcn59ps9", "oaci9hj0mjnkh9x", "lp9zbolaczthzmfj", "9v62zp3altu20g4");
            _client1.UseSandbox = false;
            _client1.UploadFile("/PUZZLE/Chen Xiaoping", "puzzle_01.jpg", File.ReadAllBytes(@"puzzle_01.jpg"));

        }
        void timer_Dropbox2(object sender, EventArgs e)
        {
            myTimer.Stop();
            //student_B
            _client2 = new DropNetClient("xz3lt8k0t83agn3", "xa5elp599sswl06", "wcdvhmgvwgohzm3j", "let25haz5uf4gow");
            _client2.UseSandbox = false;
            _client2.UploadFile("/PUZZLE/Zhan Huijing", "puzzle_01.jpg", File.ReadAllBytes(@"puzzle_01.jpg"));

        }
        */

        private void btnAnswer_Click(object sender, EventArgs e)
        {
            string answer = "six";
            if (System.Text.RegularExpressions.Regex.IsMatch(txtQuestion.Text, answer, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            {
                MessageBox.Show("Congratulations!");
                Url = "https://docs.google.com/forms/d/e/1FAIpQLScLOPiY8pl_YeGpLj4DrGd1ZZP-GHNimrREdeqiA_8eDnfnoA/viewform";
                gotoSite(Url);
                this.Close();
            }
            else
            {
                string ans = "6";
                if (System.Text.RegularExpressions.Regex.IsMatch(txtQuestion.Text, ans, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                {
                    MessageBox.Show("Congratulations!");
                    Url = "https://docs.google.com/forms/d/e/1FAIpQLScLOPiY8pl_YeGpLj4DrGd1ZZP-GHNimrREdeqiA_8eDnfnoA/viewform";
                    gotoSite(Url);
                    this.Close();
                }
                else
                    MessageBox.Show("Wrong answer!");
            }
                

        }

        //private void btnPlayGame3_Click(object sender, EventArgs e)
        //{
        //    score3 = "completed";
        //    updateScores();

            
        //    Wisielec.Form1 Letters = new Wisielec.Form1("Users\\" + name);
        //    Letters.FormClosed += new FormClosedEventHandler(Letters_FormClosed);
        //    this.Visible = false;
        //    Letters.Show();
            
        //}

        //private void btnPlayGame4_Click(object sender, EventArgs e)
        //{
        //    score4 = "completed";
        //    updateScores();

        //    Words_Category.Form1 Words = new Words_Category.Form1("Users\\" + name);
        //    Words.FormClosed += new FormClosedEventHandler(Words_FormClosed);
        //    this.Visible = false;
        //    Words.Show();
        //}

        //private void btnPlayGame5_Click(object sender, EventArgs e)
        //{
        //    score5 = "completed";
        //    updateScores();

        //    Picture_Story.Form1 Picture = new Picture_Story.Form1("Users\\" + name);
        //    Picture.FormClosed += new FormClosedEventHandler(Picture_FormClosed);
        //    this.Visible = false;
        //    Picture.Show();
        //}


        private void popupNotifier1_Click(object sender, EventArgs e)
        {
            string answer = "knock";
            //MessageBox.Show("Congratulations!");
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
            // this.Close();
        }
    }
}
