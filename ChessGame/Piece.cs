using System;
using System.Collections.Generic;
using System.Text;

namespace ChessGame
{

    public abstract class Piece
    {

        private Player player;
        private bool alive=true;
        string name;
        public Player Player { get => player; set => player = value; }
        public bool Alive { get => alive; set => alive = value; }
        public string Name { get => name; set => name = value; }

        public Piece(Player player)
        {
            this.player = player;
        }

        public abstract bool canMove(Spot start, Spot end);

        

        public bool BishopMov(int x, int y, Spot start)
        {
            if (y < 0 && x < 0)
            {
                for (int i = 1; i < Math.Abs(y); i++)
                {
                    if (Game.brd.getBox(start.X - i, start.Y - i).Piece != null)
                        return false;
                }
            }
            if (y > 0 && x > 0)
            {
                for (int i = 1; i < Math.Abs(y); i++)
                {
                    if (Game.brd.getBox(start.X + i, start.Y + i).Piece != null)
                        return false;
                }
            }
            if (y > 0 && x < 0)
            {
                for (int i = 1; i < Math.Abs(y); i++)
                {
                    if (Game.brd.getBox(start.X - i, start.Y + i).Piece != null)
                        return false;
                }
            }
            if (y < 0 && x > 0)
            {
                for (int i = 1; i < Math.Abs(y); i++)
                {
                    if (Game.brd.getBox(start.X + i, start.Y - i).Piece != null)
                        return false;
                }
            }
            return true;
        }
        public bool RookMov(int x, int y, Spot start)
        {
            //HORIZONTAL
            if (x == 0)
            {
                if (y >= 0) //RIGHT
                {
                    for (int i = 1; i < Math.Abs(y); i++)
                    {
                        if (start.Y + i > 7)
                            return false;
                        if (Game.brd.getBox(start.X, start.Y + i).Piece != null)
                            return false;
                    }
                }
                if (y <= 0) //LEFT
                {
                    for (int i = 1; i < Math.Abs(y); i++)
                    {
                        if (start.Y - i < 0)
                            return false;
                        if (Game.brd.getBox(start.X, start.Y - i).Piece != null)
                            return false;
                    }
                }
                return true;
            }

            //VERTICAL
            if (y == 0)
            {
                if (x >= 0) // DOWNWARDS
                {
                    for (int i = 1; i < Math.Abs(x); i++)
                    {
                        if (start.X + i > 7)
                            return false;
                        if (Game.brd.getBox(start.X + i, start.Y).Piece != null)
                            return false;
                    }
                }
                if (x <= 0) //UPWARDS
                {
                    for (int i = 1; i < Math.Abs(x); i++)
                    {
                        if (start.X - i < 0)
                            return false;
                        if (Game.brd.getBox(start.X - i, start.Y).Piece != null)
                            return false;
                    }
                }
                return true;
            }

            else return false;
        }
    }

    public class Pawn : Piece
    {
        
        public Pawn(Player player) : base(player)
        {
            this.Name = "P";
        }
        public override bool canMove(Spot start, Spot end)
        {
            int x = (end.X-start.X);
            int y = end.Y-start.Y;
            // CHECK IF KING IN CHECK
            // CANT MOVE TO SPOT OCCUPIED BY ALLY
            if (start.Piece == null || start.Piece.Player != this.Player)
            {
                
                return false;
            }
                
            if (end.Piece != null && end.Piece.Player == this.Player)
                return false;
            // CANT MOVE BEYOND BORDER
            if (end.X > 8 || end.Y > 8)
                return false;
            // FORWARD MOVEMENT

            if (start.Piece.Player.IsUp == true)
            {
                if (y == 0 && x == 1 && end.Piece == null)
                    return true;
                if (y == 0 && x == 2 && end.Piece == null && start.X == 1 && Game.brd.getBox(2,start.Y).Piece==null)
                    return true;
                // DIAGONAL MOVEMENT
                if (y == 1 && x == 1 && end.Piece != null || y == -1 && x == 1 && end.Piece != null)
                {
                    end.Piece.Alive = false;
                    return true;
                }
            }
            else
            {
                if (y == 0 && x == -1 && end.Piece == null)
                    return true;
                if (y == 0 && x == -2 && end.Piece == null && start.X == 6 && Game.brd.getBox(5, start.Y).Piece == null)
                    return true;
                if (y == 1 && x == -1 && end.Piece != null || y == -1 && x == -1 && end.Piece != null)
                {
                    end.Piece.Alive = false;
                    return true;
                }
            }
            //System.Diagnostics.Debug.WriteLine("x: "+x.ToString()+" y: "+y.ToString()+" up? "+ start.Piece.Player.IsUp.ToString());
            return false;
        }

    }
    public class King : Piece
    {
        private bool castled = false;
        bool moved = false;
        bool canCastleShort = false;
        bool canCastleLong = false;
        bool check = false;
        public King(Player player) : base(player)
        {
            this.Name = "K";
        }

        public bool Castled { get => castled; set => castled = value; }
        public bool Moved { get => moved; set => moved = value; }
        public bool CanCastleShort { get => canCastleShort; set => canCastleShort = value; }
        public bool CanCastleLong { get => canCastleLong; set => canCastleLong = value; }
        public bool Check { get => check; set => check = value; }

