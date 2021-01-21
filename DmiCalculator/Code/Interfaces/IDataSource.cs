using System;
using System.Collections.Generic;
using System.Text;

namespace DmiCalc {
    public interface IDataSource {
        T GetObject<T>(string name);

        void SetObject(string name, object value);

        System.Globalization.CultureInfo Culture { get; set; }

        IVariablesProvider VariablesProvider { get; set; }
    }
}
