using System;
using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public abstract class Piece
    {
        protected Piece(Player player)
        {
            Player = player;
            Moved = false;
        }

        public Player Player { get; private set; }
        public bool Moved { get; set; }

        public abstract IEnumerable<Square> GetAvailableMoves(Board board);

        public void MoveTo(Board board, Square newSquare)
        {
            var currentSquare = board.FindPiece(this);
            board.MovePiece(currentSquare, newSquare);
        }

        public IEnumerable<Square> GetLateralMoves(Board board)
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

        public IEnumerable<Square> GetDiagonalMoves(Board board)
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