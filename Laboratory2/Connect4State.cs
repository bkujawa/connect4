using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laboratory2
{
	public class Connect4State : State
	{

		private string id;
		public override string ID {
			get { return this.id; }
		}

		public override double ComputeHeuristicGrade()
		{
			throw new NotImplementedException();
		}
		//tu brakuje konstruktora ktory tworzy pusta plansze
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
	}
}
