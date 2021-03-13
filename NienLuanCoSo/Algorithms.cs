using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NienLuanCoSo
{
    class Algorithms
    {
        public int[,] InitBoard() {
            int[,] board = new int[9, 9];
            Random ran = new Random();
            int box = 81;
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    board[i, j] = 0;

            while (box > 75)
            {
                int i = ran.Next(0 , 9);
                int y = ran.Next(0 , 9);
                if (board[i, y] != 1)
                {
                    int getCorlor = ran.Next(0 , 6);
                    board[i, y] = getCorlor;
                    box--;
                }  
            }
            return board;
        }
        public void logBoard (int[,] board)
        {
            bool[,] Convert = ConvertBoardToBool(board);
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write(Convert[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
        public Hashtable loadImage(int[,] board) {
            Hashtable position = new Hashtable();
            for (int  i = 0;i < 9; i++)
            {
                for(int j = 0;j< 9; j++)
                {
                    if(board[i,j] > 0)
                    {
                        string key = "A" + i.ToString() + j.ToString();
                        position.Add(key , board[i,j]);
                    }
                }
            }
            return position;
        }
        public int FirstNumberX(string position)
        {
            int n;
            n = Int32.Parse(position[1].ToString());
            return n;
        }
        public int FirstNumberY(string position)
        {
            int n;
            n = Int32.Parse(position[2].ToString());
            return n;
        }
        public string ConvertTwoPosition(int x , int y)
        {
            return "A" + x.ToString() + y.ToString();
        }
        public bool[,] ConvertBoardToBool (int[,] board)
        {
            bool[,] CheckBoard = new bool[9, 9];
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    if (board[i, j] == 0)
                        CheckBoard[i, j] = true;

            return CheckBoard;
        }

        public void reconstructPath(Hashtable path ,int endX , int  endY ,int startX, int startY)
        {
            LinkedList<string> reconstructPath = new LinkedList<string>();
            foreach (DictionaryEntry item in path)
            {
                Console.WriteLine(item.Key + "\t" + item.Value);
            }

        }

      
        public Dictionary<string, string> BFS(int[,] board, int startX, int startY, int endX, int endY)
        {
            LinkedList<string> PathQueue = new LinkedList<string>();
            var path = new Dictionary<string, string>();

            int[,] ChiPhi = new int[9, 9];

            int[] u = { 1, 0, -1, 0 };
            int[] v = { 0, 1, 0, -1 };

            bool[,] visited = ConvertBoardToBool(board);

            PathQueue.AddLast(ConvertTwoPosition(startX, startY));
            visited[startX, startY] = false;

            while (PathQueue.Any())
            {
                int x = FirstNumberX(PathQueue.First());
                int y = FirstNumberY(PathQueue.First());

                PathQueue.RemoveFirst();
                for (int k = 0; k < 4; k++)
                {
                    int xx = x + u[k];
                    int yy = y + v[k];
                    if (xx == endX && yy == endY)
                    {
                        visited[x, y] = false;
                        return path;
                    }
                    if (!isInside(xx, yy)) continue;

                    if (visited[xx, yy] == true)
                    {
                        visited[xx, yy] = false;
                        ChiPhi[xx, yy] = ChiPhi[x, y] + 1;
                        PathQueue.AddLast(ConvertTwoPosition(xx, yy));
                        path.Add(ConvertTwoPosition(xx, yy), ConvertTwoPosition(x, y));
                    }
                }
            }
            return path;
        }

        public LinkedList<string> findPath(int[,] board, int startX , int startY , int endX , int endY )
        {
            LinkedList<string> Path = new LinkedList<string>();
            Dictionary<string, string> AllPath = BFS(board, startX, startY, endX, endY);
            int x, y;
            x = endX + 1;
            y = endY ;
            Path.AddLast(ConvertTwoPosition(x, y));
            try
            {
                while (true)
                {
                    string temp = ConvertTwoPosition(x, y);
                    string parent_of_each_path = AllPath[temp];
                    Path.AddLast(parent_of_each_path);

                    x = FirstNumberX(parent_of_each_path);
                    y = FirstNumberY(parent_of_each_path);

                    if (x == startX && y == endY)
                        break;

                }
            }
            catch(Exception e)
            {

                Console.WriteLine(e.Message);
            }
            return Path;
        }


        //end test

        //public int BFS(int[,] board, int startX, int startY, int endX, int endY)
        //{

        //    int Rows = 9;
        //    int Columns = 9;
        //    // board la ma tran cua Rows, va Colums
        //    /// rq , cq la hang doi cua bai
        //    LinkedList<string> PathQueue = new LinkedList<string>();

        //    int move_count = 0;
        //    int node_left_in_layer = 1;
        //    int Nodes_in_next_layer = 0;

        //    bool reached = false;
        //    bool[,] visited = ConvertBoardToBool(board);

        //    int[] u = { 1, 0, -1, 0 };
        //    int[] v = { 0, 1, 0, -1 };

        //    PathQueue.AddLast(ConvertTwoPosition(startX, startY));
        //    visited[startX, startY] = false;

        //    while (PathQueue.Any())
        //    {
        //        int x = FirstNumberX(PathQueue.First());
        //        int y = FirstNumberY(PathQueue.First());

        //        PathQueue.RemoveFirst();

        //        if (x == endX && y == endY)
        //        {
        //            reached = true;
        //            break;
        //        }
        //        for (int i = 0; i < 4; i++)
        //        {
        //            int newX = x + u[i];
        //            int newY = y + v[i];

        //            if (newX < 0 || newY < 0) continue;
        //            if (newX >= 9 || newY >= 9) continue;

        //            if (visited[newX, newY] == false) continue;

        //            PathQueue.AddLast(ConvertTwoPosition(newX, newY));

        //            visited[newX, newY] = false;
        //            Nodes_in_next_layer++;
        //        }
        //        node_left_in_layer--;
        //        if (node_left_in_layer == 0)
        //        {
        //            node_left_in_layer = Nodes_in_next_layer;
        //            Nodes_in_next_layer = 0;
        //            move_count++;
        //        }
        //    }
        //    if (reached)
        //    {
        //        return move_count;
        //    }
        //    return -1;
        //}

        bool isInside(int i, int j)
        {
            return (i >= 0 && i < 9 && j >= 0 && j < 9);
        }

        public int[,] startPoint(int i , int j , int[,] board)
        {
            int[,] TempBoard = board;
            // distance from the top
          for(int h = 0; h < 9; h++)
           {
                TempBoard[h, j] = Math.Abs(i - h);
           }
            // distance from the bottom ;
            for (int k = 0; k < 9; k++)
            {
                TempBoard[i, k] = Math.Abs(j - k);
            }

            return TempBoard;
        }
    }
}
