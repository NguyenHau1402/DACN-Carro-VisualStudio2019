using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Carro
{
    [Serializable]
    public class SocketData
    {
        private int command;

        public int Command { get => command; set => command = value; }
        public Point Point { get => point; set => point = value; }
        public string Message { get => message; set => message = value; }

        private Point point;
        private string message;

        public SocketData(int command,string message, Point  point)
        {
            this.Command = command;
            this.Point = point;
            this.Message = message;
        }
    }
    public enum SocketCommand
    {
        Send_Point,
        New_Game,
        Undo,
        Exit,
        End_Game,
        Time_Out,
        Draw,
        Sur,
        In4
    }

}
