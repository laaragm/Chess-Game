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
	}
}
