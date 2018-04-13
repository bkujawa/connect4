using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laboratory2
{
	public class Connect4State : State
	{
        //private static int gridRows = 6;
        //private static int gridColumns = 7;
        public const int GRIDSIZE = 8;
		private string id;

		public char [,] table;// czy to moze byc static? Uzyje aby przechwycic w KeyAction.UsedKey()
		public char [,] Table {
			get { return table; }
			set { table = value; }
		}

		public override string ID {
			get { return this.id; }
		}

		public Connect4State()
		{
			table = new char[GRIDSIZE, GRIDSIZE];
			for (int i = 0; i < GRIDSIZE; i++)
			{
				for (int j = 0; j < GRIDSIZE; j++)
				{
					table[i, j] = '0';
				}
			}

            this.id = IDMaker();
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

        public void insertToken(int column, char c)
        {
            if (column >= GRIDSIZE)
            {
                throw new Exception("Nie ma takiej kolumny (C4State::insertToken)");
            }
            else if (Table[0, column] != '0')
            {
                throw new Exception("Kolum przepełniona (C4State::insertToken)");
            }
            else
            {
                for (int row = 0; row < GRIDSIZE; row++)
                {
                    if (Table[row, column] != '0')
                    {
                        Table[row - 1, column] = c;

                        Console.Write(row);
                        break;
                    }
                    else if (row + 1 == GRIDSIZE)
                    {
                        Table[row, column] = c;
                        break;
                    }
                }
            }
        }

        private double getPoints(char c, int row, int col)
        {
            int[,] dirTable = new int[4, 3];

            // dirTable [,0] - przesunięcie w kolumnie; [,1] - przesunięcie w wierszu; [,2] - kierunek dozwolony

            // HORIZONTAL //
            dirTable[0, 0] = 0;
            dirTable[0, 1] = 1;
            dirTable[0, 2] = 1;
            if (col > 0 && Table[row, col - 1] == c)
            {
                dirTable[0, 2] = 0;
            }
            else if (col >= GRIDSIZE - 1)
            {
                dirTable[0, 2] = 0;
            }

            // VERTICAL //
            dirTable[1, 0] = 1;
            dirTable[1, 1] = 0;
            dirTable[1, 2] = 1;
            if (row > 0 && Table[row - 1, col] == c)
            {
                dirTable[1, 2] = 0;
            }
            else if (row >= GRIDSIZE - 1)
            {
                dirTable[1, 2] = 0;
            }

            // R-Diagonally 
            dirTable[2, 0] = 1;
            dirTable[2, 1] = 1;
            dirTable[2, 2] = 1;
            if (row > 0 && col > 0 && Table[row - 1, col - 1] == c)
            {
                dirTable[2, 2] = 0;
            }
            else if (row >= GRIDSIZE - 1 || col >= GRIDSIZE - 1)
            {
                dirTable[2, 2] = 0;
            }

            // L-Diag
            dirTable[3, 0] = 1;
            dirTable[3, 1] = -1;
            dirTable[3, 2] = 1;
            if (row > 0 && col < GRIDSIZE - 1 && Table[row - 1, col + 1] == c)
            {
                dirTable[3, 2] = 0;
            }
            else if (row >= GRIDSIZE - 1 || col < 1)
            {
                dirTable[3, 2] = 0;
            }


            double[] pTable = { 1, 4, 16, -1000 };
            double points = 0;

            for (int i = 0; i < 4; i++) // DIR loop 
            {
                if (dirTable[i, 2] == 1) // DIR condition field
                {
                    int deep = 0; // ilość tych samych znaków obok siebie
                    for (int j = 1; j < 4; j++) 
                    {
                        int cRow = row - dirTable[i, 0] * j;
                        int cCol = col + dirTable[i, 1] * j;

                        if (cRow > 0 && cCol < GRIDSIZE - 1 && Table[cRow, cCol] == c)
                        {
                            deep++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    points += pTable[deep];
                }
            }
            
            return points;
        }

        public override double ComputeHeuristicGrade()
        {
            //    max  min
            //      x  o
            // 1 =  1 -1
            // 2 =  4 -4
            // 3 = 16 -16
            // 4 = ~~ -~~ (infinity)

            double min = 0, max = 0;
            char minChar = 'o', maxChar = 'x';

            for (int i = 0; i < GRIDSIZE; i++)
            {
                for (int j = 0; j < GRIDSIZE; j++)
                {
                    if (Table[i, j] == maxChar)
                    {
                        max += getPoints(maxChar, i, j);
                    }
                    else if (Table[i, j] == minChar)
                    {
                        min -= getPoints(minChar, i, j);
                    }
                }
            }
            double h = max + min;

            return h;
        }

		public void Print() {
            /*
             * ╔═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╗ 
             * ║0,0║0,1║0,2║0,3║0,4║0,5║0,6║0,7║
             * ╠═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╣
             * ║...║   ║   ║   ║   ║   ║   ║   ║
             * ╠═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╣
             * ║7,0║7,1║7,2║7,3║7,4║7,5║7,6║7,7║
             * ╚═══╩═══╩═══╩═══╩═══╩═══╩═══╩═══╝
             */
                
            StringBuilder builder = new StringBuilder();
			builder.Append("╔═╦═╦═╦═╦═╦═╦═╦═╗\n");

			for (int i = 0; i < GRIDSIZE; i++)
			{
				for (int j = 0; j < GRIDSIZE; j++)
				{
					builder.Append("║");

                    char field = (Table[i, j] == '0' ? ' ' : Table[i, j]);
					builder.Append(field);
                }

                builder.Append("║");
				builder.Append("\n");

				if (i < GRIDSIZE - 1)
				{
					builder.Append("╠═╬═╬═╬═╬═╬═╬═╬═╣");
					builder.Append("\n");
				}
				
			}
			builder.Append("╚═╩═╩═╩═╩═╩═╩═╩═╝");
			builder.Append("\n");
			Console.Write(builder.ToString());
        }

        private string IDMaker()
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < GRIDSIZE; i++)
            {
                for (int j = 0; j < GRIDSIZE; j++)
                {
                    builder.Append(table[i, j]);
                }
            }

            return builder.ToString();
        }
    }
}
