using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DmiCalc.AppCode.BasicCalc;
using DmiCalc.ExpressionTokens;

namespace WindowsFormParserApp {
    public partial class DmiCalcForm : Form {
        public DmiCalcForm()
        {
            InitializeComponent();
            DebugCalculator = new DebugCalc();
            btnSplitByLexemes.Click += delegate(object sender, EventArgs args) {
                FillCalcWordsAsync(tbExpression.Text);
            };

            btnCalculationTree.Click += (s, e) => { FillCalcTree(tbExpression.Text); };
            btnCalculateResult.Click += (s, e) => CalculateAsync(tbExpression.Text);
        }


        DebugCalc DebugCalculator;

        public async void FillCalcWordsAsync(string expression) {
            var words = await DebugCalculator.GetWordsAsync(expression);
            lbLexemes.Items.Clear();
            foreach (var word in words) {
                lbLexemes.Items.Add(word.ExpressionWordString);
            }
        }

        public async void FillCalcTree(string expression) {
            var calcNodes = await DebugCalculator.GetTokensAsync(expression);
            tvNodes.Nodes.Clear();
            AddNodeToTreeView(tvNodes.Nodes, calcNodes);
        }

        private void AddNodeToTreeView(System.Collections.IList parrent, ICollection<ExpressionToken> expressionTokens) {
            foreach(ExpressionToken expressionToken in expressionTokens) {
                var expressionTokenTreeNode = new TreeNode(expressionToken.ExpressionTokenValue.ToString());
                if(expressionToken.ChildExpressionTokens?.Count > 0) {
                    AddNodeToTreeView(expressionTokenTreeNode.Nodes, expressionToken.ChildExpressionTokens);
                }
                parrent.Add(expressionTokenTreeNode);
            }
        }

        public async void CalculateAsync(string expression) {
            try {
                var result = await DebugCalculator.CalculateExpressionAsync(expression); MessageBox.Show($"{result?.ToString()} of type {result?.GetType()?.Name}", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (DmiCalc.Exceptions.DmiCalcException e) {
                MessageBox.Show(e.CalcExceptionCode.ToString());
            }
            catch(Exception e) {
                MessageBox.Show(e.Message);
            }
        }

    }
}
