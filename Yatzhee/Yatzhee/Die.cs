using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatzhee
{
	class Die
	{
		#region Variables
		private int numberOfSides, currentValue;
		private bool hold = false;
		static Random rnd = new Random();

		#endregion

		#region Constructors
		public Die(int side = 6)
		{
			numberOfSides = side;
		}

		#endregion

		#region Properties
		public int NumberOfSides
		{
			get { return numberOfSides; }
			set { numberOfSides = value; }
		}


		public bool Hold
		{
			get { return hold; }
			set { hold = value; }
		}

		public int CurrentValue
		{
			get { return currentValue; }
		}

		#endregion

		#region Methods
		public void RollDie()
		{

			if (hold == false)
			{
				currentValue = rnd.Next(1, numberOfSides + 1);
			}

		}

        
		#endregion

	}
}
