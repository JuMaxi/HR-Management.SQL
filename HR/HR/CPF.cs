using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HR
{
    public class CPF
    {
        public string Number;

        public CPF(string NumberCPF)
        {
            Number = NumberCPF;
            CPFValidate();
        }

        public string ToReplace(string CPFReplace)
        {
            CPFReplace = CPFReplace.Replace(".", "");
            CPFReplace = CPFReplace.Replace("-", "");

            return CPFReplace;
        }

        public int SumDig(int Modify)
        {
            int Number = 0;

            for (int Position = 0; Position < ToReplace(this.Number).Length - (Modify + 1); Position++)
            {
                int NumberTemp = Convert.ToInt32(Convert.ToString(ToReplace(this.Number)[Position]));

                int Temp = NumberTemp * ((ToReplace(this.Number).Length - Modify) - Position);

                Number = Number + Temp;
            }

            return Number;
        }

        public int FindDigit(int Digit)
        {
            Digit = Digit % ToReplace(Number).Length;

            if (Digit < 2)
            {
                Digit = 0;
            }
            else
            {
                Digit = ToReplace(Number).Length - Digit;
            }

            return Digit;
        }

        public void ToCompare(int DigitCompare, int Position)
        {
            int Compare = Convert.ToInt32(Convert.ToString(ToReplace(Number)[Position]));

            if (Compare != DigitCompare)
            {
                throw new Exception("This Number CPF " + Number + " is invalid.");
            }
        }

        public void CPFValidate()
        {
            int FirstDig = SumDig(1);
            int SecondDig = SumDig(0);

            FirstDig = FindDigit(FirstDig);
            SecondDig = FindDigit(SecondDig);

            ToCompare(FirstDig, ToReplace(Number).Length - 2);
            ToCompare(SecondDig, ToReplace(Number).Length - 1);
        }
    }
}
