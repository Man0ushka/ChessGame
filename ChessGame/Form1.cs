using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ChessGame.Piece;

namespace ChessGame
{
    public partial class Form1 : Form
    {
        public static Bitmap bm;
        public static int wid;
        public static int hgt;
        public static Form1 form1;
        bool mouseDown = false;
        bool mouseUp = false;
        bool mouseClick = false;

        static Spot startSpot;
        static Spot endSpot;
        public Game game;
            

        public static SolidBrush blankBrush = new SolidBrush(Color.White);
        public static SolidBrush startBrush = new SolidBrush(Color.LightBlue);
        public static SolidBrush endBrush = new SolidBrush(Color.LightSteelBlue);

        public static Pen startPen = new Pen(Color.Green,1);

        public Player turn;
        public Form1()
        {
            InitializeComponent();
            form1 = this;
            boardPicture.Image = new Bitmap(boardPicture.ClientSize.Width, boardPicture.ClientSize.Height);
            //boardPicture.BackColor = Color.White;
            wid = boardPicture.ClientSize.Width;
            hgt = boardPicture.ClientSize.Height;
            bm = new Bitmap(wid, hgt);
            boardPicture.Image = bm;
            //DrawGrid();
            game = new Game();
            turn = game.currentTurn;
            /*startSpot = game.Brd.getBox(6, 5);
            endSpot = game.Brd.getBox(5, 5);
            game.makeMove(startSpot, endSpot);*/


            //boardPicture.MouseDown += BoardPicture_MouseDown;
            //boardPicture.MouseUp += BoardPicture_MouseUp;
            boardPicture.MouseClick += BoardPicture_MouseClick;


            //DrawPiece(0, 0, "whiteP");

        }

