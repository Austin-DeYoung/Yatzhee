using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatzhee
{
	class Scorecard
	{

		#region Variables

		// Variables
		private int duplicateCheck, counter, total, upperScore = 0, lowerScore = 0;

		List<int> diceValues = new List<int>();
		List<int> sortedDice = new List<int>();
		List<int> duplicateDice = new List<int>();

        List<Die> dice = new List<Die>();
		Die die1 = new Die(6);
		Die die2 = new Die(6);
		Die die3 = new Die(6);
		Die die4 = new Die(6);
		Die die5 = new Die(6);



		#endregion


		#region Properties

		// Properties

        // Allows program to get data for the individual dice
		public List<Die> Dice
		{
            get { return dice; }
		}

        // Calculates and gets scores for each choice
		public int OneScore
		{
			get { return CalcIndividual(1); }
		}

		public int TwoScore
		{
			get { return CalcIndividual(2); }
		}

		public int ThreeScore
		{
			get { return CalcIndividual(3); }
		}

		public int FourScore
		{
			get { return CalcIndividual(4); }
		}

		public int FiveScore
		{
			get { return CalcIndividual(5); }
		}

		public int SixScore
		{
			get { return CalcIndividual(6); }
		}

		public int ThreeOfAKind
		{
			get { return CalcOfAKind(3); }
		}

		public int FourOfAKind
		{
			get { return CalcOfAKind(4); }
		}

		public int FullHouse
		{
			get { return CalcFullHouse(); }
		}

		public int SmallStraight
		{
			get { return CalcStraights("small"); }
		}

		public int LargeStraight
		{
			get { return CalcStraights("large"); }
		}

		public int Yahtzee
		{
			get { return CalcYatzhee(); }
		}

		public int Chance
		{
			get { return CalcChance(); }
		}

        // Calculates and gets total scores
		public int UpperScore
		{
			get
			{
				if (upperScore >= 63)
				{
					return upperScore + 35;
				}
				return upperScore;
			}
		}

		public int LowerScore
		{
			get { return lowerScore; }
		}

		public int TotalScore
		{
			get
			{
				if (upperScore >= 63)
				{
					return upperScore + 35 + lowerScore;
				}
				return upperScore + lowerScore;
			}
		}

		#endregion


		#region HelperMethods

		// Helper Methods

        // Is called once at the beginning, creates the list of dice
		public void SetupGame()
		{
			dice.Add(die1);
			dice.Add(die2);
			dice.Add(die3);
			dice.Add(die4);
			dice.Add(die5);

		}

        // Method to sort dice to check if they meet criteria
		private List<int> SortDiceValues()
		{
			diceValues.Clear();
			foreach (Die die in dice)
			{
				diceValues.Add(die.CurrentValue);
			}
			diceValues.Sort();
			return diceValues;
		}

		#endregion


		#region Calculate Methods
		// Calculate Methods

		// Calculates scores for: Ones, Twos, Threes, Fours, Fives, Sixes
		private int CalcIndividual(int number)
		{
			total = 0;
			foreach (Die die in dice)
			{
				if (die.CurrentValue == number)
				{
					total += number;
				}
			}

			upperScore += total;
			return total;
		}

		// Calculates scores for: Three of a kind, Four of a kind
		private int CalcOfAKind(int number)
		{
			total = 0;
			sortedDice = SortDiceValues();
			
			switch (number)
			{
				// Three of a Kind
				case 3:
					if (sortedDice[0] == sortedDice[1] && sortedDice[1] == sortedDice[2] ||
						sortedDice[1] == sortedDice[2] && sortedDice[2] == sortedDice[3] ||
						sortedDice[2] == sortedDice[3] && sortedDice[3] == sortedDice[4])
					{
						foreach (Die die in dice)
						{
							total += die.CurrentValue;
						}
					}
					break;

				//Four of a Kind
				case 4:
					if (sortedDice[0] == sortedDice[1] && sortedDice[1] == sortedDice[2] && sortedDice[2] == sortedDice[3] ||
						sortedDice[1] == sortedDice[2] && sortedDice[2] == sortedDice[3] && sortedDice[3] == sortedDice[4])
					{
						foreach (Die die in dice)
						{
							total += die.CurrentValue;
						}
					}
					break;

				default:
					total = -1;
					break;
			}

			lowerScore += total;
			return total;

		}

		// Calculates scores for: Full House
		private int CalcFullHouse()
		{
			total = 0;
			sortedDice = SortDiceValues();

                // Three of a kind followed by a two of a kind
            if (sortedDice[0] == sortedDice[1] && sortedDice[1] == sortedDice[2] && sortedDice[3] == sortedDice[4] ||
                // Two of a kind followed by a three of a kind
                sortedDice[0] == sortedDice[1] && sortedDice[2] == sortedDice[3] && sortedDice[3] == sortedDice[4])
			{
				total = 25;
			}

			lowerScore += total;
			return total;
		}

		// Calculates scores for: Small Straight, Large Straight
		private int CalcStraights(string size)
		{
			duplicateCheck = 0;
			counter = 0;
			total = 0;
			sortedDice = SortDiceValues();
            foreach (int die in sortedDice)
            {

                if (die == duplicateCheck)
                {
                    sortedDice.Remove(die);
                    sortedDice.Add(die);
                    break;
                }
                duplicateCheck = die;
                counter++;
            }

            switch (size)
			{
				// Small Straight
				case "small":
					if (sortedDice[0] == 1 && sortedDice[1] == 2 && sortedDice[2] == 3 && sortedDice[3] == 4 ||
						sortedDice[0] == 2 && sortedDice[1] == 3 && sortedDice[2] == 4 && sortedDice[3] == 5 ||
						sortedDice[0] == 3 && sortedDice[1] == 4 && sortedDice[2] == 5 && sortedDice[3] == 6 ||
						sortedDice[1] == 1 && sortedDice[2] == 2 && sortedDice[3] == 3 && sortedDice[4] == 4 ||
						sortedDice[1] == 2 && sortedDice[2] == 3 && sortedDice[3] == 4 && sortedDice[4] == 5 ||
						sortedDice[1] == 3 && sortedDice[2] == 4 && sortedDice[3] == 5 && sortedDice[4] == 6)
					{
						total = 30;
					}
					break;

				// Large Straight
				case "large":
					if (sortedDice[0] == 1 && sortedDice[1] == 2 && sortedDice[2] == 3 && sortedDice[3] == 4 && sortedDice[4] == 5 ||
						sortedDice[0] == 2 && sortedDice[1] == 3 && sortedDice[2] == 4 && sortedDice[3] == 5 && sortedDice[4] == 6)
					{
						total = 40;
					}
					break;

				default:
					total = -1;
					break;
			}

			lowerScore += total;
			return total;
		}

        // Calculates Yatzhee
		private int CalcYatzhee()
		{
			total = 0;
			sortedDice = SortDiceValues();

			if (die1.CurrentValue == die2.CurrentValue && die2.CurrentValue == die3.CurrentValue &&
				die3.CurrentValue == die4.CurrentValue && die4.CurrentValue == die5.CurrentValue)
			{
				total = 50;
			}

			lowerScore += total;
			return total;
		}

        // Calculates Chance
		private int CalcChance()
		{
			total = 0;

			foreach (Die die in dice)
			{
				total += die.CurrentValue;
			}

			lowerScore += total;
			return total;
		}

		#endregion

	}
}
