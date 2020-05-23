using Chess.Board;
using Chess.BoardLayer;
using System;

namespace Chess
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				ChessMatch match = new ChessMatch();

				while (!match.EndedMatch)
				{
					Console.Clear();
					Screen.PrintBoard(match.Board);

					Console.WriteLine();
					Console.Write("Origin: ");
					Position origin = Screen.ReadChessPosition().ToPosition();
					Console.Write("Destination: ");
					Position destination = Screen.ReadChessPosition().ToPosition();

					match.ExecuteMovement(origin, destination);
				}
			}
			catch (BoardException exception)
			{
				Console.WriteLine(exception.Message);
			}

		}
	}
}
