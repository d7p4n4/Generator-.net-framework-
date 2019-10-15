using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator_.net_framework_
{ 
        class Program
        {

        #region constant

        private const string APPSETTINGS_CLASSNAME = "className";

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion // constants

        #region base functions

        private static string GetAppConfigStringParameter(string name)
        {

            return ConfigurationManager.AppSettings.Get(name);

        }

        #endregion // base functions

        static void Main(string[] args)
            {

            log.Debug("path:"+ GetAppConfigStringParameter(APPSETTINGS_CLASSNAME));

            GenerateClass.generateClass("cs", "GuidGenerate", "Person", typeof(PersonStart));

            }
        }
    
}
