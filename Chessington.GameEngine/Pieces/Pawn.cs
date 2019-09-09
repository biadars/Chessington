﻿using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(Player player) 
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            List < Square > moves = new List<Square>();
            Square position = board.FindPiece(this);
            if (board.CurrentPlayer == Player.White)
                moves.Add(Square.At(position.Row - 1, position.Col));
            else
                moves.Add(Square.At(position.Row + 1, position.Col));
            return moves;
        }
    }
}