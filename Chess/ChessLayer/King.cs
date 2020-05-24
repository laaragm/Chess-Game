using Chess.Board;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
	class King : Piece
	{
		private ChessMatch Match;
		public King(ChessBoard Board, Color Color, ChessMatch match) : base(Board, Color)
		{
			Match = match;
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

		private bool TestRookToCastling(Position position)
		{
			Piece piece = Board.Piece(position);
			return (piece != null) && (piece is Rook) && (piece.Color == Color) && (piece.MovementsCount == 0);
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

			//Special move: Castling
			//When there is an open space between your king and rook, then you can do the castling move and you 
			//do it by moving your king two squares in the direction of the rook, and then the rook jumps over him.
			if ((MovementsCount == 0) && (!Match.Check))
			{
				Position positionR1 = new Position(Position.Row, Position.Column + 3);
				if (TestRookToCastling(positionR1))
				{
					Position p1 = new Position(Position.Row, Position.Column + 1);
					Position p2 = new Position(Position.Row, Position.Column + 2);
					if ((Board.Piece(p1) == null) && (Board.Piece(p2) == null))
					{
						matrix[Position.Row, Position.Column + 2] = true;
					}
				}

				Position positionR2 = new Position(Position.Row, Position.Column - 4);
				if (TestRookToCastling(positionR2))
				{
					Position p1 = new Position(Position.Row, Position.Column - 1);
					Position p2 = new Position(Position.Row, Position.Column - 2);
					Position p3 = new Position(Position.Row, Position.Column - 3);
					if ((Board.Piece(p1) == null) && (Board.Piece(p2) == null) && (Board.Piece(p3) == null))
					{
						matrix[Position.Row, Position.Column - 2] = true;
					}
				}
			}

			return matrix;
		}

	}
}
