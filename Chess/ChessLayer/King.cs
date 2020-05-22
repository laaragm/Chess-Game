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
	}
}
