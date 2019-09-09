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
        }

        public Player Player { get; private set; }

        public abstract IEnumerable<Square> GetAvailableMoves(Board board);

        public virtual void MoveTo(Board board, Square newSquare)
        {
            var currentSquare = board.FindPiece(this);
            board.MovePiece(currentSquare, newSquare);
        }

        public IEnumerable<Square> GetLateralMoves(Board board)
        {
            List<Square> moves = new List<Square>();
            Square position = board.FindPiece(this);
            for (int i = 0; i < GameSettings.BoardSize; i++)
            {
                moves.Add(Square.At(i, position.Col));
                moves.Add(Square.At(position.Row, i));
            }
            moves.RemoveAll(move => move == position || !board.IsValidLateralMove(position, move));
            return moves;
        }

        public IEnumerable<Square> GetDiagonalMoves(Board board)
        {
            List<Square> moves = new List<Square>();
            Square position = board.FindPiece(this);
            for (int i = (-1 * GameSettings.BoardSize + 1); i < GameSettings.BoardSize; i++) 
                moves.Add(Square.At(position.Row + i, position.Col + i));
            for (int i = (-1 * GameSettings.BoardSize + 1); i < GameSettings.BoardSize; i++)
                moves.Add(Square.At(position.Row + i, position.Col - i));
            moves.RemoveAll(move => move == position || !move.IsInBounds() || !board.IsValidDiagonalMove(position, move));
            return moves;
        }
    }
}