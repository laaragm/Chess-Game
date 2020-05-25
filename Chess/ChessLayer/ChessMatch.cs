using System;
using System.Collections.Generic;
using Chess.BoardLayer;
using System.Text;
using System.Collections.Generic;
using Chess.Board;
using Chess.ChessLayer;

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
		public Piece VulnerableToEnPassant { get; private set; }

		public ChessMatch()
		{
			Board = new ChessBoard(8, 8);
			Turn = 1;
			ActualPlayer = Color.White;
			Pieces = new HashSet<Piece>();
			Captured = new HashSet<Piece>();
			EndedMatch = false;
			Check = false;
			VulnerableToEnPassant = null;

			GetBoardReady();
		}

		public void AddNewPiece(char column, int row, Piece piece)
		{
			Board.AddPiece(piece, new ChessPosition(column, row).ToPosition());
			Pieces.Add(piece);
		}

		private void GetBoardReady()
		{
			AddNewPiece('a', 1, new Rook(Board, Color.White));
			AddNewPiece('b', 1, new Knight(Board, Color.White));
			AddNewPiece('c', 1, new Bishop(Board, Color.White));
			AddNewPiece('d', 1, new Queen(Board, Color.White));
			AddNewPiece('e', 1, new King(Board, Color.White, this));
			AddNewPiece('f', 1, new Bishop(Board, Color.White));
			AddNewPiece('g', 1, new Knight(Board, Color.White));
			AddNewPiece('h', 1, new Rook(Board, Color.White));
			AddNewPiece('a', 2, new Pawn(Board, Color.White, this));
			AddNewPiece('b', 2, new Pawn(Board, Color.White, this));
			AddNewPiece('c', 2, new Pawn(Board, Color.White, this));
			AddNewPiece('d', 2, new Pawn(Board, Color.White, this));
			AddNewPiece('e', 2, new Pawn(Board, Color.White, this));
			AddNewPiece('f', 2, new Pawn(Board, Color.White, this));
			AddNewPiece('g', 2, new Pawn(Board, Color.White, this));
			AddNewPiece('h', 2, new Pawn(Board, Color.White, this));


			AddNewPiece('a', 8, new Rook(Board, Color.Black));
			AddNewPiece('b', 8, new Knight(Board, Color.Black));
			AddNewPiece('c', 8, new Bishop(Board, Color.Black));
			AddNewPiece('d', 8, new Queen(Board, Color.Black));
			AddNewPiece('e', 8, new King(Board, Color.Black, this));
			AddNewPiece('f', 8, new Bishop(Board, Color.Black));
			AddNewPiece('g', 8, new Knight(Board, Color.Black));
			AddNewPiece('h', 8, new Rook(Board, Color.Black));
			AddNewPiece('a', 7, new Pawn(Board, Color.Black, this));
			AddNewPiece('b', 7, new Pawn(Board, Color.Black, this));
			AddNewPiece('c', 7, new Pawn(Board, Color.Black, this));
			AddNewPiece('d', 7, new Pawn(Board, Color.Black, this));
			AddNewPiece('e', 7, new Pawn(Board, Color.Black, this));
			AddNewPiece('f', 7, new Pawn(Board, Color.Black, this));
			AddNewPiece('g', 7, new Pawn(Board, Color.Black, this));
			AddNewPiece('h', 7, new Pawn(Board, Color.Black, this));
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

			//Special move: Castling
			if ((piece is King) && (destination.Column == origin.Column + 2)){
				Position originR = new Position(origin.Row, origin.Column + 3);
				Position destinationR = new Position(origin.Row, origin.Column + 1);
				Piece R = Board.RemovePiece(originR);
				R.IncreaseMovementCount();
				Board.AddPiece(R, destinationR);
			}

			if ((piece is King) && (destination.Column == origin.Column - 2))
			{
				Position originR = new Position(origin.Row, origin.Column - 4);
				Position destinationR = new Position(origin.Row, origin.Column - 1);
				Piece R = Board.RemovePiece(originR);
				R.IncreaseMovementCount();
				Board.AddPiece(R, destinationR);
			}

			//Special move: En Passant
			if (piece is Pawn)
			{
				if ((origin.Column != destination.Column) && (capturedPiece == null)){
					Position positionP;
					if (piece.Color == Color.White)
					{
						positionP = new Position(destination.Row + 1, destination.Column);
					}
					else
					{
						positionP = new Position(destination.Row - 1, destination.Column);
					}
					capturedPiece = Board.RemovePiece(positionP);
					Captured.Add(capturedPiece);
				}
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

			Piece piece = Board.Piece(destination);

			// Special move: Promotion
			if (piece is Pawn)
			{
				if ((piece.Color == Color.White && destination.Row == 0)  || (piece.Color == Color.Black && destination.Row == 7))
				{
					piece = Board.RemovePiece(destination);
					Pieces.Remove(piece);
					Piece queen = new Queen(Board, piece.Color);
					Board.AddPiece(queen, destination);
					Pieces.Add(queen);
				}
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

			//Special move: En Passant
			if (piece is Pawn && (destination.Row == origin.Row - 2 || destination.Row == origin.Row + 2))
			{
				VulnerableToEnPassant = piece;
			}
			else
			{
				VulnerableToEnPassant = null;
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

			//Special move: Castling
			if ((piece is King) && (destination.Column == origin.Column + 2))
			{
				Position originR = new Position(origin.Row, origin.Column + 3);
				Position destinationR = new Position(origin.Row, origin.Column + 1);
				Piece R = Board.RemovePiece(destinationR);
				R.DecreaseMovementCount();
				Board.AddPiece(R, originR);
			}

			if ((piece is King) && (destination.Column == origin.Column - 2))
			{
				Position originR = new Position(origin.Row, origin.Column - 4);
				Position destinationR = new Position(origin.Row, origin.Column - 1);
				Piece R = Board.RemovePiece(destinationR);
				R.DecreaseMovementCount();
				Board.AddPiece(R, originR);
			}

			//Special move: En Passant
			if (piece is Pawn)
			{
				if ((origin.Column != destination.Column) && (capturedPiece == VulnerableToEnPassant))
				{
					Piece pawn = Board.RemovePiece(destination);
					Position positionP;
					if (piece.Color == Color.White)
					{
						positionP = new Position(3, destination.Column);
					}
					else
					{
						positionP = new Position(4, destination.Column);
					}
					Board.AddPiece(pawn, positionP);
				}
			}
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

		private Piece KingPiece(Color color)
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
			Piece king = KingPiece(color);
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
