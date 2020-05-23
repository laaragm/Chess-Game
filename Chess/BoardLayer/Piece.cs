﻿using System;
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
	}
}
