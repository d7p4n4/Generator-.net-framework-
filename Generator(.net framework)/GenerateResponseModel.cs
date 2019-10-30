using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator_.net_framework_
{
    class GenerateResponseModel
    {
        public static void generateResponseModel(string languageExtension, string package, string outputPath)
        {
            string[] text = readIn("TemplateObjectResponseModel", languageExtension);

            string replaced = "";

            for (int i = 0; i < text.Length; i++)
            {
                replaced = replaced + text[i] + "\n";
            }

            replaced = replaced.Replace("#namespaceName#", package);

            writeOut(replaced, "ResponseModel", languageExtension, outputPath);
        }

        public static string[] readIn(string fileName, string languageExtension)
        {

            string textFile = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Templates\\", fileName + "." + languageExtension + "T");

            string[] text = File.ReadAllLines(textFile);

            return text;
        }

        public static void writeOut(string text, string fileName, string languageExtension, string outputPath)
        {
            File.WriteAllText(outputPath + fileName + "." + languageExtension, text);

        }
    }
}
