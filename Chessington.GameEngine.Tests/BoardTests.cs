using Chessington.GameEngine.Pieces;
using FluentAssertions;
using NUnit.Framework;

namespace Chessington.GameEngine.Tests
{
    [TestFixture]
    public class BoardTests
    {
        [Test]
        public void PawnCanBeAddedToBoard()
        {
            var board = new Board();
            var pawn = new Pawn(Player.White);
            board.AddPiece(Square.At(0, 0), pawn);

            board.GetPiece(Square.At(0, 0)).Should().BeSameAs(pawn);
        }

        [Test]
        public void PawnCanBeFoundOnBoard()
        {
            var board = new Board();
            var pawn = new Pawn(Player.White);
            var square = Square.At(6, 4);
            board.AddPiece(square, pawn);

            var location = board.FindPiece(pawn);

            location.Should().Be(square);
        }

        [Test]
        public void LateralMoveIsNotValidDiagonalMove()
        {
            var board = new Board();
            var rook = new Rook(Player.White);
            var square = Square.At(4, 4);
            board.AddPiece(square, rook);
            var destination = Square.At(4, 7);
            board.IsValidDiagonalMove(square, destination).Should().BeFalse();
        }

        [Test]
        public void KnightMoveIsNotValidDiagonalMove()
        {
            var board = new Board();
            var knight = new Knight(Player.White);
            var square = Square.At(4, 4);
            board.AddPiece(square, knight);
            var destination = Square.At(6, 5);
            board.IsValidDiagonalMove(square, destination).Should().BeFalse();
        }
    }
}
