﻿using System;
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

        public static void entityGenerateMethods()
        {

            List<Type> list = new List<Type>();
            string[] files2 =
                Directory.GetFiles(APPSETTINGS_OUTPUTPATH, "*PreProcessed.cs", SearchOption.TopDirectoryOnly);

            foreach (var f in files2)
            {
                string s = Path.GetFileNameWithoutExtension(f);
                list.Add(ReadIn.ReadLines(f, APPSETTINGS_NAMESPACE + "." + s));
            }

            foreach (var f in files2)
            {
                string s = Path.GetFileNameWithoutExtension(f);
                Generator.contextGenerate(list, s, APPSETTINGS_BASENAME, APPSETTINGS_NAMESPACE, "Template", APPSETTINGS_LANGUAGE, APPSETTINGS_OUTPUTPATH);
            }

            Dictionary<string, string> values = new Dictionary<string, string>();
            foreach (var l in list)
            {
                string name = l.Name;
                string vName = name.Substring(0, 1).ToLower();

                values.Add(name, vName);
            }

            Generator.generateEntityMethods("TemplateEntityMethods", APPSETTINGS_LANGUAGE, APPSETTINGS_NAMESPACE, list, APPSETTINGS_OUTPUTPATH);
            Generator.programGenerator("TemplateSaveProgram", APPSETTINGS_LANGUAGE, APPSETTINGS_NAMESPACE, list, values, APPSETTINGS_OUTPUTPATH);

        }
    }
}
