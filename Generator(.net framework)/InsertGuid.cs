using GuidLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Generator_.net_framework_
{
    public class InsertGuid
    {

        public static Dictionary<string, string> getProps(Type anyType)
        {
            Dictionary<string, string> props = new Dictionary<string, string>();

            PropertyInfo[] prop = anyType.GetProperties();

            foreach (var p in prop)
            {
                if (p.PropertyType.ToString().StartsWith("System.Collections.Generic."))
                {
                    string type = p.PropertyType.ToString();
                    string outType = type.Substring(0, type.IndexOf("`")).Replace("System.Collections.Generic.", "");
                    string innerType = type.Substring(type.IndexOf("`"));
                    string finalInnerType = innerType.Substring(innerType.IndexOf(".") + 1).Replace("]", "");
                    Console.WriteLine(finalInnerType);

                    props.Add(p.Name, outType + "<" + finalInnerType + ">");

                }
                else
                {
                    props.Add(p.Name, p.PropertyType.ToString().Substring(p.PropertyType.ToString().IndexOf(".") + 1));
                }
            }

            return props;
        }

        public static Dictionary<string, string> getAttrs(Type anyType)
        {
            Dictionary<string, string> attrs = new Dictionary<string, string>();

            PropertyInfo[] prop = anyType.GetProperties();

            foreach (var p in prop)
            {
                Object[] attr = p.GetCustomAttributes(typeof(GUID), false);
                if (attr.Length != 0)
                {
                    GUID guid = (GUID)attr[0];
                    attrs.Add(p.Name, guid.getGuid());
                }
                else
                {
                    attrs.Add(p.Name, "");
                }
            }

            return attrs;
        }

        public Boolean isPersistent(Type anyType)
        {
            Boolean val = false;



            return val;
        }

        public static Boolean hasClassAttr(Type anyType, Type attributeType)
        {
            Boolean value = false;

            Attribute classAttr = anyType.GetCustomAttribute(attributeType, false);

            if (classAttr != null)
            {
                value = true;
            }

            return value;
        }

        public static string[] readIn(string fileName)
        {

            string textFile = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, fileName + "Base.txt");

            string[] text = File.ReadAllLines(textFile);

            return text;


        }

        public static void writeOut(string text, string fileName)
        {
            System.IO.File.WriteAllText(Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\" + fileName + "Base.cs"), text);

        }
    }
}