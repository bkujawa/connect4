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
            char[] playersChars = { 'o', 'x' };

            int markIdx = (int)parent.Depth;
            if (isMaximizingPlayerFirst)
            {
                markIdx++;
            }
            markIdx = markIdx % 2;

            Connect4State state = (Connect4State)parent;


            for (int i = 0; i < Connect4State.GRIDSIZE; i++)
            {

                Connect4State child = new Connect4State(state, i, playersChars[markIdx]);
                //parent.Children.Add(child);
            }
		}
        
	}
}