        public override bool canMove(Spot start, Spot end)
        {
            int x = Math.Abs(end.X - start.X);
            int y = Math.Abs(end.Y - start.Y);
            // CHECK IF KING IN CHECK
            // CANT MOVE TO SPOT OCCUPIED BY ALLY
            if (start.Piece == null || start.Piece.Player != this.Player)
                return false;
            if (end.Piece != null && end.Piece.Player == this.Player)
                return false;
            // CANT MOVE BEYOND BORDER
            if (end.X > 8 || end.Y > 8)
                return false;

            // FORWARD MOVEMENT
            if ((x == 1 && y==0) || (y==1 && x==0) || (x==1 && y==1))
                return true;
            if (this.moved==false && this.castled==false && this.check==false)
            {
                if (this.canCastleShort == true)
                {
                    if (this.Player.IsUp==false)
                    {
                        if ((start.X == 7 && start.Y == 4) && ((end.X == 7 && end.Y == 6)))
                            return true;
                    }
                    else
                        if ((start.X == 0 && start.Y == 4) && ((end.X == 0 && end.Y == 6)))
                        return true;
                }
                if (this.canCastleLong == true)
                {
                    if (this.Player.IsUp == false)
                    {
                        if ((start.X == 7 && start.Y == 4) && ((end.X == 7 && end.Y == 2)))
                            return true;
                    }
                    else
                        if ((start.X == 0 && start.Y == 4) && ((end.X == 0 && end.Y == 2)))
                        return true;
                }
            }
            

            return false;
        }
    }
    public class Queen : Piece
    {
        private bool castled = false;
        public Queen(Player player) : base(player)
        {
            this.Name = "Q";
        }


        public override bool canMove(Spot start, Spot end)
        {
            int x = end.X - start.X;
            int y = end.Y - start.Y;
            // CHECK IF KING IN CHECK
            // CANT MOVE TO SPOT OCCUPIED BY ALLY
            if (start.Piece == null || start.Piece.Player != this.Player)
                return false;
            if (end.Piece!=null && end.Piece.Player == this.Player)
                return false;
            // CANT MOVE BEYOND BORDER
            if (end.X > 8 || end.Y > 8)
                return false;

            //CANT MOVE BEYOND OCCUPIED PIECE

            //DIAGONAL
            if (Math.Abs(y) == Math.Abs(x))
                return BishopMov(x, y, start);
            //LATERAL
            if (x==0||y==0)
                return RookMov(x,y,start);
            else return false;
        }
    }

    public class Knight : Piece
    {
        private bool castled = false;
        public Knight(Player player) : base(player)
        {
            this.Name = "Kn";
        }


        public override bool canMove(Spot start, Spot end)
        {
            int x = Math.Abs(end.X - start.X);
            int y = Math.Abs(end.Y - start.Y);
            // CHECK IF KING IN CHECK
            // CANT MOVE TO SPOT OCCUPIED BY ALLY
            if (start.Piece == null || start.Piece.Player != this.Player)
                return false;
            if (end.Piece != null && end.Piece.Player == this.Player)
                return false;
            // CANT MOVE BEYOND BORDER
            if (end.X > 8 || end.Y > 8)
                return false;

            //SPECIFIC MOVEMENT

            if (x * y == 2)
                return true;
            //CANT MOVE BEYOND OCCUPIED PIECE
            else return false;
        }
    }
    public class Bishop : Piece
    {
        private bool castled = false;
        public Bishop(Player player) : base(player)
        {
            this.Name = "B";
        }


        public override bool canMove(Spot start, Spot end)
        {
            int x = end.X - start.X;
            int y = end.Y - start.Y;
            Piece sourcePiece = start.Piece;
            // CHECK IF KING IN CHECK
            // CANT MOVE TO SPOT OCCUPIED BY ALLY
            if (start.Piece == null || start.Piece.Player != this.Player)
                return false;
            if (end.Piece != null && end.Piece.Player == this.Player)
                return false;
            // CANT MOVE BEYOND BORDER
            if (end.X > 8 || end.Y > 8)
                return false;

            if (Math.Abs(y) == Math.Abs(x))
                return BishopMov(x, y, start);
            //CANT MOVE BEYOND OCCUPIED PIECE

            // CAN MOVE WITHOUT BEING CHECKED
            

            return false;
        }
    }
    public class Rook : Piece
    {
        private bool castled = false;
        public Rook(Player player) : base(player)
        {
            this.Name = "R";
        }


        public override bool canMove(Spot start, Spot end)
        {
            int x = end.X - start.X;
            int y = end.Y - start.Y;
            // CHECK IF KING IN CHECK
            // CANT MOVE TO SPOT OCCUPIED BY ALLY
            if (start.Piece == null || start.Piece.Player != this.Player)
                return false;
            if (end.Piece != null && end.Piece.Player == this.Player)
                return false;
            // CANT MOVE BEYOND BORDER
            if (end.X > 8 || end.Y > 8)
                return false;

            if (x == 0 || y == 0)
                return RookMov(x, y, start);
            else return false;
        }
    }
}
