using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(Player player)
            : base(player) { }

        public override void MoveTo(Board board, Square newSquare)
        {
            base.MoveTo(board, newSquare);
            Moved = true;
        }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var moves = new List<Square>();
            var position = board.FindPiece(this);
            int rowModifier;
            if (Player == Player.White)
                rowModifier = -1;
            else
                rowModifier = 1;
            var destination = Square.At(position.Row + rowModifier, position.Col);
            if (destination.IsInBounds())
            {
                AddDiagonalMove(board, moves, Square.At(destination.Row, destination.Col - 1));
                AddDiagonalMove(board, moves, Square.At(destination.Row, destination.Col + 1));
                if (board.GetPiece(destination) != null)
                    return moves;
                moves.Add(destination);
                destination = Square.At(position.Row + 2 * rowModifier, position.Col);
                if (!Moved && board.GetPiece(destination) == null)
                    moves.Add(destination);
            }
            return moves;
        }

        private IEnumerable<Square> AddDiagonalMove(Board board, List<Square> moves, Square destination)
        {
            var position = board.FindPiece(this);
            if (destination.IsInBounds() && board.CanTakePiece(position, destination))
                moves.Add(destination);
            return moves;
        }
    }
}