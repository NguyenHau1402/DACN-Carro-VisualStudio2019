
namespace Carro
{
    partial class PvP
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PvP));
            this.PNCaro = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.avt = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.prcBar = new System.Windows.Forms.ProgressBar();
            this.button1 = new System.Windows.Forms.Button();
            this.pctMark = new System.Windows.Forms.PictureBox();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.txtPlayerName = new System.Windows.Forms.TextBox();
            this.timeCD = new System.Windows.Forms.Timer(this.components);
            this.PNCaroBoard = new System.Windows.Forms.Panel();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.avt)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctMark)).BeginInit();
            this.SuspendLayout();
            // 
            // PNCaro
            // 
            this.PNCaro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PNCaro.BackColor = System.Drawing.SystemColors.Control;
            this.PNCaro.Location = new System.Drawing.Point(1119, 12);
            this.PNCaro.Name = "PNCaro";
            this.PNCaro.Size = new System.Drawing.Size(0, 0);
            this.PNCaro.TabIndex = 0;
            this.PNCaro.Paint += new System.Windows.Forms.PaintEventHandler(this.PNCaro_Paint);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.avt);
            this.panel2.Location = new System.Drawing.Point(12, 29);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(227, 250);
            this.panel2.TabIndex = 1;
            // 
            // avt
            // 
            this.avt.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.avt.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("avt.BackgroundImage")));
            this.avt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.avt.Location = new System.Drawing.Point(3, 3);
            this.avt.Name = "avt";
            this.avt.Size = new System.Drawing.Size(221, 244);
            this.avt.TabIndex = 0;
            this.avt.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.Controls.Add(this.button5);
            this.panel3.Controls.Add(this.button4);
            this.panel3.Controls.Add(this.button2);
            this.panel3.Controls.Add(this.prcBar);
            this.panel3.Controls.Add(this.button1);
            this.panel3.Controls.Add(this.pctMark);
            this.panel3.Controls.Add(this.txtIP);
            this.panel3.Controls.Add(this.txtPlayerName);
            this.panel3.Location = new System.Drawing.Point(9, 285);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(227, 497);
            this.panel3.TabIndex = 2;
            // 
            // button5
            // 
            this.button5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button5.BackgroundImage")));
            this.button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button5.Location = new System.Drawing.Point(19, 279);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(185, 47);
            this.button5.TabIndex = 10;
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button4.BackgroundImage")));
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button4.Location = new System.Drawing.Point(19, 406);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(185, 47);
            this.button4.TabIndex = 9;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button2
            // 
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.Location = new System.Drawing.Point(19, 341);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(185, 47);
            this.button2.TabIndex = 7;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // prcBar
            // 
            this.prcBar.Location = new System.Drawing.Point(3, 38);
            this.prcBar.Name = "prcBar";
            this.prcBar.Size = new System.Drawing.Size(125, 29);
            this.prcBar.TabIndex = 6;
            this.prcBar.Click += new System.EventHandler(this.prcBar_Click_1);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(134, 71);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 29);
            this.button1.TabIndex = 4;
            this.button1.Text = "Kết nối";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pctMark
            // 
            this.pctMark.Location = new System.Drawing.Point(6, 120);
            this.pctMark.Name = "pctMark";
            this.pctMark.Size = new System.Drawing.Size(217, 126);
            this.pctMark.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pctMark.TabIndex = 3;
            this.pctMark.TabStop = false;
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(4, 73);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(125, 27);
            this.txtIP.TabIndex = 2;
            this.txtIP.Text = "127.0.0.1";
            this.txtIP.TextChanged += new System.EventHandler(this.txtIP_TextChanged);
            // 
            // txtPlayerName
            // 
            this.txtPlayerName.Enabled = false;
            this.txtPlayerName.Location = new System.Drawing.Point(4, 4);
            this.txtPlayerName.Name = "txtPlayerName";
            this.txtPlayerName.ReadOnly = true;
            this.txtPlayerName.Size = new System.Drawing.Size(125, 27);
            this.txtPlayerName.TabIndex = 0;
            // 
            // timeCD
            // 
            this.timeCD.Tick += new System.EventHandler(this.timeCD_Tick);
            // 
            // PNCaroBoard
            // 
            this.PNCaroBoard.Location = new System.Drawing.Point(282, 32);
            this.PNCaroBoard.Name = "PNCaroBoard";
            this.PNCaroBoard.Size = new System.Drawing.Size(843, 848);
            this.PNCaroBoard.TabIndex = 4;
            // 
            // PvP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1142, 892);
            this.Controls.Add(this.PNCaroBoard);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.PNCaro);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1160, 939);
            this.MinimumSize = new System.Drawing.Size(1160, 939);
            this.Name = "PvP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Caro";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.avt)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctMark)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PNCaro;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox avt;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pctMark;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.TextBox txtPlayerName;
        private System.Windows.Forms.Timer timeCD;
        private System.Windows.Forms.ProgressBar prcBar;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel PNCaroBoard;
    }
}

