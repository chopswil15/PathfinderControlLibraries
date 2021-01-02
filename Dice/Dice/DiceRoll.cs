using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Dice
{
    public static class DiceRoll
    {
        public static int RollComplexDice(string complexDiceText)
        {
            int modifier = 0;
            int rolledValue = 0;            
            string temp;

            string sign = null;
            if ( complexDiceText.Contains("+")) sign = "+";
            if (complexDiceText.Contains("-")) sign = "-";            
            int Pos = complexDiceText.IndexOf(sign);

            if (Pos > 0)
            {
                temp = complexDiceText.Substring(Pos + 1, complexDiceText.Length - 1 - Pos).Trim();
                modifier = Convert.ToInt32(temp);
                if (sign == "-") modifier *= -1;
                complexDiceText = complexDiceText.Replace(sign + temp, string.Empty).Trim();
            }


            Pos = complexDiceText.IndexOf('d');
            int multiple = 1;
            if (Pos > 0)
            {
                temp = complexDiceText.Substring(0, Pos);
                multiple = Convert.ToInt32(temp);
                complexDiceText = complexDiceText.Replace(temp + "d", string.Empty);
            }
            else
            {
                complexDiceText = complexDiceText.Replace("d", string.Empty);
            }

            int Die = Convert.ToInt32(complexDiceText.Trim()); //e.g. d8,d10

            for (int b = 1; b <= multiple; b++)
            {
                rolledValue += RollDice(Die);
            }
            return rolledValue + modifier;
        }

        public static int RollDice(int diceType)
        {
            Thread.Sleep(100);
            Random randomRoll = new Random((int)DateTime.Now.Ticks);
            return randomRoll.Next(1, Convert.ToInt32(diceType) + 1);
        }
    }
}
