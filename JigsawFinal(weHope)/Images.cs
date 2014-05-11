using JigsawFinal_weHope_.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JigsawFinal_weHope_
{
    public partial class Images : Form
    {
        ImageProcessing imp;
        public Image img { get; set; }
        public Images()
        {
            InitializeComponent();
            imp = new ImageProcessing();
            Rectangle rect = new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = imp.checkImageSize(rect, Resources.view0);
            pictureBox2.Image = imp.checkImageSize(rect, Resources.view1);
            pictureBox3.Image = imp.checkImageSize(rect, Resources.view2);
            pictureBox4.Image = imp.checkImageSize(rect, Resources.view3);
            pictureBox5.Image = imp.checkImageSize(rect, Resources.view4);
            pictureBox6.Image = imp.checkImageSize(rect, Resources.view5);
            pictureBox7.Image = imp.checkImageSize(rect, Resources.view6);
            pictureBox8.Image = imp.checkImageSize(rect, Resources.view7);
            pictureBox9.Image = imp.checkImageSize(rect, Resources.view8);
            pictureBox10.Image = imp.checkImageSize(rect, Resources.view9);
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            img = Resources.view2;
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            img = Resources.view0;
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            img = Resources.view1;
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            img = Resources.view3;
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            img = Resources.view4;
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            img = Resources.view5;
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            img = Resources.view6;
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            img = Resources.view7;
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            img = Resources.view8;
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            img = Resources.view9;
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
