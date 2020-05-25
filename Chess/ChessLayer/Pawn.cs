using Chess.Board;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.ChessLayer
{
	class Pawn : Piece
	{
		private ChessMatch Match;
		public Pawn(ChessBoard Board, Color Color, ChessMatch match) : base(Board, Color)
		{
			Match = match;
		}

		public override string ToString()
		{
			return "P";
		}

		private bool IsThereAnEnemy(Position position)
		{
			Piece piece = Board.Piece(position);
			return (piece != null) && (piece.Color != Color);
		}

		private bool FreeToMove(Position position)
		{
			return Board.Piece(position) == null;
		}

		public override bool[,] PossibleMovements()
		{
			bool[,] matrix = new bool[Board.Rows, Board.Columns];

			Position position = new Position(0, 0);

			if (Color == Color.White)
			{
				position.DefineValues(Position.Row - 1, Position.Column);
				if (Board.IsValid(position) && FreeToMove(position))
				{
					matrix[position.Row, position.Column] = true;
				}

				position.DefineValues(Position.Row - 2, Position.Column);
				Position p2 = new Position(position.Row - 1, position.Column);
				if (Board.IsValid(p2) && FreeToMove(p2) && Board.IsValid(position) && FreeToMove(position) && MovementsCount == 0)
				{
					matrix[position.Row, position.Column] = true;
				}

				position.DefineValues(Position.Row - 1, Position.Column - 1);
				if (Board.IsValid(position) && IsThereAnEnemy(position))
				{
					matrix[position.Row, position.Column] = true;
				}

				position.DefineValues(Position.Row - 1, Position.Column + 1);
				if (Board.IsValid(position) && IsThereAnEnemy(position))
				{
					matrix[position.Row, position.Column] = true;
				}

				//Special move: En Passant
				if (position.Row == 3)
				{
					Position left = new Position(position.Row, position.Column - 1);
					if (Board.IsValid(left) && IsThereAnEnemy(left) && Board.Piece(left) == Match.VulnerableToEnPassant)
					{
						matrix[left.Row - 1, left.Column] = true;
					}

					Position right = new Position(position.Row, position.Column + 1);
					if (Board.IsValid(right) && IsThereAnEnemy(right) && Board.Piece(right) == Match.VulnerableToEnPassant)
					{
						matrix[right.Row - 1, right.Column] = true;
					}
				}
			}
			else
			{
				position.DefineValues(Position.Row + 1, Position.Column);
				if (Board.IsValid(position) && FreeToMove(position))
				{
					matrix[position.Row, position.Column] = true;
				}

				position.DefineValues(Position.Row + 2, Position.Column);
				Position p2 = new Position(position.Row + 1, position.Column);
				if (Board.IsValid(p2) && FreeToMove(p2) && Board.IsValid(position) && FreeToMove(position) && MovementsCount == 0)
				{
					matrix[position.Row, position.Column] = true;
				}

				position.DefineValues(Position.Row + 1, Position.Column - 1);
				if (Board.IsValid(position) && IsThereAnEnemy(position))
				{
					matrix[position.Row, position.Column] = true;
				}

				position.DefineValues(Position.Row + 1, Position.Column + 1);
				if (Board.IsValid(position) && IsThereAnEnemy(position))
				{
					matrix[position.Row, position.Column] = true;
				}

				//Special move: En Passant
				if (position.Row == 4)
				{
					Position left = new Position(position.Row, position.Column - 1);
					if (Board.IsValid(left) && IsThereAnEnemy(left) && Board.Piece(left) == Match.VulnerableToEnPassant)
					{
						matrix[left.Row + 1, left.Column] = true;
					}

					Position right = new Position(position.Row, position.Column + 1);
					if (Board.IsValid(right) && IsThereAnEnemy(right) && Board.Piece(right) == Match.VulnerableToEnPassant)
					{
						matrix[right.Row + 1, right.Column] = true;
					}
				}
			}

			return matrix;
		}


	}
}
