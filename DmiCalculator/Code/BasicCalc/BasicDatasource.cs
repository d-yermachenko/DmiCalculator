using System.Globalization;

namespace DmiCalc.BasicCalc {
    public class BasicDatasource : IDataSource {
        public CultureInfo Culture { get; set ; }
        public IVariablesProvider VariablesProvider { get; set; }

        public T GetObject<T>(string name) {
            return default;
        }

        public void SetObject(string name, object value) {
            ;
        }
    }
}
