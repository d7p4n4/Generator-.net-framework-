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
    class EntityGenerate
    {

        #region values
        private static readonly string APPSETTINGS_OUTPUTPATH = ConfigurationManager.AppSettings["outputPath"];
        private static readonly string APPSETTINGS_BASENAME = ConfigurationManager.AppSettings["baseName"];
        #endregion

        public static void entityGenerateMethods(string[] files)
        {

            List<Ac4yClass> list = new List<Ac4yClass>();
            string[] files2 = files;

            foreach (var _file in files2)
            {
                string _filename = Path.GetFileNameWithoutExtension(_file);
                list.Add(DeserialiseMethod.deser(_file));
            }


            Generator.contextGenerate(APPSETTINGS_BASENAME, "Contexts", "Template", APPSETTINGS_OUTPUTPATH, list);


            for (var x = 0; x < files2.Length; x++)
            {
                string _filename = Path.GetFileNameWithoutExtension(files2[x]);
                            
                Generator.generateEntityMethods("TemplateEntityMethods", list[x].Namespace, list[x], APPSETTINGS_OUTPUTPATH);

                Generator.programGenerator("TemplateSaveProgram", list[x].Namespace, list[x], APPSETTINGS_OUTPUTPATH);
            }
        }
    }
}
