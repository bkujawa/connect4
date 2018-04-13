using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laboratory2
{
	public class Connect4State : State
	{
		public static int gridRows=6;
		public static int gridColumns=7;
		private string id;

		public static char [,] table;// czy to moze byc static? Uzyje aby przechwycic w KeyAction.UsedKey()
		public char [,] Table {
			get { return table; }
			set { table = value; }
		}
		public override string ID {
			get { return this.id; }
		}

		public override double ComputeHeuristicGrade()
		{
			throw new NotImplementedException();
		}
		//tu brakuje konstruktora ktory tworzy pusta plansze
		public Connect4State()
		{
			//6x7 (gridRows x gridColumns)
			for (int i = 0; i < gridRows; i++)
			{
				for (int j = 0; j < gridColumns; j++)
				{
					this.id += "0";
				}
			}
			table = new char[gridRows, gridColumns];
			for (int i = 0; i < gridRows; i++)
			{
				for (int j = 0; j < gridColumns; j++)
				{
					table[i, j] = '0';
				}
			}
		}
		public Connect4State(Connect4State parent/*, pozostale niezbedne parametry*/) : base(parent)
		{
			//reszta implementacji

			//ustawienie stringa identyfikujacego stan.
			//this.id; // = ...
					 //ustawienie na ktorym poziomie w drzewie znajduje sie stan.
			this.depth = parent.depth + 0.5;

			//Bardzo wazne nie ustawiamy na czubek drzewa z ktorego budujemy stan. Tylko na pierwsze pokolenie stanow potomnych
			if (parent.rootMove == null)
			{
				this.rootMove = this.id;
			}
			else {
				this.rootMove = parent.rootMove;
			}
			//Ustawienie stanu jako potomka rodzica
			parent.Children.Add(this);
		}

		public void Print() {
			StringBuilder builder = new StringBuilder();
			builder.Append("╔═╦═╦═╦═╦═╦═╦═╗");
			builder.Append("\n");
			for (int i = 0; i < gridRows; i++)
			{
				
				for (int j = 0; j < gridColumns; j++)
				{
					builder.Append("║");
					if (Table[i,j] == '0')
					{
						builder.Append(" ");
					}
					else
					{
						builder.Append(Table[i, j]);
					}
				}
				builder.Append("║");
				builder.Append("\n");
				if (i < gridRows - 1)
				{
					builder.Append("╠═╬═╬═╬═╬═╬═╬═╣");
					builder.Append("\n");
				}
				
			}
			builder.Append("╚═╩═╩═╩═╩═╩═╩═╝");
			builder.Append("\n");
			Console.Write(builder.ToString());
		}
	}
}
