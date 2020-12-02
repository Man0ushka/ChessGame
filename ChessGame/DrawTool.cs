using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ChessGame
{
    class DrawTool
    {
        public static void DrawSpotColor(Spot spot, Color spotColor)
        {
            int x = spot.X;
            int y = spot.Y;
            Graphics gr = Graphics.FromImage(Form1.bm);
            Rectangle rect = new Rectangle(y * Form1.hgt / 8, x * Form1.wid / 8, Form1.wid / 8, Form1.hgt / 8);

            SolidBrush brush = new SolidBrush(spotColor);
            gr.FillRectangle(brush, rect);
            Form1.form1.boardPicture.Invalidate();
            gr.Dispose();
            brush.Dispose();
        }
        public static void DrawPiece(Spot startPoint, Spot endPoint, string pieceName, bool isWhite)
        {
            int endx = endPoint.X;
            int endy = endPoint.Y;
            int sx = startPoint.X;
            int sy = startPoint.Y;

            Image image = null;
            Graphics gr = Graphics.FromImage(Form1.bm);
            Rectangle rectEnd = new Rectangle(endy * Form1.hgt / 8, endx * Form1.wid / 8, Form1.wid / 8, Form1.hgt / 8);
            Rectangle rectStart = new Rectangle(sy * Form1.hgt / 8 , sx * Form1.wid / 8 , Form1.wid / 8 , Form1.hgt / 8 );

            SolidBrush startBrush = new SolidBrush(startPoint.SpotColor);
            SolidBrush endBrush = new SolidBrush(endPoint.SpotColor);

            if (pieceName == "P" && isWhite == true)
                image = Image.FromFile("whiteP.png");
            if (pieceName == "P" && isWhite == false)
                image = Image.FromFile("blackP.png");
            if (pieceName == "R" && isWhite == true)
                image = Image.FromFile("whiteR.png");
            if (pieceName == "R" && isWhite == false)
                image = Image.FromFile("blackR.png");
            if (pieceName == "Kn" && isWhite == true)
                image = Image.FromFile("whiteKn.png");
            if (pieceName == "Kn" && isWhite == false)
                image = Image.FromFile("blackKn.png");
            if (pieceName == "B" && isWhite == true)
                image = Image.FromFile("whiteB.png");
            if (pieceName == "B" && isWhite == false)
                image = Image.FromFile("blackB.png");
            if (pieceName == "K" && isWhite == true)
                image = Image.FromFile("whiteK.png");
            if (pieceName == "K" && isWhite == false)
                image = Image.FromFile("blackK.png");
            if (pieceName == "Q" && isWhite == true)
                image = Image.FromFile("whiteQ.png");
            if (pieceName == "Q" && isWhite == false)
                image = Image.FromFile("blackQ.png");
            //KILL PIECE
            if (endPoint.Piece!=null)
                gr.FillRectangle(endBrush, rectEnd);

            gr.DrawImage(image, rectEnd);
            gr.FillRectangle(startBrush, rectStart);
           /* Form1.form1.boardPicture.Invalidate(rectStart);
            Form1.form1.boardPicture.Invalidate(rectEnd);*/
            Form1.form1.boardPicture.Invalidate();
            gr.Dispose();
            startBrush.Dispose();
            endBrush.Dispose();
        }

        public static void DrawPieceInit(int x, int y, string pieceName, bool isWhite)
        {
            Image image = null;
            Graphics gr = Graphics.FromImage(Form1.bm);
            Rectangle rect = new Rectangle(y * Form1.hgt / 8, x * Form1.wid / 8, Form1.wid / 8, Form1.hgt / 8);
            if (pieceName == "P" && isWhite == true)
                image = Image.FromFile("whiteP.png");
            if (pieceName == "P" && isWhite == false)
                image = Image.FromFile("blackP.png");
            if (pieceName == "R" && isWhite == true)
                image = Image.FromFile("whiteR.png");
            if (pieceName == "R" && isWhite == false)
                image = Image.FromFile("blackR.png");
            if (pieceName == "Kn" && isWhite == true)
                image = Image.FromFile("whiteKn.png");
            if (pieceName == "Kn" && isWhite == false)
                image = Image.FromFile("blackKn.png");
            if (pieceName == "B" && isWhite == true)
                image = Image.FromFile("whiteB.png");
            if (pieceName == "B" && isWhite == false)
                image = Image.FromFile("blackB.png");
            if (pieceName == "K" && isWhite == true)
                image = Image.FromFile("whiteK.png");
            if (pieceName == "K" && isWhite == false)
                image = Image.FromFile("blackK.png");
            if (pieceName == "Q" && isWhite == true)
                image = Image.FromFile("whiteQ.png");
            if (pieceName == "Q" && isWhite == false)
                image = Image.FromFile("blackQ.png");
            gr.DrawImage(image, rect);
            gr.Dispose();
        }

        
    }
}
