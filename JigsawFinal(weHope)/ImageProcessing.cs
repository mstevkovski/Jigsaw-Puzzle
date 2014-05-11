using JigsawFinal_weHope_.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace JigsawFinal_weHope_
{
    /// <summary>
    /// class only for maniplating with pictures 
    /// </summary>
    public class ImageProcessing
    {
        public ImageProcessing() {  }
        public  Image SetImageOpacity(Image image, float opacity)
        {
            Bitmap bmp = new Bitmap(image.Width, image.Height);  
            using (Graphics gfx = Graphics.FromImage(bmp))
            {
                ColorMatrix matrix = new ColorMatrix();
                matrix.Matrix33 = opacity; 
                ImageAttributes attributes = new ImageAttributes();
                attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                gfx.DrawImage(image, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
            }
            return bmp;
        }
        public Image checkImageSize(Rectangle bounds,Image image)
        {
            if (image.Width > bounds.Width || image.Height > bounds.Height)
            {
                int sourceWidth = image.Width;
                int sourceHeight = image.Height;
                float nPercent = 0;
                float nPercentW = 0;
                float nPercentH = 0;
                nPercentW = ((float)bounds.Width / (float)sourceWidth);
                nPercentH = ((float)bounds.Height / (float)sourceHeight);
                if (nPercentH < nPercent)
                    nPercent = nPercentH;
                else
                    nPercent = nPercentW;
                int destWidth = (int)(sourceWidth * nPercent);
                int destHeight = (int)(sourceHeight * nPercent);
                Bitmap b = new Bitmap(destWidth, destHeight);
                Graphics g = Graphics.FromImage((Image)b);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(image, 0, 0, destWidth, destHeight);
                image = (Image)b;
                return image;
            }
            return image;
            
        }
        public Image resizeImage(Image image)
        {
            int destWidth = image.Width - (image.Width % GameSettings.COLUMNS);
            int destHeight = image.Height - (image.Height % GameSettings.ROWS);
            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(image, 0, 0, destWidth, destHeight);
            g.Dispose();
            return (Image)b;
        }
        
    }
}