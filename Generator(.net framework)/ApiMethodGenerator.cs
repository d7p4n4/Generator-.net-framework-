using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CSAc4yClass.Class;

namespace Generator_.net_framework_
{
    class ApiMethodGenerator
    {

        public static void generateApiMethods(string templateName, string package, string className, List<Ac4yProperty> map, string outputPath)
        {
            string[] text = readIn(templateName);

            string replaced = "";
            string newLine = "";

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i].Contains("#getFirstBy#"))
                {
                    foreach (var pair in map)
                    {
                        newLine = text[i + 1].Replace("#Prop#", pair.Name.Substring(0, 1).ToUpper() + pair.Name.Substring(1))
                                                .Replace("#type#", pair.Type) + "\n" + text[i + 2] + "\n" + text[i + 3] +  "\n\n";
                        newLine = newLine + text[i + 5].Replace("#className#", className)
                                                        .Replace("#Prop#", pair.Name.Substring(0, 1).ToUpper() + pair.Name.Substring(1)) + "\n";
                        newLine = newLine  + "\n" + text[i + 6] + "\n" + text[i + 7] + "\n" + text[i + 8] + "\n" + text[i + 9] 
                                            + "\n" + text[i + 10];
                        replaced = replaced + newLine + "\n\n";
                        
                    }

                    i = i + 10;
                }
                else if (text[i].Equals("#getListBy#"))
                {
                    foreach (var pair in map)
                    {
                        newLine = text[i + 1].Replace("#Prop#", pair.Name.Substring(0, 1).ToUpper() + pair.Name.Substring(1))
                                                .Replace("#type#", pair.Type) + "\n" + text[i + 2] + "\n" + text[i + 3] + "\n";
                        newLine = newLine + text[i + 4].Replace("#className#", className)
                                                        .Replace("#Prop#", pair.Name.Substring(0, 1).ToUpper() + pair.Name.Substring(1)) + "\n";
                        newLine = newLine + "\n" + text[i + 6].Replace("#className#", className) + "\n" + text[i + 7] + "\n" + text[i + 8] + "\n" + text[i + 9]
                                            + "\n" + text[i + 10] + "\n" + text[i + 11] + "\n";
                        replaced = replaced + newLine + "\n\n";
                    }
                    i = i + 11;
                }
                else
                {
                    replaced = replaced + newLine + text[i] + "\n";
                }

                newLine = "";
            }
            replaced = replaced.Replace("#namespaceName#", package + "Api");
            replaced = replaced.Replace("#className#", className);

            writeOut(replaced, className, outputPath);

        }

        public static string[] readIn(string fileName)
        {

            string textFile = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Templates\\", fileName + "ApiEntityMethods.csT");

            string[] text = File.ReadAllLines(textFile);

            return text;


        }

        public static void writeOut(string text, string fileName, string outputPath)
        {
            File.WriteAllText(outputPath + fileName + "Controller.cs", text);

        }
    }
}
