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
using System.IO;
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
        string[] balls = {"",
            "E:\\C#\\NienLuanCoSo\\NienLuanCoSo\\Resources\\yellow.png" ,
            "E:\\C#\\NienLuanCoSo\\NienLuanCoSo\\Resources\\pink.png",
            "E:\\C#\\NienLuanCoSo\\NienLuanCoSo\\Resources\\blue.png",
            "E:\\C#\\NienLuanCoSo\\NienLuanCoSo\\Resources\\green.png",
            "E:\\C#\\NienLuanCoSo\\NienLuanCoSo\\Resources\\red.png",
           
            "E:\\C#\\NienLuanCoSo\\NienLuanCoSo\\Resources\\d1.gif",
            "E:\\C#\\NienLuanCoSo\\NienLuanCoSo\\Resources\\d2.gif",
            "E:\\C#\\NienLuanCoSo\\NienLuanCoSo\\Resources\\d3.gif",
            "E:\\C#\\NienLuanCoSo\\NienLuanCoSo\\Resources\\d4.gif",
            "E:\\C#\\NienLuanCoSo\\NienLuanCoSo\\Resources\\d5.gif",
        };
        public Form1()
        {
            InitializeComponent();
            board = algo.InitBoard();
            LoadImage(board);
            //GetNewHighScore();
        }
        public void GetNewHighScore()
        {
            int counter = 0;
            string line;
            // Add this textbox to form
            string FilePath = @"C:\Users\User\OneDrive\Desktop\NienLuan\NienLuan\NienLuanCoSo\bin\Debug\HighScoce.txt";
            System.IO.StreamReader file =  new System.IO.StreamReader(@"C:\Users\User\OneDrive\Desktop\NienLuan\NienLuan\NienLuanCoSo\bin\Debug\HighScoce.txt");
            List<string> Lines = new List<string>();
            int count = 0;
            while ((line = file.ReadLine()) != null)
            {
                Lines.Add(line);
            } 
            foreach (string eachline in Lines)
            {
                Console.WriteLine("hello");
                Button Mybutton = new Button();
                Mybutton.Location = new Point(169, 44);
                Mybutton.Left = 530;
                Mybutton.Top = 230 + count * 50;
                Mybutton.Text = "Submit";
                Mybutton.AutoSize = true;
                Mybutton.BackColor = Color.LightBlue;
                Mybutton.Padding = new Padding(6);
                Mybutton.Font = new Font("French Script MT", 18);
                //TextBox Mytextbox = new TextBox();
                //Mytextbox.Height = 40;
                //Mytextbox.Width = 180;
                //Mytextbox.Font = new Font(Mytextbox.Font.FontFamily, 15);
                //Mytextbox.AutoSize = true;
                //Mytextbox.Name = "text_box" + count;
                //Mytextbox.Text = eachline;
                //Mytextbox.Enabled = false;
                //Mytextbox.Left = 530;
                //Mytextbox.Top = 230 + count * 50;
                this.Controls.Add(Mybutton);
                count++;
            }
            file.Close();
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
                            Image newImage = Image.FromFile(balls[corlor]);
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
            //UndoBoard = board;
 
            if (colorPicture == 0)
            {
                //UndoBoard = board;
               
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
                    target.Image = Image.FromFile(balls[colorPicture + 5]);
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
                                PrivateFontCollection pfc = new PrivateFontCollection();
                                pfc.AddFontFile(@"C:\Users\User\OneDrive\Desktop\NienLuan\NienLuan\cursed-timer-ulil-font\CursedTimerUlil-Aznm.ttf");
                                c.Font = new Font(pfc.Families[0], 16, FontStyle.Regular);
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
                  
                }
                else if (board[EndPointX, EndPointY] > 0)
                {

                    foreach (Control c in panel1.Controls)
                    {
                        PictureBox p = (PictureBox)c;
                        if(p.Name == algo.ConvertTwoPosition(startPointX , startPointY))
                        {
                            p.Image = Image.FromFile(balls[colorPicture]);
                            target.Image = Image.FromFile(balls[board[EndPointX, EndPointY] + 5]);
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
    }
}
