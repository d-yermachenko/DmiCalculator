using DmiCalc;
using System;
using System.Collections.Generic;
using System.Text;

namespace DmiCalc.BasicCalc {
    public class FunctionDivide : IOperation {

        public string Name => "/";

        public string LocalizedName => "/";

        public string HelpKey { get; set; }

        public Func<IDataSource, dynamic[], dynamic> GetCalcMethod() {
            return SubstractOperation;
        }

        public bool IsNamed(string name) {
            return String.Equals("/", name);
        }

        dynamic SubstractOperation(IDataSource dataSource, dynamic[] args) {
            if (args.Length != 2)
                throw new InvalidOperationException("Wrong number of arguments: expected 2, got " + args.Length);
            return Divide(args[0], args[1]);
        }
        public decimal Divide(decimal a1, decimal a2) {
            return a1 / a2;
        }

        public double Divide(double a1, double a2) {
            return a1 / a2;
        }

        public double Divide(long a1, long a2) {
            return a1 / a2;
        }

        public double Divide(int a1, int a2) {
            return a1 / a2;
        }
    }
}

