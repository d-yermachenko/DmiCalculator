using DmiCalc.Interfaces;
using System;

namespace DmiCalc.ExpressionTokens {
    public class ExpressionWordToTokenConvertor : IExpressionWordToTokenConverter
        {
        public virtual ExpressionToken ToExpressionToken(ExpressionWord word) {
            return ToExpressionToken(word as dynamic);
        }

        public ExpressionToken ToExpressionToken(NumberExpressionWord word) {
            ExpressionToken result = new ExpressionToken() {
                ExpressionTokenKind = ExpressionTokenKind.NumberValue
            };
            if (Int64.TryParse(word.ExpressionWordString, out long longValue)) {
                result.ExpressionTokenValue = longValue;
            }
            else {
                if (Decimal.TryParse(word.ExpressionWordString, out decimal decimalValue)) {
                    result.ExpressionTokenValue = decimalValue;
                }
                else {
                    if (Double.TryParse(word.ExpressionWordString, out double doubleValue)) {
                        result.ExpressionTokenValue = doubleValue;
                    }
                }
            }
            return result;
        }

        public ExpressionToken ToExpressionToken(OperatorExpressionWord word) {
            ExpressionToken result = new ExpressionToken() {
                ExpressionTokenValue = word.ExpressionWordString
            };
            switch (word.ExpressionWordString.ToUpper()) {
                case "+":
                case "-":
                case "OR":
                case "||":
                    result.ExpressionTokenKind = ExpressionTokenKind.BinaryOperatorAddition;
                    break;
                case "*":
                case "/":
                case ":":
                case "AND":
                    result.ExpressionTokenKind = ExpressionTokenKind.BinaryOperatorMultiplication;
                    break;
                case "<":
                case "<=":
                case ">":
                case ">=":
                case "!=":
                case "==":
                    result.ExpressionTokenKind = ExpressionTokenKind.BinaryOperatorRelation;
                    break;
                case "!":
                    result.ExpressionTokenKind = ExpressionTokenKind.IntermediateExclamation;
                    break;
                case "~":
                case "NOT":
                    result.ExpressionTokenKind = ExpressionTokenKind.UnaryPrefixOperatorNot;
                    break;
            }
            return result;

        }

        public ExpressionToken ToExpressionToken(NameExpressionWord word) {
            return new ExpressionToken() {
                ExpressionTokenValue = word.ExpressionWordString,
                ExpressionTokenKind = ExpressionTokenKind.IntermediateName
            };
        }

        public ExpressionToken ToExpressionToken(BracketExpressionWord word) {
            var result = new ExpressionToken() {
                ExpressionTokenValue = word.ExpressionWordString,
            };
            switch (word.ExpressionWordString) {
                case "(":
                    result.ExpressionTokenKind = ExpressionTokenKind.IntermediateBracketOpen;
                    break;
                case ")":
                    result.ExpressionTokenKind = ExpressionTokenKind.IntermediateBracketClosed;
                    break;
                case "[":
                    result.ExpressionTokenKind = ExpressionTokenKind.IntermediateSquareBracketOpen;
                    break;
                case "]":
                    result.ExpressionTokenKind = ExpressionTokenKind.IntermediateSquareBracketClose;
                    break;
                case "{":
                    result.ExpressionTokenKind = ExpressionTokenKind.IntermediateBraceOpen;
                    break;
                case "}":
                    result.ExpressionTokenKind = ExpressionTokenKind.IntermediateBraceClosed;
                    break;
            }
            return result;
        }

        public ExpressionToken ToExpressionToken(StringLiteralExpressionWord word) {
            ExpressionToken result = null;
            if (DateTime.TryParse(word.ExpressionWordString, out DateTime possibleDateTimeValue)) {
                result = new ExpressionToken() {
                    ExpressionTokenValue = possibleDateTimeValue,
                    ExpressionTokenKind = ExpressionTokenKind.DateValue,
                };
            }
            else if (Boolean.TryParse(word.ExpressionWordString, out bool possibleBooleanValue)) {
                result = new ExpressionToken() {
                    ExpressionTokenValue = possibleBooleanValue,
                    ExpressionTokenKind = ExpressionTokenKind.BooleanValue
                };
            }
            else {
                result = new ExpressionToken() {
                    ExpressionTokenValue = word.ExpressionWordString,
                    ExpressionTokenKind = ExpressionTokenKind.StringLiteral
                };
            }

            return result;
        }

        public ExpressionToken ToExpressionToken(WhitespaceExpressionWord word) {
            return null;
        }

        public ExpressionToken ToExpressionToken(ArgumentSeparatorExpressionWord word) {
            return new ExpressionToken() {
                ExpressionTokenKind = ExpressionTokenKind.FunctionArgumentSeparator,
                ExpressionTokenValue = word.ExpressionWordString
            };
        }
    }
}
