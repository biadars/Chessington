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
                if (board.GetPiece(Square.At(position.Row - 1, position.Col)) == null)
                {
                    moves.Add(Square.At(position.Row - 1, position.Col));
                    if (!Moved && board.GetPiece(Square.At(position.Row - 2, position.Col)) == null)
                        moves.Add((Square.At(position.Row - 2, position.Col)));
                }
            }
            else
            {
                if (board.GetPiece(Square.At(position.Row + 1, position.Col)) == null)
                {
                    moves.Add(Square.At(position.Row + 1, position.Col));
                    if (!Moved && board.GetPiece(Square.At(position.Row + 2, position.Col)) == null)
                        moves.Add((Square.At(position.Row + 2, position.Col)));
                }
            }
            return moves;
        }
    }
}