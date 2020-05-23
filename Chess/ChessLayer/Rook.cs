using Chess.Board;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
	class Rook : Piece
	{
		public Rook(ChessBoard board, Color color) : base(board, color)
		{

		}

		public override string ToString()
		{
			return "R";
		}

		private bool AllowedToMove(Position position)
		{
			Piece piece = Board.Piece(position);
			return piece == null || piece.Color != Color;
		}

		public override bool[,] PossibleMovements()
		{
			bool[,] matrix = new bool[Board.Rows, Board.Columns];

			Position position = new Position(0, 0);

			//North
			position.DefineValues(Position.Row - 1, Position.Column);
			while (Board.IsValid(position) && AllowedToMove(position))
			{
				matrix[position.Row, position.Column] = true;
				if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
				{
					break;
				}
				position.Row--;
			}

			//South
			position.DefineValues(Position.Row + 1, Position.Column);
			while (Board.IsValid(position) && AllowedToMove(position))
			{
				matrix[position.Row, position.Column] = true;
				if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
				{
					break;
				}
				position.Row++;
			}

			//East
			position.DefineValues(Position.Row, Position.Column + 1);
			while (Board.IsValid(position) && AllowedToMove(position))
			{
				matrix[position.Row, position.Column] = true;
				if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
				{
					break;
				}
				position.Column++;
			}

			//West
			position.DefineValues(Position.Row, Position.Column - 1);
			while (Board.IsValid(position) && AllowedToMove(position))
			{
				matrix[position.Row, position.Column] = true;
				if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
				{
					break;
				}
				position.Column--;
			}

			return matrix;
		}


	}
}
