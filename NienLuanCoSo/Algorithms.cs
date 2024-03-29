﻿using System;
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
                int x = ran.Next(0 , 9);
                int y = ran.Next(0 , 9);
                if (board[x, y] != 1)
                {
                    int getCorlor = ran.Next(1 , 6);
                    board[x, y] = getCorlor;
                    box--;
                }  
            }
            return board;
        }
        public int CheckgameOver(int[,] board)
        {
            int count = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (board[i, j] != 0)
                    {
                        count++;
                    }
                }
            }
            return count;
        }


        public void RandomPointBoard(int[,] board)
        {
            int count = 0;
            Random ran = new Random();
            if(CheckgameOver(board) < 3)
            {
                
                return;
            }
            while (true)
            {
                int x = ran.Next(0, 9);
                int y = ran.Next(0, 9);
                if (board[x, y] == 0)
                {
                    board[x, y] = ran.Next(1, 6);
                    count++;
                }
                if(count == 3)
                {
                    return;
                }
            }
        }

        public Hashtable SetImage(int[,] board) {
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
                        path.Add(ConvertTwoPosition(xx, yy), ConvertTwoPosition(x, y));
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
            int cantFind = 0;
            int[] u = { 1, 0, -1, 0 };
            int[] v = { 0, 1, 0, -1 };
           
            if ( (int)Math.Abs(startX - endX) + Math.Abs(startY - endY) == 1)
            {
                Path.AddLast(ConvertTwoPosition(startX, startY));
                Path.AddLast(ConvertTwoPosition(endX, endY));
                return Path;
            }
            else {
                while (cantFind < 4)
                {

                    x = endX + u[cantFind];
                    y = endY + v[cantFind];

                    Path.Clear();
                    Path.AddLast(ConvertTwoPosition(x, y));
                    try
                    {
                        if (x == endX && y == endY)
                        {
                            return Path;
                        }
                        else
                        {
                            while (true)
                            {
                                string temp = ConvertTwoPosition(x, y);
                                string parent_of_each_path = AllPath[temp];
                                Path.AddLast(parent_of_each_path);

                                x = FirstNumberX(parent_of_each_path);
                                y = FirstNumberY(parent_of_each_path);

                                if (x == startX && y == startY)
                                {
                                    Path.AddFirst(ConvertTwoPosition(endX, endY));
                                    return PathReverse(Path);
                                }
                            }
                        }
                    }
                    catch
                    {
                        cantFind++;
                    }
                }
            }
            Path.Clear();
            Path.AddLast(ConvertTwoPosition(startX, startY));
            return PathReverse(Path);
        }
        public LinkedList<string> PathReverse(LinkedList<string> Path)
        {
            LinkedList<string> newPath = new LinkedList<string>();
            foreach (string point in Path)
            {
                newPath.AddFirst(point);
            }

            return newPath;
        }
        public LinkedList<string> CheckBehind(int[,] board,int PointX , int PointY , int i , int j , int color)
        {
            LinkedList<string> checkBehind = new LinkedList<string>();
            int tempPointX = PointX;
            int tempPointY = PointY;
            while (true)
            {
                tempPointX += i * -1;
                tempPointY += j * -1;
                if (isInside(tempPointX, tempPointY))
                {
                    if (board[tempPointX, tempPointY] == color)
                    {
                        checkBehind.AddLast(ConvertTwoPosition(tempPointX, tempPointY));
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            return checkBehind;
        }


        public LinkedList<string> ScoreBoard(int[,] board , int pointX, int pointY , int color)
        {
            int[] u = { -1, -1, -1, 0, 0 , 1 , 1 , 1};
            int[] v = { -1, 0, 1, -1, 1, -1, 0, 1 };
            LinkedList<string> ScorePoint = new LinkedList<string>();
            LinkedList<string> finnalScorePoint = new LinkedList<string>();
            for (int i = 0; i < 8; i++)
            {
                int tempPointX = pointX;
                int tempPointY = pointY;
                while (true)
                {
                    
                    tempPointX += u[i];
                    tempPointY += v[i];
                    if (isInside(tempPointX, tempPointY))
                    {
                        if (board[tempPointX, tempPointY] == color)
                        {
                            ScorePoint.AddLast(ConvertTwoPosition(tempPointX, tempPointY));
                        }
                        else if (board[tempPointX, tempPointY] != color)
                        {
                            LinkedList<string> checkBehind = CheckBehind(board, pointX, pointY, u[i], v[i], color);
                                // checkBehind
                            if(checkBehind.Count() + ScorePoint.Count() >= 4)
                            {
                               foreach (string str in ScorePoint)
                               {
                                    if (finnalScorePoint.Contains(str) == false) 
                                        finnalScorePoint.AddLast(str);
                               }
                               foreach (string str in checkBehind)
                               {
                                    if (finnalScorePoint.Contains(str) == false)
                                        finnalScorePoint.AddLast(str);
                               }
                                break;  
                            }
                            ScorePoint.Clear();
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return finnalScorePoint;
        }
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
