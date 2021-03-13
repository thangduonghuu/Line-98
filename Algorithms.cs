using System;

public class Algorithms
{
	public Algorithms()
	{
		public static void InitBoard()
        {
			int[,] board = new int[9, 9];
			for(int i = 0; i < 9; i++)
            {
				for(int j = 0; j< 9; j++)
                {
					Console.Write(board[i, j]);
                }
				Console.WriteLine();
            }
        }
	}
}
