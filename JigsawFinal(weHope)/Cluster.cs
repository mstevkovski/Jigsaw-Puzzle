using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace JigsawFinal_weHope_
{
    public class Cluster
    {
        /// <summary>
        /// List of pieces who;s changing with snaps
        /// </summary>
        public List<Piece> pieces;

        public Cluster(Piece p)
        {
            pieces = new List<Piece>();
            pieces.Add(p);
        }

        /// <summary>
        /// Cheking if 2 clusters can join to one
        /// </summary>
        /// <param name="c">Another CLuster to check</param>
        /// <returns> Boolean, whether clusters can or can not join</returns>
        public bool join(Cluster c)
        {
            bool flag = false;
            Piece theOne = null;
            for (int i = 0; i < this.pieces.Count; i++)
            {
                for (int j = 0; j < c.pieces.Count; j++)
                {
                    if (this.pieces[i].CanConnectWithPosition(c.pieces[j]))
                    {
                        flag = true;
                        theOne = c.pieces[j];
                        break;
                    }
                }
                if (flag)
                    break;
            }
            if (flag)
            {
                foreach (Piece p in c.pieces)
                {
                    if(p != theOne)
                    {
                        p.X += theOne.movedX;
                        p.Y += theOne.movedY;
                    }                     
                }
                foreach (Piece p in c.pieces)
                    pieces.Add(p);
                for (int i = c.pieces.Count-1; i >= 0; i--)
                    c.pieces.Remove(c.pieces[i]);
                return true;
            }
            return false;
        }
       
        public void Draw(Graphics g, Piece piece)
        {
            if (piece != null)
            {
                foreach (Piece p in pieces)
                {
                    if (p != piece)
                        g.DrawImage(p.bmpPiece, p.X, p.Y);
                }
                g.DrawImage(piece.bmpPiece, piece.X, piece.Y);
            }
            else
            {
                foreach (Piece p in pieces)
                {
                    g.DrawImage(p.bmpPiece, p.X, p.Y);
                }
            }
        }
        public void Move(Point move, Point mouseMove)
        {
            foreach (Piece p in pieces)
            {
                p.X += (move.X - mouseMove.X);
                p.Y += (move.Y - mouseMove.Y);
            }
        }

    }
}
