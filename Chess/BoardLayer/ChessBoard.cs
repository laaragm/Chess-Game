using Chess.BoardLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.Board
{
	class ChessBoard
	{
		public int Rows { get; set; }
		public int Columns { get; set; }
		private Piece[,] Pieces;

		public ChessBoard(int rows, int columns)
		{
			Rows = rows;
			Columns = columns;
			Pieces = new Piece[rows, columns];
		}

		public Piece Piece(int row, int column)
		{
			return Pieces[row, column];
		}

		public Piece Piece(Position position)
		{
			if (!IsValid(position))
			{
				throw new BoardException("Invalid Position");
			}
			return Pieces[position.Row, position.Column];
		}

		public void AddPiece(Piece piece, Position position)
		{
			if (PieceExists(position))
			{
				throw new BoardException("This position has already been taken.");
			}
			Pieces[position.Row, position.Column] = piece;
			piece.Position = position;
		}

		public Piece RemovePiece(Position position)
		{
			if (Piece(position) == null)
			{
				return null;
			}
			Piece aux = Piece(position);
			aux.Position = null;
			Pieces[position.Row, position.Column] = null;
			return aux;
		}

		public bool PieceExists(Position position)
		{
			IsValid(position); 
			return Piece(position) != null;
		}

		public bool IsValid(Position position)
		{
			if (position.Row < 0 || position.Row >= Rows || position.Column < 0 || position.Column >= Columns)
			{
				return false;
			}
			return true;
		}

		public void ValidatePosition(Position position)
		{
			if (!IsValid(position))
			{
				throw new BoardException("Invalid Position.");
			}
		}


	}
}
