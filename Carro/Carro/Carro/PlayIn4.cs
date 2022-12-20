using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Carro
{
    public class PlayIn4
    {
        private Point point;

        public Point Point { get => point; set => point = value; }
        public int CurrentPlayer { get => currentPlayer; set => currentPlayer = value; }

        private int currentPlayer;

        public PlayIn4(Point point, int currentPlayer)
        {
            this.Point = point;
            this.CurrentPlayer = currentPlayer;
        }

    }
}
