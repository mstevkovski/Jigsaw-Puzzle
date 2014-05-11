using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JigsawFinal_weHope_
{
    public class Piece
    {
       
        public int X { get; set; }
        public int Y { get; set; }
        /// <summary>
        /// Store piece of picture  
        /// </summary>
        public Bitmap bmpPiece { get; set; }
        public int Id { get; set; }
        /// <summary>
        /// Old position of snapped piece
        /// </summary>
        public int movedX { get; set; }
        public int movedY { get; set; }
        public Piece(int id, int x, int y,Bitmap bmp)
        {
            Id = id;
            X = x;
            Y = y;
           
            bmpPiece = bmp;
        }
        /// <summary>
        /// Func for check if piece can conect with other piece by ID
        /// </summary>
        /// <param name="p">Piece type</param>
        /// <returns>Returns Side on which the piece can conect with other piece</returns>
        public Side CanConnectWithID(Piece p)
        {
            if ((p.Id - GameSettings.COLUMNS) == this.Id)
                return Side.down;
            else if ((p.Id + GameSettings.COLUMNS) == this.Id)
                return Side.up;
            else if ((p.Id + 1) == this.Id && (this.Id % GameSettings.COLUMNS != 0))
                return Side.left;
            else if ((p.Id - 1) == this.Id && (p.Id % GameSettings.COLUMNS != 0))
                return Side.right;
            else return Side.noSide;
            
        }
        /// <summary>
        /// Checking if 2 pieces can conect by position and ID
        /// </summary>
        /// <param name="p">Piece</param>
        /// <returns>Boolean, whether the 2 piecees can be conected or not</returns>
        public bool CanConnectWithPosition(Piece p)
        {
            p.movedX = p.X;
            p.movedY = p.Y;
            //side => p vo odnos na this pr: ako side = left => p se stava od leva str na this
            Side side = this.CanConnectWithID(p);
            if (side == Side.noSide)
                return false;
            else if (side == Side.left )
            {
              //  MessageBox.Show("Left");
                if ((Math.Abs(this.X - p.X - GameSettings.PIECE_WIDTH) <= GameSettings.SNAP_DIST) && (Math.Abs(this.Y - p.Y) <= GameSettings.SNAP_DIFF))
                {              
                    p.X = this.X - GameSettings.PIECE_WIDTH;
                    p.Y = this.Y;
                    p.movedX = p.X - p.movedX;
                    p.movedY = p.Y - p.movedY;
                    return true;
                }
            }
            else if (side == Side.up)
            {
              //  MessageBox.Show("Up");
                if ((Math.Abs(this.X - p.X) <= GameSettings.SNAP_DIFF) && (Math.Abs(p.Y - this.Y + GameSettings.PIECE_HEIGHT) <= GameSettings.SNAP_DIST))
                {
                    p.X = this.X;
                    p.Y = this.Y - GameSettings.PIECE_HEIGHT;
                    p.movedX = p.X - p.movedX;
                    p.movedY = p.Y - p.movedY;
                    return true;
                }
            }
            else if (side == Side.down)
            {
              //  MessageBox.Show("Down");
                if ((Math.Abs(this.X - p.X) <= GameSettings.SNAP_DIFF) && (Math.Abs(p.Y - this.Y - GameSettings.PIECE_HEIGHT) <= GameSettings.SNAP_DIST))
                {
                    p.X = this.X; 
                    p.Y = this.Y + GameSettings.PIECE_HEIGHT;
                    p.movedX = p.X - p.movedX;
                    p.movedY = p.Y - p.movedY;
                    return true;
                }
            }
            else if (side == Side.right)
            {
              //  MessageBox.Show("Right");
                if ((Math.Abs(this.X - p.X + GameSettings.PIECE_WIDTH) <= GameSettings.SNAP_DIST) && (Math.Abs(this.Y - p.Y) <= GameSettings.SNAP_DIFF))
                {
                    p.X = this.X + GameSettings.PIECE_WIDTH;
                    p.Y = this.Y;
                    p.movedX = p.X - p.movedX;
                    p.movedY = p.Y - p.movedY;
                    return true; 
                }
            }
           
            return false;

            }
        public void move(Point point)
        {
            X +=point.X;
            Y += point.Y;         
        }
        public override string ToString()
        {
            return this.Id.ToString();
        }

        }
    }
