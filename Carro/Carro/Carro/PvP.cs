using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Carro
{
    public partial class PvP : Form
    {
        #region Properties
        QL ChessBoard;
        SocketMN socket;
        #endregion

        public PvP()
        {
            InitializeComponent();
            
            ChessBoard = new QL(PNCaroBoard, txtPlayerName, pctMark);
            ChessBoard.EndedGame += ChessBoard_EndedGame;
            ChessBoard.PlayerMarked += ChessBoard_PlayerMarked;
            prcBar.Step = ThongTin.cd_Step;
            prcBar.Maximum = ThongTin.cd_Time;
            prcBar.Value = 0;

            timeCD.Interval = ThongTin.cd_Interval;
            socket = new SocketMN();
            ChessBoard.DrawBoard();

        }

        void EndGame()
        {
            timeCD.Stop();
            PNCaroBoard.Enabled = false;
            button2.Enabled = false;
           
            button4.Enabled = false;
            MessageBox.Show("Game đã kết thúc!");
            
        }

        void NewGame()
        {
            button2.Enabled = true;
            
            button4.Enabled = true;
            prcBar.Value = 0;
            timeCD.Stop();
            ChessBoard.DrawBoard();
        }
        void TimeUp()
        {
            MessageBox.Show("Hết thời gian.");
            EndGame();
        }

        void Exit()
        {
            Application.Exit();
        }

        

        private void ChessBoard_PlayerMarked(object sender, BtnClickEvent e)
        {
            
            timeCD.Start();
            PNCaroBoard.Enabled = false;
            prcBar.Value = 0;
            socket.Send(new SocketData((int)SocketCommand.Send_Point, "", e.ClickedPoint));
            button2.Enabled = false;
            
            Listen();
        }

        private void ChessBoard_EndedGame(object sender, EventArgs e)
        {
            EndGame();
            socket.Send(new SocketData((int)SocketCommand.End_Game, "", new Point()));
        }

        private void timeCD_Tick(object sender, EventArgs e)
        {
            prcBar.PerformStep();
            if(prcBar.Value >= prcBar.Maximum)
            {
                socket.Send(new SocketData((int)SocketCommand.Time_Out, "", new Point()));
                EndGame();
                
            }
        }
        private void txtIP_TextChanged(object sender, EventArgs e)
        {

        }

        private void PNCaro_Paint(object sender, PaintEventArgs e)
        {

        }

        void Undo()
        {
            prcBar.Value = 0;
            
            ChessBoard.Undo();
        }

        

        private void prcBar_Click(object sender, EventArgs e)
        {

        }

       

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát?", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
                e.Cancel = true;
            else
            {
                try
                {

                socket.Send(new SocketData((int)SocketCommand.Exit, "", new Point()));
                }
                catch
                {

                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn chơi ván mới?", "Lưu ý!!", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                NewGame();
                socket.Send(new SocketData((int)SocketCommand.New_Game, "", new Point()));
                PNCaroBoard.Enabled = true;
            } 
            
           
        }

       

        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Xin thua?", "Thông báo!", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                EndGame();
                socket.Send(new SocketData((int)SocketCommand.Sur, "", new Point()));
                button2.Enabled = false;

                button4.Enabled = false;
                MessageBox.Show("Game đã kết thúc!");
            }
                

        }

        private void button2_Click(object sender, EventArgs e)
        {
                MessageBox.Show("Đối phương dủ lòng thương cho đi lại!");
                socket.Send(new SocketData((int)SocketCommand.Undo, "", new Point()));
                Undo();
            button2.Enabled = false;

        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            txtIP.Text = socket.GetLocalIPv4(NetworkInterfaceType.Wireless80211);
            if (string.IsNullOrEmpty(txtIP.Text))
            {
                txtIP.Text = socket.GetLocalIPv4(NetworkInterfaceType.Ethernet);
            }
        }

        private void  button1_Click(object sender, EventArgs e)
        {
            socket.IP = txtIP.Text;
            
            if (!socket.ConnectServer())
            {
                socket.isServer = true;
                
                socket.CreateServer();
                button1.Enabled = false;
                MessageBox.Show("Chưa có server nào, đã tạo 1 server...");
                PNCaroBoard.Enabled = true;
            }
            else
            {
                socket.isServer = false;
                PNCaroBoard.Enabled = false;    
                Listen();
                button1.Enabled = false;
                MessageBox.Show("Đã có server, đang tham gia...");
            }
           
            
        }

        void Listen()
        {
            Thread listenThread = new Thread(() =>
            {
                try
                {
                SocketData data = (SocketData)socket.Receive();
                ProcessData(data);
                }
                catch
                {

                }
            });
            listenThread.IsBackground = true;
            listenThread.Start();
            
        }
        private void ProcessData(SocketData data)
        {
            
            switch (data.Command)
            {
                case (int)SocketCommand.In4:
                    MessageBox.Show(data.Message);
                    break;
                case (int)SocketCommand.New_Game:
                    this.Invoke((MethodInvoker)(() =>
                    {
                        NewGame();
                        PNCaroBoard.Enabled = false;
                    }));
                    break;
                case (int)SocketCommand.Sur:
                    this.Invoke((MethodInvoker)(() =>
                    {
                        EndGame();
                        PNCaroBoard.Enabled = false;
                        button2.Enabled = false;
                       
                        button4.Enabled = false;
                    }));
                    break;
                case (int)SocketCommand.Draw:
                    this.Invoke((MethodInvoker)(() =>
                    {
                        timeCD.Stop();
                        PNCaroBoard.Enabled = false;
                        button2.Enabled = false;
                        
                        button4.Enabled = false;
                    }));
                    break;
                case (int)SocketCommand.Send_Point:
                    this.Invoke((MethodInvoker)(() =>
                    {
                        prcBar.Value = 0;
                        PNCaroBoard.Enabled = true;
                        timeCD.Start();
                        ChessBoard.OtherPlayerMark(data.Point);
                        button2.Enabled = true;
                    }));
                    
                    break;
                case (int)SocketCommand.Undo:
                    Undo();
                    prcBar.Value = 0;
                    break;
                case (int)SocketCommand.End_Game:
                    
                    break;
                case (int)SocketCommand.Time_Out:
                    MessageBox.Show("Hết giờ!");
                    button4.Enabled = false;
                    button2.Enabled = false;
                    break;
                case (int)SocketCommand.Exit:
                    timeCD.Stop();
                    MessageBox.Show("Người chơi đã thoát");
                    button4.Enabled = false;
                    button2.Enabled = false;
                    button5.Enabled = false;
                    PNCaroBoard.Enabled = false;
                    break;

                default:
                    break;
            }
            Listen();
        }

        private void prcBar_Click_1(object sender, EventArgs e)
        {

        }
    }
}
