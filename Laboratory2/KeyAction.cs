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
			this.position = 0;
		}

		public int getColNum()
		{
            while (true)
            {
                string spacesString = new string(' ', position * 2 + 1);

                StringBuilder pointerString = new StringBuilder();
                pointerString.Append(spacesString + "^");

                Console.Write("\r");
                Console.Write(pointerString.ToString());

                cki = Console.ReadKey();
                if (cki.Key.ToString() == "LeftArrow")
                {
                    if (this.position > 0)
                    {
                        this.position -= 1;
                    }
                    else
                    {
                        this.position = Connect4State.GRIDSIZE - 1;
                    }
                }

                if (cki.Key.ToString() == "RightArrow")
                {
                    this.position = ++this.position % Connect4State.GRIDSIZE;
                }

                if (cki.Key.ToString() == "Enter")
                {
                    break;
                }

                string cleaner = '\r' + new string(' ', 30);
                Console.Write(cleaner); // nie pytaj xD
            }
            return position;
		}
        
	}
}
