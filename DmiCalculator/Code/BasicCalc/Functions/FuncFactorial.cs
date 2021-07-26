using System;


namespace DmiCalc.BasicCalc  {
    public class FuncExclamation : IOperation {
        public string Name => "!";

        public string LocalizedName => "!";

        public string HelpKey { get; set; }

        public Func<IDataSource, dynamic[], dynamic> GetCalcMethod() {
            return CalculateExcl;
        }

        public dynamic CalculateExcl(IDataSource ds, dynamic[] args) {
            if (args?.Length == 1)
                return CalculatExclamation(args[0]);
            else
                throw new InvalidOperationException("Wrong number of arguments: expected 1, got " + args.Length);
        }

        public dynamic CalculatExclamation(bool argument) {
            return !argument;
        }

        public long CalculatExclamation(long argument) {
            long res = 1;
                while (argument != 1) {
                    res *= argument;
                    argument -= 1;
                }
            return res;
        }

        public ulong CalculatExclamation(ulong argument) {
            ulong res = 1;
                while (argument != 1) {
                    res *= argument;
                    argument -= 1;
                }
            return res;
        }


        public long CalculatExclamation(int argument) {
            long res = 1;
            while (argument != 1) {
                res *= argument;
                argument -= 1;
            }
            return res;
        }

        public ulong CalculatExclamation(uint argument) {
            ulong res = 1;
            while (argument != 1) {
                res *= argument;
                argument -= 1;
            }
            return res;
        }


        public dynamic CalculatExclamation(double argument) {
            double res = 1;
            while (argument != 1) {
                res *= argument;
                argument -= 1;
            }
            return res;
        }

        public double CalculatExclamation(float argument) {
            float res = 1;
            while (argument != 1) {
                res *= argument;
                argument -=1;
            }
            return res;
        }

        public decimal CalculatExclamation(decimal argument) {
            decimal res = 1;
            while (argument != 1) {
                res *= argument;
                argument -= 1;
            }
            return res;
        }

        public bool IsNamed(string name) {
            return String.Equals("!", name)||
                String.Equals("not", name?.ToLower())
                || String.Equals("fact", name?.ToLower());
        }
    }
}
