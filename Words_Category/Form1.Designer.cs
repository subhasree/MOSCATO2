namespace Words_Category
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
            this.btnStart = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.txtInstruction = new System.Windows.Forms.TextBox();
            this.txtCategoryA = new System.Windows.Forms.TextBox();
            this.txtCategoryB = new System.Windows.Forms.TextBox();
            this.lbl_timer = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("Lucida Sans", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Location = new System.Drawing.Point(908, 766);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(132, 45);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "START";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(238)))), ((int)(((byte)(237)))));
            this.btnExit.Font = new System.Drawing.Font("Lucida Sans", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(1772, 706);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(118, 49);
            this.btnExit.TabIndex = 8;
            this.btnExit.Text = "EXIT";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btn_Exit);
            // 
            // txtInstruction
            // 
            this.txtInstruction.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtInstruction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(238)))), ((int)(((byte)(237)))));
            this.txtInstruction.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtInstruction.Font = new System.Drawing.Font("Lucida Sans", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInstruction.Location = new System.Drawing.Point(325, 85);
            this.txtInstruction.Multiline = true;
            this.txtInstruction.Name = "txtInstruction";
            this.txtInstruction.ReadOnly = true;
            this.txtInstruction.ShortcutsEnabled = false;
            this.txtInstruction.Size = new System.Drawing.Size(1216, 604);
            this.txtInstruction.TabIndex = 9;
            this.txtInstruction.Text = resources.GetString("txtInstruction.Text");
            this.txtInstruction.TextChanged += new System.EventHandler(this.txtInstruction_TextChanged);
            // 
            // txtCategoryA
            // 
            this.txtCategoryA.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(238)))), ((int)(((byte)(237)))));
            this.txtCategoryA.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCategoryA.Font = new System.Drawing.Font("Lucida Sans", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCategoryA.ForeColor = System.Drawing.Color.Black;
            this.txtCategoryA.Location = new System.Drawing.Point(12, 12);
            this.txtCategoryA.Multiline = true;
            this.txtCategoryA.Name = "txtCategoryA";
            this.txtCategoryA.ReadOnly = true;
            this.txtCategoryA.Size = new System.Drawing.Size(500, 285);
            this.txtCategoryA.TabIndex = 10;
            this.txtCategoryA.TextChanged += new System.EventHandler(this.txtCategoryA_TextChanged);
            // 
            // txtCategoryB
            // 
            this.txtCategoryB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(238)))), ((int)(((byte)(237)))));
            this.txtCategoryB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCategoryB.Font = new System.Drawing.Font("Lucida Sans", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCategoryB.ForeColor = System.Drawing.Color.Black;
            this.txtCategoryB.Location = new System.Drawing.Point(1435, 25);
            this.txtCategoryB.Multiline = true;
            this.txtCategoryB.Name = "txtCategoryB";
            this.txtCategoryB.ReadOnly = true;
            this.txtCategoryB.Size = new System.Drawing.Size(500, 285);
            this.txtCategoryB.TabIndex = 11;
            // 
            // lbl_timer
            // 
            this.lbl_timer.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl_timer.AutoSize = true;
            this.lbl_timer.BackColor = System.Drawing.Color.Black;
            this.lbl_timer.Font = new System.Drawing.Font("Lucida Sans", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_timer.ForeColor = System.Drawing.Color.FloralWhite;
            this.lbl_timer.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbl_timer.Location = new System.Drawing.Point(899, 9);
            this.lbl_timer.Name = "lbl_timer";
            this.lbl_timer.Size = new System.Drawing.Size(0, 49);
            this.lbl_timer.TabIndex = 12;
            this.lbl_timer.Click += new System.EventHandler(this.lbl_timer_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(238)))), ((int)(((byte)(237)))));
            this.ClientSize = new System.Drawing.Size(1924, 932);
            this.ControlBox = false;
            this.Controls.Add(this.lbl_timer);
            this.Controls.Add(this.txtCategoryB);
            this.Controls.Add(this.txtCategoryA);
            this.Controls.Add(this.txtInstruction);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox txtInstruction;
        private System.Windows.Forms.TextBox txtCategoryA;
        private System.Windows.Forms.TextBox txtCategoryB;
        private System.Windows.Forms.Label lbl_timer;
        private System.Windows.Forms.Timer timer1;
    }
}

