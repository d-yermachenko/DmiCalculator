using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DmiCalc
{
    public interface ICalculable
    {
        dynamic Calculate(IDataSource dataSource);
    }

    public interface ICalculableAsync {
        Task<dynamic> CalculateAsync(IDataSource dataSource);
    }
}
