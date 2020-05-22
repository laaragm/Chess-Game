using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.BoardLayer
{
	class BoardException : Exception
	{ 
		public BoardException(string message) : base(message)
		{

		}
	}
}
