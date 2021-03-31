using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ut;

namespace SkakovskiyKursova
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
//            Application.EnableVisualStyles();
//            Application.SetCompatibleTextRenderingDefault(false);

           Graph3D x = new Graph3D("3d example", 15);
           //x.ShowDialog();
            Application.Run(x);
        }
    }
}
