
using System;

namespace Carro
{
    partial class SingerPlay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SingerPlay));
            this.avt = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.prcBar = new System.Windows.Forms.ProgressBar();
            this.pctMark = new System.Windows.Forms.PictureBox();
            this.txtPlayerName = new System.Windows.Forms.TextBox();
            this.PNCaroBoard = new System.Windows.Forms.Panel();
            this.timeCD = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.avt)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctMark)).BeginInit();
            this.SuspendLayout();
            // 
            // avt
            // 
            this.avt.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.avt.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("avt.BackgroundImage")));
            this.avt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.avt.Location = new System.Drawing.Point(12, 3);
            this.avt.Name = "avt";
            this.avt.Size = new System.Drawing.Size(221, 244);
            this.avt.TabIndex = 1;
            this.avt.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.Controls.Add(this.button3);
            this.panel3.Controls.Add(this.button4);
            this.panel3.Controls.Add(this.button2);
            this.panel3.Controls.Add(this.prcBar);
            this.panel3.Controls.Add(this.pctMark);
            this.panel3.Controls.Add(this.txtPlayerName);
            this.panel3.Location = new System.Drawing.Point(6, 253);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(227, 497);
            this.panel3.TabIndex = 3;
            // 
            // button3
            // 
            this.button3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button3.BackgroundImage")));
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button3.Location = new System.Drawing.Point(19, 271);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(185, 44);
            this.button3.TabIndex = 10;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
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
            this.prcBar.Location = new System.Drawing.Point(3, 53);
            this.prcBar.Name = "prcBar";
            this.prcBar.Size = new System.Drawing.Size(201, 46);
            this.prcBar.TabIndex = 6;
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
            // txtPlayerName
            // 
            this.txtPlayerName.Enabled = false;
            this.txtPlayerName.Location = new System.Drawing.Point(4, 20);
            this.txtPlayerName.Name = "txtPlayerName";
            this.txtPlayerName.ReadOnly = true;
            this.txtPlayerName.Size = new System.Drawing.Size(200, 27);
            this.txtPlayerName.TabIndex = 0;
            // 
            // PNCaroBoard
            // 
            this.PNCaroBoard.Location = new System.Drawing.Point(249, 12);
            this.PNCaroBoard.Name = "PNCaroBoard";
            this.PNCaroBoard.Size = new System.Drawing.Size(843, 848);
            this.PNCaroBoard.TabIndex = 5;
            // 
            // timeCD
            // 
            this.timeCD.Tick += new System.EventHandler(this.timeCD_Tick);
            // 
            // SingerPlay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1142, 892);
            this.Controls.Add(this.PNCaroBoard);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.avt);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1160, 939);
            this.MinimumSize = new System.Drawing.Size(1160, 939);
            this.Name = "SingerPlay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SingerPlay";
            ((System.ComponentModel.ISupportInitialize)(this.avt)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctMark)).EndInit();
            this.ResumeLayout(false);

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.PictureBox avt;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ProgressBar prcBar;
        private System.Windows.Forms.PictureBox pctMark;
        private System.Windows.Forms.TextBox txtPlayerName;
        private System.Windows.Forms.Panel PNCaroBoard;
        private System.Windows.Forms.Timer timeCD;
        private System.Windows.Forms.Button button3;
    }
}