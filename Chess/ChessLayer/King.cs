using Chess.Board;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
	class King : Piece
	{
		public King(ChessBoard board, Color color) : base(board, color)
		{

		}

		public override string ToString()
		{
			return "K";
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

			//North
			position.DefineValues(Position.Row - 1, Position.Column);
			if (Board.IsValid(position) && AllowedToMove(position)){
				matrix[position.Row, position.Column] = true;
			}

			//North-East
			position.DefineValues(Position.Row - 1, Position.Column + 1);
			if (Board.IsValid(position) && AllowedToMove(position)){
				matrix[position.Row, position.Column] = true;
			}

			//East
			position.DefineValues(Position.Row, Position.Column + 1);
			if (Board.IsValid(position) && AllowedToMove(position))
			{
				matrix[position.Row, position.Column] = true;
			}

			//South-East
			position.DefineValues(Position.Row + 1, Position.Column + 1);
			if (Board.IsValid(position) && AllowedToMove(position))
			{
				matrix[position.Row, position.Column] = true;
			}

			//South
			position.DefineValues(Position.Row + 1, Position.Column);
			if (Board.IsValid(position) && AllowedToMove(position))
			{
				matrix[position.Row, position.Column] = true;
			}

			//South-West
			position.DefineValues(Position.Row + 1, Position.Column - 1);
			if (Board.IsValid(position) && AllowedToMove(position))
			{
				matrix[position.Row, position.Column] = true;
			}

			//West
			position.DefineValues(Position.Row, Position.Column - 1);
			if (Board.IsValid(position) && AllowedToMove(position))
			{
				matrix[position.Row, position.Column] = true;
			}

			//North-West
			position.DefineValues(Position.Row - 1, Position.Column - 1);
			if (Board.IsValid(position) && AllowedToMove(position))
			{
				matrix[position.Row, position.Column] = true;
			}

			return matrix;
		}

	}
}
