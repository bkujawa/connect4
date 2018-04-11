using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laboratory2
{
	public class Connect4Search : AlphaBetaSearcher
	{
		public Connect4Search(IState startState, bool isMaximizingPlayerFirst, int maximumDepth) : base(startState, isMaximizingPlayerFirst, maximumDepth)
		{

		}

		protected override void buildChildren(IState parent)
		{
			throw new NotImplementedException();
		}
	}
}
