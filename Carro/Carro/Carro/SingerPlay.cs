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
    public partial class SingerPlay : Form
    {
        #region Properties
        private List<Player> player;
        private Panel chessBoard;

       
        public List<Player> Player { get => player; set => player = value; }
        public int Current { get => current; set => current = value; }
        public List<List<Button>> Matrix { get => matrix; set => matrix = value; }
        public Stack<Point> PlayTimeLine { get => playTimeLine; set => playTimeLine = value; }
        
        public Panel ChessBoard { get => chessBoard; set => chessBoard = value; }
        public Stack<Point> Adv { get => adv; set => adv = value; }

        private int current;
        private List<List<Button>> matrix;
        private Stack<Point> playTimeLine;
        private Stack<Point> adv;
        private long[] Atk = new long[6] { 0, 64, 4096, 262144, 16777216, 1073741824 };
        private long[] Def = new long[6] { 0, 8, 512, 32768, 2097152, 134217728 };
        string P1;
        
        #endregion

        public SingerPlay()
        {
            InitializeComponent();
            prcBar.Step = ThongTin.cd_Step;
            prcBar.Maximum = ThongTin.cd_Time; ;
            prcBar.Value = 0;
            timeCD.Interval = ThongTin.cd_Interval;
        }
        

        public void DrawBoard()
        {
            
            PNCaroBoard.Controls.Clear();
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
                        Tag = i.ToString(),
                    };
                    btn.Click += btn_Click;
                    PNCaroBoard.Controls.Add(btn);
                    Matrix[i].Add(btn);


                    oldbtn = btn;
                }
                oldbtn.Location = new Point(0, oldbtn.Location.Y + ThongTin.Chess_height);
                oldbtn.Width = 0;
                oldbtn.Height = 0;
            }
        }
        
        void NewGame()
        {
            if (MessageBox.Show("Bạn có muốn chơi ván mới?", "Game Caro", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                
                timeCD.Stop();
                prcBar.Value = 0;
                PNCaroBoard.Enabled = false;
                button3.Enabled = true;
                button3.Enabled = false;
            }

        }

        private void Sound()
        {
            
            System.Media.SoundPlayer tick = new System.Media.SoundPlayer(Application.StartupPath + "\\Resources\\tick.mp3");
            tick.Play();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            Sound();
            adv = new Stack<Point>();

            Button btn = sender as Button;
            if (btn.BackgroundImage != null)
                return;

            Mark(btn);
            PlayTimeLine.Push(GetChessPoint(btn));
            Current = Current == 1 ? 0 : 1;
            
            changePlayer();
            timeCD.Start();
            prcBar.Value = 0;

            if (isEndGame(btn))
            {
                EndGame();

            }
            if (Current == 0 && PNCaroBoard.Enabled == true)
            {
                Current = Current == 1 ? 0 : 1;
                changePlayer();
                StartComputer(btn);
            }
        }

        void PvC(String P1)
        {
                this.Player = new List<Player>() {
                new Player("Computer", Image.FromFile(Application.StartupPath+ "\\Resources\\o.png")),
                new Player("X", Image.FromFile(Application.StartupPath+ "\\Resources\\x.png")),
                };

                PlayTimeLine = new Stack<Point>();
                current = 1;
                changePlayer();
        }

        private void StartComputer(Button btn)
        {
            Point point = PointCOM();

            btn = Matrix[point.X][point.Y];
            btn.BackgroundImage = Player[0].Mark;
            //Sound();
            playTimeLine.Push(GetChessPoint(btn));
            if (isEndGame(btn))
            {
                EndGame();
            }
        }

        private Point PointCOM()
        {
            Button btn = new Button();
            Point ChessPoint = new Point();

            long DiemMax = 0;

            for (int i = 0; i < ThongTin.Chess_board_height; i++)
            {
                for (int j = 0; j < ThongTin.Chess_board_width; j++)
                {
                    if (Matrix[i][j].BackgroundImage == null)
                    {
                        long AtkS = AtkDoc(i, j) + AtkNgang(i, j) + AtkCheo(i, j) + AtkCheoNguoc(i, j);
                        long DefS = DefDoc(i, j) + DefNgang(i, j) + DefCheo(i, j) + DefCheoNguoc(i, j);
                        long DiemTam = AtkS > DefS ? AtkS : DefS;
                        long TotalS = (AtkS + DefS) > DiemTam ? (AtkS + DefS) : DiemTam;
                        if (DiemMax < TotalS)
                        {

                            DiemMax = TotalS;
                            ChessPoint = new Point(i, j);
                        }

                    }

                }
            }

            return ChessPoint;
        }

        private long DefCheoNguoc(int dong, int cot)
        {
            long TotalS = 0;
            int QtaCount = 0;
            int QdichCount = 0;
            int QtaCount2 = 0;
            int QdichCount2 = 0;

            for (int dem = 1; dem < 6 && dem + dong < ThongTin.Chess_board_height && cot - dem >= 0; dem++)
            {

                if (Matrix[dong + dem][cot - dem].BackgroundImage == Player[0].Mark)
                {
                    QtaCount++;
                    break;
                }
                else
                {
                    if (Matrix[dong + dem][cot - dem].BackgroundImage == Player[1].Mark)
                        QdichCount++;
                    else
                    {
                        for (int dem2 = 2; dem2 < 6 && dong + dem2 < ThongTin.Chess_board_height && cot - dem2 >= 0; dem2++)
                           
                            if (Matrix[dong + dem2][cot - dem2].BackgroundImage == Player[0].Mark)
                            {
                                QtaCount2++;
                                break;
                            }
                            else if (Matrix[dong + dem2][cot + -dem2].BackgroundImage == Player[1].Mark)
                            {
                                QdichCount2++;
                            }
                            else
                                break;
                        break;
                    }

                }
            }
            for (int dem = 1; dem < 6 && dong - dem >= 0 && cot + dem < ThongTin.Chess_board_width; dem++)
            {

                if (Matrix[dong - dem][cot + dem].BackgroundImage == Player[0].Mark)
                {
                    QtaCount++;
                    break;
                }
                else
                {
                    if (Matrix[dong - dem][cot + dem].BackgroundImage == Player[1].Mark)
                        QdichCount++;
                    else
                    {
                        for (int dem2 = 2; dem2 < 6 && dong - dem2 >= 0 && cot + dem2 < ThongTin.Chess_board_width; dem2++)
                           
                            if (Matrix[dong - dem2][cot + dem2].BackgroundImage == Player[0].Mark)
                            {
                                QtaCount2++;
                                break;
                            }
                            else if (Matrix[dong - dem2][cot + dem2].BackgroundImage == Player[1].Mark)
                            {
                                QdichCount2++;
                            }
                            else
                                break;
                        break;
                    }
                }
            }

            if (QtaCount == 2)
                return 0;
            if (QtaCount == 0)
                TotalS += Def[QdichCount] * 2;
            else
                TotalS += Def[QdichCount];
            
            if (QdichCount >= QdichCount2)
                TotalS -= 1;
            else
                TotalS -= 2;
            if (QdichCount == 4)
                TotalS *= 2;

            return TotalS;
        }

        private long DefCheo(int dong, int cot)
        {
            long TotalS = 0;

            int QtaCount = 0;
            int QdichCount = 0;
            int QtaCount2 = 0;
            int QdichCount2 = 0;

            for (int dem = 1; dem < 6 && dem + dong < ThongTin.Chess_board_height && cot + dem < ThongTin.Chess_board_width; dem++)
            {

                if (Matrix[dong + dem][cot + dem].BackgroundImage == Player[0].Mark)
                {
                    QtaCount++;
                    break;
                }
                else
                {
                    if (Matrix[dong + dem][cot + dem].BackgroundImage == Player[1].Mark)
                        QdichCount++;
                    else
                    {
                        for (int dem2 = 2; dem2 < 6 && dong + dem2 < ThongTin.Chess_board_height && cot + dem2 < ThongTin.Chess_board_width; dem2++)
                           
                            if (Matrix[dong + dem2][cot + dem2].BackgroundImage == Player[0].Mark)
                            {
                                QtaCount2++;
                                break;
                            }
                            else if (Matrix[dong + dem2][cot + dem2].BackgroundImage == Player[1].Mark)
                            {
                                QdichCount2++;
                            }
                            else
                                break;
                        break;
                    }

                }
            }
            for (int dem = 1; dem < 6 && dong - dem >= 0 && cot - dem >= 0; dem++)
            {

                if (Matrix[dong - dem][cot - dem].BackgroundImage == Player[0].Mark)
                {
                    QtaCount++;
                    break;
                }
                else
                {
                    if (Matrix[dong - dem][cot - dem].BackgroundImage == Player[1].Mark)
                        QdichCount++;
                    else
                    {
                        for (int dem2 = 2; dem2 < 6 && cot - dem2 >= 0 && dong - dem2 >= 0; dem2++)
                            if (Matrix[dong - dem2][cot - dem2].BackgroundImage == Player[0].Mark)
                            {
                                QtaCount2++;
                                break;
                            }
                            else if (Matrix[dong - dem2][cot - dem2].BackgroundImage == Player[1].Mark)
                            {
                                QdichCount2++;
                            }
                            else
                                break;
                        break;
                    }

                }
            }

            if (QtaCount == 2)
                return 0;
            if (QtaCount == 0)
                TotalS += Def[QdichCount] * 2;
            else
                TotalS += Def[QdichCount];
            if (QdichCount >= QdichCount2)
                TotalS -= 1;
            else
                TotalS -= 2;
            if (QdichCount == 4)
                TotalS *= 2;

            return TotalS;
        }

        private long DefNgang(int dong, int cot)
        {
            long TotalS = 0;

            int QtaCount = 0;
            int QdichCount = 0;
            int QtaCount2 = 0;
            int QdichCount2 = 0;

            for (int dem = 1; dem < 6 && dem + cot < ThongTin.Chess_board_width; dem++)
            {

                if (Matrix[dong][cot + dem].BackgroundImage == Player[0].Mark)
                {
                    QtaCount++;
                    break;
                }
                else
                {
                    if (Matrix[dong][cot + dem].BackgroundImage == Player[1].Mark)
                        QdichCount++;
                    else
                    {
                        for (int dem2 = 2; dem2 < 6 && cot + dem2 < ThongTin.Chess_board_width; dem2++)
                           
                            if (Matrix[dong][cot + dem2].BackgroundImage == Player[0].Mark)
                            {
                                QtaCount2++;
                                break;
                            }
                            else if (Matrix[dong][cot + dem2].BackgroundImage == Player[1].Mark)
                            {
                                QdichCount2++;
                            }
                            else
                                break;
                        break;
                    }
                }
            }
            for (int dem = 1; dem < 6 && cot - dem >= 0; dem++)
            {

                if (Matrix[dong][cot - dem].BackgroundImage == Player[0].Mark)
                {
                    QtaCount++;
                    break;
                }
                else
                {
                    if (Matrix[dong][cot - dem].BackgroundImage == Player[1].Mark)
                        QdichCount++;
                    else
                    {
                        for (int dem2 = 2; dem2 < 6 && cot - dem2 >= 0; dem2++)
                           
                            if (Matrix[dong][cot - dem2].BackgroundImage == Player[0].Mark)
                            {
                                QtaCount2++;
                                break;
                            }
                            else if (Matrix[dong][cot - dem2].BackgroundImage == Player[1].Mark)
                            {
                                QdichCount2++;
                            }
                            else break;
                        break;
                    }
                }
            }
            if (QtaCount == 2)
                return 0;
            if (QtaCount == 0)
                TotalS += Def[QdichCount] * 2;
            else
                TotalS += Def[QdichCount];
            if (QdichCount >= QdichCount2)
                TotalS -= 1;
            else
                TotalS -= 2;
            if (QdichCount == 4)
                TotalS *= 2;

            return TotalS;
        }

        private long DefDoc(int dong, int cot)
        {
            long TotalS = 0;

            int QtaCount = 0;
            int QdichCount = 0;
            int QtaCount2 = 0;
            int QdichCount2 = 0;

            for (int dem = 1; dem < 6 && dem + dong < ThongTin.Chess_board_height; dem++)
            {

                if (Matrix[dong + dem][cot].BackgroundImage == Player[0].Mark)
                {
                    QtaCount++;
                    break;
                }

                else
                {
                    if (Matrix[dong + dem][cot].BackgroundImage == Player[1].Mark)
                        QdichCount++;
                    else
                    {
                        for (int dem2 = 2; dem2 < 6 && dong + dem2 < ThongTin.Chess_board_height; dem2++)
                           
                            if (Matrix[dong + dem2][cot].BackgroundImage == Player[0].Mark)
                            {
                                QtaCount2++;
                                break;
                            }
                            else if (Matrix[dong + dem2][cot].BackgroundImage == Player[1].Mark)
                            {
                                QdichCount2++;
                            }
                            else
                                break;
                        break;
                    }
                }
            }
            for (int dem = 1; dem < 6 && dong - dem >= 0; dem++)
            {

                if (Matrix[dong - dem][cot].BackgroundImage == Player[0].Mark)
                {
                    QtaCount++;
                    break;
                }
                else
                {
                    if (Matrix[dong - dem][cot].BackgroundImage == Player[1].Mark)
                        QdichCount++;
                    else
                    {
                        for (int dem2 = 2; dem2 < 6 && dong - dem2 >= 0; dem2++)
                            
                            if (Matrix[dong - dem2][cot].BackgroundImage == Player[0].Mark)
                            {
                                QtaCount2++;
                                break;
                            }
                            else if (Matrix[dong - dem2][cot].BackgroundImage == Player[1].Mark)
                            {
                                QdichCount2++;
                            }
                            else
                                break;
                        break;
                    }
                }
            }



            if (QtaCount == 2)
                return 0;
            if (QtaCount == 0)
                TotalS += Def[QdichCount] * 2;
            else
                TotalS += Def[QdichCount];
            if (QdichCount >= QdichCount2)
                TotalS -= 1;
            else
                TotalS -= 2;
            if (QdichCount == 4)
                TotalS *= 2;

            return TotalS;
        }

        private long AtkCheoNguoc(int dong, int cot)
        {
            long TotalS = 0;

            int QtaCount = 0;
            int QdichCount = 0;
            int QtaCount2 = 0;
            int QdichCount2 = 0;

            for (int dem = 1; dem < 6 && dem + dong < ThongTin.Chess_board_height && cot - dem >= 0; dem++)
            {

                if (Matrix[dong + dem][cot - dem].BackgroundImage == Player[0].Mark)
                    QtaCount++;
                else
                {
                    if (Matrix[dong + dem][cot - dem].BackgroundImage == Player[1].Mark)
                    {
                        QdichCount++;
                        break;
                    }
                    else
                    {
                        for (int dem2 = 2; dem2 < 6 && cot - dem2 >= 0 && dong + dem2 < ThongTin.Chess_board_height; dem2++)
                            if (Matrix[dong + dem2][cot - dem2].BackgroundImage == Player[0].Mark)
                            {
                                QtaCount2++;
                            }
                            else if (Matrix[dong + dem2][cot - dem2].BackgroundImage == Player[1].Mark)
                            {
                                QdichCount2++;
                                break;
                            }
                            else
                                break;
                        break;
                    }
                }
            }
            for (int dem = 1; dem < 6 && dong - dem >= 0 && cot + dem < ThongTin.Chess_board_width; dem++)
            {

                if (Matrix[dong - dem][cot + dem].BackgroundImage == Player[0].Mark)
                    QtaCount++;
                else
                {
                    if (Matrix[dong - dem][cot + dem].BackgroundImage == Player[1].Mark)
                    {
                        QdichCount++;
                        break;
                    }
                    else
                    {
                        for (int dem2 = 1; dem2 < 6 && cot + dem2 < ThongTin.Chess_board_width && dong - dem2 >= 0; dem2++)
                            if (Matrix[dong - dem2][cot + dem2].BackgroundImage == Player[0].Mark)
                            {
                                QtaCount2++;
                            }
                            else if (Matrix[dong - dem2][cot + dem2].BackgroundImage == Player[1].Mark)
                            {
                                QdichCount2++;
                                break;
                            }
                            else
                                break;
                        break;
                    }
                }
            }

            if (QdichCount == 2)
                return 0;
            if (QdichCount == 0)
                TotalS += Atk[QtaCount] * 2;
            else
                TotalS += Atk[QtaCount];
            if (QdichCount2 == 0)
                TotalS += Atk[QtaCount2] * 2;
            else
                TotalS += Atk[QtaCount2];
            if (QtaCount >= QtaCount2)
                TotalS -= 1;
            else
                TotalS -= 2;
            if (QtaCount == 4)
                TotalS *= 2;
            if (QtaCount == 0)
                TotalS += Def[QdichCount] * 2;
            else
                TotalS += Def[QdichCount];
            if (QtaCount2 == 0)
                TotalS += Def[QdichCount2] * 2;
            else
                TotalS += Def[QdichCount2];

            return TotalS;
        }

        private long AtkCheo(int dong, int cot)
        {
            long TotalS = 0;
            int QtaCount = 0;
            int QdichCount = 0;
            int QtaCount2 = 0;
            int QdichCount2 = 0;

            for (int dem = 1; dem < 6 && dem + dong < ThongTin.Chess_board_height && cot + dem < ThongTin.Chess_board_width; dem++)
            {

                if (Matrix[dong + dem][cot + dem].BackgroundImage == Player[0].Mark)
                    QtaCount++;
                else
                {
                    if (Matrix[dong + dem][cot + dem].BackgroundImage == Player[1].Mark)
                    {
                        QdichCount++;
                        break;
                    }
                    else
                    {
                        for (int dem2 = 2; dem2 < 6 && cot + dem2 < ThongTin.Chess_board_width && dong + dem2 < ThongTin.Chess_board_height; dem2++)
                            if (Matrix[dong + dem2][cot + dem2].BackgroundImage == Player[0].Mark)
                            {
                                QtaCount2++;
                            }
                            else if (Matrix[dong + dem2][cot + dem2].BackgroundImage == Player[1].Mark)
                            {
                                QdichCount2++;
                                break;
                            }
                            else
                                break;
                        break;
                    }
                }
            }
            for (int dem = 1; dem < 6 && dong - dem >= 0 && cot - dem >= 0; dem++)
            {
                if (Matrix[dong - dem][cot - dem].BackgroundImage == Player[0].Mark)
                    QtaCount++;
                else
                {
                    if (Matrix[dong - dem][cot - dem].BackgroundImage == Player[1].Mark)
                    {
                        QdichCount++;
                        break;
                    }
                    else
                    {
                        for (int dem2 = 2; dem2 < 6 && cot - dem2 >= 0 && dong - dem2 >= 0; dem2++)
                            if (Matrix[dong - dem2][cot - dem2].BackgroundImage == Player[0].Mark)
                            {
                                QtaCount2++;
                            }
                            else if (Matrix[dong - dem2][cot - dem2].BackgroundImage == Player[1].Mark)
                            {
                                QdichCount2++;
                                break;
                            }
                            else
                                break;
                        break;
                    }
                }
            }

            if (QdichCount == 2)
                return 0;
            if (QdichCount == 0)
                TotalS += Atk[QtaCount] * 2;
            else
                TotalS += Atk[QtaCount];
            if (QdichCount2 == 0)
                TotalS += Atk[QtaCount2] * 2;
            else
                TotalS += Atk[QtaCount2];
            if (QtaCount >= QtaCount2)
                TotalS -= 1;
            else
                TotalS -= 2;

            if (QtaCount == 4)
                TotalS *= 2;
            if (QtaCount == 0)
                TotalS += Def[QdichCount] * 2;
            else
                TotalS += Def[QdichCount];
            if (QtaCount2 == 0)
                TotalS += Def[QdichCount2] * 2;
            else
                TotalS += Def[QdichCount2];

            return TotalS;
        }

        private long AtkNgang(int dong, int cot)
        {
            long TotalS = 0;

            int QtaCount = 0;
            int QdichCount = 0;
            int QtaCount2 = 0;
            int QdichCount2 = 0;


            for (int dem = 1; dem < 6 && dem + cot < ThongTin.Chess_board_width; dem++)
            {

                if (Matrix[dong][cot + dem].BackgroundImage == Player[0].Mark)
                    QtaCount++;
                else
                {
                    if (Matrix[dong][cot + dem].BackgroundImage == Player[1].Mark)
                    {
                        QdichCount++;
                        break;
                    }
                    else
                    {
                        for (int dem2 = 2; dem2 < 6 && cot + dem2 < ThongTin.Chess_board_width; dem2++)
                            if (Matrix[dong][cot + dem2].BackgroundImage == Player[0].Mark)
                            {
                                QtaCount2++;
                            }
                            else if (Matrix[dong][cot + dem2].BackgroundImage == Player[1].Mark)
                            {
                                QdichCount2++;
                                break;
                            }
                            else
                                break;
                        break;
                    }
                }

            }
            for (int dem = 1; dem < 6 && cot - dem >= 0; dem++)
            {

                if (Matrix[dong][cot - dem].BackgroundImage == Player[0].Mark)
                    QtaCount++;
                else
                {
                    if (Matrix[dong][cot - dem].BackgroundImage == Player[1].Mark)
                    {
                        QdichCount++;
                        break;
                    }
                    else
                    {
                        for (int dem2 = 2; dem2 < 6 && cot - dem2 >= 0; dem2++)
                            if (Matrix[dong][cot - dem2].BackgroundImage == Player[0].Mark)
                            {
                                QtaCount2++;
                            }
                            else if (Matrix[dong][cot - dem2].BackgroundImage == Player[1].Mark)
                            {
                                QdichCount2++;
                                break;
                            }
                            else
                                break;
                        break;
                    }
                }
            }

            if (QdichCount == 2)
                return 0;
            if (QdichCount == 0)
                TotalS += Atk[QtaCount] * 2;
            else
                TotalS += Atk[QtaCount];
            if (QdichCount2 == 0)
                TotalS += Atk[QtaCount2] * 2;
            else
                TotalS += Atk[QtaCount2];
            if (QtaCount >= QtaCount2)
                TotalS -= 1;
            else
                TotalS -= 2;
            if (QtaCount == 4)
                TotalS *= 2;
            if (QtaCount == 0)
                TotalS += Def[QdichCount] * 2;
            else
                TotalS += Def[QdichCount];
            if (QtaCount2 == 0)
                TotalS += Def[QdichCount2] * 2;
            else
                TotalS += Def[QdichCount2];

            return TotalS;
        }

        private long AtkDoc(int dong, int cot)
        {
            long TotalS = 0;
            int QtaCount = 0;
            int QdichCount = 0;
            int QtaCount2 = 0;
            int QdichCount2 = 0;
            for (int dem = 1; dem < 6 && dem + dong < ThongTin.Chess_board_height; dem++)
            {

                if (Matrix[dong + dem][cot].BackgroundImage == Player[0].Mark)
                    QtaCount++;
                else
                {
                    if (Matrix[dong + dem][cot].BackgroundImage == Player[1].Mark)
                    {
                        QdichCount++;
                        break;
                    }
                    else
                    {
                        for (int dem2 = 2; dem2 < 6 && dong + dem2 < ThongTin.Chess_board_height; dem2++)
                            if (Matrix[dong + dem2][cot].BackgroundImage == Player[0].Mark)
                            {
                                QtaCount2++;
                            }
                            else if (Matrix[dong + dem2][cot].BackgroundImage == Player[1].Mark)
                            {
                                QdichCount2++;
                                break;
                            }
                            else
                                break;
                        break;
                    }
                }
            }

            for (int dem = 1; dem < 6 && dong - dem >= 0; dem++)
            {

                if (Matrix[dong - dem][cot].BackgroundImage == Player[0].Mark)
                    QtaCount++;
                else
                {
                    if (Matrix[dong - dem][cot].BackgroundImage == Player[1].Mark)
                    {
                        QdichCount++;
                        break;
                    }
                    else
                    {
                        for (int dem2 = 2; dem2 < 6 && dong - dem2 >= 0; dem2++)
                            if (Matrix[dong - dem2][cot].BackgroundImage == Player[0].Mark)
                            {
                                QtaCount2++;
                            }
                            else if (Matrix[dong - dem2][cot].BackgroundImage == Player[1].Mark)
                            {
                                QdichCount2++;
                                break;
                            }
                            else
                                break;
                        break;
                    }
                }
            }



            if (QdichCount == 2)
                return 0;
            if (QdichCount == 0)
                TotalS += Atk[QtaCount] * 2;
            else
                TotalS += Atk[QtaCount];
            if (QdichCount2 == 0)
                TotalS += Atk[QtaCount2] * 2;
            else
                TotalS += Atk[QtaCount2];
            if (QtaCount >= QtaCount2)
                TotalS -= 1;
            else
                TotalS -= 2;
            if (QtaCount == 4)
                TotalS *= 2;
            if (QtaCount == 0)
                TotalS += Def[QdichCount] * 2;
            else
                TotalS += Def[QdichCount];
            if (QtaCount2 == 0)
                TotalS += Def[QdichCount2] * 2;
            else
                TotalS += Def[QdichCount2];

            return TotalS;
        }

        private void EndGame()
        {
            timeCD.Stop();
            PNCaroBoard.Enabled = false;
            button2.Enabled = false;
            if (current == 0)
            {
                MessageBox.Show("Chúc mừng " + Player[1].Name + " đã chiến thắng" + Player[current].Name + "!");
            }
            else if (current == 1)
            {
                MessageBox.Show("Chúc mừng " + Player[0].Name + " đã chiến thắng" + Player[current].Name + "!");
            }
        }

        private bool isEndGame(Button btn)
        {
            return isEndNgang(btn) || isEndDoc(btn) || isEndCheo(btn) || isEndCheoNguoc(btn);
        }

        private bool isEndCheoNguoc(Button btn)
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

        private bool isEndCheo(Button btn)
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
                else break;
            }

            int countBottom = 0;
            for (int i = point.Y + 1; i < ThongTin.Chess_board_height; i++)
            {
                if (Matrix[i][point.X].BackgroundImage == btn.BackgroundImage)
                {
                    countBottom++;
                }
                else break;
            }


            return countTop + countBottom == 5;
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
                else break;
            }

            int countRight = 0;
            for (int i = point.X + 1; i < ThongTin.Chess_board_width; i++)
            {
                if (Matrix[point.Y][i].BackgroundImage == btn.BackgroundImage)
                {
                    countRight++;
                }
                else break;
            }


            return countLeft + countRight == 5;
        }

        private void changePlayer()
        {
            txtPlayerName.Text = Player[current].Name;
            pctMark.Image = Player[current].Mark;
        }

        private Point GetChessPoint(Button btn)
        {
            int doc = Convert.ToInt32(btn.Tag);
            int ngang = Matrix[doc].IndexOf(btn);

            Point point = new Point(ngang, doc);

            return point;
        }

        private void Mark(Button btn)
        {
            btn.BackgroundImage = Player[current].Mark;
            //Sound();
        }

        private void timeCD_Tick(object sender, EventArgs e)
        {
            prcBar.PerformStep();
            if (prcBar.Value >= prcBar.Maximum)
            {
                EndGame();
            }
        }
        



        private bool Undo()
        {
            if (playTimeLine.Count <= 0)
            {
                MessageBox.Show("Không còn nước nào để đi lại!", "Thông báo!", MessageBoxButtons.OK);
                return false;
            }

                prcBar.Value = 0;
                timeCD.Start();
                Point PointC = playTimeLine.Pop();
                Button btnCom = Matrix[PointC.Y][PointC.X];
                btnCom.BackgroundImage = null;
                Point PointP = playTimeLine.Pop();
                Button btnPlayer = Matrix[PointP.Y][PointP.X];
                btnPlayer.BackgroundImage = null;

                changePlayer();

                return true;

                
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DrawBoard();
            PNCaroBoard.Enabled = true;
            button2.Enabled = true;
            PvC(P1);
            P1 = null;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Undo();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Exit?", "Notice", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Application.Exit();
            }
            else if (dialogResult == DialogResult.No)
            {
                
            }
        }
    }
}

        