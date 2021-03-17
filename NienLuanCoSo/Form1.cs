using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Threading;
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
        Algorithms algo = new Algorithms();
        string[] balls = {"",
            "E:\\C#\\NienLuanCoSo\\NienLuanCoSo\\Resources\\brown.png" ,
            "E:\\C#\\NienLuanCoSo\\NienLuanCoSo\\Resources\\green.jpg",
            "E:\\C#\\NienLuanCoSo\\NienLuanCoSo\\Resources\\blue.jpg",
            "E:\\C#\\NienLuanCoSo\\NienLuanCoSo\\Resources\\red.png",
            "E:\\C#\\NienLuanCoSo\\NienLuanCoSo\\Resources\\yellow.png"
        };
        public Form1()
        {
            InitializeComponent();
            board = algo.InitBoard();
            
            LoadImage(board);

            //algo.doiMauDiemCuoi("A88", board);

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        public void setColor(string Point , int color)
        {
            foreach (Control c in panel1.Controls)
            {
                PictureBox p = (PictureBox)c;
                if (p.Name == Point)
                {
                    Image newImage = Image.FromFile(balls[color]);
                    p.Image = newImage;
                }
            }
        }
        private void LoadImage(int[,] board )
        {
            Random ran = new Random();
            // init 
            try
            {
                NewBoard = algo.SetImage(board);
                foreach (DictionaryEntry pair in NewBoard)
                {
                    Console.WriteLine("{0}={1}", pair.Key, pair.Value);
                }

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
            //do not delete start
        }
        private async void Change_value(object sender, EventArgs e)
        {
            PictureBox target = (PictureBox)sender;
            if (colorPicture == 0)
            {
                startPointX = algo.FirstNumberX(target.Name);
                startPointY = algo.FirstNumberY(target.Name);
                colorPicture = board[startPointX, startPointY];
            }
            else
            {
                EndPointX = algo.FirstNumberX(target.Name);
                EndPointY = algo.FirstNumberY(target.Name);
                int tempX = 0;
                int tempY = 0;
                LinkedList<string> findPath = algo.findPath(board, startPointX, startPointY, EndPointX, EndPointY);
                foreach (string item in findPath)
                {

                    NewBoard.Remove(algo.ConvertTwoPosition(tempX, tempY));
                    
                    int x = algo.FirstNumberX(item);
                    int y = algo.FirstNumberY(item);
                   
                    tempX = x;
                    tempY = y;
                    //Console.Write(item + " ");
                    board[x, y] = board[startPointX, startPointY];
                    await Task.Delay(100);
                    LoadImage(board);
                    //NewBoard.Remove(item);
                }
                //for (int i = 0; i < 9; i++)
                //{
                //    for (int j = 0; j < 9; j++)
                //    {
                //        Console.Write(board[i, j] + " ");
                //    }
                //    Console.WriteLine();
                //}
                Console.WriteLine();
                colorPicture = 0;
            }
        }
    }
}
