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
namespace NienLuanCoSo
{
    
    public partial class Form1 : Form
    {
        int startPointX;
        int startPointY;
        int colorPicture;
        int EndPointX;
        int EndPointY;
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
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Random ran = new Random();

            // init 
            board = algo.InitBoard();
           
            //LinkedList<string> findPath = algo.findPath(board, 5, 5, 0, 0);
            

            // in tat ca duong di
            //foreach (string item in findPath)
            //{
            //    Console.Write(item + " ");
            //}
            //Console.WriteLine();

            //Image newImage = Image.FromFile("E:\\C#\\NienLuanCoSo\\NienLuanCoSo\\Resources\\brown.png");
            //do not delete start
            try
            {
                Hashtable NewBoard = algo.loadImage(board);
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
        private void Change_value(object sender, EventArgs e)
        {
            PictureBox target = (PictureBox)sender;
            if (colorPicture == 0)
            {
                startPointX = algo.FirstNumberX(target.Name);
                startPointY = algo.FirstNumberY(target.Name);
                colorPicture = board[startPointX, startPointY];
                
                MessageBox.Show("da chay gia tri dau");
            }
            else
            {
                EndPointX = algo.FirstNumberX(target.Name);
                EndPointY = algo.FirstNumberY(target.Name);
                LinkedList<string> findPath = algo.findPath(board, startPointX, startPointY, EndPointX, EndPointY);
                foreach (string item in findPath)
                {
                    Console.Write(item + " ");
                }
                MessageBox.Show("da chay gia tri dich");
                colorPicture = 0;
            }
        }
    }
}
