using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Yatzhee
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		Scorecard scorecard = new Scorecard();
		List<CheckBox> checkBoxes = new List<CheckBox>();
        List<RadioButton> radioButtons = new List<RadioButton>();
		int counter, decision, rollNumber = 0, gameTurns = 0;
		
		public MainWindow()
		{
			InitializeComponent();

			scorecard.SetupGame();

            // Added CheckBoxes and RadioButtons to lists to use them more effectively later
			checkBoxes.Add(chBoxDie1);
			checkBoxes.Add(chBoxDie2);
			checkBoxes.Add(chBoxDie3);
			checkBoxes.Add(chBoxDie4);
			checkBoxes.Add(chBoxDie5);

            radioButtons.Add(radioAces);
            radioButtons.Add(radioTwos);
            radioButtons.Add(radioThrees);
            radioButtons.Add(radioFours);
            radioButtons.Add(radioFives);
            radioButtons.Add(radioSixes);
            radioButtons.Add(radioThreeOfAKind);
            radioButtons.Add(radioFourOfAKind);
            radioButtons.Add(radioFullHouse);
            radioButtons.Add(radioSmallStraight);
            radioButtons.Add(radioLargeStraight);
            radioButtons.Add(radioYatzhee);
            radioButtons.Add(radioChance);



        }

        #region Buttons

        // Button that roll dice
        private void BtnRoll_Click(object sender, RoutedEventArgs e)
		{
            btnSubmit.IsEnabled = true;
			counter = 0;

            // Makes Checkboxes usable after at least first roll
            foreach (CheckBox box in checkBoxes)
            {
                box.IsEnabled = true;
            }
            // Rolls dice and displays them
			foreach (Die die in scorecard.Dice)
			{
				die.RollDie();
				checkBoxes[counter].Content = die.CurrentValue;
                counter++;
			}

            // Only lets you roll three times
            rollNumber++;
            if (rollNumber == 3)
            {
                btnRoll.IsEnabled = false;
            }
		}

        // Button that submits and calculates answer
		private void BtnSubmit_Click(object sender, RoutedEventArgs e)
		{
            decision = 0;
            // Checks each radiobutton to see which one is chosen, then disables and clears it
            foreach (RadioButton button in radioButtons)
            {
                if ((bool)button.IsChecked)
                {
                    decision = Convert.ToInt32(button.Tag);
                    button.IsEnabled = false;
                    button.IsChecked = false;
                }
            }

            // Calculates and displays the value of the chosen option
            switch (decision)
            {
                case 1:
                    lbAces.Content = scorecard.OneScore;
                    break;
                case 2:
                    lbTwos.Content = scorecard.TwoScore;
                    break;
                case 3:
                    lbThrees.Content = scorecard.ThreeScore;
                    break;
                case 4:
                    lbFours.Content = scorecard.FourScore;
                    break;
                case 5:
                    lbFives.Content = scorecard.FiveScore;
                    break;
                case 6:
                    lbSixes.Content = scorecard.SixScore;
                    break;
                case 7:
                    lbThreeOfAKind.Content = scorecard.ThreeOfAKind;
                    break;
                case 8:
                    lbFourOfAKind.Content = scorecard.FourOfAKind;
                    break;
                case 9:
                    lbFullHouse.Content = scorecard.FullHouse;
                    break;
                case 10:
                    lbsmallStraight.Content = scorecard.SmallStraight;
                    break;
                case 11:
                    lbLargeStraight.Content = scorecard.LargeStraight;
                    break;
                case 12:
                    lbYatzhee.Content = scorecard.Yahtzee;
                    break;
                case 13:
                    lbChance.Content = scorecard.Chance;
                    break;
                default:
                    break;
            }

            // If choice is in upper section, calculates upper score
            if (decision != 0 && decision <= 6)
            {
                lbUpperTotalNum.Content = scorecard.UpperScore;
            }

            // If choice is in lower section, calculates lower score
            if (decision > 6)
            {
                lbLowerTotalNum.Content = scorecard.LowerScore;
            }

            // If a choice has been made: Calculates grand total, Disables submit button
            // Enables roll button as long as game is not over, Disables and clears checkboxes
            if (decision != 0)
            {
                lbGrandTotalNum.Content = scorecard.TotalScore;
                btnSubmit.IsEnabled = false;
                rollNumber = 0;
                btnRoll.IsEnabled = true;

                gameTurns++;
                if (gameTurns >= 13)
                {
                    btnRoll.IsEnabled = false;
                }                             

                foreach (CheckBox box in checkBoxes)
                {
                    box.IsChecked = false;
                    box.IsEnabled = false;
                }
            }
            
        }

        #endregion


        #region CheckBoxes

        // When checked, holds the die so that it won't be rolled
        private void ChBoxDice_Checked(object sender, RoutedEventArgs e)
		{
            scorecard.Dice[Convert.ToInt32(((CheckBox)sender).Tag)].Hold = true;
		}

        // When unchecked, unholds the die so that it can be rolled
		private void ChBoxDice_Unchecked(object sender, RoutedEventArgs e)
		{
            scorecard.Dice[Convert.ToInt32(((CheckBox)sender).Tag)].Hold = false;
        }

        #endregion
    }
}
