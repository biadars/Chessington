using System;
using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            List<Square> moves = new List<Square>();
            Square position = board.FindPiece(this);
            for (int i = -7; i < 8; i++)
                moves.Add(Square.At(position.Row + i, position.Col + i));
            for (int i = -7; i < 8; i++)
                moves.Add(Square.At(position.Row + i, position.Col - i));
            moves.RemoveAll(move => move == position || !Board.InBounds(move));
            return moves;
        }
    }
}