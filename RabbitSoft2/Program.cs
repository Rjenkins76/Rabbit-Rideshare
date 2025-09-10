using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace RabbitSoft2
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            IronXL.License.LicenseKey = "IRONSUITE.JENKINSROGERRABBIT.YAHOO.COM.3371-B5159B51A6-BVYIBCFGDQKH6LL7-VEZ46Q44LXFC-E4EYJL2YISXK-VTUTUW5PZNFA-NVGFSJBHFY4C-62ZMLZDOB5TN-F7ASYS-T4U2GBGGUNCQEA-DEPLOYMENT.TRIAL-XEDOOP.TRIAL.EXPIRES.01.OCT.2025";
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
