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
        public static void generateResponseModel(string className, string outputPath)
        {
            string[] text = readIn("TemplateObjectResponseModel");

            string replaced = "";

            for (int i = 0; i < text.Length; i++)
            {
                replaced = replaced + text[i] + "\n";
            }

            replaced = replaced.Replace("#namespaceName#", className);

            writeOut(replaced, className + "ResponseModel", outputPath);
        }

        public static string[] readIn(string fileName)
        {

            string textFile = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Templates\\", fileName + ".csT");

            string[] text = File.ReadAllLines(textFile);

            return text;
        }

        public static void writeOut(string text, string fileName, string outputPath)
        {
            File.WriteAllText(outputPath + fileName + ".cs", text);

        }
    }
}
