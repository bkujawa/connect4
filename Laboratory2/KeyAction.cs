using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laboratory2
{
	class KeyAction 
	{
		private int position;
		private ConsoleKeyInfo cki;

		public KeyAction()
		{
			this.position = 1;
		}
		public void UsedKey()
		{
			cki = Console.ReadKey();
			if (cki.Key.ToString() == "LeftArrow")
			{
				if (this.position > 1)
				{
					this.position -= 1;
				}
			}
			if (cki.Key.ToString() == "RightArrow")
			{
				if (this.position < Connect4State.gridColumns)
				{
					this.position += 1;
				}
			}
			if (cki.Key.ToString() == "Enter")
			{
				if (Connect4State.table[0,(this.position-1)]=='0') {
					int flaga= Connect4State.gridRows;
					bool end = false;
					do
					{
						flaga--;
						if (Connect4State.table[flaga, (this.position - 1)] == '0')
						{
							Connect4State.table[flaga, (this.position - 1)] = 'X';
							end = true;
						}
					} while (Connect4State.table[flaga, (this.position - 1)] != '0' && end==false);
					
				}
			}
		}
		public void Print()
		{
			StringBuilder builder = new StringBuilder();
			for (int i = 1; i < (this.position * 2); i++)
			{
				builder.Append(" ");
			}
			builder.Append("X");
			Console.Write(builder.ToString());
		}
	}
}
