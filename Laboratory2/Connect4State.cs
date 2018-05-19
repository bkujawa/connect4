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
        private double[] pointsTable = { 0, 2, 4, 16, 1000 };

        public char[,] table;// czy to moze byc static? Uzyje aby przechwycic w KeyAction.UsedKey()
        public char[,] Table
        {
            get { return table; }
            set { table = value; }
        }

        public override string ID
        {
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

        public Connect4State(Connect4State parent, int col, char playerToken) : base(parent)
        {
            this.table = parent.table;

            //Przypisanie odpowiedniej heurystyki, 0 jeśli nie ma miejsca w kolumnie
            if (this.getEmptyHight(col) > 0)
            {
                insertToken(col, playerToken);
                this.h = ComputeHeuristicGrade();
            }
            else
            {
                this.h = 0;
            }

            //ustawienie stringa identyfikujacego stan.
            this.id = IDMaker();

            //ustawienie na ktorym poziomie w drzewie znajduje sie stan.
            this.depth = parent.depth + 1; // 0.5 (?) this.depth = parent.depth + 0.5;

            //Bardzo wazne nie ustawiamy na czubek drzewa z ktorego budujemy stan. Tylko na pierwsze pokolenie stanow potomnych
            if (parent.rootMove == null)
            {
                this.rootMove = this.id;
            }
            else
            {
                this.rootMove = parent.rootMove;
            }
            //Ustawienie stanu jako potomka rodzica
            parent.Children.Add(this);
        }
        /*public Connect4State(Connect4State state, string id) : base()
          {
            depth = parent.depth + 0.5;
            if (id == null) //Jesli algorytm nie wybral stanu, wtedy np. stworz nowy stan w pierwszym wolnym miejscu
            else
            {
                for (int i = 0; i < GRIDSIZE; ++i)
                {
                    for (int j = 0; j < GRIDSIZE; ++j)
                    {
                        table[i,j] = id[i * GRIDSIZE + j];
                    }
                }
            }
            this.h = ComputeHeuristicGrade();
          } 
        */

        public override double ComputeHeuristicGrade()
        {
            //    max  min
            //      x  o
            // 1 =  1 -1
            // 2 =  4 -4
            // 3 = 16 -16
            // 4 = ~~ -~~ (infinity)

            double minPoints = 0, maxPoints = 0;
            char minChar = 'o', maxChar = 'x';

            // Liczenie punktów dla każdej kolumny
            for (int col = 0; col < GRIDSIZE; col++)
            {
                for (int row = 0; row < GRIDSIZE; row++)
                {
                    if (Table[row, col] == minChar)
                    {
                        minPoints -= getPoints(minChar, row, col);
                    }
                    else if (Table[row, col] == maxChar)
                    {
                        maxPoints += getPoints(maxChar, row, col);
                    }
                }
            }
            double h = maxPoints + minPoints;

            return h;
        }

        private double getPoints(char c, int row, int col)
        {
            double points = 0;

            points += getColPoints(c, row, col);
            points += getRowPoints(c, row, col);
            points += getLDiagPoints(c, row, col);
            points += getRDiagPoints(c, row, col);

            return points;
        }

        public void Print()
        {
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

        public void insertToken(int column, char c)
        {
            if (column >= GRIDSIZE)
            {
                throw new Exception("Nie ma takiej kolumny (C4State::insertToken)");
            }
            else if (Table[0, column] != '0')
            {
                //throw new Exception("Kolum przepełniona (C4State::insertToken)");
            }
            else
            {
                for (int row = GRIDSIZE - 1; row >= 0; row--)
                {
                    //Console.Write("Row:" + row + " col:" + column + " Tble:" + Table[row, column] + "\n");
                    if (Table[row, column] == '0')
                    {
                        Table[row, column] = c;

                        //Console.Write(row);
                        break;
                    }
                }
                //Console.Write("Break \n");
                //Console.ReadKey();
            }
        }

        public int getEmptyHight(int colIdx)
        {
            int counter = 0;
            if (colIdx < 0 || colIdx >= GRIDSIZE)
            {
                return --counter;
            }
            else
            {
                for (int i = 0; i < GRIDSIZE; i++)
                {
                    if (Table[i, colIdx] == '0')
                    {
                        counter++;
                    }
                    else
                    {
                        break;
                    }
                } 
            }
            return counter;
        }

        private double getRowPoints(char c, int row, int col)
        {
            int counter = 1;

            int i = 1;
            for (int right = col + 1; right < GRIDSIZE && i++ < 4; right++)
            {
                if (table[row, right] == c)
                {
                    counter++;
                }
                else
                {
                    break;
                }
            }

            return pointsTable[counter];
        }
        
        private double getColPoints(char c, int row, int col)
        {
            int counter = 1;

            int i = 0;
            for (int down = row + 1; down < GRIDSIZE && ++i < 4; down++)
            {
                if (table[down, col] == c)
                {
                    counter++;
                }
                else
                {
                    break;
                }
            }

            return pointsTable[counter];
        }

        private double getRDiagPoints(char c, int row, int col)
        {
            int counter = 0;

            int i = 0;
            for (int down = row + 1; down < GRIDSIZE && ++i < 4; down++)
            {
                if (++col < GRIDSIZE && Table[down, col] == c)
                {
                    counter++;
                }
                else
                {
                    break;
                }
            }

            return pointsTable[counter];
        }

        private double getLDiagPoints(char c, int row, int col)
        {
            int counter = 0;

            int i = 0;
            for (int down = row + 1; down < GRIDSIZE && ++i < 4; down++)
            {
                if (--col > 0 && Table[down, col] == c)
                {
                    counter++;
                }
                else
                {
                    break;
                }
            }

            return pointsTable[counter];
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
