using System;
using DmiCalc;
using System.Collections.Generic;
using System.Globalization;
using System.Text;


namespace DmiCalcUnitTests.ExpressionToken.Fakes {
    internal class FunctionFoo : IOperation {
        public string Name => "Foo";
        public string LocalizedName => "Foo";
        public string HelpKey { get; set; }
        public Func<IDataSource, dynamic[], dynamic> GetCalcMethod() {
            return "Foo" as dynamic;
        }

        public bool IsNamed(string name) {
            return String.Equals(LocalizedName, name);
        }


    }
}
