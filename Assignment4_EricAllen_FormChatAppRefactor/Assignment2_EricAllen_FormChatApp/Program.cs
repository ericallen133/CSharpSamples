using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Interfaces;
using ChatLogger;
using Microsoft.Practices.Unity;
using Ninject;
using EricAllen_Logger;
using PaulGothreau_Logger;
using w0269804.Logger;

namespace Assignment2_EricAllen_FormChatApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application, implements IOC container to create logger
        /// </summary>
        [STAThread]
        static void Main()
        {
            //regular style injection
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new mainForm(new Logger()));

            var container = new UnityContainer();

            //My logger
            //container.RegisterType<ILoggingService, Logger>();

            //Greg's logger
            //container.RegisterType<ILoggingService,CustomLogger>();

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(container.Resolve<mainForm>());

            //Ninject IOC

            //IKernel kernel = new StandardKernel();
            //kernel.Bind<ILoggingService>().To<myLogger>();
            //ILoggingService loggingService = kernel.Get<ILoggingService>();
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new mainForm(loggingService));
        }
    }
}
