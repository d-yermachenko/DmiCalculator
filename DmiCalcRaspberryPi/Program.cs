using System;
using DmiCalc.BasicCalc;

namespace rpicalc
{
    class Program
    {
        static void Main(string[] args)
        {
            string expression = string.Empty;
            bool continueDialog = true;
            BasicCalculator basicCalc = new BasicCalculator();
            Console.WriteLine("Please insert your formulas..");
            while (continueDialog) {
                expression = Console.ReadLine();
                continueDialog = !string.Equals("exit", expression?.ToLower());
                if (continueDialog) {
                    try {
                        Console.WriteLine(basicCalc.CalculateExpression(expression));
                    }
                    catch(Exception e) {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }
    }
}
