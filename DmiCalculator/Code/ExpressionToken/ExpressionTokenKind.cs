using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DmiCalc.ExpressionTokens  {


    public class ExpressionTokenKind : IComparable, IEqualityComparer<ExpressionTokenKind> {

        #region Mechanics

        static ExpressionTokenKind() {
            KnownNamesField = new HashSet<string>();
        }
        private static readonly HashSet<string> KnownNamesField;

        private static HashSet<string> KnownNames {
            get {
                if(KnownNamesField.Count == 0) {
                    foreach (var declaredMember in GetAllDeclaredValues())
                        KnownNamesField.Add(declaredMember.Name);
                }
                return KnownNamesField;
            }
        }

        private static IList<ExpressionTokenKind> GetAllDeclaredValues() {
            List<ExpressionTokenKind> result = new List<ExpressionTokenKind>();
            IEnumerable<Type> referencedTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => t == typeof(ExpressionTokenKind));
            foreach(Type assemblyType in referencedTypes) {
                var knownMembers = typeof(ExpressionTokenKind).GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

                result.AddRange(knownMembers.Select(x => x.GetValue(null, null)).Cast<ExpressionTokenKind>());
            }
            //var knownMembers = typeof(ExpressionTokenKind).GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
            return result;
        }

        public static IEnumerable<ExpressionTokenKind> GetAllValues() {
            List<ExpressionTokenKind> result = new List<ExpressionTokenKind>();
            return KnownNames.Select(x => new ExpressionTokenKind(x));
        }

        public int CompareTo(object obj) {
            if (obj == null)
                return this.Name.CompareTo(Undefined.Name);
            if (!(obj is ExpressionTokenKind))
                throw new ArgumentException($"Can compare only with {this.GetType().Name}");
            return (((ExpressionTokenKind)obj).Name.CompareTo(this.Name));
        }

        public bool Equals(ExpressionTokenKind x, ExpressionTokenKind y) {
            return x.CompareTo(y) == 0;
        }

        public int GetHashCode(ExpressionTokenKind obj) {
            return obj.Name.GetHashCode();
        }

        public string Name { get; protected set; }

        protected ExpressionTokenKind(string name) {
            Name = name;
        }

        public static bool operator ==(ExpressionTokenKind x, ExpressionTokenKind y) {
            return string.Equals(x?.Name, y?.Name);
        }

        public static bool operator !=(ExpressionTokenKind x, ExpressionTokenKind y) {
            return !string.Equals(x?.Name, y?.Name);
        }

        public override int GetHashCode() {
            return GetHashCode(this);
        }

        public override bool Equals(object obj) {
            return this.CompareTo(obj) == 0;
        }

        public override string ToString() {
            return Name;
        }

        public static implicit operator string(ExpressionTokenKind argument) {
            return argument.Name;
        }

        public static implicit operator ExpressionTokenKind(string value) {
            if (KnownNames.Any(x=>String.Equals(x, value)))
                return new ExpressionTokenKind(value);
            KnownNames.Add(value);
            return new ExpressionTokenKind(value);
        }
        #endregion


        public static ExpressionTokenKind Undefined => new ExpressionTokenKind("Undefined");

        public static ExpressionTokenKind UnaryPrefixOperatorNot => new ExpressionTokenKind("UnaryPrefixOperatorNot");

        public static ExpressionTokenKind UnaryPostfixOperatorFactorial => new ExpressionTokenKind("UnaryPostfixOperatorFactorial");

        public static ExpressionTokenKind BinaryOperatorAddition => new ExpressionTokenKind("BinaryOperatorAddition");

        public static ExpressionTokenKind BinaryOperatorMultiplication => new ExpressionTokenKind("BinaryOperatorMultiplication");

        public static ExpressionTokenKind BinaryOperatorRelation => new ExpressionTokenKind("BinaryOperatorRelation");

        public static ExpressionTokenKind StringLiteral => new ExpressionTokenKind("StringLiteral");

        public static ExpressionTokenKind Function => new ExpressionTokenKind("Function");

        public static ExpressionTokenKind Variable => new ExpressionTokenKind("Variable");

        public static ExpressionTokenKind BooleanValue => new ExpressionTokenKind("BooleanValue");

        public static ExpressionTokenKind DateValue => new ExpressionTokenKind("DateValue");

        public static ExpressionTokenKind NumberValue => new ExpressionTokenKind("NumberValue");

        public static ExpressionTokenKind IntermediateName => new ExpressionTokenKind("IntermediateName");

        public static ExpressionTokenKind IntermediateBracketOpen => new ExpressionTokenKind("IntermediateBracketOpen");

        public static ExpressionTokenKind IntermediateBracketClosed => new ExpressionTokenKind("IntermediateBracketClosed");

        public static ExpressionTokenKind IntermediateSquareBracketOpen => new ExpressionTokenKind("IntermediateSquareBracketOpen");

        public static ExpressionTokenKind IntermediateSquareBracketClose => new ExpressionTokenKind("IntermediateSquareBracketClose");

        public static ExpressionTokenKind IntermediateBraceOpen => new ExpressionTokenKind("IntermediateBraceOpen");

        public static ExpressionTokenKind IntermediateBraceClosed => new ExpressionTokenKind("IntermediateBraceClosed");

        public static ExpressionTokenKind IntermediateExclamation => new ExpressionTokenKind("IntermediateExclamation");

        public static ExpressionTokenKind FunctionArgumentSeparator => new ExpressionTokenKind("FunctionArgumentSeparator");
    }

}
