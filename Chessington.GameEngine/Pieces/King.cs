using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class King : Piece
    {
        public King(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            List<Square> moves = new List<Square>();
            Square position = board.FindPiece(this);
            moves.Add(Square.At(position.Row - 1, position.Col));
            moves.Add(Square.At(position.Row - 1, position.Col + 1 ));
            moves.Add(Square.At(position.Row, position.Col + 1));
            moves.Add(Square.At(position.Row + 1, position.Col + 1));
            moves.Add(Square.At(position.Row + 1, position.Col));
            moves.Add(Square.At(position.Row + 1, position.Col - 1));
            moves.Add(Square.At(position.Row, position.Col - 1));
            moves.Add(Square.At(position.Row - 1, position.Col - 1));
            moves.RemoveAll(move => !Board.InBounds(move) || !board.SquareFreeOrEnemy(position, move));
            return moves;
        }
    }
}