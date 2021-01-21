using System;
using Gtk;

namespace DmiCalcGTK
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.Init();

            var app = new Application("org.DmiCalcGTK.DmiCalcGTK", GLib.ApplicationFlags.None);
            app.Register(GLib.Cancellable.Current);
            var win = new CalcWindow();
            //var win = new MainWindow();
            app.AddWindow(win);

            win.Show();
            Application.Run();
        }
    }
}
