using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DmiCalc {
    public interface IOperation {
        string Name { get; }

        string LocalizedName { get; }

        bool IsNamed(string name);

        Func<IDataSource, dynamic[], dynamic> GetCalcMethod();

        string HelpKey { get; }
    }
}
