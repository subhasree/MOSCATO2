using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using DropNet;
using System.IO;

namespace Wisielec
{

    public partial class Form1 : Form
    {
        string path = null;
        Timer myTimer1 = new Timer();
        int rn, idx, word_no = 0, ctr_message=0;
        static string[] words = {"F E _ _", "P R O _ _ _", "_ U I L T", "_ _ A D",
            "_ _ _ T I L E", "_ E R _ Y", "_ _ A R _ D", "S M _ _ _", "_ _ _ _ O U S",
            "E X _ _ _ _", "_ _ N S E", "_ O Y", "_ _ S E T", "_ _ E E", "D I S _ _ _ _ _",
            "_ _ P P Y", "A F _ _ _ D", "C H _ _ R", "_ R O _ N", "_ _ L L Y"};
        static string[] words2 = {"F E _ _", "P R O _ _ _", "_ U I L T", "_ _ A D",
            "_ _ _ T I L E", "_ E R _ Y", "_ _ A R _ D", "S M _ _ _", "_ _ _ _ O U S",
            "E X _ _ _ _", "_ _ N S E", "_ O Y", "_ _ S E T", "_ _ E E", "D I S _ _ _ _ _",
            "_ _ P P Y", "A F _ _ _ D", "C H _ _ R", "_ R O _ N", "_ _ L L Y"};
        int word_cnt = words.Length;

        Rectangle BoundRect;
        Rectangle OldRect = Rectangle.Empty;

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern void BlockInput([In, MarshalAs(UnmanagedType.Bool)]bool fBlockIt);

