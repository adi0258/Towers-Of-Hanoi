using System;
using System.Windows.Forms;

namespace Towers_Of_Hanoi
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GameSetupWindow());
        }
    }
}
