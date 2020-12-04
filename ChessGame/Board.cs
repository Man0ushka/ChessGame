using System;
using System.Collections.Generic;
using System.Text;

namespace ChessGame
{
    public class Board
    {
        Spot[,] boxes = new Spot[8, 8];
        public Board()
        {
            //this.resetBoard();
        }

        public Spot[,] Boxes { get => boxes; set => boxes = value; }

        public Spot getBox(int x, int y)
        {

            if (x < 0 || x > 7 || y < 0 || y > 7)
            {
                throw new Exception("Index out of bound");
            }

            return boxes[x,y];
        }
        public void replaceBox(int x, int y, Piece piece)
        {
            boxes[x, y] = new Spot(x, y, piece);
        }

        public void resetBoard(Player up, Player down)
        {
            /*if (Form1.form1.isFlipped==false)
            {
                boxes[0, 3] = new Spot(0, 3, new Queen(up));
                boxes[0, 4] = new Spot(0, 4, new King(up));

                boxes[7, 3] = new Spot(7, 3, new Queen(down));
                boxes[7, 4] = new Spot(7, 4, new King(down));

            }
            else
            {
                boxes[0, 3] = new Spot(0, 3, new King(up));
                boxes[0, 4] = new Spot(0, 4, new Queen(up));

                boxes[7, 3] = new Spot(7, 3, new King(down));
                boxes[7, 4] = new Spot(7, 4, new Queen(down));
            }*/
            // initialize black pieces 

            boxes[0, 3] = new Spot(0, 3, new Queen(up));
            boxes[0, 4] = new Spot(0, 4, new King(up));

            boxes[7, 3] = new Spot(7, 3, new Queen(down));
            boxes[7, 4] = new Spot(7, 4, new King(down));

            boxes[0,0] = new Spot(0, 0, new Rook(up));
            boxes[0,1] = new Spot(0, 1, new Knight(up));
            boxes[0,2] = new Spot(0, 2, new Bishop(up));
           
            boxes[0, 5] = new Spot(0, 5, new Bishop(up));
            boxes[0, 6] = new Spot(0, 6, new Knight(up));
            boxes[0, 7] = new Spot(0, 7, new Rook(up));

            //... 
            boxes[1,0] = new Spot(1, 0, new Pawn(up));
            boxes[1,1] = new Spot(1, 1, new Pawn(up));
            boxes[1, 2] = new Spot(1, 2, new Pawn(up));
            boxes[1, 3] = new Spot(1, 3, new Pawn(up));
            boxes[1, 4] = new Spot(1, 4, new Pawn(up));
            boxes[1, 5] = new Spot(1, 5, new Pawn(up));
            boxes[1, 6] = new Spot(1, 6, new Pawn(up));
            boxes[1, 7] = new Spot(1, 7, new Pawn(up));
            //... 

            // initialize white pieces 
            boxes[7,0] = new Spot(7, 0, new Rook(down));
            boxes[7,1] = new Spot(7, 1, new Knight(down));
            boxes[7,2] = new Spot(7, 2, new Bishop(down));
            
            boxes[7, 5] = new Spot(7, 5, new Bishop(down));
            boxes[7, 6] = new Spot(7, 6, new Knight(down));
            boxes[7, 7] = new Spot(7, 7, new Rook(down));
            //... 
            boxes[6,0] = new Spot(6, 0, new Pawn(down));
            boxes[6,1] = new Spot(6, 1, new Pawn(down));
            boxes[6, 2] = new Spot(6, 2, new Pawn(down));
            boxes[6, 3] = new Spot(6, 3, new Pawn(down));
            boxes[6, 4] = new Spot(6, 4, new Pawn(down));
            boxes[6, 5] = new Spot(6, 5, new Pawn(down));
            boxes[6, 6] = new Spot(6, 6, new Pawn(down));
            boxes[6, 7] = new Spot(6, 7, new Pawn(down));



            //... 
            for (int i = 2; i < 6; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    boxes[i, j] = new Spot(i, j, null);
                    DrawTool.DrawSpotColor(getBox(i, j));
                }
            }
            foreach (Spot spot in boxes)
            {
               
                if ( spot== null||spot.Piece == null)
                {

                }
                else
                {
                    DrawTool.DrawSpotColor(spot);
                    DrawTool.DrawPieceInit(spot.X, spot.Y, spot.Piece.Name, spot.Piece.Player.IsWhite);
                    Game.piecesAlive[spot.Piece]=spot;
                    //Form1.form1.game.movPoss.Add(spot.Piece,new List<Spot>());
                }
                    
            }
            // initialize remaining boxes without any piece 
           

        }
    }
}
