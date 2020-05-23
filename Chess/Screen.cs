﻿using Chess.Board;
using System;
using System.Collections.Generic;
using System.Text;
using Chess;

namespace Chess
{
	class Screen
	{
		public static void PrintBoard(ChessBoard board)
		{
			for (int i = 0; i < board.Rows; i++)
			{
				Console.Write(8 - i + " ");
				for (int j = 0; j < board.Columns; j++)
				{
					PrintPiece(board.Piece(i, j));
				}
				Console.WriteLine();
			}
			Console.WriteLine("  a b c d e f g h");
		}

		public static void PrintBoard(ChessBoard board, bool[,] possiblePositions)
		{
			ConsoleColor originalBackground = Console.BackgroundColor;
			ConsoleColor changedBackground = ConsoleColor.DarkGray;


			for (int i = 0; i < board.Rows; i++)
			{
				Console.Write(8 - i + " ");
				for (int j = 0; j < board.Columns; j++)
				{
					if (possiblePositions[i, j])
					{
						Console.BackgroundColor = changedBackground;
					}
					else
					{
						Console.BackgroundColor = originalBackground;
					}
					PrintPiece(board.Piece(i, j));
					Console.BackgroundColor = originalBackground;
				}
				Console.WriteLine();
			}
			Console.WriteLine("  a b c d e f g h");
		}

		public static ChessPosition ReadChessPosition()
		{
			string s = Console.ReadLine();
			char column = s[0];
			int row = int.Parse(s[1] + "");

			return new ChessPosition(column, row);
		}

		public static void PrintPiece(Piece piece)
		{
			if (piece == null)
			{
				Console.Write("- ");
			}
			else
			{
				if (piece.Color == Color.White)
				{
					Console.Write(piece);
				}
				else
				{
					ConsoleColor aux = Console.ForegroundColor; //get the console's actual color 
					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.Write(piece);
					Console.ForegroundColor = aux;
				}
				Console.Write(" ");
			}
		}
	}
}
