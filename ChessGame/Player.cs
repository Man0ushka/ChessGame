using System;
using System.Collections.Generic;
using System.Text;

namespace ChessGame
{
    public abstract class Player
    {
        bool isHuman;
        bool isWhite;
        bool isUp;
        public bool IsHuman { get => isHuman; set => isHuman = value; }
        public bool IsWhite { get => isWhite; set => isWhite = value; }
        public bool IsUp { get => isUp; set => isUp = value; }
    }
    public class WhiteHuman : Player
    {
        public WhiteHuman(bool isUp)
        {
            this.IsHuman = true;
            this.IsWhite = true;
            this.IsUp = isUp;
        }
    }
    public class BlackHuman : Player
    {
        public BlackHuman(bool isUp)
        {
            this.IsHuman = true;
            this.IsWhite = false;
            this.IsUp = isUp;
        }
    }
    public class WhiteComputer : Player
    {
        public WhiteComputer(bool isUp)
        {
            this.IsHuman = false;
            this.IsWhite = true;
            this.IsUp = isUp;
        }
    }
    public class BlackComputer : Player
    {
        public BlackComputer(bool isUp)
        {
            this.IsHuman = false;
            this.IsWhite = false;
            this.IsUp = isUp;
        }
    }
}
