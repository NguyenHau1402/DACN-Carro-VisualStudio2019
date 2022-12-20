using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Carro
{
    public class QL
    {
        #region Properties
        private Panel chessBoard;

        public Panel ChessBoard
        {
            get { return chessBoard; }
            set { chessBoard = value; }
        }

        public List<Player> Player { get => player; set => player = value; }
        public int CurrentPlayer { get => currentPlayer; set => currentPlayer = value; }
        public TextBox PlayerName { get => playerName; set => playerName = value; }
        public PictureBox PlayerMark { get => playerMark; set => playerMark = value; }
        public List<List<Button>> Matrix
        {
            get { return matrix; }
            set { matrix = value; }

        }

        public Stack<PlayIn4> PlayTimeLIne { get => playTimeLIne; set => playTimeLIne = value; }

        private List<Player> player;
        private int currentPlayer;
        private TextBox playerName;
        private PictureBox playerMark;
        private List<List<Button>> matrix;
        private event EventHandler<BtnClickEvent> playerMarked;
        public event EventHandler<BtnClickEvent> PlayerMarked
        {
            add
            {
                playerMarked += value;
            }
            remove
            {
                playerMarked -= value;
            }
        }

        private event EventHandler endedGame;
        public event EventHandler EndedGame
        {
            add
            {
                endedGame += value;
            }
            remove
            {
                endedGame -= value;
            }
        }
        private Stack<PlayIn4> playTimeLIne;

        #endregion

        #region Initialize
        public QL(Panel chessBoard, TextBox playerName, PictureBox  mark)
        {
            this.ChessBoard = chessBoard;
            this.PlayerName = playerName;
            this.PlayerMark = mark;

            this.Player = new List<Player>() { 
                new Player("X", Image.FromFile(Application.StartupPath+ "\\Resources\\x.png")),
                new Player("O", Image.FromFile(Application.StartupPath+ "\\Resources\\o.png"))
            };
            
            
        }
        #endregion
        #region Methods
        public void DrawBoard()
        {
            ChessBoard.Enabled = false;
            ChessBoard.Controls.Clear();
            PlayTimeLIne = new Stack<PlayIn4>();
            CurrentPlayer = 0;
            ChangePlayer();
            Matrix = new List<List<Button>>();
            Button oldbtn = new Button() { Width = 0, Location = new Point(0, 0) };
            for (int i = 0; i < ThongTin.Chess_board_height; i++)
            {
                Matrix.Add(new List<Button>());
                for (int j = 0; j <= ThongTin.Chess_board_width; j++)
                {
                    Button btn = new Button()
                    {
                        Width = ThongTin.Chess_width,
                        Height = ThongTin.Chess_height,
                        Location = new Point(oldbtn.Location.X + oldbtn.Width, oldbtn.Location.Y),
                        BackgroundImageLayout = ImageLayout.Stretch,
                        Tag = i.ToString()
                    };
                    btn.Click += btn_Click;
                    ChessBoard.Controls.Add(btn);
                    Matrix[i].Add(btn);
                    oldbtn = btn;
                }
                oldbtn.Location = new Point(0, oldbtn.Location.Y + ThongTin.Chess_height);
                oldbtn.Width = 0;
                oldbtn.Height = 0;

            }

        }

        private void Sound()
        {

            System.Media.SoundPlayer tick = new System.Media.SoundPlayer(Application.StartupPath + "\\Resources\\tick.mp3");
            tick.Play();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.BackgroundImage != null)
                return;
            

            Mark(btn);
            PlayTimeLIne.Push(new PlayIn4 (GetChessPoint(btn), CurrentPlayer));
            Sound();
            CurrentPlayer = CurrentPlayer == 1 ? 0 : 1;
            ChangePlayer();

            if (playerMarked != null)
                playerMarked(this, new BtnClickEvent(GetChessPoint(btn)));
            if (isEndGame(btn))
            {
                EndGame();
            }

        }

        public void OtherPlayerMark(Point point)
        {
            Button btn = Matrix[point.Y][point.X];
            if (btn.BackgroundImage != null)
                return;
            


            Mark(btn);
            PlayTimeLIne.Push(new PlayIn4(GetChessPoint(btn), CurrentPlayer));
            CurrentPlayer = CurrentPlayer == 1 ? 0 : 1;
            Sound();
            ChangePlayer();
           
            
            if (isEndGame(btn))
            {
                EndGame();
            }
        }

        
        private Point PointCom()
        {
            throw new NotImplementedException();
        }

        public void EndGame() 
        {
            if (endedGame != null)
                endedGame(this, new EventArgs());
        }
        
        private bool isEndGame(Button btn)
        {
            return isEndNgang(btn) || isEndDoc(btn) || isEndCheoChinh(btn) || isEndCheoPhu(btn);
        }

        private Point GetChessPoint(Button btn)
        {
            
            int doc = Convert.ToInt32(btn.Tag);
            int ngang = Matrix[doc].IndexOf(btn);
            Point point = new Point(ngang, doc);
            return point;
        }
        private bool isEndNgang(Button btn)
        {
            Point point = GetChessPoint(btn);
            int countLeft = 0;
            for (int i = point.X; i >= 0; i--)
            {
                
                if (Matrix[point.Y][i].BackgroundImage == btn.BackgroundImage)
                {
                    countLeft++;
                }
                else
                    break;
            }
            
            int countRight = 0;
            for (int i = point.X +1; i <ThongTin.Chess_board_width; i++)
            {
                if (Matrix[point.Y][i].BackgroundImage == btn.BackgroundImage)
                {
                    countRight++;
                }
                else
                    break;
            }

            return countLeft + countRight == 5;
            
        }
        private bool isEndDoc(Button btn)
        {
            
            Point point = GetChessPoint(btn);
            int countTop = 0;
            for (int i = point.Y; i >= 0; i--)
            {
                if (Matrix[i][point.X].BackgroundImage == btn.BackgroundImage)
                {
                    countTop++;
                }
                else
                    break;
            }

            int countBottom = 0;
            for (int i = point.Y + 1; i < ThongTin.Chess_board_height; i++)
            {
                if (Matrix[i][point.X].BackgroundImage == btn.BackgroundImage)
                {
                    countBottom++;
                }
                else
                    break;
            }

            return countTop + countBottom == 5;
        }
        private bool isEndCheoChinh(Button btn)
        {
            Point point = GetChessPoint(btn);

            int countTop = 0;

            for (int i = 0; i <= point.X; i++)
            {
                if (point.X - i < 0 || point.Y - i < 0) break;

                if (Matrix[point.Y - i][point.X - i].BackgroundImage == btn.BackgroundImage)
                {
                    countTop++;
                }
                else break;
            }

            int countBottom = 0;
            for (int i = 1; i <= ThongTin.Chess_board_width - point.X; i++)
            {
                if (point.Y + i >= ThongTin.Chess_board_height || point.X + i >= ThongTin.Chess_board_width) break;

                if (Matrix[point.Y + i][point.X + i].BackgroundImage == btn.BackgroundImage)
                {
                    countBottom++;
                }
                else break;
            }


            return countTop + countBottom == 5;
        }
        private bool isEndCheoPhu(Button btn)
        {
            Point point = GetChessPoint(btn);

            int countTop = 0;

            for (int i = 0; i <= point.X; i++)
            {
                if (point.X + i > ThongTin.Chess_board_width || point.Y - i < 0) break;

                if (Matrix[point.Y - i][point.X + i].BackgroundImage == btn.BackgroundImage)
                {
                    countTop++;
                }
                else break;
            }

            int countBottom = 0;
            for (int i = 1; i <= ThongTin.Chess_board_width - point.X; i++)
            {
                if (point.Y + i >= ThongTin.Chess_board_height || point.X - i < 0) break;

                if (Matrix[point.Y + i][point.X - i].BackgroundImage == btn.BackgroundImage)
                {
                    countBottom++;
                }
                else break;
            }


            return countTop + countBottom == 5;
        }
        private void Mark(Button btn)
        {
            btn.BackgroundImage = Player[CurrentPlayer].Mark;
            
        }
        private void ChangePlayer()
        {
            PlayerName.Text = Player[CurrentPlayer].Name;
            PlayerMark.Image = Player[CurrentPlayer].Mark;
        }
        public bool Undo()
        {
            if (PlayTimeLIne.Count <= 2)
            {
                MessageBox.Show("Không thể đi lại", "Thông báo!", MessageBoxButtons.OK);
                return false;
            }
                bool isUndo1 = UndoAStep();
                bool isUndo2 = UndoAStep();
                PlayIn4 oldPoint = playTimeLIne.Peek();
                CurrentPlayer = oldPoint.CurrentPlayer == 1 ? 0 : 1;
                return isUndo1 && isUndo2;
           
            




        }
        

        private bool UndoAStep()
        {

            if (PlayTimeLIne.Count <= 0)
                return false;
            PlayIn4 oldPoint = playTimeLIne.Pop();
            Button btn = Matrix[oldPoint.Point.Y][oldPoint.Point.X];
            btn.BackgroundImage = null;

            if (PlayTimeLIne.Count == 0)
                CurrentPlayer = 0;
            else
            {
                oldPoint = PlayTimeLIne.Peek();
                
            }

            ChangePlayer();

            return true;
        }
       
        #endregion


    }
    public class BtnClickEvent : EventArgs
    {
        private Point clickedPoint;

        public Point ClickedPoint { get => clickedPoint; set => clickedPoint = value; }

        public BtnClickEvent(Point point)
        {
            this.ClickedPoint = point;
        }
    }
}
