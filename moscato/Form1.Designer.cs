namespace MOSCATO
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.lblLogin = new System.Windows.Forms.Label();
            this.picMindef = new System.Windows.Forms.PictureBox();
            this.popupNotifier1 = new NotificationWindow.PopupNotifier();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.btnAnswer = new System.Windows.Forms.Button();
            this.txtQuestion = new System.Windows.Forms.TextBox();
            this.lblQuestion = new System.Windows.Forms.Label();
            this.lblPuzzle = new System.Windows.Forms.Label();
            this.btnSolvePuzzle = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.lbl_timer = new System.Windows.Forms.Label();
            this.button_pic_story = new System.Windows.Forms.Button();
            this.button_letters = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button_game4 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.timer4 = new System.Windows.Forms.Timer(this.components);
            this.timer5 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMindef)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel1.Controls.Add(this.txtPassword);
            this.panel1.Controls.Add(this.lblPassword);
            this.panel1.Controls.Add(this.btnStart);
            this.panel1.Controls.Add(this.txtLogin);
            this.panel1.Controls.Add(this.lblLogin);
            this.panel1.Controls.Add(this.picMindef);
            this.panel1.Location = new System.Drawing.Point(88, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1006, 739);
            this.panel1.TabIndex = 4;
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtPassword.Location = new System.Drawing.Point(372, 577);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(371, 44);
            this.txtPassword.TabIndex = 2;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblPassword.Location = new System.Drawing.Point(246, 577);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(167, 37);
            this.lblPassword.TabIndex = 4;
            this.lblPassword.Text = "Password:";
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnStart.Location = new System.Drawing.Point(485, 656);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(119, 38);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "START";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // txtLogin
            // 
            this.txtLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtLogin.Location = new System.Drawing.Point(372, 489);
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(371, 44);
            this.txtLogin.TabIndex = 1;
            // 
            // lblLogin
            // 
            this.lblLogin.AutoSize = true;
            this.lblLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblLogin.Location = new System.Drawing.Point(284, 492);
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Size = new System.Drawing.Size(105, 37);
            this.lblLogin.TabIndex = 1;
            this.lblLogin.Text = "Login:";
            // 
            // picMindef
            // 
            this.picMindef.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.picMindef.Image = global::MOSCATO.Properties.Resources.NTU_logo;
            this.picMindef.Location = new System.Drawing.Point(0, 0);
            this.picMindef.Name = "picMindef";
            this.picMindef.Size = new System.Drawing.Size(1014, 359);
            this.picMindef.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picMindef.TabIndex = 0;
            this.picMindef.TabStop = false;
            // 
            // popupNotifier1
            // 
            this.popupNotifier1.BodyColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.popupNotifier1.ContentFont = new System.Drawing.Font("Tahoma", 8F);
            this.popupNotifier1.ContentText = null;
            this.popupNotifier1.GradientPower = 300;
            this.popupNotifier1.HeaderHeight = 20;
            this.popupNotifier1.Image = null;
            this.popupNotifier1.OptionsMenu = null;
            this.popupNotifier1.ShowOptionsButton = true;
            this.popupNotifier1.Size = new System.Drawing.Size(400, 100);
            this.popupNotifier1.TitleFont = new System.Drawing.Font("Segoe UI", 9F);
            this.popupNotifier1.TitleText = null;
            this.popupNotifier1.Click += new System.EventHandler(this.popupNotifier1_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // btnAnswer
            // 
            this.btnAnswer.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAnswer.Font = new System.Drawing.Font("Lucida Sans", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnswer.Location = new System.Drawing.Point(899, 735);
            this.btnAnswer.Name = "btnAnswer";
            this.btnAnswer.Size = new System.Drawing.Size(99, 37);
            this.btnAnswer.TabIndex = 9;
            this.btnAnswer.Text = "SUBMIT";
            this.btnAnswer.UseVisualStyleBackColor = true;
            this.btnAnswer.Click += new System.EventHandler(this.btnAnswer_Click);
            // 
            // txtQuestion
            // 
            this.txtQuestion.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtQuestion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtQuestion.Location = new System.Drawing.Point(697, 735);
            this.txtQuestion.Name = "txtQuestion";
            this.txtQuestion.Size = new System.Drawing.Size(168, 44);
            this.txtQuestion.TabIndex = 8;
            // 
            // lblQuestion
            // 
            this.lblQuestion.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblQuestion.AutoSize = true;
            this.lblQuestion.Font = new System.Drawing.Font("Lucida Sans", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuestion.Location = new System.Drawing.Point(28, 741);
            this.lblQuestion.Name = "lblQuestion";
            this.lblQuestion.Size = new System.Drawing.Size(1026, 37);
            this.lblQuestion.TabIndex = 7;
            this.lblQuestion.Text = "How many times in total do you need to knock to meet Tsuto?";
            // 
            // lblPuzzle
            // 
            this.lblPuzzle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblPuzzle.AutoSize = true;
            this.lblPuzzle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblPuzzle.Location = new System.Drawing.Point(842, 41);
            this.lblPuzzle.Name = "lblPuzzle";
            this.lblPuzzle.Size = new System.Drawing.Size(112, 37);
            this.lblPuzzle.TabIndex = 3;
            this.lblPuzzle.Text = "Puzzle";
            this.lblPuzzle.Visible = false;
            // 
            // btnSolvePuzzle
            // 
            this.btnSolvePuzzle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSolvePuzzle.BackColor = System.Drawing.Color.SkyBlue;
            this.btnSolvePuzzle.Font = new System.Drawing.Font("Lucida Sans", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSolvePuzzle.Location = new System.Drawing.Point(899, 120);
            this.btnSolvePuzzle.Margin = new System.Windows.Forms.Padding(0);
            this.btnSolvePuzzle.Name = "btnSolvePuzzle";
            this.btnSolvePuzzle.Size = new System.Drawing.Size(275, 34);
            this.btnSolvePuzzle.TabIndex = 6;
            this.btnSolvePuzzle.Text = "PIECE UP THE PUZZLE";
            this.btnSolvePuzzle.UseVisualStyleBackColor = false;
            this.btnSolvePuzzle.Click += new System.EventHandler(this.btnSolvePuzzle_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(1029, 9);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(150, 100);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 27;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(238)))), ((int)(((byte)(237)))));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(91, 157);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1006, 549);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 38;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click_2);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(238)))), ((int)(((byte)(237)))));
            this.panel2.Controls.Add(this.pictureBox3);
            this.panel2.Controls.Add(this.lbl_timer);
            this.panel2.Controls.Add(this.button_pic_story);
            this.panel2.Controls.Add(this.button_letters);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.button_game4);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Controls.Add(this.btnSolvePuzzle);
            this.panel2.Controls.Add(this.lblPuzzle);
            this.panel2.Controls.Add(this.lblQuestion);
            this.panel2.Controls.Add(this.txtQuestion);
            this.panel2.Controls.Add(this.btnAnswer);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1182, 803);
            this.panel2.TabIndex = 5;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.Enabled = false;
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(1046, 717);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(128, 83);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 46;
            this.pictureBox3.TabStop = false;
            // 
            // lbl_timer
            // 
            this.lbl_timer.AutoSize = true;
            this.lbl_timer.BackColor = System.Drawing.Color.Black;
            this.lbl_timer.Font = new System.Drawing.Font("Lucida Sans", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_timer.ForeColor = System.Drawing.Color.FloralWhite;
            this.lbl_timer.Location = new System.Drawing.Point(494, 0);
            this.lbl_timer.Name = "lbl_timer";
            this.lbl_timer.Size = new System.Drawing.Size(0, 78);
            this.lbl_timer.TabIndex = 45;
            // 
            // button_pic_story
            // 
            this.button_pic_story.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button_pic_story.BackColor = System.Drawing.Color.Transparent;
            this.button_pic_story.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_pic_story.BackgroundImage")));
            this.button_pic_story.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_pic_story.Enabled = false;
            this.button_pic_story.FlatAppearance.BorderSize = 0;
            this.button_pic_story.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_pic_story.Location = new System.Drawing.Point(812, 249);
            this.button_pic_story.Name = "button_pic_story";
            this.button_pic_story.Size = new System.Drawing.Size(130, 90);
            this.button_pic_story.TabIndex = 44;
            this.button_pic_story.UseVisualStyleBackColor = false;
            this.button_pic_story.Click += new System.EventHandler(this.button_pic_story_Click);
            // 
            // button_letters
            // 
            this.button_letters.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button_letters.BackColor = System.Drawing.Color.Gray;
            this.button_letters.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_letters.BackgroundImage")));
            this.button_letters.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_letters.Enabled = false;
            this.button_letters.FlatAppearance.BorderSize = 0;
            this.button_letters.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_letters.Location = new System.Drawing.Point(408, 356);
            this.button_letters.Name = "button_letters";
            this.button_letters.Size = new System.Drawing.Size(118, 99);
            this.button_letters.TabIndex = 43;
            this.button_letters.UseVisualStyleBackColor = false;
            this.button_letters.Click += new System.EventHandler(this.button_letters_Click);
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.Enabled = false;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(162, 326);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(130, 89);
            this.button2.TabIndex = 42;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button3
            // 
            this.button3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button3.BackgroundImage")));
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button3.Enabled = false;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Location = new System.Drawing.Point(271, 454);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(119, 84);
            this.button3.TabIndex = 41;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button_game4
            // 
            this.button_game4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button_game4.BackColor = System.Drawing.Color.Transparent;
            this.button_game4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_game4.BackgroundImage")));
            this.button_game4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_game4.Enabled = false;
            this.button_game4.FlatAppearance.BorderSize = 0;
            this.button_game4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_game4.Location = new System.Drawing.Point(573, 484);
            this.button_game4.Name = "button_game4";
            this.button_game4.Size = new System.Drawing.Size(130, 90);
            this.button_game4.TabIndex = 40;
            this.button_game4.UseVisualStyleBackColor = false;
            this.button_game4.Click += new System.EventHandler(this.button_game4_Click);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button1.Location = new System.Drawing.Point(639, 346);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 90);
            this.button1.TabIndex = 39;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // timer4
            // 
            this.timer4.Interval = 1000;
            this.timer4.Tick += new System.EventHandler(this.timer4_Tick);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1182, 803);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "MOSCATO";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMindef)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.PictureBox picMindef;
        private NotificationWindow.PopupNotifier popupNotifier1;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btnAnswer;
        private System.Windows.Forms.TextBox txtQuestion;
        private System.Windows.Forms.Label lblQuestion;
        private System.Windows.Forms.Label lblPuzzle;
        private System.Windows.Forms.Button btnSolvePuzzle;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button_pic_story;
        private System.Windows.Forms.Button button_letters;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button_game4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lbl_timer;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.Timer timer4;
        private System.Windows.Forms.Timer timer5;
    }
}

