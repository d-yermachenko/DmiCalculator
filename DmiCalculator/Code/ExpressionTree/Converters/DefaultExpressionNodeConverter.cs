using DmiCalc.ExpressionTokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DmiCalc.ExpressionTree {
    public class DefaultExpressionNodeConverter
    {
        public IVariablesProvider Variables { get; set; }

        public IOperationsProvider Functions { get; set; }


        public ExpressionNode[] BuildExpressionTree(IList<ExpressionToken> expressionTokens) {
            var nodes = new ExpressionNode[expressionTokens.Count];
            for (int i = 0; i < expressionTokens.Count; i++) {
                nodes[i] = GetExpressionNode(expressionTokens[i]);
                if (expressionTokens[i].ChildExpressionTokens.Count > 0)
                    nodes[i].ChildNodes = BuildExpressionTree(expressionTokens[i].ChildExpressionTokens).ToList();
            }
            return nodes;
        }

        private ExpressionNode GetExpressionNode(ExpressionToken expressionToken) {
            ExpressionNode result = null;
            bool nodeConverted = false;
            for(int i = 0; i < ConvertersChain.Count && !nodeConverted; i++) {
                var answer = ConvertersChain[i](expressionToken);
                nodeConverted = answer.Item1;
                result = answer.Item2;
            }
            return result;
        }


        List<Func<ExpressionToken, Tuple<bool, ExpressionNode>>> _ConvertersChain;

        List<Func<ExpressionToken, Tuple<bool, ExpressionNode>>> ConvertersChain {
            get {
                if(_ConvertersChain == null) {
                    _ConvertersChain = new List<Func<ExpressionToken, Tuple<bool, ExpressionNode>>>() {
                        new OperationExpressionNodeConverter() { OperationsGetter = this.Functions}.TryConvertToNode,
                        new ArgumentSeparatorExpressionNodeConverter().TryConvertToNode,
                        new ValueExpressionNodeConverter().TryConvertToNode,
                        new VariableExpressionNodeConverter(){ VariableGetter = this.Variables }.TryConvertToNode
                    };
                }
                return _ConvertersChain;
            }
        }

        public void Reset() {
            _ConvertersChain = null;
        }
    }
}
