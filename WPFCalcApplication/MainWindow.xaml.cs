using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFCalcApplication {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private readonly TreeView _CalcTreeTreeView;
        private readonly ListView _TokensListView;
        private readonly TextBox _FormulaTextBox;
        private readonly DmiCalc.DebugCalc.DebugCalc _DebugCalculator;

        public MainWindow()
        {
            InitializeComponent();
            _CalcTreeTreeView = this.MainLayout.RecursiveFilter<TreeView>(x=>x.Name== "CalcTreeView").First();
            _TokensListView = this.MainLayout.RecursiveFilter<ListView>(x => x.Name == "LexemesListBox").First();
            _FormulaTextBox = this.MainLayout.RecursiveFilter<TextBox>(x => x.Name == "FormulaTextBox").First();
            _DebugCalculator = new DmiCalc.DebugCalc.DebugCalc();

            this.MainLayout.RecursiveFilter<Button>(x => x.Name == "GetLexemesButton").First().Click += GetExpressionLexemes;
            //this.MainLayout.RecursiveFilter<Button>(x => x.Name == "BuildTreeButton").First().Click += GetCalculationTree;
            this.MainLayout.RecursiveFilter<Button>(x => x.Name == "CalculateButton").First().Click += CalculateExpression;
            this.MainLayout.RecursiveFilter<Button>(x => x.Name == "BuildTreeButton").First().Click += GetCalculationTreeTemplated;
        }

        private async void CalculateExpression(object sender, RoutedEventArgs e) {
            var result = await _DebugCalculator.CalculateExpressionAsync(_FormulaTextBox.Text);
            MessageBox.Show($"Result is {result} as {result.GetType().Name}");
        }

        private async void GetCalculationTree(object sender, RoutedEventArgs e) {
            var nodes = await _DebugCalculator.GetNodesAsync(_FormulaTextBox.Text);
            _CalcTreeTreeView.Items.Clear();
            List<TreeViewItem> items = new List<TreeViewItem>();
            foreach(var node in nodes) {
                items.Add(PopulateTreeViewItem(node));
            }
            _CalcTreeTreeView.ItemsSource = items;
        }

        private async void GetCalculationTreeTemplated(object sender, RoutedEventArgs e) {
            var nodes = await _DebugCalculator.GetNodesAsync(_FormulaTextBox.Text);
            ICollectionView view = CollectionViewSource.GetDefaultView(nodes);
            _CalcTreeTreeView.ItemsSource = view;
        }

        private TreeViewItem PopulateTreeViewItem(DmiCalc.ExpressionTokens.ExpressionToken token) {
            TreeViewItem thisTokenItem = new TreeViewItem();
            thisTokenItem.Header = $"{token.ExpressionTokenKind} with value {token.ExpressionTokenValue}";
            if(token.ChildExpressionTokens.Count > 0) {
                thisTokenItem.IsExpanded = true;
                foreach (var childToken in token.ChildExpressionTokens) {
                    thisTokenItem.Items.Add(PopulateTreeViewItem(childToken));
                }
            }
            return thisTokenItem;
        }

        private async void GetExpressionLexemes(object sender, RoutedEventArgs e) {
            var tokens = await _DebugCalculator.GetWordsAsync(_FormulaTextBox.Text);
            _TokensListView.Items.Clear();
            foreach(var token in tokens) {
                _TokensListView.Items.Add(new { TypeName = token.GetType().Name, 
                    Value = token.ExpressionWordString });
            }
        }


    }
}
