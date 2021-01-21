using System;
using System.Linq;

namespace DmiCalc
{



    public abstract class ExpressionWord {

        public string ExpressionWordString {
            get;
            protected set;
        }

        public int NextCursorPosition {
            get;
            protected set;
        }


        private System.Globalization.CultureInfo _Culture;

        public System.Globalization.CultureInfo Culture {
            get => _Culture ?? System.Threading.Thread.CurrentThread.CurrentCulture;
            set { _Culture = value; }
        }

        protected ExpressionWord() {
            ExpressionWordString = String.Empty;
        }

        //public abstract int FinalizeExpressionToken(string expression, int indent);

        /// <summary>
        /// Key method which defines expression word. Tries to create expression world of the kind
        /// and returns true in the first member of result tuple 
        /// </summary>
        /// <param name="expression">Expression to parse</param>
        /// <param name="indent">Position to start matching</param>
        /// <returns>
        /// Tuple<bool, ExpressionWord>. First item of tupple is result. True, if expression word matches 
        /// In second, Expression word 
        /// </returns>
        public abstract ExpressionWordResult TryCreateExpressionToken(string expression, int indent);



    }

    public class ExpressionWordResult {
        /// <summary>
        /// Is it given Expression word
        /// </summary>
        public bool Corresponds { get; set; }

        /// <summary>
        /// If expression word 
        /// </summary>
        public ExpressionWord Word { get; set; }

        public ExpressionWordResult(bool corresponds, ExpressionWord result) {
            Corresponds = corresponds;
            Word = result;
        }
    }
}