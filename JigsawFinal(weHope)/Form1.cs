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
    public partial class Form1 : Form
    {
        Graphics graphics;
        Bitmap bmpImg;
        Game game;
        Mode mode;
        public Form1()
        {
            InitializeComponent();
            graphics = panel1.CreateGraphics();
            DoubleBuffered = true;
            mode = Mode.easy;
            Invalidate(true);
        } 
        /// <summary>
        /// Func for restarting the current game
        /// </summary>
        private void newGame()
        {
            game = new Game(bmpImg, mode);
            game.bounds = new Rectangle(0, 0, panel1.Bounds.Width, panel1.Bounds.Height);
            game.InitilazeGame();
            game.draw(graphics);
        }
        /// <summary>
        /// Double buffering the drawing
        /// </summary>
        /// 
        private void draw()
        {
            Bitmap b = new Bitmap(panel1.Width, panel1.Height);
            Graphics g = Graphics.FromImage(b);
            game.draw(g);
            graphics.DrawImageUnscaled(b, 0, 0);
            g.Dispose();
            b.Dispose();

        }

        #region events
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                if (bmpImg != null)
                {
                    bmpImg.Dispose();
                    bmpImg = null;
                }
                try
                {
                    String path = openFileDialog1.FileName;
                    if (
                        path.Contains("jpg") ||
                        path.Contains("jpeg") ||
                        path.Contains("png") ||
                        path.Contains("gif") ||
                        path.Contains("bmp") ||
                        path.Contains("svg")
                        )
                    {

                        bmpImg = new Bitmap(path);
                        if (bmpImg.Width < GameSettings.MIN_PICTURE_WIDTH || bmpImg.Height < GameSettings.MIN_PICTURE_HEIGHT)
                            MessageBox.Show("Select bigger image");
                        else
                            newGame();
                    }
                    else
                        MessageBox.Show("Please open an image");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file \"" + openFileDialog1.FileName + "\" from disk. Origilan error: " + ex.Message);
                }
            }
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {

            if (game != null)
            {
                if (e.Button == MouseButtons.Left)
                {

                    game.mouseClick = e.Location;
                    if (game.selectedPiece() != -1)
                    {
                        game.mouseMove = e.Location;
                        this.Cursor = Cursors.Hand;
                    }
                }
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (game != null)
            {
                this.Cursor = Cursors.Default;
                if (game.selectedP != null)
                {
                    game.canSnap();
                }
                game.selectedP = null;
                draw();
                if (game.clusters.Count == 1)
                    MessageBox.Show("Well Done");
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (game != null)
            {
                if (e.Button == MouseButtons.Left && this.Cursor == Cursors.Hand)
                {
                    game.movePiece(e.Location);
                    draw();
                }
                if (game.clusters.Count == 1)
                    MessageBox.Show("Well Done");
            }           
        }
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Images i = new Images();
            i.ShowDialog();
            if (i.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                bmpImg = new Bitmap(i.img);
                newGame();
            }
        }
        private void easyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mode == Mode.easy)
            {
                easyToolStripMenuItem.Text = "Mode: Medium";
                mode = Mode.medium;
            }
            else if (mode == Mode.medium)
            {
                easyToolStripMenuItem.Text = "Mode: Hard";
                mode = Mode.hard;
            }
            else if (mode == Mode.hard)
            {
                easyToolStripMenuItem.Text = "Mode: Easy";
                mode = Mode.easy;
            }

        }
        private void oToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (game != null)
                newGame();
        }
        private void hideImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (game != null)
            {
                if (game.HintImage)
                {
                    hideImageToolStripMenuItem.Text = "Show Hint Image";
                    game.HintImage = false;
                }
                else
                {
                    hideImageToolStripMenuItem.Text = "Hide Hint Image";
                    game.HintImage = true;
                }
                draw();
            }
        }
        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
                e.Effect = DragDropEffects.None;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            Array a = (Array)e.Data.GetData(DataFormats.FileDrop);
            if (a != null)
            {
                if (a.Length > 1)
                    MessageBox.Show("Pleas drop only 1 image");
                else
                {
                    String path = a.GetValue(0).ToString();
                    if (
                        path.Contains("jpg") ||
                        path.Contains("jpeg") ||
                        path.Contains("png") ||
                        path.Contains("gif") ||
                        path.Contains("bmp") ||
                        path.Contains("svg")
                        )
                    {
                        bmpImg = new Bitmap(path);
                        if (bmpImg.Width < GameSettings.MIN_PICTURE_WIDTH || bmpImg.Height < GameSettings.MIN_PICTURE_HEIGHT)
                            MessageBox.Show("Select bigger image");
                        else
                            newGame();
                    }
                    else
                        MessageBox.Show("Please drop an image");

                }
            }
        }
        #endregion    
        


    }
}
