using System;
using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Pawn : Piece
    {
        public bool Moved { get; set; } = false;

        public Pawn(Player player)
            : base(player) { }

        public override void MoveTo(Board board, Square newSquare)
        {
            var currentSquare = board.FindPiece(this);
            board.MovePiece(currentSquare, newSquare);
            Moved = true;
        }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var moves = new List<Square>();
            var currentPosition = board.FindPiece(this);
            int rowModifier = GetRowModifier();
            AddFrontMove(board, moves, currentPosition, rowModifier);
            AddDoubleFrontMove(board, moves, currentPosition, rowModifier);
            AddDiagonalMove(board, moves, Square.At(currentPosition.Row + rowModifier, currentPosition.Col - 1));
            AddDiagonalMove(board, moves, Square.At(currentPosition.Row + rowModifier, currentPosition.Col + 1));
            return moves;
        }

        private int GetRowModifier()
        {
            if (Player == Player.White)
                return -1;
            return 1;
        }

        private IEnumerable<Square> AddDiagonalMove(Board board, List<Square> moves, Square destination)
        {
            var position = board.FindPiece(this);
            if (destination.IsInBounds() && board.CanTakePiece(position, destination))
                moves.Add(destination);
            return moves;
        }

        private IEnumerable<Square> AddFrontMove(Board board, List<Square> moves, Square currentPosition, int rowModifier)
        {
            var destination = Square.At(currentPosition.Row + rowModifier, currentPosition.Col);
            if (destination.IsInBounds() && board.GetPiece(destination) == null)
                moves.Add(destination);
            return moves;
        }

        private IEnumerable<Square> AddDoubleFrontMove(Board board, List<Square> moves, Square currentPosition,
            int rowModifier)
        {
            if (Moved)
                return moves;
            var destination = Square.At(currentPosition.Row + rowModifier, currentPosition.Col);
            if (!destination.IsInBounds() || board.GetPiece(destination) != null)
                return moves;
            destination = Square.At(destination.Row + rowModifier, destination.Col);
            if (destination.IsInBounds() && board.GetPiece(destination) == null)
                moves.Add(destination);
                return moves;
        }
    }
}