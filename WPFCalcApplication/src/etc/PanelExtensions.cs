using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WPFCalcApplication {
    public static class PanelExtensions {

        public static IList<System.Windows.UIElement> RecursiveFilter(this System.Windows.Controls.Panel panel, Expression<Func<System.Windows.UIElement, bool>> filter) {
            Func<System.Windows.UIElement, bool> filterFunc;
            if (panel == null)
                throw new ArgumentNullException(nameof(panel));
            if (filter == null)
                filterFunc = (el) => true;
            else
                filterFunc = filter.Compile();
            List<System.Windows.UIElement> uiElements = new List<System.Windows.UIElement>();
            foreach (var panelElement in panel.Children) {
                if (panelElement is System.Windows.Controls.Panel) {
                    uiElements.AddRange((panelElement as System.Windows.Controls.Panel).RecursiveFilter(filter));
                }
                else if (filterFunc.Invoke(panelElement as System.Windows.UIElement) == true)
                    uiElements.Add(panelElement as System.Windows.UIElement);
            }
            return uiElements;

        }

        public static IList<T> RecursiveFilter<T>(this System.Windows.Controls.Panel panel, Expression<Func<T, bool>> filter = null)
            where T : System.Windows.UIElement {
            Func<T, bool> filterFunc;
            if (panel == null)
                throw new ArgumentNullException(nameof(panel));
            if (filter == null)
                filterFunc = (el) => true;
            else
                filterFunc = filter.Compile();
            List<T> uiElements = new List<T>();
            foreach (var panelElement in panel.Children) {
                if (panelElement is System.Windows.Controls.Panel) {
                    uiElements.AddRange((panelElement as System.Windows.Controls.Panel).RecursiveFilter<T>(filter));
                }
                else if (panelElement is T && (filterFunc?.Invoke(panelElement as T) ?? true) == true)
                    uiElements.Add(panelElement as T);
            }
            return uiElements;
        }

    }
}
