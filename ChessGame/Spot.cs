using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ChessGame
{
    public class Spot
    {
        int x;
        private int y;
        private Piece piece;
        char row;
        char col;
        Color spotColor;

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public Piece Piece { get => piece; set => piece = value; }
        public Color SpotColor { get => spotColor; set => spotColor = value; }
        public char Row { get => row; set => row = value; }
        public char Col { get => col; set => col = value; }

        public Spot(int x, int y, Piece piece)
        {
            this.x = x;
            this.y = y;
            this.piece = piece;
            if (x % 2 == y % 2)
                spotColor = Color.White;
            else
                spotColor = Color.Gray;
            setSpotString();

        }
        public void setSpotString()
        {

            switch(x, Form1.form1.isFlipped)
                {
                    case (0,false):
                        row = 'a';
                        break;
                    case (1, false):
                        row = 'b';
                        break;
                    case (2, false):
                        row = 'c';
                        break;
                    case (3, false):
                        row = 'd';
                        break;
                    case (4, false):
                        row = 'e';
                        break;
                    case (5, false):
                        row = 'f';
                        break;
                    case (6, false):
                        row = 'h';
                        break;
                    case (7, false):
                        row = 'g';
                        break;
                    ///////////////////////

                    case (7, true):
                        row = 'a';
                        break;
                    case (6, true):
                        row = 'b';
                        break;
                    case (5, true):
                        row = 'c';
                        break;
                    case (4, true):
                        row = 'd';
                        break;
                    case (3, true):
                        row = 'e';
                        break;
                    case (2, true):
                        row = 'f';
                        break;
                    case (1, true):
                        row = 'h';
                        break;
                    case (0, true):
                        row = 'g';
                        break;
            }
            switch (y, Form1.form1.isFlipped)
            {
                case (0, false):
                    col = '0';
                    break;
                case (1, false):
                    col = '1';
                    break;
                case (2, false):
                    col = '2';
                    break;
                case (3, false):
                    col = '3';
                    break;
                case (4, false):
                    col = '4';
                    break;
                case (5, false):
                    col = '5';
                    break;
                case (6, false):
                    col = '6';
                    break;
                case (7, false):
                    col = '7';
                    break;
                ///////////////////////

                case (7, true):
                    col = '0';
                    break;
                case (6, true):
                    col = '1';
                    break;
                case (5, true):
                    col = '2';
                    break;
                case (4, true):
                    col = '3';
                    break;
                case (3, true):
                    col = '4';
                    break;
                case (2, true):
                    col = '5';
                    break;
                case (1, true):
                    col = '6';
                    break;
                case (0, true):
                    col = '7';
                    break;
            }

        }
        }




    }
}
