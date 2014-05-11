using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JigsawFinal_weHope_
{
    public class panelEditedClass  : System.Windows.Forms.Panel
    {
        public panelEditedClass()
        {
            this.SetStyle(
                System.Windows.Forms.ControlStyles.UserPaint |
                System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
                System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer,
                true);
        }  //making panel double buffered
    }
    public enum Side
    {
        up,
        down,
        left,
        right,
        noSide
    }
    public enum Mode
    {
        easy,
        medium,
        hard
    }
    class Game
    {
        Image image;
        public Rectangle bounds;
        public bool HintImage { get; set; }
        public List<Cluster> clusters { get; set; }
        public Mode mode { get; set; }
        public ImageProcessing imgProcessing { get; set; }     
        public Piece selectedP { get; set; }
        public Point mouseClick { get; set; }
        public Point mouseMove { get; set; }
        public Game(Image image, Mode mode)
        {
            this.image = image;
            this.mode = mode;
            imgProcessing = new ImageProcessing();
            HintImage = true;
        }
        /// <summary>
        /// Func for initaliazing a new game 
        /// </summary>
        public void InitilazeGame()
        {
            this.image = imgProcessing.checkImageSize(bounds, image);
            GameSettings.setMode(mode, image.Width, image.Height);
            image = imgProcessing.resizeImage(image);
            GameSettings.SetPieceSize(image.Size);
            bounds.Width -= GameSettings.PIECE_WIDTH + 2;
            bounds.Height -= GameSettings.PIECE_HEIGHT + 2;
            clusters = new List<Cluster>();
            makePieces();
        }    
        /// <summary>
        /// Func that for given picture, cuts it in equal pieces and creates all the pieces and clusters objects
        /// </summary>
        private void makePieces()
        {

            Pen pen = new Pen(Brushes.BlueViolet);
            pen.Width = 1;
            Bitmap bitmap = new Bitmap(image);
            int count = 0;
            Random r = new Random();

            for (int i = 0; i < GameSettings.ROWS; i++)
            {
                for (int j = 0; j < GameSettings.COLUMNS; j++)
                {
                    Image img = new Bitmap(GameSettings.PIECE_WIDTH, GameSettings.PIECE_HEIGHT);
                    var graphics = Graphics.FromImage(img);
                    Rectangle rec = new Rectangle(1, 1, GameSettings.PIECE_WIDTH + 2, GameSettings.PIECE_HEIGHT + 2);
                    graphics.DrawImage(image, rec, j * GameSettings.PIECE_WIDTH, i * GameSettings.PIECE_HEIGHT, GameSettings.PIECE_WIDTH, GameSettings.PIECE_HEIGHT, GraphicsUnit.Pixel);
                    graphics.DrawRectangle(pen, 0, 0, GameSettings.PIECE_WIDTH - 1, GameSettings.PIECE_HEIGHT - 1);
                    Piece p = new Piece(count++, r.Next(bounds.Width - 2), r.Next(bounds.Height - 2), new Bitmap(img));
                    clusters.Add(new Cluster(p));
                }

            }
        }
        public void putClusterAtTheEnd(Piece selectedPiece)
        {
            Cluster cluster = null;
            Piece piece = null;
            foreach (Cluster c in clusters)
            {
                foreach (Piece p in c.pieces)
                {
                    if (p == selectedPiece) {
                        cluster = c;
                        piece = p;
                    }
                }
            }
            if (piece != null && cluster != null)
            {
                cluster.pieces.Remove(piece);
                cluster.pieces.Add(piece);
                clusters.Remove(cluster);
                clusters.Add(cluster);
            }
        }

        /// <summary>
        /// Function for getting the current selected piece by the player
        /// </summary>
        /// <returns>Integer value, -1 for non selected or piece ID of the current selected</returns>
        public int selectedPiece()
        {
            int id = -1;
            foreach (Cluster c in clusters)
            {
                foreach (Piece pic in c.pieces)
                {
                    if (mouseClick.X > pic.X && mouseClick.X < (pic.X + GameSettings.PIECE_WIDTH))
                        if (mouseClick.Y > pic.Y && mouseClick.Y < (pic.Y + GameSettings.PIECE_HEIGHT))
                        {
                            selectedP = pic;
                            id = pic.Id;
                        }
                }

            }
            putClusterAtTheEnd(selectedP);
            return id;
        }
        /// <summary>
        /// Func for getting the current selected cluster
        /// </summary>
        /// <returns>Cluster type</returns>
        private Cluster findSelectedCluster()
        {
            foreach (Cluster c in clusters)
                if (c.pieces.Contains(selectedP))
                    return c;
            return null;
        }
        /// <summary>
        /// Func for moving the cluster 
        /// </summary>
        /// <param name="move">Parametars is current mouse position, or the position where cluster to be moved</param>
        public void movePiece(Point move)
        {
            Cluster c = findSelectedCluster();
            if (c != null)
            {
                c.Move(move, mouseMove);
                mouseMove = move;
            }                      
        }
        /// <summary>
        /// Func for checking if current selected cluster can snap with clusters around him
        /// </summary>
        public void canSnap()
        {
                   
            Cluster cluster = null;
            foreach (Cluster c in clusters)
            {
                if (c.pieces.Contains(selectedP))
                {
                    cluster = c;
                }
            }
            bool flag = false;
            foreach (Cluster c in clusters)
            {
                if (cluster != null && c != cluster)
                {
                   
                    flag = c.join(cluster);
                }
            }
            if (flag) clusters.Remove(cluster);
         
        }
        public void draw(Graphics g)
        {
            g.Clear(Form1.DefaultBackColor);
            if (HintImage)
                g.DrawImageUnscaled(imgProcessing.SetImageOpacity(image, (float)0.5), (bounds.Width - image.Width) / 2, (bounds.Height - image.Height) / 2);
            Cluster cluster = findSelectedCluster();
            foreach (Cluster c in clusters)
            {
                c.Draw(g, selectedP);
            }
            if (cluster != null)
                cluster.Draw(g, selectedP);
        }
    }
            
}


