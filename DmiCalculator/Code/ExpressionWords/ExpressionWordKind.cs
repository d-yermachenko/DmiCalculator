using System;

namespace DmiCalc  {
    public enum ExpressionPartKind {
        Undefined,
        Whitespace,
        Number,
        Operator,
        StringLiteral,
        Name,
        Brace,
        ArgumentSeparator
    }

   

}
