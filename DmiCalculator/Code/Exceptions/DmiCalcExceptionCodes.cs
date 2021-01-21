using System;
using System.Collections.Generic;
using System.Text;

namespace DmiCalc.Exceptions {
    public enum DmiCalcExceptionCodes {
        UnexplainedException,
        UnaryPrefixOperationOnTheEndOfExpression,
        UnableToDetermineWordType,
        WrongBraceKind,
        WrongBraceBalance,
        CantIdentiFyExplanationOperation,
        BinaryOperationLeftPartIsNull,
        BinaryOperationRightPartIsNull,
        ArgumentSepartatorHasManyNodes
    }
}
