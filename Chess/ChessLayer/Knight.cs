using Chess.Board;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.ChessLayer
{
	class Knight : Piece
	{
		public Knight(ChessBoard Board, Color Color) : base(Board, Color)
		{

		}

		public override string ToString()
		{
			return "H";
		}

		private bool AllowedToMove(Position position)
		{
			Piece piece = Board.Piece(position);
			return (piece == null) || (piece.Color != Color);
		}

		public override bool[,] PossibleMovements()
		{
			bool[,] matrix = new bool[Board.Rows, Board.Columns];

			Position position = new Position(0, 0);

			position.DefineValues(Position.Row - 1, Position.Column - 2);
			if (Board.IsValid(position) && AllowedToMove(position))
			{
				matrix[position.Row, position.Column] = true;
			}

			position.DefineValues(Position.Row - 2, Position.Column - 1);
			if (Board.IsValid(position) && AllowedToMove(position))
			{
				matrix[position.Row, position.Column] = true;
			}

			position.DefineValues(Position.Row - 2, Position.Column + 1);
			if (Board.IsValid(position) && AllowedToMove(position))
			{
				matrix[position.Row, position.Column] = true;
			}

			position.DefineValues(Position.Row - 1, Position.Column + 2);
			if (Board.IsValid(position) && AllowedToMove(position))
			{
				matrix[position.Row, position.Column] = true;
			}

			position.DefineValues(Position.Row + 1, Position.Column + 2);
			if (Board.IsValid(position) && AllowedToMove(position))
			{
				matrix[position.Row, position.Column] = true;
			}

			position.DefineValues(Position.Row + 2, Position.Column + 1);
			if (Board.IsValid(position) && AllowedToMove(position))
			{
				matrix[position.Row, position.Column] = true;
			}

			position.DefineValues(Position.Row + 2, Position.Column - 1);
			if (Board.IsValid(position) && AllowedToMove(position))
			{
				matrix[position.Row, position.Column] = true;
			}

			position.DefineValues(Position.Row + 1, Position.Column - 2);
			if (Board.IsValid(position) && AllowedToMove(position))
			{
				matrix[position.Row, position.Column] = true;
			}

			return matrix;
		}
	}
}
