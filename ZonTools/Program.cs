using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZonTools.Controllers;

namespace ZonTools
{
    static class Program
    {

        public static IServiceProvider ServiceProvider { get; set; }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false); 
            
            ConfigureServices();


            Application.Run(new MainForm());

            
        }

        static void ConfigureServices()
        {
            var services = new ServiceCollection();

            var baseAddress = new Uri(ConfigurationManager.AppSettings["BaseAddress"]);
            services.AddHttpClient("ZonTools", (c) => { c.BaseAddress = baseAddress; });
            services.AddTransient<IOptionsController, OptionsController>();
            ServiceProvider = services.BuildServiceProvider();            
        }
    }
}
