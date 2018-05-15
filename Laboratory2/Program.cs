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
                Console.Write("Punkty: " + startState.ComputeHeuristicGrade() + "\n");
                Console.Write("KKKK: " + playersMark[i++] + "\n");
                i = i % 2;

                startState.Print();
				int choosenColumn = keyAction.getColNum();


                startState.insertToken(choosenColumn, playersMark[i]);


                Console.Clear();
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
