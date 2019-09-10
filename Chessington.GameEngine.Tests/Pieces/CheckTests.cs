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
            var king = new King(Player.White);
            var kingSquare = Square.At(4, 4);
            board.AddPiece(kingSquare, king);
            var pawn = new Pawn(Player.Black);
            board.AddPiece(Square.At(1, 5), pawn);

            Check check = new Check(board);
            check.IsCheck(Player.White, kingSquare).Should().BeFalse();
        }

        [Test]
        public void Rook_CanCheck_King()
        {
            var board = new Board();
            var king = new King(Player.Black);
            var kingSquare = Square.At(4, 4);
            board.AddPiece(kingSquare, king);
            var rook = new Rook(Player.White);
            board.AddPiece(Square.At(4, 5), rook);

            Check check = new Check(board);
            check.IsCheck(Player.Black, kingSquare).Should().BeTrue();
        }
    }
}