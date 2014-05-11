using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace JigsawFinal_weHope_
{
    /// <summary>
    /// Store game's static members like picture width, snap distance,piece height, etc..
    /// </summary>
    static class GameSettings
    {
        public static int COLUMNS;
        public static int ROWS;

        public static  readonly int MIN_PICTURE_WIDTH = 300;
        public static  readonly int MIN_PICTURE_HEIGHT = 300;
        /// <summary>
        /// Max distance to allow snap
        /// </summary>
        public static int SNAP_DIST;
        /// <summary>
        /// Max diffrence to allow snap
        /// </summary>
        public static int SNAP_DIFF;
        public static int PIECE_WIDTH;
        public static int PIECE_HEIGHT;
        public static void setMode(Mode mode,int ImageWidth, int ImageHeight)
        {

            if (mode == Mode.easy)
            {
                SNAP_DIST = 30;
                SNAP_DIFF = 10;
                if (ImageHeight < ImageWidth)
                {
                    COLUMNS = 5;
                    ROWS = 4;
                }
                else if (ImageHeight > ImageWidth)
                {
                    ROWS = 5;
                    COLUMNS = 4;
                }
                else
                {
                    ROWS = COLUMNS = 5;
                }

            }

            else if (mode == Mode.medium)
            {
                SNAP_DIST = 20;
                SNAP_DIFF = 8;
                if (ImageHeight < ImageWidth)
                {
                    COLUMNS = 8;
                    ROWS = 7;
                }
                else if (ImageHeight > ImageWidth)
                {
                    ROWS = 8;
                    COLUMNS = 7;
                }
                else
                {
                    ROWS = COLUMNS = 8;
                }
            }
            else if (mode == Mode.hard)
            {
                SNAP_DIST = 15;
                SNAP_DIFF = 5;
                if (ImageHeight < ImageWidth)
                {
                    COLUMNS = 10;
                    ROWS = 9;
                }
                else if (ImageHeight > ImageWidth)
                {
                    ROWS = 10;
                    COLUMNS = 9;
                }
                else
                {
                    ROWS = COLUMNS = 10;
                }
                
            }
           
        }

        public static void SetPieceSize(Size size)
        {
             PIECE_HEIGHT = size.Height / ROWS;
             PIECE_WIDTH = size.Width / COLUMNS;
        }
    }
}
