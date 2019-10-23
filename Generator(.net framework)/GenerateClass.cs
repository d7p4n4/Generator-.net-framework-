using CSAc4yClass.Class;
using GuidLibrary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace Generator_.net_framework_
{
    class GenerateClass
    {
        public static void generateClass(string languageExtension, Ac4yClass anyType, string outputPath, string[] files)
        {
            string className = anyType.Name;
            string package = anyType.Namespace;
            string parentClassLibrary = anyType.Ancestor;
            string _classGuid = "";
            string _guidValue = "";
            Boolean persistent = false;

            //get the properties and its type
            List<Ac4yProperty> map = anyType.PropertyList;

            if (package == null)
            {
                package = ConfigurationManager.AppSettings["namespace"];
            }

            //get the GUID value of the properties
            //Dictionary<string, string> guid = InsertGuid.getAttrs(anyType);

            //Has the class GUID value or not
            /*Boolean classAttr = InsertGuid.hasClassAttr(anyType, typeof(GUID));
            Console.WriteLine(classAttr);
            Boolean persistent = InsertGuid.hasClassAttr(anyType, typeof(Persistent));

            if (classAttr)
            {
                GUID _guidClassAttr = (GUID)anyType.GetCustomAttribute(typeof(GUID), true);
                _guidValue = _guidClassAttr.getGuid();
                Console.WriteLine(_guidValue);
            }*/

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
                if (text[i].Contains("#constructor#"))
                {
                    string newLine = "";
                    string props = "";
                    foreach(var _prop in map)
                    {
                        props = props + _prop.Name + ", ";
                    }
                    newLine = text[i + 1].Replace("#allProps#", props) + "\n";

                    foreach(var _prop in map)
                    {
                        newLine = newLine + text[i + 2].Replace("#prop#", _prop.Name) + "\n";
                    }
                    replaced = replaced + "\n" + newLine;

                    i = i + 2;
                }
                else if (text[i].Contains("#parentLibrary#"))
                {/*
                    string newLine = "";

                    if (!parentClassLibrary.Equals("System.Object"))
                    {
                        newLine = text[i].Replace("#parentLibrary#", "using " + parentLibrary + ";") + "\n";
                    }
                    else
                    {
                        newLine = text[i].Replace("#parentLibrary#", "") + "\n";
                    }

                    replaced = replaced + "\n" + newLine;*/
                } 
                else if (text[i].Contains("#parentClass#"))
                {
                    string newLine = "";

                    if (anyType.Ancestor != null && !anyType.Ancestor.Equals(""))
                    {
                        newLine = text[i].Replace("#parentClass#", ": " + anyType.Ancestor) + "\n";
                    }
                    else
                    {
                        newLine = text[i].Replace("#parentClass#", "") + "\n";
                    }

                    replaced = replaced + "\n" + newLine;
                }
                else if (text[i].Equals("#constructor#"))
                {/*
                    string propNames = "";
                    string newLine = "";

                    foreach (var pair in map)
                    {
                        propNames = propNames + pair.Name + ", ";
                    }
                    newLine = text[i + 1].Replace("#allProps#", propNames.Substring(0, propNames.Length - 2)) + "\n";
                    foreach (var pair in map)
                    {
                        newLine = newLine + text[i + 2].Replace("#prop#", pair.Name) + "\n";
                    }
                    newLine = newLine + text[i + 3];

                    replaced = replaced + "\n" + newLine;

                    i = i + 3;*/
                }
                else if (text[i].Equals("#properties#"))
                {
                    foreach (var pair in map)
                    {
                        string newLine = "";

                        newLine = newLine + text[i + 1].Replace("#type#", pair.Type);
                        newLine = newLine.Replace("#prop#", pair.Name);

                        replaced = replaced + "\n" + newLine;
                    }
                    i++;
                }
                
                else if (text[i].Equals("#getter#"))
                {
                    foreach (var pair in map)
                    {
                        string newLine = text[i + 1].Replace("#type#", pair.Type);
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
                        string newLine = text[i + 1].Replace("#prop#", pair.Name.Substring(0, 1).ToUpper() + pair.Name.Substring(1));
                        newLine = newLine.Replace("#type#", pair.Type);

                        replaced = replaced + "\n" + newLine;

                        newLine = text[i + 2].Replace("#prop#", pair.Name);
                        replaced = replaced + "\n" + newLine + "\n        }\n";
                    }
                    i = i + 3;
                }
                
                else if (text[i].Contains("#guid#"))
                {
                    string newLine = "";
                    if (anyType.GUID == null)
                    {
                        Guid id = Guid.NewGuid();
                        
                        if (languageExtension.Equals("cs"))
                        {
                            newLine = text[i].Replace("#guid#", id + "");
                        }
                        else if (languageExtension.Equals("java"))
                        {
                            newLine = "            @GUID(\"" + id + "\")\n";
                        }
                        replaced = replaced + "\n" + newLine;
                    }
                    else
                    {

                        if (languageExtension.Equals("cs"))
                        {
                            newLine = text[i].Replace("#guid#", anyType.GUID + "");
                        }
                        else if (languageExtension.Equals("java"))
                        {
                            newLine = "            @GUID(\"" + anyType.GUID + "\")\n";
                        }
                        replaced = replaced + "\n" + newLine;
                    }
                }
                else if (text[i].Contains("#classGUID#"))
                {
                    Guid id = Guid.NewGuid();
                    string newLine = "";

                    if (languageExtension.Equals("cs") && anyType.GUID == null)
                    {
                        newLine = text[i].Replace("#classGUID#", "            [GUID(\"" + id + "\")]");
                    }
                    else if(languageExtension.Equals("cs") && anyType.GUID != null)
                    {
                        newLine = text[i].Replace("#classGUID#", "            [GUID(\"" + anyType.GUID + "\")]");
                    }

                    replaced = replaced + "\n" + newLine;

                }
                else
                {
                    replaced = replaced + "\n" + text[i];
                }

            }
            replaced = replaced.Replace("#namespaceName#", package);

            replaced = replaced.Replace("#className#", className + "PreProcessed");
            writeOut(replaced, className, languageExtension, outputPath);

            if (!languageExtension.Equals("js"))
            {
                GenerateClassAlgebra.generateClass("Template", languageExtension, package, className, map, outputPath, files);
            }
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