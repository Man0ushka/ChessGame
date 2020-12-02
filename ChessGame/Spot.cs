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
        Color spotColor;

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public Piece Piece { get => piece; set => piece = value; }
        public Color SpotColor { get => spotColor; set => spotColor = value; }

        public Spot(int x, int y, Piece piece)
        {
            this.x = x;
            this.y = y;
            this.piece = piece;
            if (x % 2 == y % 2)
                spotColor = Color.White;
            else
                spotColor = Color.Gray;
        }




    }
}
