using DmiCalc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace DmiCalcReflection.Operations {
    public class ReflectionOperation : IOperation {
        public string Name => ".";

        public string LocalizedName => ".";

        public string HelpKey => "";

        public Func<IDataSource, dynamic[], dynamic> GetCalcMethod() {
            throw new NotImplementedException();
        }

        public bool IsNamed(string name) {
            throw new NotImplementedException();
        }

        public dynamic Calculate(IDataSource datasource, dynamic[] args) {
            if (args?.Length != 2)
                throw new DmiCalc.Exceptions.DmiCalcException("Unexpected number of arguments");
            Type argType = (Type)args[0].GetType();
            dynamic result = null;
            try {
                result = argType.GetProperty(args[1] as string).GetValue(args[0]);
            }
            catch  {
                Trace.TraceInformation($"No property named '{args[1] as string}'");
                try {
                    result = argType.GetField(args[1] as string).GetValue(args[0]);
                }
                catch {
                    Trace.TraceInformation($"No field named '{args[1] as string}'");
                    throw new DmiCalc.Exceptions.DmiCalcException("Unable find property or field in given variable");
                }
            }
            return result;
             
        }
    }
}
