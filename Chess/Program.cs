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
					try
					{
						Console.Clear();
						Screen.PrintBoard(match.Board);

						Console.WriteLine();
						Console.WriteLine("Turn: " + match.Turn);
						Console.WriteLine("Waiting for movement: " + match.ActualPlayer);

						Console.WriteLine();
						Console.Write("Origin: ");
						Position origin = Screen.ReadChessPosition().ToPosition();
						match.ValidateOriginPosition(origin);

						bool[,] possiblePositions = match.Board.Piece(origin).PossibleMovements();

						Console.Clear();
						Screen.PrintBoard(match.Board, possiblePositions);

						Console.WriteLine();
						Console.Write("Destination: ");
						Position destination = Screen.ReadChessPosition().ToPosition();
						match.ValidateDestinationPosition(origin, destination);

						match.PerformMovement(origin, destination);
					}
					catch (BoardException exception)
					{
						Console.WriteLine(exception.Message);
						Console.WriteLine("Press Enter to continue...");
						Console.ReadLine();
					}
				}
			}
			catch (BoardException exception)
			{
				Console.WriteLine(exception.Message);
			}

		}
	}
}
