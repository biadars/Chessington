using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chessington.GameEngine.Pieces;

namespace Chessington.GameEngine
{
    public class Check
    {
        private Board Board;
        public Dictionary<Player, King> Kings;

        public Check(Board checkBoard)
        {
            Board = checkBoard;
            Kings = new Dictionary<Player, King>();
        }

        public bool IsInCheck(Player player)
        {
            var king = Board.FindPiece(Kings[player]);
            return CanBeAttacked(player, king);
        }

        public bool CanBeAttacked(Player targetPlayer, Square targetSquare)
        {
            Player opponent;
            if (targetPlayer == Player.White)
                opponent = Player.Black;
            else
                opponent = Player.White;
            var pieces = Board.GetPiecesByPlayer(opponent);
            for (var i = 0; i < pieces.Count; i++)
            {
                var moves = pieces[i].GetAvailableMoves(Board);
                if (moves.Contains(targetSquare))
                    return true;
            }
            return false;
        }
    }
}
