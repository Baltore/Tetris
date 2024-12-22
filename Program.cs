using System;
using System.Windows.Forms;

namespace Tetris
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new UI.MainMenu()); // Lancer le menu principal
        }
    }
}
