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
            for (int i = 0; i < GameSettings.BoardSize; i++)
            {
                Square destination = Square.At(i, position.Col);
                if (board.IsValidLateralMove(position, destination))
                    moves.Add(destination);
                destination = Square.At(position.Row, i);
                if (board.IsValidLateralMove(position, destination))
                    moves.Add(destination);
            }
            moves.RemoveAll(move => move == position);
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
            moves.RemoveAll(move => move == position || !Board.InBounds(move));
            return moves;
        }
    }
}