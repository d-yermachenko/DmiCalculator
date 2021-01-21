using System;
using System.Collections.Generic;
using System.Text;
using DmiCalc;

namespace DmiCalc.BasicCalc {
    public class FunctionAdd : IOperation {
        public string Name => "+";

        public string LocalizedName => "+";

        public string HelpKey { get ; set ; }

        public Func<IDataSource, dynamic[], dynamic> GetCalcMethod() {
            return AddOperation;
        }

        public bool IsNamed(string name) {
            return String.Equals("+", name);
        }

        dynamic AddOperation(IDataSource dataSource, dynamic[] args) => Add(args[0], args[1]);
        public decimal Add(decimal a1, decimal a2) {
            return a1 + a2;
        }

        public double Add(double a1, double a2) {
            return a1 + a2;
        }

        public double Add(long a1, long a2) {
            return a1 + a2;
        }

        public double Add(int a1, int a2) {
            return a1 + a2;
        }
    }
}
