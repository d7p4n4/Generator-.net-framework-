using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Generator_.net_framework_
{
    class GenerateClass
    {
        public static void generateClass(string languageExtension, string package, string className, Type anyType, string outputPath, string[] files)
        {

            //get the properties and its type
            Dictionary<string, string> map = InsertGuid.getProps(anyType);

            //get the GUID value of the properties
            Dictionary<string, string> guid = InsertGuid.getAttrs(anyType);

            //Has the class GUID value or not
            Boolean classAttr = InsertGuid.hasClassAttr(anyType, typeof(GUID));
            Boolean persistent = InsertGuid.hasClassAttr(anyType, typeof(Persistent));

            string[] text = new String[0];

            if (persistent && languageExtension.Equals("cs"))
            {
                text = readIn("TemplatePersistent", languageExtension);
            }
            else
            {
                text = readIn("Template", languageExtension);
            }
            string replaced = "";

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i].Equals("#constructor#"))
                {
                    string propNames = "";
                    string newLine = "";

                    foreach (var pair in map)
                    {
                        propNames = propNames + pair.Key + ", ";
                    }
                    newLine = text[i + 1].Replace("#allProps#", propNames.Substring(0, propNames.Length - 2)) + "\n";
                    foreach (var pair in map)
                    {
                        newLine = newLine + text[i + 2].Replace("#prop#", pair.Key) + "\n";
                    }
                    newLine = newLine + text[i + 3];

                    replaced = replaced + "\n" + newLine;

                    i = i + 3;
                }
                else if (text[i].Equals("#properties#"))
                {
                    foreach (var pair in map)
                    {
                        foreach (var _guid in guid)
                        {
                            if (_guid.Key.Equals(pair.Key) && _guid.Value.Equals(""))
                            {
                                
                                Guid id = Guid.NewGuid();
                                string newLine = "";

                                if (languageExtension.Equals("cs"))
                                {
                                    //newLine = "            [GUID(\"" + id + "\")]\n";
                                }
                                else if (languageExtension.Equals("java"))
                                {
                                    //newLine = "            @GUID(\"" + id + "\")\n";
                                }
                                newLine = newLine + text[i + 1].Replace("#type#", pair.Value);
                                newLine = newLine.Replace("#prop#", pair.Key);

                                replaced = replaced + "\n" + newLine;
                                break;
                            }
                            else if (_guid.Key.Equals(pair.Key))
                            {
                                string newLine = "";

                                if (languageExtension.Equals("cs"))
                                {
                                    //newLine = "            [GUID(\"" + _guid.Value + "\")]\n";
                                }
                                else if (languageExtension.Equals("java"))
                                {
                                    //newLine = "            @GUID(\"" + _guid.Value + "\")\n";
                                }
                                newLine = newLine + text[i + 1].Replace("#type#", pair.Value);
                                newLine = newLine.Replace("#prop#", pair.Key);

                                replaced = replaced + "\n" + newLine;
                                break;
                            }
                        }
                    }
                    i++;
                }
                
                else if (text[i].Equals("#getter#"))
                {
                    foreach (var pair in map)
                    {
                        string newLine = text[i + 1].Replace("#type#", pair.Value);
                        newLine = newLine.Replace("#prop#", pair.Key.Substring(0, 1).ToUpper() + pair.Key.Substring(1));

                        replaced = replaced + "\n" + newLine;

                        newLine = text[i + 2].Replace("#prop#", pair.Key);
                        replaced = replaced + "\n" + newLine + "\n        }\n";
                    }
                    i = i + 3;
                }
                else if (text[i].Equals("#setter#"))
                {
                    foreach (var pair in map)
                    {
                        string newLine = text[i + 1].Replace("#prop#", pair.Key.Substring(0, 1).ToUpper() + pair.Key.Substring(1));
                        newLine = newLine.Replace("#type#", pair.Value);

                        replaced = replaced + "\n" + newLine;

                        newLine = text[i + 2].Replace("#prop#", pair.Key);
                        replaced = replaced + "\n" + newLine + "\n        }\n";
                    }
                    i = i + 3;
                }
                
                else if (!classAttr && text[i].Contains("public class #className#"))
                {
                    if (text[i].Contains("#className#"))
                    {
                        Guid id = Guid.NewGuid();
                        string newLine = "";

                        if (languageExtension.Equals("cs"))
                        {
                            //newLine = "            [GUID(\"" + id + "\")]\n";
                        }
                        else if (languageExtension.Equals("java"))
                        {
                            //newLine = "            @GUID(\"" + id + "\")\n";
                        }
                        replaced = replaced + newLine + text[i];
                    }
                }
                else
                {
                    replaced = replaced + "\n" + text[i];
                }
            }
            replaced = replaced.Replace("#namespaceName#", package);

            if (persistent && languageExtension.Equals("cs"))
            {
                replaced = replaced.Replace("#className#", className + "PreProcessed");
                writeOut(replaced, className, languageExtension, outputPath);
            }
            else
            {
                replaced = replaced.Replace("#className#", className + "PreProcessed");
                writeOut(replaced, className, languageExtension, outputPath);
            }

            GenerateClassAlgebra.generateClass("Template", languageExtension, package, className, map, outputPath, files);
        }

        public static string[] readIn(string fileName, string languageExtension)
        {

            string textFile = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Templates\\", fileName + "PreProcessed." + languageExtension + "T");

            string[] text = File.ReadAllLines(textFile);
            Console.WriteLine(textFile);

            return text;


        }

        public static void writeOut(string text, string fileName, string languageExtension, string outputPath)
        {
            System.IO.File.WriteAllText(outputPath + fileName + "PreProcessed." + languageExtension, text);

        }
    }
}