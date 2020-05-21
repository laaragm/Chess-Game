using Chess.Board;
using System;

namespace Chess
{
	class Program
	{
		static void Main(string[] args)
		{
			ChessBoard board = new ChessBoard(8, 8);
			Screen.PrintBoard(board);
		}
	}
}
