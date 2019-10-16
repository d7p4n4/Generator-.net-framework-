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

            log.Debug("path:"+ GetAppConfigStringParameter(APPSETTINGS_CLASSNAME));

            string[] files =
                Directory.GetFiles(APPSETTINGS_INPATH, "*.cs", SearchOption.TopDirectoryOnly);

            foreach (var f in files)
            {
                string s = Path.GetFileNameWithoutExtension(f);
                Console.WriteLine(s);
                GenerateClass.generateClass(APPSETTINGS_LANGUAGE, APPSETTINGS_NAMESPACE, s, ReadIn.ReadLines(f, APPSETTINGS_INPUTNAMESPACE + "." + s), APPSETTINGS_OUTPUTPATH);
            }
            List<Type> list = new List<Type>();
            string[] files2 =
                Directory.GetFiles(APPSETTINGS_OUTPUTPATH, "*PreProcessed.cs", SearchOption.TopDirectoryOnly);

            foreach(var f in files2)
            {
                string s = Path.GetFileNameWithoutExtension(f);
                list.Add(ReadIn.ReadLines(f, APPSETTINGS_NAMESPACE + "." + s));
            }
            /*list.Add(ReadIn.ReadLines("d:\\Server\\Visual_studio\\GeneratedClasses\\GeneratedClasses\\PersonPreProcessed.cs", "GuidGenerate.PersonPreProcessed"));

            Dictionary<string, string> values = new Dictionary<string, string>();
            foreach (var l in list)
            {
                string name = l.Name;
                string vName = name.Substring(0, 1).ToLower();

                values.Add(name, vName);
            }
            */
            foreach (var f in files2)
            {
                string s = Path.GetFileNameWithoutExtension(f);
                Generator.contextGenerate(list, s, APPSETTINGS_BASENAME, APPSETTINGS_NAMESPACE, "Template", APPSETTINGS_LANGUAGE, APPSETTINGS_OUTPUTPATH);
            }
            Generator.generateEntityMethods("TemplateEntityMethods", APPSETTINGS_LANGUAGE, APPSETTINGS_NAMESPACE, ReadIn.ReadLines("d:\\Server\\Visual_studio\\GeneratedClasses\\GeneratedClasses\\PersonStartPreProcessedContext.cs", "GuidGenerate.PersonStartPreProcessedContext"), list, APPSETTINGS_OUTPUTPATH);
       
        }
    }
    
}
