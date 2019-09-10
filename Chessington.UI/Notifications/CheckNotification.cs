using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chessington.GameEngine;

namespace Chessington.UI.Notifications
{
    public class CheckNotification
    {
        public CheckNotification(Square square)
        {
            Square = square;
        }

        public Square Square { get; set; }
    }
}
