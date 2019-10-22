using CSAc4yClass.Class;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator_.net_framework_
{ 
        class Program
        {

        #region constant

        private static readonly string APPSETTINGS_LANGUAGE = ConfigurationManager.AppSettings["language"];
        private static readonly string APPSETTINGS_NAMESPACE = ConfigurationManager.AppSettings["namespace"];
        private static readonly string APPSETTINGS_CLASSNAME = ConfigurationManager.AppSettings["className"];
        private static readonly string APPSETTINGS_OUTPUTPATH = ConfigurationManager.AppSettings["outputPath"];
        private static readonly string APPSETTINGS_BASENAME = ConfigurationManager.AppSettings["baseName"];
        private static readonly string APPSETTINGS_INPATH = ConfigurationManager.AppSettings["inputPath"];
        private static readonly string APPSETTINGS_INPATHPREPROC = ConfigurationManager.AppSettings["inputPathPreProc"];
        private static readonly string APPSETTINGS_INPUTNAMESPACE = ConfigurationManager.AppSettings["inputNamespace"];

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

            //Date: 2019. 10. 22. 20:11

            log.Debug("path:"+ GetAppConfigStringParameter(APPSETTINGS_CLASSNAME));

            try
            {
                string[] files =
                    Directory.GetFiles(APPSETTINGS_INPATH, "*.xml", SearchOption.TopDirectoryOnly);

                foreach (var _file in files)
                {
                    string _filename = Path.GetFileNameWithoutExtension(_file);
                    Console.WriteLine(_filename);

                    Ac4yClass ac4y = DeserialiseMethod.deser(_file);

                    GenerateClass.generateClass(APPSETTINGS_LANGUAGE, ac4y, APPSETTINGS_OUTPUTPATH, files);
                }
            } catch (Exception _exception)
            {
                log.Error(_exception.StackTrace);
            }
        }
    }
    
}
