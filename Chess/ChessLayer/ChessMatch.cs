using System;
using System.Collections.Generic;
using Chess.BoardLayer;
using System.Text;
using Chess.Board;

namespace Chess
{
	class ChessMatch
	{
		public ChessBoard Board { get; private set; }
		private int Turn;
		private Color ActualPlayer;
		public bool EndedMatch { get; private set; }

		public ChessMatch()
		{
			Board = new ChessBoard(8, 8);
			Turn = 1;
			ActualPlayer = Color.White;
			GetBoardReady();
		}

		private void GetBoardReady()
		{
			Board.AddPiece(new Rook(Board, Color.White), new ChessPosition('c', 1).ToPosition());
			Board.AddPiece(new Rook(Board, Color.White), new ChessPosition('c', 2).ToPosition());
			Board.AddPiece(new Rook(Board, Color.White), new ChessPosition('d', 2).ToPosition());
			Board.AddPiece(new Rook(Board, Color.White), new ChessPosition('e', 2).ToPosition());
			Board.AddPiece(new Rook(Board, Color.White), new ChessPosition('e', 1).ToPosition());
			Board.AddPiece(new King(Board, Color.White), new ChessPosition('d', 1).ToPosition());

			Board.AddPiece(new Rook(Board, Color.Black), new ChessPosition('c', 7).ToPosition());
			Board.AddPiece(new Rook(Board, Color.Black), new ChessPosition('c', 8).ToPosition());
			Board.AddPiece(new Rook(Board, Color.Black), new ChessPosition('d', 7).ToPosition());
			Board.AddPiece(new Rook(Board, Color.Black), new ChessPosition('e', 7).ToPosition());
			Board.AddPiece(new Rook(Board, Color.Black), new ChessPosition('e', 8).ToPosition());
			Board.AddPiece(new Rook(Board, Color.Black), new ChessPosition('d', 8).ToPosition());
		}

		public void ExecuteMovement(Position origin, Position destination)
		{
			Piece piece = Board.RemovePiece(origin);
			piece.IncreaseMovementCount();
			Piece capturedPiece = Board.RemovePiece(destination);
			Board.AddPiece(piece, destination);
		}
	}
}
