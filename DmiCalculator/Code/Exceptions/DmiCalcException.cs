using System;
using System.Collections.Generic;
using System.Text;

namespace DmiCalc.Exceptions {
    public class DmiCalcException : Exception{

        public DmiCalcException() : base() { }

        public DmiCalcException(string message) : base(message) { }

        public DmiCalcException(string message, Exception innerException) : base(message, innerException) { }

        public DmiCalcException(DmiCalcExceptionCodes exceptionCode) {
            CalcExceptionCode = exceptionCode;
        }

        public DmiCalcException(DmiCalcExceptionCodes exceptionCode, object [] faultNodes) {
            CalcExceptionCode = exceptionCode;
            FaultNodes = faultNodes;
        }

        public DmiCalcException(object[] faultNodes) {
            CalcExceptionCode = DmiCalcExceptionCodes.UnexplainedException;
            FaultNodes = faultNodes;
        }



        public DmiCalcExceptionCodes CalcExceptionCode { get; internal set; }

        public string Phrase { get; set; }

        public object[] FaultNodes;

        public int[] LocalPositions;
    }
}
