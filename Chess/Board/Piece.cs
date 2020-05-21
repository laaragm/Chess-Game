using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.Board
{
	class Piece
	{
		public Position Position { get; set; }
		public Color Color { get; protected set; }
		public int MovementsCount { get; protected set; }
		public ChessBoard Board { get; protected set; }

		public Piece(Position position, Color color, ChessBoard board)
		{
			Position = position;
			Color = color;
			MovementsCount = 0;
			Board = board;
		}
	}
}