        private void BoardPicture_MouseClick(object sender, MouseEventArgs e)
        {
            Point p = new Point( MousePosition.X, MousePosition.Y);
            Point q = boardPicture.PointToClient(p);
            
            // Rien de sélectionner
            if (mouseClick==false)
            {
                game.getSpotList();
                startSpot = game.Brd.getBox(q.Y/(hgt/8), q.X/(wid/8));

                if (startSpot.Piece == null || startSpot.Piece.Player!=game.currentTurn)
                    return;
                // DRAW CLICKED OUTLINE
                Graphics gr = Graphics.FromImage(Form1.bm);
                Rectangle rectStart = new Rectangle(startSpot.Y * Form1.hgt / 8, startSpot.X * Form1.wid / 8, Form1.wid / 8, Form1.hgt / 8);
                //gr.DrawRectangle(startPen, rectStart);
                gr.FillRectangle(startBrush, rectStart);
                DrawTool.DrawPieceInit(startSpot.X, startSpot.Y, startSpot.Piece.Name, game.currentTurn.IsWhite);


                //boardPicture.Invalidate(rectStart);
                boardPicture.Invalidate(rectStart);
                mouseClick = true;
                gr.Dispose();

                if (game.movPoss[startSpot.Piece].Count == 0)
                    System.Diagnostics.Debug.WriteLine(startSpot.Piece.Name + " cannot move!");
                foreach (Spot spot in game.movPoss[startSpot.Piece])
                {
                    System.Diagnostics.Debug.WriteLine(startSpot.Piece.Name + " can move to: x: " + spot.X.ToString() + ", y: " + spot.Y.ToString() + " Count: " + game.movPoss[startSpot.Piece].Count.ToString());
                    
                }

            }
            else if (mouseClick==true)
            {
                game.getSpotList();

                //REMOVE START OUTLINE
                /*Graphics gr = Graphics.FromImage(Form1.bm);
                gr.DrawRectangle(startPen, new Rectangle(startSpot.Y * Form1.hgt / 8, startSpot.X * Form1.wid / 8, Form1.wid / 8, Form1.hgt / 8));
                boardPicture.Invalidate(new Rectangle(startSpot.Y * Form1.hgt / 8, startSpot.X * Form1.wid / 8, Form1.wid / 8, Form1.hgt / 8));*/
                endSpot = game.Brd.getBox(q.Y / (hgt / 8), q.X / (wid / 8));

                if (endSpot.Piece != null && endSpot.Piece.Player == game.currentTurn)
                {
                    if (game.movPoss[endSpot.Piece].Count==0)
                        System.Diagnostics.Debug.WriteLine(endSpot.Piece.Name + " cannot move!");
                        foreach (Spot spot in game.movPoss[endSpot.Piece])
                    {
                        System.Diagnostics.Debug.WriteLine(endSpot.Piece.Name + " can move to: x: " + spot.X.ToString() + ", y: " + spot.Y.ToString()+" Count: " + game.movPoss[endSpot.Piece].Count.ToString());

                    }
                    Graphics gr = Graphics.FromImage(Form1.bm);
                    Pen blankPen = new Pen(startSpot.SpotColor, 1);
                    SolidBrush blankBrush = new SolidBrush(startSpot.SpotColor);
                    //gr.DrawRectangle(blankPen, new Rectangle(startSpot.Y * Form1.hgt / 8, startSpot.X * Form1.wid / 8, Form1.wid / 8-1, Form1.hgt / 8-1));
                    //boardPicture.Invalidate(new Rectangle(startSpot.Y * Form1.hgt / 8, startSpot.X * Form1.wid / 8, Form1.wid / 8, Form1.hgt / 8));
                    gr.FillRectangle(blankBrush, new Rectangle(startSpot.Y * Form1.hgt / 8, startSpot.X * Form1.wid / 8, Form1.wid / 8 , Form1.hgt / 8 ));
                    DrawTool.DrawPieceInit(startSpot.X, startSpot.Y, startSpot.Piece.Name, game.currentTurn.IsWhite);
                    //boardPicture.Invalidate(new Rectangle(startSpot.Y * Form1.hgt / 8, startSpot.X * Form1.wid / 8, Form1.wid / 8, Form1.hgt / 8));
                    startSpot = endSpot;
                    gr.FillRectangle(startBrush, new Rectangle(startSpot.Y * Form1.hgt / 8, startSpot.X * Form1.wid / 8, Form1.wid / 8 , Form1.hgt / 8 ));
                    DrawTool.DrawPieceInit(startSpot.X, startSpot.Y, startSpot.Piece.Name, game.currentTurn.IsWhite);
                    //gr.DrawRectangle(startPen, new Rectangle(startSpot.Y * Form1.hgt / 8, startSpot.X * Form1.wid / 8, Form1.wid / 8-1, Form1.hgt / 8-1));
                    //boardPicture.Invalidate(new Rectangle(startSpot.Y * Form1.hgt / 8, startSpot.X * Form1.wid / 8, Form1.wid / 8, Form1.hgt / 8));
                    //boardPicture.Invalidate(new Rectangle(startSpot.Y * Form1.hgt / 8, startSpot.X * Form1.wid / 8, Form1.wid / 8, Form1.hgt / 8));
                    boardPicture.Invalidate();
                    blankBrush.Dispose();
                    gr.Dispose();
                    return;
                }
                
                //Graphics g = Graphics.FromImage(Form1.bm);
                //g.FillRectangle(endBrush, new Rectangle(endSpot.Y * Form1.hgt / 8, endSpot.X * Form1.wid / 8, Form1.wid / 8, Form1.hgt / 8));
                if (game.makeMove(startSpot, endSpot) == false)
                {
                    
                    //g.FillRectangle(blankBrush, new Rectangle(endSpot.Y * Form1.hgt / 8, endSpot.X * Form1.wid / 8, Form1.wid / 8, Form1.hgt / 8));
                    return;
                }
                else if (game.moveList.Count>0)
                {
                    SolidBrush blankBrush = new SolidBrush(endSpot.SpotColor);
                    Graphics g = Graphics.FromImage(Form1.bm);
                    Spot lastSpotStart = game.moveList.Last().StartSpot;
                    Spot lastSpotEnd = game.moveList.Last().EndSpot;
                    Piece piece = game.moveList.Last().MovedPiece;
                    
                    g.FillRectangle(endBrush, new Rectangle(lastSpotEnd.Y * Form1.hgt / 8, lastSpotEnd.X * Form1.wid / 8, Form1.wid / 8, Form1.hgt / 8));
                    DrawTool.DrawPieceInit(lastSpotEnd.X, lastSpotEnd.Y, piece.Name, piece.Player.IsWhite);
                    //boardPicture.Invalidate(new Rectangle(lastSpotEnd.Y * Form1.hgt / 8, lastSpotEnd.X * Form1.wid / 8, Form1.wid / 8, Form1.hgt / 8));
                    g.FillRectangle(startBrush, new Rectangle(lastSpotStart.Y * Form1.hgt / 8, lastSpotStart.X * Form1.wid / 8, Form1.wid / 8, Form1.hgt / 8));
                    //boardPicture.Invalidate(new Rectangle(lastSpotStart.Y * Form1.hgt / 8, lastSpotStart.X * Form1.wid / 8, Form1.wid / 8, Form1.hgt / 8));
                    boardPicture.Invalidate();
                    if (turn.IsWhite!=game.currentTurn.IsWhite)
                    {
                        if (game.moveList.Count - 2 >= 0)
                        {
                            Graphics gm = Graphics.FromImage(Form1.bm);
                            int lastlastIndex = game.moveList.Count - 2;
                            Move lastlastMove = game.moveList[lastlastIndex];
                            
                            Spot lastlastEndSpot = lastlastMove.EndSpot;
                            Spot lastlastStartSpot = lastlastMove.StartSpot;
                            Piece pieceLastlast = lastlastMove.MovedPiece;
                            Piece pieceLastlastKilled = lastlastMove.KilledPiece;
                            SolidBrush lastEndBrush = new SolidBrush(lastlastEndSpot.SpotColor);
                            SolidBrush lastStartBrush = new SolidBrush(lastlastStartSpot.SpotColor);
                            // COLOR BLANK MOVED PIECE OPPOSITE ADVERSE
                            if (endSpot.Piece==null)
                            {
                                //System.Diagnostics.Debug.WriteLine("endSpot Piece == null"+" last last move piece moved: "+pieceLastlast.Name);
                                gm.FillRectangle(lastEndBrush, new Rectangle(lastlastEndSpot.Y * Form1.hgt / 8, lastlastEndSpot.X * Form1.wid / 8, Form1.wid / 8, Form1.hgt / 8));
                                DrawTool.DrawPieceInit(lastlastEndSpot.X, lastlastEndSpot.Y, pieceLastlast.Name, pieceLastlast.Player.IsWhite);
                                //boardPicture.Invalidate(new Rectangle(lastlastEndSpot.Y * Form1.hgt / 8, lastlastEndSpot.X * Form1.wid / 8, Form1.wid / 8, Form1.hgt / 8));
                                boardPicture.Invalidate();
                            }
                            else
                            {
                                //System.Diagnostics.Debug.WriteLine("endSpot Piece NOT null");
                                gm.FillRectangle(endBrush, new Rectangle(lastSpotEnd.Y * Form1.hgt / 8, lastSpotEnd.X * Form1.wid / 8, Form1.wid / 8, Form1.hgt / 8));
                                DrawTool.DrawPieceInit(lastSpotEnd.X, lastSpotEnd.Y, piece.Name, piece.Player.IsWhite);
                                ////boardPicture.Invalidate(new Rectangle(lastSpotEnd.Y * Form1.hgt / 8, lastSpotEnd.X * Form1.wid / 8, Form1.wid / 8, Form1.hgt / 8));
                                boardPicture.Invalidate();
                            }

                            //boardPicture.Invalidate();




                            //DrawTool.DrawPiece(lastlastStartSpot, lastlastEndSpot, pieceLastlast.Name, pieceLastlast.Player);



                            // COLOR BLANK START PIECE OPPOSITE ADVERSE
                            if (endSpot.X == lastlastStartSpot.X && endSpot.Y == lastlastStartSpot.Y)
                            {

                            }
                            else
                                g.FillRectangle(lastStartBrush, new Rectangle(lastlastStartSpot.Y * Form1.hgt / 8, lastlastStartSpot.X * Form1.wid / 8, Form1.wid / 8, Form1.hgt / 8));

                            //boardPicture.Invalidate(new Rectangle(lastlastStartSpot.Y * Form1.hgt / 8, lastlastStartSpot.X * Form1.wid / 8, Form1.wid / 8, Form1.hgt / 8));
                            boardPicture.Invalidate();
                            g.Dispose();
                            gm.Dispose();
                            lastEndBrush.Dispose();
                            
                        }
                        turn = game.currentTurn;


                    }
                    else
                    {
                        
                        g.Dispose();
                        boardPicture.Invalidate();
                    }
                }



                boardPicture.Invalidate();
                mouseClick = false;
               // game.makeMove(startSpot, endSpot);
            }
        }

