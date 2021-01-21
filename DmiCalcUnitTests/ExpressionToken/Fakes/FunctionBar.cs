using System;
using DmiCalc;
using System.Collections.Generic;
using System.Text;


namespace DmiCalcUnitTests.ExpressionToken.Fakes {
    public class FunctionBar : IOperation {
        public string Name => "Bar";

        public string LocalizedName => "Bar";

        public string HelpKey { get ; set ; }

        public Func<IDataSource, dynamic[], dynamic> GetCalcMethod() {
            return Name as dynamic;
        }

        public bool IsNamed(string name) {
            return String.Equals(name, Name);
        }
    }
}
