using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Threading;
using System.Runtime.InteropServices;
using System.IO;
using System.Reflection;

namespace NienLuanCoSo
{
    
    public partial class Form1 : Form
    {
        int startPointX;
        int startPointY;
        int colorPicture;
        int EndPointX;
        int EndPointY;
        Hashtable NewBoard;
        int[,] board;
        int[,] UndoBoard;
        Boolean canUndo = false;
        String undoPoint;
        Algorithms algo = new Algorithms();
        static private List<PrivateFontCollection> _fontCollections;
        Image[] images = { null,
        (Image)Properties.Resources.yellow,
        (Image)Properties.Resources.pink ,
        (Image)Properties.Resources.blue ,
        (Image)Properties.Resources.green ,
        (Image)Properties.Resources.red ,

        (Image)Properties.Resources.d1 ,
        (Image)Properties.Resources.d2 ,
        (Image)Properties.Resources.d3 ,
        (Image)Properties.Resources.d4 ,
        (Image)Properties.Resources.d5 ,

        };
        public Form1()
        {
            InitializeComponent();
            board = algo.InitBoard();
            LoadImage(board);
            
            //Console.WriteLine(Properties.Resources.yellow.ToString());
            //GetNewHighScore();
        }
  
        public void ClearPointWhenGetScore(int[,] board, LinkedList<string> ScorePoint)
        {
            
                foreach (Control c in panel1.Controls)
                {
                    PictureBox p = (PictureBox)c;
                    if (ScorePoint.Contains(p.Name))
                    {
                        p.Image = null;
                        board[algo.FirstNumberX(p.Name), algo.FirstNumberY(p.Name)] = 0;
                    }
                }
           
        }
        private void LoadImage(int[,] board )
        {
            Random ran = new Random();
         
            try
            {
                NewBoard = algo.SetImage(board);
                foreach (object key in NewBoard.Keys)
                {
                    foreach (Control c in panel1.Controls)
                    {
                        PictureBox p = (PictureBox)c;
                        if (p.Name == key.ToString())
                        {
                            int corlor = Int32.Parse(NewBoard[key].ToString());
                            Image newImage = images[corlor];
                            p.Image = newImage;
                        }
                    }
                }
            }
            catch
            {

            }
            
        }
        private async void Change_value(object sender, EventArgs e)
        {
            PictureBox target = (PictureBox)sender;
            if (colorPicture == 0)
            {
                    UndoBoard = (int[,])board.Clone();
                    
                foreach (Control c in this.Controls)
                {
                    if (c is TextBox && c.Name == "Score")
                    {
                        undoPoint = c.Text;
                    }
                }
                startPointX = algo.FirstNumberX(target.Name);
                startPointY = algo.FirstNumberY(target.Name);
                colorPicture = board[startPointX, startPointY];
                if (colorPicture !=0 ) {
                    target.Image = images[colorPicture + 5];
                }

            }
            else if (startPointX == algo.FirstNumberX(target.Name) && startPointY == algo.FirstNumberY(target.Name))
            {
                startPointX = 0;
                startPointY = 0;
                EndPointX = 0;
                EndPointY = 0;
                colorPicture = 0;
            }
            else
            {
                foreach (Control c in this.Controls)
                {
                    if(c is Panel)
                    {
                        c.Enabled = false;
                    }
                }
                EndPointX = algo.FirstNumberX(target.Name);
                EndPointY = algo.FirstNumberY(target.Name);

                if (board[EndPointX, EndPointY] == 0)
                {
                    int tempX = 0;
                    int tempY = 0;
                    bool CreatePoint = false;
                    
                    LinkedList<string> findPath = algo.findPath(board, startPointX, startPointY, EndPointX, EndPointY);
                    if (findPath.Count() > 1)
                    {
                        foreach (string item in findPath)
                        {
                            int x = algo.FirstNumberX(item);
                            int y = algo.FirstNumberY(item);

                            tempX = x;
                            tempY = y;
                          
                            board[x, y] = colorPicture;
                            LoadImage(board);
                            await Task.Delay(50);
                            
                            foreach (Control c in panel1.Controls)
                            {
                                PictureBox p = (PictureBox)c;

                                if (p.Name == algo.ConvertTwoPosition(tempX, tempY))
                                {
                                    p.Image = null;
                                    if (x != EndPointX || y != EndPointY)
                                    {
                                        board[tempX, tempY] = 0;
                                    }
                                }
                            }
                        }
                        CreatePoint = true;
                    }
                    LinkedList<string> ScorePoint = new LinkedList<string>();
                    ScorePoint = algo.ScoreBoard(board, EndPointX, EndPointY, colorPicture);
                    if (ScorePoint.Count() >= 4)
                    {
                        ScorePoint.AddLast(algo.ConvertTwoPosition(EndPointX, EndPointY));
                        ClearPointWhenGetScore(board, ScorePoint);
                        int Score = ScorePoint.Count() * 5;
                        foreach (Control c in this.Controls)
                        {
                            if (c is TextBox && c.Name == "Score")
                            {
                           
                                c.Font = new Font("Microsoft Sans Serif", 16, FontStyle.Regular);
                            
                                string TempText = c.Text;
                                c.Text = c.Text + " +" + Score.ToString();
                                await Task.Delay(500);
                                int startScore = 0;
                                int EndScore = 0;
                                c.Text = TempText;
                                if (c.Text != "")
                                {
                                    startScore = Int32.Parse(c.Text);
                                    EndScore = Score + Int32.Parse(c.Text);
                                }
                                else
                                {
                                    startScore = 0;
                                    EndScore = Score;
                                }
                                for (int i = startScore; i <= EndScore; i++)
                                {
                                    c.Text = i.ToString();
                                    await Task.Delay(10);
                                }
                            }
                        }
                        LoadImage(board);
                    }
                    else
                    {
                        if (CreatePoint) 
                        {
                            algo.RandomPointBoard(board);  
                            CreatePoint = false;
                        }
                     
                    }
                 
                    LoadImage(board);
                    colorPicture = 0;
                    Console.WriteLine(algo.CheckgameOver(board));
                    if (algo.CheckgameOver(board) >78)
                    {
                        foreach (Control c in this.Controls)
                        {
                            if (c is TextBox && c.Name == "Score")
                            {
                                
                                var form3 = new Form3(c.Text);
                                form3.Show();
                                this.Hide();
                            }
                        }

                    }

                }
                else if (board[EndPointX, EndPointY] > 0)
                {

                    foreach (Control c in panel1.Controls)
                    {
                        PictureBox p = (PictureBox)c;
                        if(p.Name == algo.ConvertTwoPosition(startPointX , startPointY))
                        {
                            p.Image = images[colorPicture];
                            target.Image = images[board[EndPointX, EndPointY] + 5];
                        }
                    }
                    colorPicture = board[EndPointX, EndPointY];
                    startPointX = EndPointX ;
                    startPointY = EndPointY;
                    EndPointX = 0;
                    EndPointY = 0;

                }

                foreach (Control c in this.Controls)
                {
                    if (c is Panel)
                    {
                        c.Enabled = true;
                    }
                }
                canUndo = true;
            }
        }
        static public Font GetCustomFont(byte[] fontData, float size, FontStyle style)
        {
            if (_fontCollections == null) _fontCollections = new List<PrivateFontCollection>();
            PrivateFontCollection fontCol = new PrivateFontCollection();
            IntPtr fontPtr = Marshal.AllocCoTaskMem(fontData.Length);
            Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            fontCol.AddMemoryFont(fontPtr, fontData.Length);
            Marshal.FreeCoTaskMem(fontPtr);     //<-- It works!
            _fontCollections.Add(fontCol);
            return new Font(fontCol.Families[0], size, style);
        }
        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    board[i, j] = 0;
                }
            }
            foreach (Control c in this.Controls)
            {
                if (c is TextBox && c.Name == "Score")
                {
                    c.Text = "0";
                }
            }
            foreach (Control c in panel1.Controls)
            {
                PictureBox p = (PictureBox)c;
                p.Image = null;
            }
     
            LoadImage(board);
            board = algo.InitBoard();

            LoadImage(board);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void UndoStep(object sender, EventArgs e)
        {
            if (canUndo)
            {
                foreach (Control c in this.Controls)
                {
                    if (c is TextBox && c.Name == "Score")
                    {
                        c.Text = undoPoint;
                    }
                }
                foreach (Control c in panel1.Controls)
                {
                    PictureBox p = (PictureBox)c;
                    p.Image = null;
                }
                LoadImage(UndoBoard);
                board = (int[,])UndoBoard.Clone();
            }
        }

        private void showLeaderBoard(object sender, EventArgs e)
        {
            //this.Hide();
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }

 
    }
}
