using Chessington.GameEngine.Pieces;
using FluentAssertions;
using NUnit.Framework;

namespace Chessington.GameEngine.Tests.Pieces
{
    [TestFixture]
    public class CheckTests
    {
        [Test]
        public void OutOfRangePawn_CannotCheck_King()
        {
            var board = new Board();
            var whiteKing = new King(Player.White);
            var blackKing = new King(Player.Black);
            var check = new Check(board, whiteKing, blackKing);
            var kingSquare = Square.At(4, 4);
            var pawn = new Pawn(Player.Black);

            board.AddPiece(kingSquare, whiteKing);
            board.AddPiece(Square.At(0,0),blackKing );
            board.AddPiece(Square.At(1, 5), pawn);

            check.CanBeAttacked(Player.White, kingSquare).Should().BeFalse();
        }

        [Test]
        public void Rook_CanCheck_King()
        {
            var board = new Board();
            var blackKing = new King(Player.Black);
            var whiteKing = new King(Player.White);
            var check = new Check(board, whiteKing, blackKing);
            var kingSquare = Square.At(4, 4);
            var rook = new Rook(Player.White);

            board.AddPiece(kingSquare, blackKing);
            board.AddPiece(Square.At(0, 0), whiteKing);
            board.AddPiece(Square.At(4, 5), rook);

            check.CanBeAttacked(Player.Black, kingSquare).Should().BeTrue();
        }
    }
}