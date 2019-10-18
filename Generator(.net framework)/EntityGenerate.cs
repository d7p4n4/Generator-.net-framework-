using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator_.net_framework_
{
    class EntityGenerate
    {

        #region values
        private static readonly string APPSETTINGS_LANGUAGE = ConfigurationManager.AppSettings["language"];
        private static readonly string APPSETTINGS_NAMESPACE = ConfigurationManager.AppSettings["namespace"];
        private static readonly string APPSETTINGS_CLASSNAME = ConfigurationManager.AppSettings["className"];
        private static readonly string APPSETTINGS_OUTPUTPATH = ConfigurationManager.AppSettings["outputPath"];
        private static readonly string APPSETTINGS_BASENAME = ConfigurationManager.AppSettings["baseName"];
        private static readonly string APPSETTINGS_INPATH = ConfigurationManager.AppSettings["inputPath"];
        private static readonly string APPSETTINGS_INPATHPREPROC = ConfigurationManager.AppSettings["inputPathPreProc"];
        private static readonly string APPSETTINGS_INPUTNAMESPACE = ConfigurationManager.AppSettings["inputNamespace"];
        #endregion

        public static void entityGenerateMethods(string[] files)
        {

            List<Type> list = new List<Type>();
            string[] files2 = files;

            foreach (var _file in files2)
            {
                string _filename = Path.GetFileNameWithoutExtension(_file);
                list.Add(ReadIn.ReadLines(_file));
            }

            for (var x = 0; x < files2.Length; x++)
            {
                string _filename = Path.GetFileNameWithoutExtension(files2[x]);
                Generator.contextGenerate(list[x], _filename, APPSETTINGS_BASENAME, APPSETTINGS_NAMESPACE, "Template", APPSETTINGS_LANGUAGE, APPSETTINGS_OUTPUTPATH);
            }

            Dictionary<string, string> values = new Dictionary<string, string>();
            foreach (var _listElement in list)
            {
                string name = _listElement.Name;
                string vName = name.Substring(0, 1).ToLower();

                values.Add(name, vName);
            }

            foreach (var _listElement in list)
            {
                Generator.generateEntityMethods("TemplateEntityMethods", APPSETTINGS_LANGUAGE, APPSETTINGS_NAMESPACE, _listElement, APPSETTINGS_OUTPUTPATH);

                Generator.programGenerator("TemplateSaveProgram", APPSETTINGS_LANGUAGE, APPSETTINGS_NAMESPACE, _listElement, values, APPSETTINGS_OUTPUTPATH);
            }
        }
    }
}
