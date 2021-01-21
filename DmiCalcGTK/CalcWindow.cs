using System;
using System.Collections.Generic;
using DmiCalcGTK.Resources;
using Gtk;

namespace DmiCalcGTK {
    class CalcWindow : Window {
        DmiCalc.BasicCalc.BasicCalculator BasicCalculator = null;

        #region Window components
        /*[Gtk.Builder.Object]
        Label FormulaLabel = null;*/

        [Gtk.Builder.Object]
        Entry FormulaEntry = null;

        [Gtk.Builder.Object]
        Button RunExpressionButton = null;

        [Gtk.Builder.Object]
        TreeView ExpressionTreeView = null;


        [Gtk.Builder.Object]
        TextView ExpressionLexemesView = null;

        [Gtk.Builder.Object]
        Label ExpressionResultLabel= null;
        #endregion


        public CalcWindow() : this(new Builder("CalcWindow.glade")) {

        }

        public CalcWindow(Builder builder) : base(builder.GetObject("CalcWindow").Handle)  {
            builder.Autoconnect(this);
            InitComponents();
            
            DeleteEvent += (s, e) => { Application.Quit(); };

        }

        public void InitComponents() {
            BasicCalculator = new DmiCalc.BasicCalc.BasicCalculator();
            RunExpressionButton.Clicked += RunExpressionButton_Clicked;
            InitCalcTreeView();

        }

        private void RunExpressionButton_Clicked(object sender, EventArgs e) {
            var result = BasicCalculator.CalculateExpressionMetaData(FormulaEntry.Text);
            SetExpressionResultLabel(result.ExpressionResult);
            SetExpressionResultNodes(result.ExpressionTokens);
            SetExpressionResultWords(result.ExpressionWords);
        }

        private void SetExpressionResultLabel(dynamic result) {
            ExpressionResultLabel.Text = result.ToString();
        }

        private void SetExpressionResultWords(IList<DmiCalc.ExpressionWord> expressionTokens) {
            String tokensRepresentations = String.Empty;
            foreach (var expressionToken in expressionTokens) {
                tokensRepresentations += $"Kind: {expressionToken?.GetType()?.Name??"Unknown"}, value: {expressionToken.ExpressionWordString} \n";
            }
            ExpressionLexemesView.Buffer.Text= tokensRepresentations;
        }

        private void InitCalcTreeView() {
            Gtk.CellRendererText typeRenderrer = new CellRendererText();
            Gtk.TreeViewColumn typeColumn = new TreeViewColumn();
            typeColumn.Title = Captions.ResourceManager.GetString("ColumnNameType");
            typeColumn.PackStart(typeRenderrer, true);

            Gtk.CellRendererText valueRenderrer = new CellRendererText();
            Gtk.TreeViewColumn valueColumn = new TreeViewColumn();
            valueColumn.Title = Captions.ResourceManager.GetString("ColumnNameValue");
            valueColumn.PackStart(valueRenderrer, true);

            ExpressionTreeView.AppendColumn(typeColumn);
            ExpressionTreeView.AppendColumn(valueColumn);

            typeColumn.AddAttribute(typeRenderrer, "text", 0);
            valueColumn.AddAttribute(valueRenderrer, "text", 1);

        }


        private void SetExpressionResultNodes(IList<DmiCalc.ExpressionTokens.ExpressionToken> expressionTree) {
            Gtk.TreeStore treeStore = new TreeStore(typeof(string), typeof(string));
            CreateTreeIter(treeStore, expressionTree, null);
            if(ExpressionTreeView.Model is Gtk.TreeStore) {
                var tempModel = (Gtk.TreeStore)ExpressionTreeView.Model;
                ExpressionTreeView.Model = null;
                tempModel.Dispose();
            }
            ExpressionTreeView.Model = treeStore;

        }

        private void CreateTreeIter(Gtk.TreeStore treeStore, IList<DmiCalc.ExpressionTokens.ExpressionToken> expressionTree, Gtk.TreeIter? parentIter = null) {
            for(int i = 0; i < expressionTree.Count; i++) {
                Gtk.TreeIter currentIteration;
                Array expressionValues = new object[] { expressionTree[i].ExpressionTokenKind.ToString(), expressionTree[i].ExpressionTokenValue };
                if (parentIter == null)
                    currentIteration = treeStore.AppendValues(expressionValues);
                else
                    currentIteration = treeStore.AppendValues(parentIter, expressionValues);
                if (expressionTree[i].ChildExpressionTokens.Count > 0)
                    CreateTreeIter(treeStore, expressionTree[i].ChildExpressionTokens, currentIteration);
            }
        }
    }
}
