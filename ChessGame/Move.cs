﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ChessGame
{
    public class Move
    {
        Player player;
        Spot startSpot;
        Spot endSpot;
        Piece movedPiece;
        Piece killedPiece;

        public Move(Player player, Spot startSpot, Spot endSpot, Piece movedPiece, Piece killedPiece)
        {
            this.player = player;
            this.startSpot = startSpot;
            this.endSpot = endSpot;
            this.movedPiece = movedPiece;
            this.killedPiece = killedPiece;
        }

        public Spot EndSpot { get => endSpot; set => endSpot = value; }
        public Piece MovedPiece { get => movedPiece; set => movedPiece = value; }
        public Piece KilledPiece { get => killedPiece; set => killedPiece = value; }
        public Spot StartSpot { get => startSpot; set => startSpot = value; }
        public Player Player { get => player; set => player = value; }
    }

    public enum GameStatus
    {
        ACTIVE,
        BLACK_WIN,
        WHITE_WIN,
        FORFEIT,
        STALEMATE,
        RESIGNATION
    }
}
