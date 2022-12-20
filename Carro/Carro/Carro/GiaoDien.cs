using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace Carro
{
    public partial class GiaoDien : Form
    {
        public GiaoDien()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PvP f1 = new PvP();
            f1.Show();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Help h = new Help();
            h.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SingerPlay s1 = new SingerPlay();
            s1.Show();
        }
    }
}
