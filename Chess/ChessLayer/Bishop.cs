using Chess.Board;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.ChessLayer
{
	class Bishop : Piece
	{
		public Bishop(ChessBoard board, Color color) : base(board, color)
		{

		}

		public override string ToString()
		{
			return "B";
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
			
			//North-East
			position.DefineValues(Position.Row - 1, Position.Column + 1);
			while (Board.IsValid(position) && AllowedToMove(position))
			{
				matrix[position.Row, position.Column] = true;
				if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
				{
					break;
				}
				position.DefineValues(position.Row - 1, position.Column - 1);
			}

			//South-East
			position.DefineValues(Position.Row + 1, Position.Column + 1);
			while (Board.IsValid(position) && AllowedToMove(position))
			{
				matrix[position.Row, position.Column] = true;
				if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
				{
					break;
				}
				position.DefineValues(position.Row + 1, position.Column + 1);
			}

			//South-West
			position.DefineValues(Position.Row + 1, Position.Column - 1);
			while (Board.IsValid(position) && AllowedToMove(position))
			{
				matrix[position.Row, position.Column] = true;
				if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
				{
					break;
				}
				position.DefineValues(position.Row + 1, position.Column - 1);
			}

			//North-West
			position.DefineValues(Position.Row - 1, Position.Column - 1);
			while (Board.IsValid(position) && AllowedToMove(position))
			{
				matrix[position.Row, position.Column] = true;
				if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
				{
					break;
				}
				position.DefineValues(position.Row - 1, position.Column - 1);
			}

			return matrix;
		}
	}
}
