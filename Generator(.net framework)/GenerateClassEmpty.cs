using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Generator_.net_framework_
{
    class GenerateClassEmpty
    {
        public static void generateClass(string templateName, string package, string className, string outputPath, string[] files)
        {
            string[] text = readIn(templateName);

            string replaced = "";

            for (int i = 0; i < text.Length; i++)
            {
                replaced = replaced + text[i] + "\n";
            }

            replaced = replaced.Replace("#namespaceName#", package);
            replaced = replaced.Replace("#className#", className);
            replaced = replaced.Replace("#parentClassName#", className + "Algebra");

            writeOut(replaced, className, outputPath);

            EntityGenerate.entityGenerateMethods(files);
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
