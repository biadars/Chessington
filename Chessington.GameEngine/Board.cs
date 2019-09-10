using System;
using System.Collections.Generic;
using System.Linq;
using Chessington.GameEngine.Pieces;

namespace Chessington.GameEngine
{
    public class Board
    {
        private readonly Piece[,] board;
        public Player CurrentPlayer { get; private set; }
        public IList<Piece> CapturedPieces { get; private set; }
        public EnPassantTarget EnPassantTarget { private get; set; }
        public Piece LastMovedPiece { get; private set; }

        public Board()
            : this(Player.White) { }

        public Board(Player currentPlayer, Piece[,] boardState = null)
        {
            board = boardState ?? new Piece[GameSettings.BoardSize, GameSettings.BoardSize]; 
            CurrentPlayer = currentPlayer;
            CapturedPieces = new List<Piece>();
        }

        public void AddPiece(Square square, Piece pawn)
        {
            board[square.Row, square.Col] = pawn;
        }
    
        public Piece GetPiece(Square square)
        {
            return board[square.Row, square.Col];
        }
        
        public Square FindPiece(Piece piece)
        {
            for (var row = 0; row < GameSettings.BoardSize; row++)
                for (var col = 0; col < GameSettings.BoardSize; col++)
                    if (board[row, col] == piece)
                        return Square.At(row, col);

            throw new ArgumentException("The supplied piece is not on the board.", "piece");
        }

        public List<Piece> GetPiecesByPlayer(Player player)
        {
            var pieces = new List<Piece>();
            for (var row = 0; row < GameSettings.BoardSize; row++)
            for (var col = 0; col < GameSettings.BoardSize; col++)
                if (board[row, col]?.Player == player)
                    pieces.Add(board[row, col]);
            return pieces;
        }

        public void MovePiece(Square from, Square to)
        {
            var movingPiece = board[from.Row, from.Col];
            if (movingPiece == null) { return; }

            if (movingPiece.Player != CurrentPlayer)
            {
                throw new ArgumentException("The supplied piece does not belong to the current player.");
            }


            //If the space we're moving to is occupied, we need to mark it as captured.
            if (board[to.Row, to.Col] != null)
            {
                OnPieceCaptured(board[to.Row, to.Col]);
            }

            if (EnPassantTarget?.TargetSquare == to)
            {
                OnPieceCaptured(EnPassantTarget.TargetPiece);
                var square = FindPiece(EnPassantTarget.TargetPiece);
                board[square.Row, square.Col] = null;
            }

            //Move the piece and set the 'from' square to be empty.
            board[to.Row, to.Col] = board[from.Row, from.Col];
            board[from.Row, from.Col] = null;
            LastMovedPiece = movingPiece;

            SetEnPassantTarget(movingPiece as Pawn, from, to);
            PerformPromotion(movingPiece, to);

            CurrentPlayer = movingPiece.Player == Player.White ? Player.Black : Player.White;
            OnCurrentPlayerChanged(CurrentPlayer);
        }


        public delegate void PieceCapturedEventHandler(Piece piece);
        
        public event PieceCapturedEventHandler PieceCaptured;

        protected virtual void OnPieceCaptured(Piece piece)
        {
            var handler = PieceCaptured;
            if (handler != null) handler(piece);
        }

        public delegate void CurrentPlayerChangedEventHandler(Player player);

        public event CurrentPlayerChangedEventHandler CurrentPlayerChanged;

        protected virtual void OnCurrentPlayerChanged(Player player)
        {
            var handler = CurrentPlayerChanged;
            if (handler != null) handler(player);
        }

        public bool IsValidLateralMove(Square from, Square to)
        {
            if (from.Row == to.Row)
            {
                for (int i = Math.Min(from.Col, to.Col) + 1; i < Math.Max(from.Col, to.Col); i++)
                    if (GetPiece(Square.At(from.Row, i)) != null)
                        return false;
            }
            else
            {
                for (int i = Math.Min(from.Row, from.Row) + 1; i < Math.Max(from.Row, to.Row); i++)
                {
                    if (GetPiece(Square.At(i, from.Col)) != null)
                        return false;
                }
            }

            return IsValidDestination(from, to);
        }

        public bool IsValidDiagonalMove(Square from, Square to)
        {
            int x, y;
            if (from.Row <= to.Row)
                x = 1;
            else
                x = -1;
            if (from.Col <= to.Col)
                y = 1;
            else
                y = -1;
            for (Square current = Square.At(from.Row + x, from.Col + y);
                current != to;
                current = Square.At(current.Row + x, current.Col + y))
            {
                if (!current.IsInBounds() || GetPiece(current) != null)
                    return false;
            }

            return IsValidDestination(from, to);
        }

        public bool IsValidDestination(Square from, Square to)
        {
            if (!to.IsInBounds() || GetPiece(to) != null && GetPiece(to).Player == GetPiece(from).Player)
                return false;
            return true;
        }

        public bool CanTakePiece(Square from, Square to)
        {
            if (GetPiece(to) == null && EnPassantTarget?.TargetSquare != to)
                return false;
            if ((GetPiece(to) ?? EnPassantTarget.TargetPiece).Player == GetPiece(from).Player)
                return false;
            return true;
        }

        public void SetEnPassantTarget(Pawn piece, Square from, Square to)
        {
            if (Math.Abs(to.Row - from.Row) == 2 && piece != null)
            {
                EnPassantTarget = new EnPassantTarget
                {
                    TargetPiece = piece,
                    TargetSquare = Square.At((to.Row + from.Row) / 2, from.Col)
                };
            }
            else
            {
                EnPassantTarget = null;
            }
        }

        private void PerformPromotion(Piece piece, Square square)
        {
            if (piece.IsPromotionCandidate(this))
            {
                board[square.Row, square.Col] = null;
                var queen = new Queen(CurrentPlayer);
                AddPiece(square, queen);
            }
        }
    }

    public class EnPassantTarget
    {
        public Square TargetSquare;
        public Pawn TargetPiece;
    }
}
