using Chess.Board;
using Chess.BoardLayer;
using System;

namespace Chess
{
	class Program
	{
		static void Main(string[] args)
		{
			ChessBoard board = new ChessBoard(8, 8);

			try
			{
				board.AddPiece(new Rook(board, Color.Black), new Position(0, 0));
				board.AddPiece(new Rook(board, Color.Black), new Position(1, 3));
				board.AddPiece(new King(board, Color.Black), new Position(3, 2));

				Screen.PrintBoard(board);
			}
			catch (BoardException exception)
			{
				Console.WriteLine(exception.Message);
			}

		}
	}
}
