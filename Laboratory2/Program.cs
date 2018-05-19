using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laboratory2 {
    class Program {
		public static bool computerFirst; //1 zaczyna komputer; 0 czlowiek

		static void Main(string[] args) {
			//Losowy start jak skoñczê heurystykê
			computerFirst = true; //true jesli komputer zaczyna (pozniej zostanie zmienione)
                                  //Console.SetWindowSize(50,20);//wysokosc i szerokosc konsoli w razie czego zmienic lub usunac


			KeyAction keyAction = new KeyAction();
			Connect4State startState = new Connect4State();

            char[] playersMark = { 'o', 'x' };
            int i = 0;
			while (true)
			{
                Console.Clear();
                if (i % 2 == 0)
                {
                    Console.Write("Punkty: " + startState.ComputeHeuristicGrade() + "\n");
                    Console.Write("KKKK: " + playersMark[i] + "\n");

                    startState.Print();
                    int choosenColumn = keyAction.getColNum();

                    startState.insertToken(choosenColumn, playersMark[i]);
                }
                else
                {
                    Connect4Search search = new Connect4Search(startState, true, 1);
                    search.DoSearch();


                    Console.Write("Length: " + search.MovesMiniMaxes.Count + '\n');
                    foreach (KeyValuePair<string, double> kvp in search.MovesMiniMaxes)
                    {
                        Console.Write(kvp.Key.Length + "<- le ");
                        Console.Write(kvp.Key + " " + kvp.Value + '\n');
                    }
			        Console.ReadKey();
                }
                i = (++i) % 2;
			}


			//cki = Console.ReadKey();
			//Console.WriteLine(cki.Key.ToString());
			//if (cki.Key.ToString() == "LeftArrow")
			//{
			//	Console.WriteLine("dupa");
			//}

			Console.ReadKey();


		}
    }
}