        private void BoardPicture_MouseUp(object sender, MouseEventArgs e)
        {
            mouseUp = true;
        }

        private void BoardPicture_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
        }

        public void DrawGrid()
        {
            Pen gridPen = new Pen(Color.Black, 1);
            Graphics graphics = Graphics.FromImage(Form1.bm);
            int i = 0;
            while (i <= Form1.hgt)
            {
                Point f = boardPicture.PointToScreen(new Point(i, 0));
                Point g = boardPicture.PointToScreen(new Point(i, wid));
                Point p = boardPicture.PointToClient(f);
                Point q = boardPicture.PointToClient(g);
                graphics.DrawImage(boardPicture.Image, 0, 0);
                graphics.DrawLine(gridPen, new Point(q.X, q.Y), new Point(p.X, p.Y));
                i = i + hgt / 8;
            }
            int j = 0;
            // Vertical
            while (j <= wid)
            {

                Point f = boardPicture.PointToScreen(new Point(0, j));
                Point g = boardPicture.PointToScreen(new Point(hgt, j));
                Point p = boardPicture.PointToClient(f);
                Point q = boardPicture.PointToClient(g);
                graphics.DrawImage(boardPicture.Image, 0, 0);
                graphics.DrawLine(gridPen, new Point(q.X, q.Y), new Point(p.X, p.Y));
                j = j + wid / 8;
            }
            boardPicture.Image = bm;
            graphics.Dispose();
        }



    }
}
