using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.Board
{
	abstract class Piece
	{
		public Position Position { get; set; }
		public Color Color { get; protected set; }
		public int MovementsCount { get; protected set; }
		public ChessBoard Board { get; protected set; }

		public Piece(ChessBoard board, Color color)
		{
			Position = null;
			Color = color;
			MovementsCount = 0;
			Board = board;
		}

		public void IncreaseMovementCount()
		{
			MovementsCount++;
		}

		public void DecreaseMovementCount()
		{
			MovementsCount--;
		}

		public abstract bool[,] PossibleMovements();

		public bool PossibleMovement(Position position)
		{
			return PossibleMovements()[position.Row, position.Column];
		}

		public bool IsThereAPossibleMovement()
		{
			bool[,] matrix = PossibleMovements();
			for (int i = 0; i < Board.Rows; i++)
			{
				for (int j = 0; j < Board.Columns; j++)
				{
					if (matrix[i, j])
					{
						return true;
					}
				}
			}

			return false;
		}

		public bool IsAllowedMoveTo(Position position)
		{
			return PossibleMovements()[position.Row, position.Column];
		}
	}
}
