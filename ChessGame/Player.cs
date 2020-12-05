using System;
using System.Collections.Generic;
using System.Text;

namespace ChessGame
{
    public class Player
    {
        bool isHuman;
        bool isWhite;
        bool isUp;
        public bool IsHuman { get => isHuman; set => isHuman = value; }
        public bool IsWhite { get => isWhite; set => isWhite = value; }
        public bool IsUp { get => isUp; set => isUp = value; }

        public Player(bool isWhite, bool isUp, bool isHuman)
        {
            this.isHuman = isHuman;
            this.isWhite = isWhite;
            this.isUp = isUp;
        }
    }
    
}
