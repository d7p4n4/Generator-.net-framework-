using CSAc4yClass.Class;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Generator_.net_framework_
{
    class GenerateClassAlgebra
    {
        public static void generateClass(string templateName, string package, string className, List<Ac4yProperty> map, string outputPath, string[] files)
        {
            string[] text = readIn(templateName);

            string replaced = "";
            string newLine = "";

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i].Contains("#has#"))
                {
                    foreach (var pair in map)
                    {
                        if (!pair.Type.StartsWith("List") && !pair.Type.StartsWith("Boolean") && !pair.Type.StartsWith("Dictionary"))
                        {
                            newLine = text[i + 1].Replace("#propName#", pair.Name.Substring(0, 1).ToUpper() + pair.Name.Substring(1)) + "\n";
                            newLine = newLine + text[i + 2].Replace("#propName#", pair.Name.Substring(0, 1).ToUpper() + pair.Name.Substring(1)) + "\n";
                            newLine = newLine + "\n" + text[i + 3] + "\n" + text[i + 4] + "\n" + text[i + 5] + "\n" +
                                text[i + 6] + "\n" + text[i + 7] + "\n" + text[i + 8] + "\n" + text[i + 9];
                            replaced = replaced + newLine + "\n\n";
                        }
                    }

                    i = i + 9;
                }
                else if (text[i].Equals("#getter#"))
                {
                    foreach (var pair in map)
                    {
                        newLine = text[i + 1].Replace("#type#", pair.Type);
                        newLine = newLine.Replace("#prop#", pair.Name.Substring(0, 1).ToUpper() + pair.Name.Substring(1));

                        replaced = replaced + "\n" + newLine;

                        newLine = text[i + 2].Replace("#prop#", pair.Name);
                        replaced = replaced + "\n" + newLine + "\n        }\n";
                    }
                    i = i + 3;
                }
                else if (text[i].Equals("#setter#"))
                {
                    foreach (var pair in map)
                    {
                        newLine = text[i + 1].Replace("#prop#", pair.Name.Substring(0, 1).ToUpper() + pair.Name.Substring(1));
                        newLine = newLine.Replace("#type#", pair.Type);

                        replaced = replaced + "\n" + newLine;

                        newLine = text[i + 2].Replace("#prop#", pair.Name);
                        replaced = replaced + "\n" + newLine + "\n        }\n";
                    }
                    i = i + 3;
                }
                else if (text[i].Contains("#setGetHas#"))
                {
                    foreach(var _prop in map)
                    {
                        //set
                        newLine = newLine + text[i + 1].Replace("#Prop#", _prop.Name.Substring(0, 1).ToUpper() + _prop.Name.Substring(1))
                                                     .Replace("#prop#", _prop.Name) + "\n";
                        newLine = newLine + text[i + 2].Replace("#prop#", _prop.Name) + "\n" + text[i + 3] + "\n";
                        //get
                        newLine = newLine + text[i + 4].Replace("#Prop#", _prop.Name.Substring(0, 1).ToUpper() + _prop.Name.Substring(1))
                                                     .Replace("#prop#", _prop.Name) + "\n";
                        newLine = newLine + text[i + 5].Replace("#prop#", _prop.Name) + "\n" + text[i + 6] + "\n";
                        //has
                        newLine = newLine + text[i + 7].Replace("#Prop#", _prop.Name.Substring(0, 1).ToUpper() + _prop.Name.Substring(1))
                                                     .Replace("#prop#", _prop.Name) + "\n";
                        newLine = newLine + text[i + 8].Replace("#prop#", _prop.Name) + "\n" + text[i + 9] + "\n\n";


                    }
                    replaced = replaced + newLine + "\n";

                    i = i + 9;
                }
                else if (text[i].Contains("#rebuild#"))
                {
                    foreach (var _prop in map) {
                        newLine = newLine + text[i + 1].Replace("#prop#", _prop.Name)
                                                       .Replace("#Prop#", _prop.Name.Substring(0, 1).ToUpper() + _prop.Name.Substring(1)) + "\n";
                    }
                    replaced = replaced + newLine + "\n";

                    i = i + 1;
                }
                else if (text[i].Contains("#copy#"))
                {
                    foreach (var _prop in map)
                    {
                        newLine = newLine + text[i + 1].Replace("#prop#", _prop.Name)
                                                       .Replace("#Prop#", _prop.Name.Substring(0, 1).ToUpper() + _prop.Name.Substring(1)) + "\n";
                    }
                    replaced = replaced + newLine + "\n";

                    i = i + 1;
                }
                else if (text[i].Contains("#is#"))
                {
                    foreach (var pair in map)
                    {
                        if (pair.Type.StartsWith("Boolean"))
                        {
                            newLine = text[i + 1].Replace("#propName#", pair.Name.Substring(0, 1).ToUpper() + pair.Name.Substring(1)) + "\n";
                            newLine = newLine + text[i + 2].Replace("#propName#", pair.Name.Substring(0, 1).ToUpper() + pair.Name.Substring(1)) + "\n";
                            newLine = newLine + "\n" + text[i + 3] + "\n";
                            replaced = replaced + newLine + "\n\n";
                        }
                    }

                    i = i + 3;
                }
                else if (text[i].Contains("#count#"))
                {
                    foreach (var pair in map)
                    {
                        if (pair.Type.StartsWith("List") || pair.Type.StartsWith("Dictionary"))
                        {
                            newLine = text[i + 1].Replace("#propName#", pair.Name.Substring(0, 1).ToUpper() + pair.Name.Substring(1));
                            newLine = newLine + "\n" + text[i + 2] + "\n";
                            newLine = newLine + text[i + 3].Replace("#propName#", pair.Name.Substring(0, 1).ToUpper() + pair.Name.Substring(1));
                            newLine = newLine + "\n" + text[i + 4] + "\n" + text[i + 5] + "\n" + text[i + 6] + "\n";
                            replaced = replaced + newLine + "\n\n";
                        }
                    }

                    i = i + 6;
                }
                else if (text[i].Contains("#countMember#"))
                {
                    foreach (var pair in map)
                    {
                        if (pair.Type.StartsWith("List") || pair.Type.StartsWith("Dictionary"))
                        {
                            newLine = text[i + 1].Replace("#propName#", pair.Name.Substring(0, 1).ToUpper() + pair.Name.Substring(1));
                            newLine = newLine + "\n" + text[i + 2] + "\n";
                            newLine = newLine + text[i + 3].Replace("#propName#", pair.Name.Substring(0, 1).ToUpper() + pair.Name.Substring(1));
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
            replaced = replaced.Replace("#parentClassName#", className + "PreProcessed");

            writeOut(replaced, className, outputPath);

            GenerateClassEmpty.generateClass(templateName, package, className, outputPath, files);
            
        }

        public static string[] readIn(string fileName)
        {

            string textFile = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Templates\\", fileName + "Algebra.csT");
            
            string[] text = File.ReadAllLines(textFile);

            return text;


        }

        public static void writeOut(string text, string fileName, string outputPath)
        {
            File.WriteAllText(outputPath + fileName + "Algebra.cs", text);

        }
    }
}