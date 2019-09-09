using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Knight : Piece
    {
        public Knight(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            List<Square> moves = new List<Square>();
            Square position = board.FindPiece(this);
            moves.Add(Square.At(position.Row - 2, position.Col + 1));
            moves.Add(Square.At(position.Row - 1, position.Col + 2));
            moves.Add(Square.At(position.Row + 1, position.Col + 2));
            moves.Add(Square.At(position.Row + 2, position.Col + 1));
            moves.Add(Square.At(position.Row + 2, position.Col - 1));
            moves.Add(Square.At(position.Row + 1, position.Col - 2));
            moves.Add(Square.At(position.Row - 1, position.Col - 2));
            moves.Add(Square.At(position.Row - 2, position.Col - 1));
            moves.RemoveAll(move => !Board.InBounds(move));
            return moves;
        }
    }
}