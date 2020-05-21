using Chess.Board;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
	class Screen
	{
		public static void PrintBoard(ChessBoard Board)
		{
			for (int i = 0; i < Board.Rows; i++)
			{
				for (int j = 0; j < Board.Columns; j++)
				{
					if (Board.Piece(i, j) == null)
					{
						Console.Write("- "); 
					}
					else
					{
						Console.Write(Board.Piece(i, j) + " ");
					}
				}
				Console.WriteLine();
			}

			
		}
	}
}
