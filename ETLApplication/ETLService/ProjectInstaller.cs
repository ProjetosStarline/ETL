using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ETLService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        private void serviceProcessInstaller1_BeforeInstall(object sender, InstallEventArgs e)
        {
            serviceInstaller1.DisplayName = Global.NomeServico;
            //serviceInstaller1.ServiceName = Global.NomeServico;
        }
    }
}
