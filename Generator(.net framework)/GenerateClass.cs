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
        public static void generateClass(Ac4yClass anyType, string outputPath, string[] files)
        {
            string className = anyType.Name;
            string package = anyType.Namespace;
            Boolean guidExists = false;

            //get the properties and its type
            List<Ac4yProperty> map = anyType.PropertyList;

            if (package == null)
            {
                package = ConfigurationManager.AppSettings["namespace"];
            }

            string[] text = new String[0];

            text = readIn("Template");
            
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
                {
                    //Még kellhet
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
                else if (text[i].Equals("#properties#"))
                {
                    foreach (var pair in map)
                    {
                        if (pair.Name.Equals("GUID"))
                        {
                            guidExists = true;
                        }

                        string newLine = "";

                        newLine = newLine + text[i + 1].Replace("#type#", pair.Type);
                        newLine = newLine.Replace("#prop#", pair.Name);

                        replaced = replaced + "\n" + newLine;
                    }

                    if (!guidExists)
                    {
                        string newLine = "";

                        newLine = newLine + text[i + 1].Replace("#type#", "string");
                        newLine = newLine.Replace("#prop#", "GUID");

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
                        
                            newLine = text[i].Replace("#guid#", id + "");
                        replaced = replaced + "\n" + newLine;
                    }
                    else
                    {

                            newLine = text[i].Replace("#guid#", anyType.GUID + "");
                        replaced = replaced + "\n" + newLine;
                    }
                }
                else if (text[i].Contains("#classGUID#"))
                {
                    Guid id = Guid.NewGuid();
                    string newLine = "";

                    if (anyType.GUID == null)
                    {
                        newLine = text[i].Replace("#classGUID#", "            [GUID(\"" + id + "\")]");
                    }
                    else if(anyType.GUID != null)
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
            writeOut(replaced, className, outputPath);

            GenerateClassAlgebra.generateClass("Template", package, className, map, outputPath, files);
            ApiMethodGenerator.generateApiMethods("Template", package, className, map, outputPath);
            GenerateResponseModel.generateResponseModel(className, outputPath);
            
        }

        public static string[] readIn(string fileName)
        {

            string textFile = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Templates\\", fileName + "PreProcessed.csT");

            string[] text = File.ReadAllLines(textFile);

            return text;


        }

        public static void writeOut(string text, string fileName, string outputPath)
        {
            System.IO.File.WriteAllText(outputPath + fileName + "PreProcessed.cs", text);

        }
    }
}