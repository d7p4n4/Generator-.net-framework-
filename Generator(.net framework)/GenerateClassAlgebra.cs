using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace Generator_.net_framework_
{
    class GenerateClassAlgebra
    {
        public static void generateClass(string templateName, string languageExtension, string package, string className, Dictionary<string, string> map)
        {
            string[] text = readIn(templateName, languageExtension);

            string replaced = "";
            string newLine = "";

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i].Contains("#has#"))
                {
                    foreach (var p in map)
                    {
                        if (!p.Value.StartsWith("List") && !p.Value.StartsWith("Boolean") && !p.Value.StartsWith("Dictionary"))
                        {
                            newLine = text[i + 1].Replace("#propName#", p.Key.Substring(0, 1).ToUpper() + p.Key.Substring(1)) + "\n";
                            newLine = newLine + text[i + 2].Replace("#propName#", p.Key.Substring(0, 1).ToUpper() + p.Key.Substring(1)) + "\n";
                            newLine = newLine + "\n" + text[i + 3] + "\n" + text[i + 4] + "\n" + text[i + 5] + "\n" +
                                text[i + 6] + "\n" + text[i + 7] + "\n" + text[i + 8] + "\n" + text[i + 9];
                            replaced = replaced + newLine + "\n\n";
                        }
                    }

                    i = i + 9;
                }
                else if (text[i].Contains("#is#"))
                {
                    foreach (var p in map)
                    {
                        if (p.Value.StartsWith("Boolean"))
                        {
                            newLine = text[i + 1].Replace("#propName#", p.Key.Substring(0, 1).ToUpper() + p.Key.Substring(1)) + "\n";
                            newLine = newLine + text[i + 2].Replace("#propName#", p.Key.Substring(0, 1).ToUpper() + p.Key.Substring(1)) + "\n";
                            newLine = newLine + "\n" + text[i + 3] + "\n";
                            replaced = replaced + newLine + "\n\n";
                        }
                    }

                    i = i + 3;
                }
                else if (text[i].Contains("#count#"))
                {
                    foreach (var p in map)
                    {
                        if (p.Value.StartsWith("List") || p.Value.StartsWith("Dictionary"))
                        {
                            newLine = text[i + 1].Replace("#propName#", p.Key.Substring(0, 1).ToUpper() + p.Key.Substring(1));
                            newLine = newLine + "\n" + text[i + 2] + "\n";
                            newLine = newLine + text[i + 3].Replace("#propName#", p.Key.Substring(0, 1).ToUpper() + p.Key.Substring(1));
                            newLine = newLine + "\n" + text[i + 4] + "\n" + text[i + 5] + "\n" + text[i + 6] + "\n";
                            replaced = replaced + newLine + "\n\n";
                        }
                    }

                    i = i + 6;
                }
                else if (text[i].Contains("#countMember#"))
                {
                    foreach (var p in map)
                    {
                        if (p.Value.StartsWith("List") || p.Value.StartsWith("Dictionary"))
                        {
                            newLine = text[i + 1].Replace("#propName#", p.Key.Substring(0, 1).ToUpper() + p.Key.Substring(1));
                            newLine = newLine + "\n" + text[i + 2] + "\n";
                            newLine = newLine + text[i + 3].Replace("#propName#", p.Key.Substring(0, 1).ToUpper() + p.Key.Substring(1));
                            newLine = newLine + "\n" + text[i + 4] + "\n" + text[i + 5] + "\n" + text[i + 6] + "\n" +
                                text[i + 7] + "\n" + text[i + 8] + "\n" + text[i + 9] + "\n" + text[i + 10] + "\n" +
                                text[i + 11] + "\n" + text[i + 12] + "\n" + text[i + 13];
                            replaced = replaced + newLine + "\n\n";
                        }
                    }

                    i = i + 13;
                }
                else
                {
                    replaced = replaced + newLine + text[i] + "\n";
                }

                newLine = "";
            }
            replaced = replaced.Replace("#namespaceName#", package);
            replaced = replaced.Replace("#className#", className + "Algebra");
            replaced = replaced.Replace("#parentClassName#", className + "Base");

            writeOut(replaced, className, languageExtension);

            GenerateClassEmpty.generateClass(templateName, languageExtension, package, className);
        }

        public static string[] readIn(string fileName, string languageExtension)
        {

            string textFile = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Templates\\", fileName + "Algebra." + languageExtension + "T");
            Console.WriteLine(textFile);

            string[] text = File.ReadAllLines(textFile);

            return text;


        }

        public static void writeOut(string text, string fileName, string languageExtension)
        {

            string path = ConfigurationManager.AppSettings["inputPath"];
            File.WriteAllText(path + fileName + "Algebra." + languageExtension, text);

        }
    }
}