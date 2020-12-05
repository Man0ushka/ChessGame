﻿using System;
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
        public bool isFlipped;

        public static Bitmap bm;
        public static int wid;
        public static int hgt;
        public static Form1 form1;
        bool mouseDown = false;
        bool mouseUp = false;
        bool mouseClick = false;

        bool selected = false;

        static Spot startSpot;
        static Spot endSpot;
        public Game game;

        Player newP1;
        Player newP2;

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
            newP1 = new Player(true, false, true);
            newP2 = new Player(false, true, true);
            game = new Game(newP1,newP2);

            turn = game.currentTurn;
            isFlipped = false;
            /*startSpot = game.Brd.getBox(6, 5);
            endSpot = game.Brd.getBox(5, 5);
            game.makeMove(startSpot, endSpot);*/


            //boardPicture.MouseDown += BoardPicture_MouseDown;
            //boardPicture.MouseUp += BoardPicture_MouseUp;
            boardPicture.MouseClick += BoardPicture_MouseClick;
            btnFlipBoard.MouseClick += btnFlipBoard_Click;

            //DrawPiece(0, 0, "whiteP");

        }
        public Spot SpotSymetryColor(Spot spot)
        {
            Spot newSpot = new Spot(spot.X, spot.Y, spot.Piece);
            if (spot.SpotColor == Color.Gray)
                newSpot.SpotColor = Color.White;
            else newSpot.SpotColor = Color.Gray;
            return newSpot;
        }
        public int Symetry(int x)
        {
            
            int originalX = x;

                switch (x)
                {
                    case 0:
                        originalX = 7;
                        break;
                    case 1:
                        originalX = 6;
                        break;
                    case 2:
                        originalX = 5;
                        break;
                    case 3:
                        originalX = 4;
                        break;
                    case 4:
                        originalX = 3;
                        break;
                    case 5:
                        originalX = 2;
                        break;
                    case 6:
                        originalX = 1;
                        break;
                    case 7:
                        originalX = 0;
                        break;
                }
            


            return originalX;
        }

        private void BoardPicture_MouseClick(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Picture clicked");
            Point p = new Point( MousePosition.X, MousePosition.Y);
            Point q = boardPicture.PointToClient(p);
            int sX = q.Y / (hgt / 8);
            int sY = q.X / (wid / 8);
            //game.getSpotList();
            // Rien de sélectionner
            if (mouseClick == false)
            {
                System.Diagnostics.Debug.WriteLine("Mouse click: " + mouseClick.ToString());
                game.getSpotList();
                startSpot = game.Brd.Boxes[sX, sY];
                if (startSpot.Piece == null || startSpot.Piece.Player!=game.currentTurn)
                    return;
                System.Diagnostics.Debug.WriteLine(" X: " + sX.ToString() + " Y: " + sY.ToString() + " Piece: " + startSpot.Piece.Name + " Player: " + startSpot.Piece.Player.IsWhite.ToString() + " Isup: " + startSpot.Piece.Player.IsUp.ToString());

                // DRAW CLICKED OUTLINE
                Graphics gr = Graphics.FromImage(Form1.bm);
                Rectangle rectStart = new Rectangle(sY * Form1.hgt / 8, sX * Form1.wid / 8, Form1.wid / 8, Form1.hgt / 8);
                //gr.DrawRectangle(startPen, rectStart);
                gr.FillRectangle(startBrush, rectStart);
                DrawTool.DrawPieceInit(sX, sY, startSpot.Piece.Name, game.currentTurn.IsWhite);


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
                // IS BOARD FLIPPED?
                //startSpot = game.Brd.Boxes[Symetry(startSpot.X), Symetry(startSpot.Y)];

                game.getSpotList();

                //REMOVE START OUTLINE
                /*Graphics gr = Graphics.FromImage(Form1.bm);
                gr.DrawRectangle(startPen, new Rectangle(startSpot.Y * Form1.hgt / 8, startSpot.X * Form1.wid / 8, Form1.wid / 8, Form1.hgt / 8));
                boardPicture.Invalidate(new Rectangle(startSpot.Y * Form1.hgt / 8, startSpot.X * Form1.wid / 8, Form1.wid / 8, Form1.hgt / 8));*/
                //endSpot = game.Brd.getBox(q.Y / (hgt / 8), (q.X / (wid / 8)));
                endSpot= game.Brd.Boxes[sX, sY];
                // endSpot = game.Brd.Boxes[Symetry(endSpot.X), Symetry(endSpot.Y)];
               
                if (endSpot.Piece != null && endSpot.Piece.Player == game.currentTurn)
                {
                    System.Diagnostics.Debug.WriteLine(" X: " + sX.ToString() + " Y: " + sY.ToString() + " Piece: " + endSpot.Piece.Name + " Player: " + endSpot.Piece.Player.IsWhite.ToString() + " Isup: " + endSpot.Piece.Player.IsUp.ToString());
                    
                    if (game.movPoss[endSpot.Piece].Count == 0)
                        System.Diagnostics.Debug.WriteLine(endSpot.Piece.Name + " cannot move!");
                    foreach (Spot spot in game.movPoss[endSpot.Piece])
                    {
                        System.Diagnostics.Debug.WriteLine(endSpot.Piece.Name + " can move to: x: " + spot.X.ToString() + ", y: " + spot.Y.ToString() + " Count: " + game.movPoss[endSpot.Piece].Count.ToString());

                    }
                    Graphics gr = Graphics.FromImage(Form1.bm);
                    Pen blankPen = new Pen(startSpot.SpotColor, 1);
                    SolidBrush blankBrush = new SolidBrush(startSpot.SpotColor);
                    
                    //gr.DrawRectangle(blankPen, new Rectangle(startSpot.Y * Form1.hgt / 8, startSpot.X * Form1.wid / 8, Form1.wid / 8-1, Form1.hgt / 8-1));
                    //boardPicture.Invalidate(new Rectangle(startSpot.Y * Form1.hgt / 8, startSpot.X * Form1.wid / 8, Form1.wid / 8, Form1.hgt / 8));
                    gr.FillRectangle(blankBrush, new Rectangle(startSpot.Y * Form1.hgt / 8, startSpot.X * Form1.wid / 8, Form1.wid / 8, Form1.hgt / 8));
                    DrawTool.DrawPieceInit(startSpot.X, startSpot.Y, startSpot.Piece.Name, game.currentTurn.IsWhite);
                    //boardPicture.Invalidate(new Rectangle(startSpot.Y * Form1.hgt / 8, startSpot.X * Form1.wid / 8, Form1.wid / 8, Form1.hgt / 8));
                    startSpot = endSpot;
                    gr.FillRectangle(startBrush, new Rectangle(endSpot.Y * Form1.hgt / 8, endSpot.X * Form1.wid / 8, Form1.wid / 8, Form1.hgt / 8));
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
                    //mouseClick = false;
                    //g.FillRectangle(blankBrush, new Rectangle(endSpot.Y * Form1.hgt / 8, endSpot.X * Form1.wid / 8, Form1.wid / 8, Form1.hgt / 8));
                    return;
                }
                else if (game.moveList.Count > 0)
                {
                    selected = true;
                    SolidBrush blankBrush = new SolidBrush(endSpot.SpotColor);
                    Graphics g = Graphics.FromImage(Form1.bm);
                    Move lastMove = game.moveList.Last();
                    Spot lastSpotStart = lastMove.StartSpot;
                    Spot lastSpotEnd = lastMove.EndSpot;
                    Piece piece = lastMove.MovedPiece;

                   if (lastMove.IsFlipped!= isFlipped)
                    {

                        lastSpotStart = SpotSymetryColor(lastSpotStart);
                        lastSpotEnd = SpotSymetryColor(lastSpotEnd);

                        lastSpotStart.X = Symetry(lastSpotStart.X);
                        lastSpotEnd.Y = Symetry(lastSpotStart.Y);
                        lastSpotStart.X = Symetry(lastSpotStart.X);
                        lastSpotEnd.Y = Symetry(lastSpotStart.Y);
                    }

                    //lastSpotStart.X = Symetry(lastSpotStart.X);
                    //lastSpotStart.Y = Symetry(lastSpotStart.Y);
                    //lastSpotEnd.X = Symetry(lastSpotEnd.X);
                    //lastSpotEnd.Y = Symetry(lastSpotEnd.Y);




                    g.FillRectangle(endBrush, new Rectangle(lastSpotEnd.Y * Form1.hgt / 8, lastSpotEnd.X * Form1.wid / 8, Form1.wid / 8, Form1.hgt / 8));
                    DrawTool.DrawPieceInit(lastSpotEnd.X, lastSpotEnd.Y, piece.Name, piece.Player.IsWhite);
                    //boardPicture.Invalidate(new Rectangle(lastSpotEnd.Y * Form1.hgt / 8, lastSpotEnd.X * Form1.wid / 8, Form1.wid / 8, Form1.hgt / 8));
                    g.FillRectangle(startBrush, new Rectangle(lastSpotStart.Y * Form1.hgt / 8, lastSpotStart.X * Form1.wid / 8, Form1.wid / 8, Form1.hgt / 8));
                    //boardPicture.Invalidate(new Rectangle(lastSpotStart.Y * Form1.hgt / 8, lastSpotStart.X * Form1.wid / 8, Form1.wid / 8, Form1.hgt / 8));
                    boardPicture.Invalidate();
                    if (turn.IsWhite != game.currentTurn.IsWhite)
                    {
                        if (game.moveList.Count - 2 >= 0)
                        {
                            Graphics gm = Graphics.FromImage(Form1.bm);
                            int lastlastIndex = game.moveList.Count - 2;
                            Move lastlastMove = game.moveList[lastlastIndex];

                            Spot lastlastEndSpot = lastlastMove.EndSpot;
                            Spot lastlastStartSpot = lastlastMove.StartSpot;

                            if (lastlastMove.IsFlipped != isFlipped)
                            {

                                lastlastStartSpot = SpotSymetryColor(lastlastStartSpot);
                                lastlastEndSpot = SpotSymetryColor(lastlastEndSpot);

                                lastlastStartSpot.X = Symetry(lastlastStartSpot.X);
                                lastlastStartSpot.Y = Symetry(lastlastStartSpot.Y);
                                lastlastEndSpot.X = Symetry(lastlastEndSpot.X);
                                lastlastEndSpot.Y = Symetry(lastlastEndSpot.Y);
                            }
                            //lastlastEndSpot.X = Symetry(lastlastEndSpot.X);
                            //lastlastEndSpot.Y = Symetry(lastlastEndSpot.Y);
                            //lastlastStartSpot.X = Symetry(lastlastStartSpot.X);
                            //lastlastStartSpot.Y= Symetry(lastlastStartSpot.Y);

                            Piece pieceLastlast = lastlastMove.MovedPiece;
                            Piece pieceLastlastKilled = lastlastMove.KilledPiece;
                            SolidBrush lastEndBrush = new SolidBrush(lastlastEndSpot.SpotColor);
                            SolidBrush lastStartBrush = new SolidBrush(lastlastStartSpot.SpotColor);
                            // COLOR BLANK MOVED PIECE OPPOSITE ADVERSE
                            if (endSpot.Piece == null)
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

        private void btnFlipBoard_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Flip button pressed");

            if (isFlipped == false)
                isFlipped = true;
            else
                isFlipped = false;
            if (player1.BackColor == Color.Green)
            {
                player1.BackColor = Color.White;
                player2.BackColor = Color.Green;
            }
            else
            {
                player1.BackColor = Color.Green;
                player2.BackColor = Color.White;
            }
            if (player1.Text == "PLAYER 2")
            {
                player1.Text = "PLAYER 1";
                player2.Text = "PLAYER 2";
            }
            else
            {
                player1.Text = "PLAYER 2";
                player2.Text = "PLAYER 1";
            }



            System.Diagnostics.Debug.WriteLine("Isflipped: " + isFlipped.ToString());
            newP1.IsUp = !newP1.IsUp;
            newP2.IsUp = !newP2.IsUp;

            if (newP1.IsWhite==true)
            {
                if (game.currentTurn.IsWhite == true)
                    game.currentTurn = newP1;
                else
                    game.currentTurn = newP2;
            }
            else
            {
                if (game.currentTurn.IsWhite == false)
                    game.currentTurn = newP1;
                else
                    game.currentTurn = newP2;
            }


            Game.piecesAlive.Clear();
            game.movPoss.Clear();
            Board board = new Board();
            

            foreach (Spot spot in game.Brd.Boxes)
            {
                board.Boxes[spot.X, spot.Y] = new Spot(spot.X, spot.Y, spot.Piece);
                
            }

            for (int i=0;i<8;i++)
            {
                for (int j = 0; j < 8;j++)
                {

                    Piece piece = null;

                    if (board.Boxes[Symetry(i), Symetry(j)].Piece != null)
                    {
                        if (board.Boxes[Symetry(i), Symetry(j)].Piece.Player.IsWhite == newP1.IsWhite)
                        {
                            if (board.Boxes[Symetry(i), Symetry(j)].Piece.Name == "P")
                            {

                                piece = new Pawn(newP1);
                            }
                            else if (board.Boxes[Symetry(i), Symetry(j)].Piece.Name == "R")
                            {
                                piece = new Rook(newP1);
                            }
                            else if (board.Boxes[Symetry(i), Symetry(j)].Piece.Name == "Kn")
                            {
                                piece = new Knight(newP1);
                            }
                            else if (board.Boxes[Symetry(i), Symetry(j)].Piece.Name == "B")
                            {
                                piece = new Bishop(newP1);
                            }
                            else if (board.Boxes[Symetry(i), Symetry(j)].Piece.Name == "Q")
                            {
                                piece = new Queen(newP1);
                            }
                            else if (board.Boxes[Symetry(i), Symetry(j)].Piece.Name == "K")
                            {
                                piece = new King(newP1);
                            }
                        }
                        else if (board.Boxes[Symetry(i), Symetry(j)].Piece.Player.IsWhite == newP2.IsWhite)
                        {
                            if (board.Boxes[Symetry(i), Symetry(j)].Piece.Name == "P")
                            {

                                piece = new Pawn(newP2);
                            }
                            else if (board.Boxes[Symetry(i), Symetry(j)].Piece.Name == "R")
                            {
                                piece = new Rook(newP2);
                            }
                            else if (board.Boxes[Symetry(i), Symetry(j)].Piece.Name == "Kn")
                            {
                                piece = new Knight(newP2);
                            }
                            else if (board.Boxes[Symetry(i), Symetry(j)].Piece.Name == "B")
                            {
                                piece = new Bishop(newP2);
                            }
                            else if (board.Boxes[Symetry(i), Symetry(j)].Piece.Name == "Q")
                            {
                                piece = new Queen(newP2);
                            }
                            else if (board.Boxes[Symetry(i), Symetry(j)].Piece.Name == "K")
                            {
                                piece = new King(newP2);
                            }
                        }
                            
                    }
                    game.Brd.Boxes[i, j] = new Spot(i, j, piece);

                }
            }
            //Game.playerList.Clear();
            //Game.playerList.Add(newP1);
            //Game.playerList.Add(newP2);

            //game.p1 = game.playerList[0];
            //game.p2 = game.playerList[1];
            foreach (Spot spot in game.Brd.Boxes)
            {
                
                DrawTool.DrawSpotColor(spot);
                if (spot == null || spot.Piece == null)
                {

                }
                else
                {
                    
                    //spot.Piece.Player.IsUp = !spot.Piece.Player.IsUp;
                    //DrawTool.DrawSpotColor(spot);
                    DrawTool.DrawPieceInit(spot.X, spot.Y, spot.Piece.Name, spot.Piece.Player.IsWhite);
                    Game.piecesAlive[spot.Piece] = spot;
                    Game.piecesAlive[spot.Piece].Piece.Player.IsUp = !spot.Piece.Player.IsUp;
                    if (spot.Piece.Name == "K")
                        game.kingPos[spot.Piece.Player.IsWhite] = spot;
                    boardPicture.Invalidate();
                    //Form1.form1.game.movPoss.Add(spot.Piece,new List<Spot>());
                }

            }


            game.getSpotList();
            Graphics gr = Graphics.FromImage(Form1.bm);
            

            
            if ((mouseClick==true) && startSpot!=null)
            {
                Rectangle rectEnd = new Rectangle(Symetry(startSpot.Y) * Form1.hgt / 8, Symetry(startSpot.X) * Form1.wid / 8, Form1.wid / 8, Form1.hgt / 8);


                //gr.DrawRectangle(startPen, rectStart);
                gr.FillRectangle(startBrush, rectEnd);
                DrawTool.DrawPieceInit(Symetry(startSpot.X), Symetry(startSpot.Y), startSpot.Piece.Name, startSpot.Piece.Player.IsWhite);
                startSpot = game.Brd.Boxes[Symetry(startSpot.X), Symetry(startSpot.Y)];
                mouseClick = true;
            }
            if ((endSpot!=null || mouseClick==false) && startSpot!=null)
            {
                Rectangle rect = new Rectangle(Symetry(endSpot.Y) * Form1.hgt / 8, Symetry(endSpot.X) * Form1.wid / 8, Form1.wid / 8, Form1.hgt / 8);
                gr.FillRectangle(endBrush, rect);
                endSpot = game.Brd.Boxes[Symetry(endSpot.X), Symetry(endSpot.Y)];
                DrawTool.DrawPieceInit(endSpot.X, endSpot.Y, endSpot.Piece.Name, endSpot.Piece.Player.IsWhite);
               // mouseClick = false;
                if (mouseClick == true)
                    mouseClick = true;
                else mouseClick = false;
            }
            gr.Dispose();
            
            boardPicture.Invalidate();
            //mouseClick = true;


        }

        private void btnFpBoard_Click(object sender, EventArgs e)
        {

        }
    }
}
