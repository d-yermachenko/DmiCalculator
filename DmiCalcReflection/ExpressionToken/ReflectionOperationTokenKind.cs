using System;
using System.Collections.Generic;
using System.Text;
using DmiCalc.ExpressionTokens;

namespace DmiCalcReflection.ExpressionToken {
    public class ReflectionOperationTokenKind : ExpressionTokenKind {
        protected ReflectionOperationTokenKind(string name): base(name) {

        }

        public static ExpressionTokenKind ReflectionOperation => new ReflectionOperationTokenKind("ReflectionOperation");

        public static ExpressionTokenKind ReflectionProperty => new ReflectionOperationTokenKind("ReflectionProperty");
    }
}
