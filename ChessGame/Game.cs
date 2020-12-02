using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChessGame
{
    public class Game
    {
        List<Player> playerList = new List<Player>();
        public List<Move> moveList = new List<Move>();
        public static Board brd = new Board();
        GameStatus status;
        public Player currentTurn;
        public Dictionary<bool,Spot> kingPos = new Dictionary<bool, Spot>();
        Spot whiteKingSpot;
        Spot blackKingSpot;
        public Dictionary<bool, bool> isCheck = new Dictionary<bool, bool>();
        public Dictionary<bool, bool> canBlock =new Dictionary<bool, bool>();
        public Dictionary<bool, bool> checkMate = new Dictionary<bool, bool>();


        public GameStatus Status { get => status; set => status = value; }
        public Board Brd { get => brd; set => brd = value; }

        public Game()
        {
            
            Player p1 = new BlackHuman(false);
            Player p2 = new WhiteHuman(true);
            initializeGame(p1, p2);
            playerList.Add(p1);
            playerList.Add(p2);
        }

        public void initializeGame(Player p1, Player p2)
        {
            if (p1.IsWhite == true)
                currentTurn = p1;
            else currentTurn = p2;
            if (p1.IsUp == true)
            {

                
                brd.resetBoard(p1, p2);
            }
                
            else
            {

                
                brd.resetBoard(p2, p1);
            }
            if (currentTurn == p1)
            {
                Form1.form1.player1.BackColor = Color.Green;
                Form1.form1.player2.BackColor = Color.White;
            }
            else
            {
                Form1.form1.player2.BackColor = Color.Green;
                Form1.form1.player1.BackColor = Color.White;
            }
            
            kingPos.Clear();
            isCheck.Clear();
            canBlock.Clear();
            checkMate.Clear();
            if (p1.IsUp==true)
            {
                whiteKingSpot = new Spot(7, 4, new King(p2));
                blackKingSpot = new Spot(0, 4, new King(p1));
                kingPos.Add(p2.IsWhite, whiteKingSpot);
                kingPos.Add(p1.IsWhite, blackKingSpot);
            }
            else
            {
                whiteKingSpot = new Spot(7, 4, new King(p1));
                blackKingSpot = new Spot(0, 4, new King(p2));
                kingPos.Add(p1.IsWhite, whiteKingSpot);
                kingPos.Add(p2.IsWhite, blackKingSpot);
            }


            isCheck.Add(currentTurn.IsWhite, false);
            isCheck.Add(!currentTurn.IsWhite, false);
            canBlock.Add(currentTurn.IsWhite, true);
            canBlock.Add(!currentTurn.IsWhite, true);
            checkMate.Add(currentTurn.IsWhite, false);
            checkMate.Add(!currentTurn.IsWhite, false);
            moveList.Clear();
            
        }

        public Spot isInCheck(Player p)
        {
            bool white = p.IsWhite;

            for (int x=0;x<8;x++)
            {
                for (int y=0;y<8;y++)
                {
                    Spot spot = brd.getBox(x, y);

                    if (spot.Piece != null && spot.Piece.Player.IsWhite!=white)
                    {
                        if (spot.Piece.Name == "P")
                        {
                            int xP = (kingPos[white].X - spot.X);
                            int yP = kingPos[white].Y - spot.Y;
                            if (spot.Piece.Player.IsUp == true)
                            {
                                
                                // DIAGONAL MOVEMENT
                                if ((yP == 1 && xP == 1) || (yP == -1 && xP == 1))
                                {
                                    return spot;
                                }
                            }
                            else
                            {
                                
                                if ((yP == 1 && xP == -1) || (yP == -1 && xP == -1))
                                {
                                    return spot;
                                }
                            }
                        }
                        else if (spot.Piece.canMove(spot, kingPos[white]))
                        {
                            
                            return spot;
                        }
                            



                    }
                        
                    
                }
            }
            return null;
        }

        public bool isCheckMate(Player p)
        {
            // CHECKMATE CASE
            
            // IF NO PIECE OF YOURS CAN MOVE IN FRONT OF ATTACKER
            // IF KING CANT MOVE
            Spot attackerSpot = isInCheck(p);
            if (isCheck[p.IsWhite])
            {
                for (int x = 0; x < 8; x++)
                {
                    for (int y = 0; y < 8; y++)
                    {
                        Spot spot = brd.getBox(x, y);
                        if (spot.Piece!=null && spot.Piece.Player.IsWhite==p.IsWhite)
                        {
                            bool canMove = spot.Piece.canMove(spot, attackerSpot);
                            bool canMoveWithoutCheck = CanMovWithoutChecked(spot.Piece,attackerSpot.Piece, spot, attackerSpot);
                            if (canMove)
                            {
                                if (canMoveWithoutCheck == true)
                                    return false;
                            }
                            
                               

                                for (int i = 0; i < 8; i++)
                                {
                                    for (int j = 0; j < 8; j++)
                                    {
                                        Spot endPoint = brd.getBox(i, j);
                                        if (spot.Piece.canMove(spot, endPoint))
                                        {
                                            if (CanMovWithoutChecked(spot.Piece,endPoint.Piece, spot, endPoint))
                                                return false;
                                        }

                                    }
                                }
                            

                        }
                       
                        
                        
                        // IF NO PIECE OF YOURS CAN MOVE TO THE SPOT OF ATTACKER OR IN FRONT
                        
                        

                    }
                }
            }
            return true;
        }
        public bool CanMovWithoutChecked(Piece sourcePiece,Piece endPiece, Spot start, Spot end)
        {
            if (sourcePiece.canMove(start, end))
            {
                
                Game.brd.replaceBox(start.X, start.Y, null);
                Game.brd.replaceBox(end.X, end.Y, sourcePiece);
                if (sourcePiece.Name == "K")
                    kingPos[currentTurn.IsWhite] = brd.getBox(end.X,end.Y);
                if (isInCheck(currentTurn) != null)
                {
                    
                    Game.brd.replaceBox(start.X, start.Y, sourcePiece);
                    Game.brd.replaceBox(end.X, end.Y, endPiece);
                    if (sourcePiece.Name == "K")
                        kingPos[currentTurn.IsWhite] = brd.getBox(start.X, start.Y);
                    return false;
                }
                Game.brd.replaceBox(start.X, start.Y, sourcePiece);
                Game.brd.replaceBox(end.X, end.Y, endPiece);
                if (sourcePiece.Name == "K")
                    kingPos[currentTurn.IsWhite] = brd.getBox(start.X, start.Y);
            }
            else
            {
                
                return false;
            }
                
            
            return true;
        }
        public bool makeMove(Spot startPoint, Spot endPoint)
        {
            
            Piece sourcePiece = startPoint.Piece;
            Piece endPiece = endPoint.Piece;
            King king = (King)kingPos[currentTurn.IsWhite].Piece;

            if (isCheck[currentTurn.IsWhite])
                king.Check = true;
            else
                king.Check = false;
            
            if (sourcePiece.Player.IsWhite!=currentTurn.IsWhite)
            {
                System.Diagnostics.Debug.WriteLine("not your turn!");
                return false;
            }
            if (sourcePiece.Name == "K" && king.Check==false)
            {

                //king = sourcePiece as King;
                if (currentTurn.IsUp == false)
                {
                    //WHITE CASTLE SHORT
                    if (king.Castled == false && king.Moved == false && Brd.getBox(7, 5).Piece == null && Brd.getBox(7, 6).Piece == null && Brd.getBox(7, 7).Piece != null && Brd.getBox(7, 7).Piece.Name == "R" && Brd.getBox(7, 7).Piece.Player.IsWhite == currentTurn.IsWhite)
                        king.CanCastleShort = true;
                    if (king.Castled == false && king.Moved == false && Brd.getBox(7, 3).Piece == null && Brd.getBox(7, 2).Piece == null && Brd.getBox(7, 1).Piece == null && Brd.getBox(7, 0).Piece != null && Brd.getBox(7, 0).Piece.Name == "R" && Brd.getBox(7, 0).Piece.Player.IsWhite == currentTurn.IsWhite)
                        king.CanCastleLong = true;
                    sourcePiece = king;
                }
                else
                {
                    //BLACK CASTLE SHORT
                    if (king.Castled == false && king.Moved == false && Brd.getBox(0, 5).Piece == null && Brd.getBox(0, 6).Piece == null && Brd.getBox(0, 7).Piece != null && Brd.getBox(0, 7).Piece.Name == "R" && Brd.getBox(0, 7).Piece.Player.IsWhite == currentTurn.IsWhite)
                        king.CanCastleShort = true;
                    if (king.Castled == false && king.Moved == false && Brd.getBox(0, 3).Piece == null && Brd.getBox(0, 2).Piece == null && Brd.getBox(0, 1).Piece == null && Brd.getBox(0, 0).Piece != null && Brd.getBox(0, 0).Piece.Name == "R" && Brd.getBox(0, 0).Piece.Player.IsWhite == currentTurn.IsWhite)
                        king.CanCastleLong = true;
                    sourcePiece = king;
                }
            }
            bool canMoveWithoutChecked = CanMovWithoutChecked(sourcePiece,endPiece, startPoint, endPoint);
            bool canMove = sourcePiece.canMove(startPoint, endPoint);

            
            // CHECK CASE

            // CAN YOU MOVE WITHOUT BEING CHECKED
            /* if (canMove)
                 {
                     if (sourcePiece.Name == "K")
                         kingPos[currentTurn.IsWhite] = endPoint;
                     brd.replaceBox(startPoint.X, startPoint.Y, null);
                     brd.replaceBox(endPoint.X, endPoint.Y, sourcePiece);
                     if (isInCheck(currentTurn) != null)
                     {
                         brd.replaceBox(startPoint.X, startPoint.Y, sourcePiece);
                         brd.replaceBox(endPoint.X, endPoint.Y, null);
                         if (sourcePiece.Name == "K")
                             kingPos[currentTurn.IsWhite] = startPoint;
                         return false;
                     }
                     brd.replaceBox(startPoint.X, startPoint.Y, sourcePiece);
                     brd.replaceBox(endPoint.X, endPoint.Y, null);
                     if (sourcePiece.Name == "K")
                         kingPos[currentTurn.IsWhite] = startPoint;
                 }
                 else return false;*/



            //////////////// KING CASTLING
            if (canMove)
            {
                if (canMoveWithoutChecked == false)
                {
                    
                    return false;
                }

            }
            else
            {
                
                return false;
            }


            ///////////////////////////////

            
            if (canMove)
            {
                
                if (sourcePiece.Name == "K")
                {
                    
                    // REMEMBER KING POSITION


                   // king = sourcePiece as King;
                    if (startPoint.Y - endPoint.Y == 2 && king.Check==false) //CASTLE LONG
                    {
                        king.Castled = true;
                        king.CanCastleShort = false;
                        king.CanCastleLong = false;
                        System.Diagnostics.Debug.WriteLine("Castled Queen Side: " + king.Castled.ToString());
                        // MOVE ROOK WHITE
                        if (currentTurn.IsUp==false)
                        {
                            DrawTool.DrawPiece(brd.getBox(7,0), brd.getBox(7, 3), brd.getBox(7, 0).Piece.Name, brd.getBox(7, 0).Piece.Player.IsWhite);
                            brd.replaceBox(brd.getBox(7, 3).X, brd.getBox(7, 3).Y, brd.getBox(7, 0).Piece);
                            brd.replaceBox(brd.getBox(7, 0).X, brd.getBox(7, 0).Y, null);
                           
                            
                        }
                        else //MOVE ROOK BLACK
                        {
                            DrawTool.DrawPiece(brd.getBox(0, 0), brd.getBox(0, 3), brd.getBox(0, 0).Piece.Name, brd.getBox(0, 0).Piece.Player.IsWhite);
                            brd.replaceBox(brd.getBox(0, 3).X, brd.getBox(0, 3).Y, brd.getBox(0, 0).Piece);
                            brd.replaceBox(brd.getBox(0, 0).X, brd.getBox(0, 0).Y, null);
                            
                        }
                        
                    }
                    else if (startPoint.Y - endPoint.Y == -2 && king.Check == false) //CASTLE SHORT
                    {
                        king.Castled = true;
                        king.CanCastleShort = false;
                        king.CanCastleLong = false;
                        System.Diagnostics.Debug.WriteLine("Castled King Side: " + king.Castled.ToString());
                        // MOVE ROOK WHITE
                        if (currentTurn.IsUp == false)
                        {
                            DrawTool.DrawPiece(brd.getBox(7, 7), brd.getBox(7, 5), brd.getBox(7, 7).Piece.Name, brd.getBox(7, 7).Piece.Player.IsWhite);
                            brd.replaceBox(brd.getBox(7, 5).X, brd.getBox(7, 5).Y, brd.getBox(7, 7).Piece);
                            brd.replaceBox(brd.getBox(7, 7).X, brd.getBox(7, 7).Y, null);
                            
                        }
                        else //MOVE ROOK BLACK
                        {
                            DrawTool.DrawPiece(brd.getBox(0, 7), brd.getBox(0, 5), brd.getBox(0, 7).Piece.Name, brd.getBox(0, 7).Piece.Player.IsWhite);
                            brd.replaceBox(brd.getBox(0, 5).X, brd.getBox(0, 5).Y, brd.getBox(0, 7).Piece);
                            brd.replaceBox(brd.getBox(0, 7).X, brd.getBox(0, 7).Y, null);
                            
                        }

                    }

                    king.Moved = true;
                    //sourcePiece = king;
                    System.Diagnostics.Debug.WriteLine("Moved: " + king.Moved.ToString());
                    isCheck[currentTurn.IsWhite] = false;
                    king.Check = false;
                }

                if (endPiece != null) //KILL PIECE
                {
                    endPoint.Piece.Alive = false;
                    // DELETE PIECE FROM DRAWING
                   // DrawTool.DrawPiece(startPoint, endPoint, sourcePiece.Name, sourcePiece.Player);
                    brd.replaceBox(startPoint.X, startPoint.Y, null);
                    brd.replaceBox(endPoint.X, endPoint.Y, sourcePiece);
                    
                    moveList.Add(new Move(currentTurn, startPoint, endPoint, sourcePiece, endPiece));
                    System.Diagnostics.Debug.WriteLine("Player: " + currentTurn.IsWhite.ToString() + " moved " + startPoint.Piece.Name +" x: "+startPoint.X+" y: "+startPoint.Y+ " to " + "x: " + endPoint.X + " y: " + endPoint.Y+" killed: "+endPiece.Name);
                }
                else //MOVE PIECE
                {
                    brd.replaceBox(startPoint.X, startPoint.Y, null);
                    brd.replaceBox(endPoint.X, endPoint.Y, sourcePiece);
                    //DrawTool.DrawPiece(startPoint, endPoint, sourcePiece.Name, sourcePiece.Player);
                    moveList.Add(new Move(currentTurn, startPoint, endPoint, sourcePiece, null));
                    System.Diagnostics.Debug.WriteLine("Player: " + currentTurn.IsWhite.ToString() + " moved " + startPoint.Piece.Name + " x: " + startPoint.X + " y: " + startPoint.Y + " to " + "x: " + endPoint.X + " y: " + endPoint.Y);
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("illegal move!");
                return false;
            }
            if (sourcePiece.Name=="K")
            kingPos[currentTurn.IsWhite] = brd.getBox(endPoint.X, endPoint.Y);

            if (currentTurn == playerList[0])
                currentTurn = playerList[1];
            else currentTurn = playerList[0];

            // CHECK OR CHECKMATE

            if (isInCheck(currentTurn) != null)
            {
                isCheck[currentTurn.IsWhite] = true;
                
                if (isCheckMate(currentTurn))
                {
                    System.Diagnostics.Debug.WriteLine("YOU ARE IN CHECKMATE" + currentTurn.IsWhite.ToString());
                    initializeGame(playerList[0], playerList[1]);
                }
                    
                else
                System.Diagnostics.Debug.WriteLine("YOU ARE IN CHECK" + currentTurn.IsWhite.ToString());
            }
            else isCheck[currentTurn.IsWhite] = false;

            // CHANGE COLOR LABEL
            if (currentTurn == playerList[0])
            {
                Form1.form1.player1.BackColor = Color.Green;
                Form1.form1.player2.BackColor = Color.White;
            }
            else
            {
                Form1.form1.player2.BackColor = Color.Green;
                Form1.form1.player1.BackColor = Color.White;
            }
            return true;
        }
        
    }
}
