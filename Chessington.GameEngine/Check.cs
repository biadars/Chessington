using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chessington.GameEngine
{
    public class Check
    {
        private Board Board;

        public Check(Board checkBoard)
        {
            Board = checkBoard;
        }

        public bool IsCheck(Player targetPlayer, Square checkLocation)
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
                if (moves.Contains(checkLocation))
                    return true;
            }
            return false;
        }
    }
}
