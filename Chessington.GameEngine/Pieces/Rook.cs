using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Rook : Piece
    {
        public Rook(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            List<Square> moves = new List<Square>();
            Square position = board.FindPiece(this);
            for (int i = 0; i < 8; i++)
            {
                moves.Add(Square.At(i, position.Col));
                moves.Add(Square.At(position.Row, i));
            }
            moves.Remove(position);
            return moves;
        }
    }
}