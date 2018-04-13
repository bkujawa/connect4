using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laboratory2 {
    class Program {
		public static bool computerFirst; //1 zaczyna komputer; 0 czlowiek

		static void Main(string[] args) {
			//Mo¿e niech zaczyna X a pozniej O (jak okulary)
			computerFirst = true;//true jesli komputer zaczyna (pozniej zostanie zmienione)
			Console.SetWindowSize(50,20);//wysokosc i szerokosc konsoli w razie czego zmienic lub usunac
			KeyAction keyAction = new KeyAction();
			Connect4State startState = new Connect4State();
			//Connect4Search searcher = new Connect4Search(startState,true,20);
			//Connect4Search searcher = new Connect4Search();
			//startState.Print();
			//keyAction.Print();
			//keyAction.UsedKey();
			//startState.Print();
			//keyAction.Print();
			while (true)
			{
				startState.Print();
				keyAction.Print();
				keyAction.UsedKey();
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