        //Added by Subhasree for countdown timer
        private Timer _timer;
        public NotificationWindow.PopupNotifier popupNotifier1;
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
            this.KeyPreview = true;
            this.KeyPress += new KeyPressEventHandler(Form1_KeyPress);
            //this.KeyPress += new KeyEventHandler(Form1_KeyPress);
            this.KeyDown += new KeyEventHandler(Form1_KeyDown); 
            InitializeComponent();
            //BlockInput(true);
            //Application.UseWaitCursor = true; 
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
            //btnExit.Visible = false;
            btnExit.Location = new Point(2000, this.btnExit.Location.Y);
            //this.BackColor = System.Drawing.Color.Black;
            //txtInstruction.BackColor = System.Drawing.Color.Black;
            //txtInstruction.ForeColor = System.Drawing.Color.White;
            //OpenExcel(images);
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
            if (score5 == "completed")
            {
                pictureBox1.Image = Image.FromFile("Resources\\Progress Bar_2.png");
            }
            else
            {
                pictureBox1.Image = Image.FromFile("Resources\\Progress Bar_1.png");
            }
            txtInstruction.Font = new Font(txtInstruction.Font.FontFamily, 20);
            txtInstruction.AppendText("In this task, you will be presented with some letters with blanks in between.");
            txtInstruction.AppendText(Environment.NewLine);
            txtInstruction.AppendText("You will be required to fill in letters to form an English word.");
            txtInstruction.AppendText(Environment.NewLine);
            txtInstruction.AppendText("Please enter the first word that comes into your mind as quickly as possible and click “Enter” ");
            txtInstruction.AppendText(Environment.NewLine);
            txtInstruction.AppendText("to move on to the next word.");
            txtInstruction.AppendText(Environment.NewLine);
            txtInstruction.AppendText("The following is an example:");
            txtInstruction.AppendText(Environment.NewLine);
            txtInstruction.AppendText(Environment.NewLine);
            txtInstruction.AppendText("_ _ T C H ==> W A T C H");
            
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);

        }

        void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //MessageBox.Show(e.KeyChar.Equals((char)13).ToString());
            //if (e.KeyChar.Equals((char)13))
            //    MessageBox.Show("ENTER has been pressed!");
            //else
            //    throw new NotImplementedException();
        }

        private void @new(object sender, KeyPressEventArgs e)
        {
            throw new NotImplementedException();
        }

        //Added by Subhasree
        private void DisableMouseClicks()
        {
            if (this.Filter == null)
            {
                this.Filter = new MouseClickMessageFilter();
                Application.AddMessageFilter(this.Filter);
            }
        }

        private void EnableMouseClicks()
        {
            if ((this.Filter != null))
            {
                Application.RemoveMessageFilter(this.Filter);
                this.Filter = null;
            }
        }

        private MouseClickMessageFilter Filter;

        private const int LButtonDown = 0x201;
        private const int LButtonUp = 0x202;
        private const int LButtonDoubleClick = 0x203;

        public class MouseClickMessageFilter : IMessageFilter
        {

            public bool PreFilterMessage(ref System.Windows.Forms.Message m)
            {
                if (m.Msg == 0x201 || m.Msg == 0x202 || m.Msg == 0x203) return true;
                if (m.Msg == 0x204 || m.Msg == 0x205 || m.Msg == 0x206) return true;
                return false;
                switch (m.Msg)
                {
                    case LButtonDown:
                    case LButtonUp:
                    case LButtonDoubleClick:
                        return true;
                }
            }
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
                dropbox_puzzle = 5;
                bw.RunWorkerAsync();
            }
            myTimer2.Stop();
        }

        void timer_Dropbox3(object sender, EventArgs e)
        {
            if (bw.IsBusy != true)
            {
                dropbox_user = 3;
                dropbox_puzzle = 3;
                bw.RunWorkerAsync();
            }
            myTimer3.Stop();
        }



        private void OpenExcel(string[] images)
        {
            Excel.Application xlApp ;
            Excel.Workbook xlWorkBook ;
            Excel.Worksheet xlWorkSheet ;
            Excel.Range range ;

            string str;
            int rCnt ;
            int cCnt ;
            int rw = 0;
            int cl = 0;
            int cnt = 0;

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Open(@"C:\_FLOW_\Alvin.xlsx");
            xlWorkSheet = xlWorkBook.Sheets[1];
            
            range = xlWorkSheet.UsedRange;
            rw = range.Rows.Count;
            cl = range.Columns.Count;

            for (int i = 1; i <= rw; i++)
            {
                for (int j = 1; j <= cl; j++)
                {
                    if (j == 1)
                        Console.Write("\r\n");

                    if (range.Cells[i, j] != null && range.Cells[i, j].Value2 != null)
                    {
                        Console.Write(range.Cells[i, j].Value2.ToString() + "\t");
                        images[cnt] = range.Cells[i, j].Value2.ToString();
                        cnt++;
                    }
                }
            }

            xlWorkBook.Close(true, null, null);
            xlApp.Quit();

            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);
        }


        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Visible = false;
            //Cursor.Hide();
            DisableMouseClicks();
            txtInstruction.TextAlign = HorizontalAlignment.Center;
            txtInstruction.Font = new Font(txtInstruction.Font.FontFamily, 50);
            //new Random().Shuffle(words);
            txtInstruction.Text = words[word_no].ToString();
            //txtInstruction.Cursor = null;

            myTimer2.Dispose();
            myTimer2 = new Timer();
            myTimer2.Interval = 4000;
            myTimer2.Tick += new EventHandler(timer_Dropbox2);
            myTimer2.Start();

            myTimer3.Dispose();
            myTimer3 = new Timer();
            myTimer3.Interval = 13000;
            myTimer3.Tick += new EventHandler(timer_Dropbox3);
            myTimer3.Start();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime datetime = DateTime.Now;
            this.lbl_timer.Text = datetime.ToString("HH:mm:ss");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //private void EnableMouse()
        //{
        //    Cursor.Clip = OldRect;
        //    Cursor.Show();
        //    Application.RemoveMessageFilter(this);
        //}
        //public bool PreFilterMessage(ref Message m)
        //{
        //    if (m.Msg == 0x201 || m.Msg == 0x202 || m.Msg == 0x203) return true;
        //    if (m.Msg == 0x204 || m.Msg == 0x205 || m.Msg == 0x206) return true;
        //    return false;
        //}

        //private void DisableMouse()
        //{
        //    OldRect = Cursor.Clip;
        //    // Arbitrary location.
        //    BoundRect = new Rectangle(50, 50, 1, 1);
        //    Cursor.Clip = BoundRect;
        //    Cursor.Hide();
        //    Application.AddMessageFilter(this);
        //}

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

        void timer_action_1(object sender, EventArgs e)
        {
            myTimer1.Stop();
            myTimer1.Dispose();
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            word_no += 1;
            if (word_no >= 20)
            {
                //MessageBox.Show("The END!");
                words[word_no - 1] = words[word_no - 1].Replace(" ", "");
                System.IO.File.WriteAllLines(path + "\\letters.txt", words);
                //Environment.Exit(1);
                Cursor.Show();
                this.Close();
                EnableMouseClicks();
                var result = MessageBox.Show("You have received one puzzle piece. Do you want to go to folder?", "Puzzle Reward", MessageBoxButtons.OK);
                if (result == DialogResult.OK)
                {
                    //string path = @"C:\Users\Geraldine\Dropbox\PUZZLE";
                    //string path = @"C:\Users\user\Dropbox\PUZZLE";
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
                
            }
            else
            {
                MessageBox.Show("Inside Timer Action"+ txtInstruction.Text);
                txtInstruction.Text = words[word_no];
                words[word_no - 1] = words[word_no - 1].Replace(" ", "");
            }
        }


        private void btn_Exit(object sender, EventArgs e)
        {
            if (word_no >= 20)
            {
                words[word_no - 1] = words[word_no - 1].Replace(" ", "");
                System.IO.File.WriteAllLines(path + "\\letters.txt", words);
                Cursor.Show();
                this.Close();
                EnableMouseClicks();
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
                    Application.UseWaitCursor = false;
                    BlockInput(true);
                }
                //MessageBox.Show("You have received one puzzles. Congratulations!");
                //this.Close();
            }
            else
            {
                //MessageBox.Show("Inside Exit:"+ txtInstruction.Text);
                string test = txtInstruction.Text;
                int n = test.IndexOf("_");
                int count = test.Split('_').Length - 1;
                //MessageBox.Show(count.ToString());
                if (n != -1)
                {
                    //MessageBox.Show("Found at: " + n.ToString());
                    MessageBox.Show("Please complete the word");
                    //return;
                    //MessageBox.Show(words[word_no]);
                    //txtInstruction.Text = words[word_no];
                    //words[word_no] = words[word_no].Replace(" ", "");
                    //break;
                }
                else
                {
                    word_no += 1;
                    //MessageBox.Show(word_no.ToString());
                    if (word_no<20)
                    {
                        txtInstruction.Text = words[word_no];
                        words[word_no - 1] = words[word_no - 1].Replace(" ", "");
                    }    
                    else
                    {
                        ;
                    }
                }

            }
            //this.Close();
            //System.Environment.Exit(1);
        }

        void StartTimer()
        {

            timer2.Interval = 1000;
            timer3.Interval = 30000;

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

        private void txtInstruction_TextChanged(object sender, EventArgs e)
        {
            ;
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            if (pictureBox1.Visible == true)
                pictureBox1.Visible = false;
            else
                pictureBox1.Visible = true;
        }
        private void showTextBaloonTooltip(string contentText, int fontSize, int windowDelay)
        {
           // public NotificationWindow.PopupNotifier popupNotifier1;
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

        private void mainIntervalTimer_Tick(object sender, System.EventArgs e)
        {
            // do what you need here //
            //MessageBox.Show("Please do faster");
            if ((ctr_message % 3) == 0)
            {
                //MessageBox.Show("start Pop-up");
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

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            char[] letters;
            string s = "";

            //MessageBox.Show(e.KeyCode.ToString());
            if (e.KeyCode == Keys.Enter)
            {
                MessageBox.Show("Pressed enter.");
            }
            //MessageBox.Show("beg"+txtInstruction.Text);
            /*
            if (e.KeyCode == Keys.Enter)
            {
                word_no += 1;
                if (word_no >= 20)
                {
                    words[word_no - 1] = words[word_no - 1].Replace(" ", "");
                    System.IO.File.WriteAllLines(path + "\\letters.txt", words);
                    Cursor.Show();
                    this.Close();
                }
                else
                {
                    txtInstruction.Text = words[word_no];
                    words[word_no - 1] = words[word_no - 1].Replace(" ", "");
                }
            }
            */
            if (e.KeyCode == Keys.Back)
            {
                words[word_no] = words2[word_no];
                //txtInstruction.Text = words2[word_no];
                //MessageBox.Show(txtInstruction.Text);
                //Console.WriteLine("TEST");
                //words2[word_no - 1] = words2[word_no - 1].Replace(" ", "");
            }

            if (Char.IsLetter((char)e.KeyCode))
            {
                //MessageBox.Show("text is:"+ txtInstruction.Text);
                letters = words[word_no].ToCharArray();
                //string s;
                idx = words[word_no].IndexOf("_");
                //MessageBox.Show(idx.ToString());
                if (idx != -1)
                {
                    letters[idx] = (char)e.KeyCode;
                    s = new string(letters);
                    words[word_no] = s;
                    string[] uname = path.Split('\\');
                    //MessageBox.Show(s);
                    using (System.IO.StreamWriter writer2 = File.AppendText(path + "\\log.txt"))
                    {
                        DateTime now = DateTime.Now;
                        //Console.WriteLine(now);
                        string date1 = "";
                        date1 = date1 + now.ToString("hh.mm.ss.ffffff");
                        writer2.Write(date1 + " ");
                        //writer2.Write(" ");
                        writer2.Write(uname[1] + " ");
                        writer2.Write("letters ");
                        writer2.Write(s + " ");
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
                        writer2.Write(uname[1] + ",");
                        writer2.Write("letters" + ",");
                        writer2.Write(s + ",");
                        writer2.Write(System.Environment.NewLine);
                        writer2.Close();
                    }
                    txtInstruction.Text = s;
                    idx = words[word_no].IndexOf("_");
                }
               // MessageBox.Show("Final "+txtInstruction.Text);
                //txtInstruction.Text = words[word_no];

                /*
                if (idx == -1)
                {
                    //myTimer1.Dispose();
                    this.KeyDown -= new KeyEventHandler(Form1_KeyDown);
                    myTimer1 = new Timer();
                    myTimer1.Interval = 3000;
                    myTimer1.Tick += new EventHandler(timer_action_1);
                    myTimer1.Start();

                }
                */
            }
            else
            {
               //MessageBox.Show("Enter is pressed") ;
               //MessageBox.Show(s);
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
