using System;
using Chessington.GameEngine;
using Chessington.GameEngine.Pieces;

namespace Chessington.UI.Factories
{
    /// <summary>
    /// Owns the logic of how to set up a chess board.
    /// </summary>
    public static class StartingPositionFactory
    {
        public static void Setup(Board board, Check check)
        {
            for (var i = 0; i < GameSettings.BoardSize; i++)
            {
                board.AddPiece(Square.At(1, i), new Pawn(Player.Black));
                board.AddPiece(Square.At(6, i), new Pawn(Player.White));
            }

            var whiteKing = new King(Player.White);
            var blackKing = new King(Player.Black);
            board.AddPiece(Square.At(0, 4), blackKing);
            check.Kings[Player.Black] = blackKing;
            board.AddPiece(Square.At(7, 4), whiteKing);
            check.Kings[Player.White] = whiteKing;


            board.AddPiece(Square.At(0, 0), new Rook(Player.Black));
            board.AddPiece(Square.At(0, 1), new Knight(Player.Black));
            board.AddPiece(Square.At(0, 2), new Bishop(Player.Black));
            board.AddPiece(Square.At(0, 3), new Queen(Player.Black));
            board.AddPiece(Square.At(0, 5), new Bishop(Player.Black));
            board.AddPiece(Square.At(0, 6), new Knight(Player.Black));
            board.AddPiece(Square.At(0, 7), new Rook(Player.Black));

            board.AddPiece(Square.At(7, 0), new Rook(Player.White));
            board.AddPiece(Square.At(7, 1), new Knight(Player.White));
            board.AddPiece(Square.At(7, 2), new Bishop(Player.White));
            board.AddPiece(Square.At(7, 3), new Queen(Player.White));
            board.AddPiece(Square.At(7, 5), new Bishop(Player.White));
            board.AddPiece(Square.At(7, 6), new Knight(Player.White));
            board.AddPiece(Square.At(7, 7), new Rook(Player.White));

        }
    }
}