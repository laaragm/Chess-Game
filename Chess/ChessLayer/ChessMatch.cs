using System;
using System.Collections.Generic;
using Chess.BoardLayer;
using System.Text;
using System.Collections.Generic;
using Chess.Board;

namespace Chess
{
	class ChessMatch
	{
		public ChessBoard Board { get; private set; }
		public int Turn { get; private set; }
		public Color ActualPlayer { get; private set; }
		public bool EndedMatch { get; private set; }
		private HashSet<Piece> Pieces; //in order to store all the pieces from the match
		private HashSet<Piece> Captured; 

		public ChessMatch()
		{
			Board = new ChessBoard(8, 8);
			Turn = 1;
			ActualPlayer = Color.White;
			Pieces = new HashSet<Piece>();
			Captured = new HashSet<Piece>();

			GetBoardReady();
		}

		public void AddNewPiece(char column, int row, Piece piece)
		{
			Board.AddPiece(piece, new ChessPosition(column, row).ToPosition());
			Pieces.Add(piece);
		}

		private void GetBoardReady()
		{
			AddNewPiece('c', 1, new Rook(Board, Color.White));
			AddNewPiece('c', 2, new Rook(Board, Color.White));
			AddNewPiece('d', 2, new Rook(Board, Color.White));
			AddNewPiece('e', 2, new Rook(Board, Color.White));
			AddNewPiece('e', 1, new Rook(Board, Color.White));
			AddNewPiece('d', 1, new King(Board, Color.White));

			AddNewPiece('c', 7, new Rook(Board, Color.Black));
			AddNewPiece('c', 8, new Rook(Board, Color.Black));
			AddNewPiece('d', 7, new Rook(Board, Color.Black));
			AddNewPiece('e', 7, new Rook(Board, Color.Black));
			AddNewPiece('e', 8, new Rook(Board, Color.Black));
			AddNewPiece('d', 8, new King(Board, Color.Black));
		}

		public void ExecuteMovement(Position origin, Position destination)
		{
			Piece piece = Board.RemovePiece(origin);
			piece.IncreaseMovementCount();
			Piece capturedPiece = Board.RemovePiece(destination);
			Board.AddPiece(piece, destination);
			if (capturedPiece != null)
			{
				Captured.Add(capturedPiece);
			}
		}

		public HashSet<Piece> CapturedPieces(Color color)
		{
			HashSet<Piece> aux = new HashSet<Piece>();
			foreach (Piece piece in Captured)
			{
				if (piece.Color == color)
				{
					aux.Add(piece);
				}
			}

			return aux;
		}

		public HashSet<Piece> PiecesInPlay(Color color)
		{
			HashSet<Piece> aux = new HashSet<Piece>();
			foreach (Piece piece in Pieces)
			{
				if (piece.Color == color)
				{
					aux.Add(piece);
				}
			}
			aux.ExceptWith(CapturedPieces(color));

			return aux;
		}

		public void ChangePlayer()
		{
			if (ActualPlayer == Color.White)
			{
				ActualPlayer = Color.Black;
			}
			else
			{
				ActualPlayer = Color.White;
			}
		}

		public void PerformMovement(Position origin, Position destination)
		{
			ExecuteMovement(origin, destination);
			Turn++;
			ChangePlayer();
		}

		public void ValidateOriginPosition(Position position)
		{
			if (Board.Piece(position) == null)
			{
				throw new BoardException("There is no piece in chosen origin.");
			}
			if (ActualPlayer != Board.Piece(position).Color)
			{
				throw new BoardException("Chosen piece is not yours.");
			}
			if (!Board.Piece(position).IsThereAPossibleMovement())
			{
				throw new BoardException("There are no possible movements for chosen piece.");
			}
		}

		public void ValidateDestinationPosition(Position origin, Position destination)
		{
			if (!Board.Piece(origin).IsAllowedMoveTo(destination))
			{
				throw new BoardException("Invalid destination position.");
			}
		}
	}
}
