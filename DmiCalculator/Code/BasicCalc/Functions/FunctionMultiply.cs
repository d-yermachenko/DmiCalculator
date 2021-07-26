using DmiCalc;
using System;
using System.Collections.Generic;
using System.Text;

namespace DmiCalc.BasicCalc {
    public class FunctionMultiply : IOperation {

        public string Name => "*";

        public string LocalizedName => "*";

        public string HelpKey { get; set; }

        public Func<IDataSource, dynamic[], dynamic> GetCalcMethod() {
            return SubstractOperation;
        }

        public bool IsNamed(string name) {
            return String.Equals("*", name);
        }

        dynamic SubstractOperation(IDataSource dataSource, dynamic[] args) {
            if (args.Length != 2)
                throw new InvalidOperationException("Wrong number of arguments: expected 2, got " + args.Length);
            return Multiply(args[0], args[1]);
        }
        public decimal Multiply(decimal a1, decimal a2) {
            return a1 * a2;
        }

        public double Multiply(double a1, double a2) {
            return a1 * a2;
        }

        public long Multiply(long a1, long a2) {
            return a1 * a2;
        }

        public long Multiply(int a1, int a2) {
            return a1 * a2;
        }
    }
}
