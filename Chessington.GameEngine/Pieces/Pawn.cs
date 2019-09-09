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
            List < Square > moves = new List<Square>();
            Square position = board.FindPiece(this);
            if (Player == Player.White)
            {
                Square destination = Square.At(position.Row - 1, position.Col);
                if (Board.InBounds(destination) && board.GetPiece(destination) == null)
                {
                    moves.Add(destination);
                    destination = Square.At(destination.Row, destination.Col - 1);
                    if (Board.InBounds(destination) && board.CanTakePiece(position, destination))
                        moves.Add(destination);
                    destination = Square.At(destination.Row, destination.Col + 2);
                    if (Board.InBounds(destination) && board.CanTakePiece(position, destination))
                        moves.Add(destination);
                    destination = Square.At(position.Row - 2, position.Col);
                    if (!Moved && board.GetPiece(destination) == null)
                        moves.Add(destination);
                }
            }
            else
            {
                Square destination = Square.At(position.Row + 1, position.Col);
                if (Board.InBounds(destination) && board.GetPiece(destination) == null)
                {
                    moves.Add(destination);
                    destination = Square.At(destination.Row, destination.Col - 1);
                    if (Board.InBounds(destination) && board.CanTakePiece(position, destination))
                    moves.Add(destination);
                    destination = Square.At(destination.Row, destination.Col + 2);
                    if (Board.InBounds(destination) && board.CanTakePiece(position, destination))
                    moves.Add(destination);
                    destination = Square.At(position.Row + 2, position.Col);
                    if (!Moved && board.GetPiece(destination) == null)
                        moves.Add((destination));
                }
            }

            return moves;
        }
    }
}