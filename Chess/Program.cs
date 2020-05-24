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
						Console.WriteLine("User Guide: K-King; B-Bishop; H-Knight; R-Rook; Q-Queen; P-Pawn");
						Screen.PrintMatch(match);

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
				Console.Clear();
				Screen.PrintMatch(match);
			}
			catch (BoardException exception)
			{
				Console.WriteLine(exception.Message);
			}

		}
	}
}
