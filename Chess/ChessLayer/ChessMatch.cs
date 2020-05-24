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
		public bool Check { get; private set; }

		public ChessMatch()
		{
			Board = new ChessBoard(8, 8);
			Turn = 1;
			ActualPlayer = Color.White;
			Pieces = new HashSet<Piece>();
			Captured = new HashSet<Piece>();
			EndedMatch = false;
			Check = false;

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

		public Piece ExecuteMovement(Position origin, Position destination)
		{
			Piece piece = Board.RemovePiece(origin);
			piece.IncreaseMovementCount();
			Piece capturedPiece = Board.RemovePiece(destination);
			Board.AddPiece(piece, destination);
			if (capturedPiece != null)
			{
				Captured.Add(capturedPiece);
			}

			return capturedPiece;
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
			Piece capturedPiece = ExecuteMovement(origin, destination);

			if (IsInCheck(ActualPlayer))
			{
				UndoMovement(origin, destination, capturedPiece);
				throw new BoardException("You cannot put your own king in check.");
			}

			if (IsInCheck(AdversaryColor(ActualPlayer))){
				Check = true;
			}
			else
			{
				Check = false;
			}

			if (TestCheckMate(AdversaryColor(ActualPlayer)))
			{
				EndedMatch = true;
			}
			else
			{
				Turn++;
				ChangePlayer();
			}
		}

		private void UndoMovement(Position origin, Position destination, Piece capturedPiece)
		{
			Piece piece = Board.RemovePiece(destination);
			piece.DecreaseMovementCount();
			if (capturedPiece != null)
			{
				Board.AddPiece(capturedPiece, destination);
				Captured.Remove(capturedPiece);
			}
			Board.AddPiece(piece, origin);
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

		private Piece King(Color color)
		{
			foreach (Piece piece in PiecesInPlay(color))
			{
				if (piece is King)
				{
					return piece;
				}
			}

			return null;
		}

		//A check is a condition in chess that occurs when a player's king is under threat of capture 
		//on their opponent's next turn. 
		//The method below tests whether a king is in check.
		public bool IsInCheck(Color color)
		{
			Piece king = King(color);
			if(king == null) //this is not supposed to happen
			{
				throw new BoardException("There is no " + color + " king playing.");
			}
			foreach (Piece piece in PiecesInPlay(AdversaryColor(color)))
			{
				bool[,] matrix = piece.PossibleMovements();
				if(matrix[king.Position.Row, king.Position.Column])
				{
					return true;
				}
			}

			return false;
		}

		//Checkmate is a game position in chess and other chess-like games in which a player's king is 
		//in check (threatened with capture) and there is no way to remove the threat.
		public bool TestCheckMate(Color color)
		{
			if (!IsInCheck(color))
			{
				return false;
			}

			foreach (Piece piece in PiecesInPlay(color))
			{
				bool[,] matrix = piece.PossibleMovements();
				for (int i = 0; i < Board.Rows; i++)
				{
					for (int j = 0; j < Board.Columns; j++)
					{
						if (matrix[i, j])
						{
							Position origin = piece.Position;
							Position destination = new Position(i, j);
							Piece capturedPiece = ExecuteMovement(origin, destination);
							bool checkTest = IsInCheck(color);
							UndoMovement(origin, destination, capturedPiece);
							if (!checkTest)
							{
								return false;
							}
						}
					}
				}
			}

			return true;
		}

		private Color AdversaryColor(Color color)
		{
			if (color == Color.White)
			{
				return Color.Black;
			}
			else
			{
				return Color.White;
			}
		}
	}
}
