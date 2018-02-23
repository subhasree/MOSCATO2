namespace PictureTiles
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOptionsEasy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOptionsMedium = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOptionsHard = new System.Windows.Forms.ToolStripMenuItem();
            this.ofdPicture = new System.Windows.Forms.OpenFileDialog();
            this.picPuzzle = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPuzzle)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(284, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileOpen,
            this.toolStripMenuItem1,
            this.mnuFileExit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // mnuFileOpen
            // 
            this.mnuFileOpen.Name = "mnuFileOpen";
            this.mnuFileOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.mnuFileOpen.Size = new System.Drawing.Size(146, 22);
            this.mnuFileOpen.Text = "&Open";
            this.mnuFileOpen.Click += new System.EventHandler(this.mnuFileOpen_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(143, 6);
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.Size = new System.Drawing.Size(146, 22);
            this.mnuFileExit.Text = "E&xit";
            this.mnuFileExit.Click += new System.EventHandler(this.mnuFileExit_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOptionsEasy,
            this.mnuOptionsMedium,
            this.mnuOptionsHard});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "&Options";
            // 
            // mnuOptionsEasy
            // 
            this.mnuOptionsEasy.Checked = true;
            this.mnuOptionsEasy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuOptionsEasy.Name = "mnuOptionsEasy";
            this.mnuOptionsEasy.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.mnuOptionsEasy.Size = new System.Drawing.Size(152, 22);
            this.mnuOptionsEasy.Tag = "200";
            this.mnuOptionsEasy.Text = "&Easy";
            this.mnuOptionsEasy.Click += new System.EventHandler(this.mnuOptionsLevel_Click);
            // 
            // mnuOptionsMedium
            // 
            this.mnuOptionsMedium.Name = "mnuOptionsMedium";
            this.mnuOptionsMedium.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.mnuOptionsMedium.Size = new System.Drawing.Size(152, 22);
            this.mnuOptionsMedium.Tag = "100";
            this.mnuOptionsMedium.Text = "&Medium";
            this.mnuOptionsMedium.Click += new System.EventHandler(this.mnuOptionsLevel_Click);
            // 
            // mnuOptionsHard
            // 
            this.mnuOptionsHard.Name = "mnuOptionsHard";
            this.mnuOptionsHard.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.mnuOptionsHard.Size = new System.Drawing.Size(152, 22);
            this.mnuOptionsHard.Tag = "50";
            this.mnuOptionsHard.Text = "&Hard";
            this.mnuOptionsHard.Click += new System.EventHandler(this.mnuOptionsLevel_Click);
            // 
            // ofdPicture
            // 
            this.ofdPicture.Filter = "Graphics Files|*.jpg;*.gif;*.png|All Files|*.*";
            // 
            // picPuzzle
            // 
            this.picPuzzle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.picPuzzle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picPuzzle.Location = new System.Drawing.Point(12, 27);
            this.picPuzzle.Name = "picPuzzle";
            this.picPuzzle.Size = new System.Drawing.Size(52, 52);
            this.picPuzzle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picPuzzle.TabIndex = 1;
            this.picPuzzle.TabStop = false;
            this.picPuzzle.Visible = false;
            this.picPuzzle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picPuzzle_MouseMove);
            this.picPuzzle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picPuzzle_MouseDown);
            this.picPuzzle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picPuzzle_MouseUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 264);
            this.Controls.Add(this.picPuzzle);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "PictureTiles";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPuzzle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuFileOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
        private System.Windows.Forms.OpenFileDialog ofdPicture;
        private System.Windows.Forms.PictureBox picPuzzle;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuOptionsEasy;
        private System.Windows.Forms.ToolStripMenuItem mnuOptionsMedium;
        private System.Windows.Forms.ToolStripMenuItem mnuOptionsHard;
    }
}

